using Afdian.Server.Configuration;
using Afdian.Server.Services;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;

namespace Afdian.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            TgBotConfig = Configuration.GetSection("TgBotConfiguration").Get<TgBotConfiguration>();
            AfdianConfig = Configuration.GetSection("AfdianConfiguration").Get<AfdianConfiguration>();
            Environment = env;
        }

        public IConfiguration Configuration { get; }
        private TgBotConfiguration TgBotConfig { get; }

        private AfdianConfiguration AfdianConfig { get; }

        private IWebHostEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<TgBotConfiguration>(Configuration.GetSection("TgBotConfiguration"));
            services.Configure<AfdianConfiguration>(Configuration.GetSection("AfdianConfiguration"));


            // There are several strategies for completing asynchronous tasks during startup.
            // Some of them could be found in this article https://andrewlock.net/running-async-tasks-on-app-startup-in-asp-net-core-part-1/
            // We are going to use IHostedService to add and later remove Webhook
            if (Environment.IsProduction())
            {
                services.AddHostedService<ConfigureTgWebhook>();
            }

            // Register named HttpClient to get benefits of IHttpClientFactory
            // and consume it with ITelegramBotClient typed client.
            // More read:
            //  https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-5.0#typed-clients
            //  https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests
            services.AddHttpClient("tgwebhook")
                    .AddTypedClient<ITelegramBotClient>(httpClient
                        => new TelegramBotClient(TgBotConfig.BotToken, httpClient));

            // Dummy business-logic service
            services.AddScoped<HandleTgUpdateService>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                // https://docs.microsoft.com/zh-cn/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-6.0&tabs=visual-studio
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "爱发电 Badge",
                    Description = "爱发电 Badge - 由 Afdian.Server 构建",
                    TermsOfService = new Uri("https://github.com/yiyungent/Afdian.Sdk"),
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "Contact",
                        Url = new Uri("https://github.com/yiyungent/Afdian.Sdk/issues")
                    },
                    License = new Microsoft.OpenApi.Models.OpenApiLicense
                    {
                        Name = "MIT License",
                        Url = new Uri("https://github.com/yiyungent/Afdian.Sdk/blob/main/LICENSE")
                    },
                    //Extensions = new Microsoft.OpenApi.Models.OpenApiExtensibleDictionary<string, string>() { }
                });

                var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            // 任意跨域
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policyBuilder =>
                {
                    policyBuilder.AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin();
                });
            });

            // The Telegram.Bot library heavily depends on Newtonsoft.Json library to deserialize
            // incoming webhook updates and send serialized responses back.
            // Read more about adding Newtonsoft.Json to ASP.NET Core pipeline:
            //   https://docs.microsoft.com/en-us/aspnet/core/web-api/advanced/formatting?view=aspnetcore-5.0#add-newtonsoftjson-based-json-format-support
            services.AddControllers()
                .AddNewtonsoftJson();

            services.AddHttpContextAccessor();

            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<Models.ApplicationDbContext>(options =>
                options.UseSqlite(connectionString));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseRouting();
            app.UseCors();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // Configure custom endpoint per Telegram API recommendations:
                // https://core.telegram.org/bots/api#setwebhook
                // If you'd like to make sure that the Webhook request comes from Telegram, we recommend
                // using a secret path in the URL, e.g. https://www.example.com/<token>.
                // Since nobody else knows your bot's token, you can be pretty sure it's us.
                var token = TgBotConfig.BotToken;
                endpoints.MapControllerRoute(name: "tgwebhook",
                                             pattern: $"webhook/tgbot/{token}",
                                             new { controller = "TgWebhook", action = "Post" });

                string vToken = AfdianConfig.VToken;
                endpoints.MapControllerRoute(name: "afdianwebhook",
                                             pattern: $"webhook/afdian/{vToken}",
                                             new { controller = "AfdianWebhook", action = "Post" });

                endpoints.MapControllers();
            });
        }
    }
}
