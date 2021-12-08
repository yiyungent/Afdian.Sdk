using Microsoft.Extensions.Configuration;
using Xunit;
using Xunit.Abstractions;

namespace Afdian.Sdk.Tests
{
    /// <summary>
    /// xunit 输出到控制台: https://www.cnblogs.com/chucklu/p/10027726.html
    /// </summary>
    public class AfdianApiTest
    {
        protected readonly ITestOutputHelper Output;

        public AfdianClient AfdianClient { get; set; }

        public IConfiguration Configuration { get; }

        public AfdianApiTest(ITestOutputHelper output, IConfiguration configuration)
        {
            this.Configuration = configuration;
            this.AfdianClient = new AfdianClient(userId: this.Configuration["SecretsKeys:AfdianUserId"], token: this.Configuration["SecretsKeys:AfdianToken"]);
            this.Output = output;
        }

        [Fact]
        public void Ping()
        {
            string jsonStr = this.AfdianClient.Ping().Result;

            Output.WriteLine($"{nameof(AfdianApiTest.Ping)}:");
            Output.WriteLine(jsonStr);

            Assert.NotEmpty(jsonStr);
        }

        [Fact]
        public void QueryOrder()
        {
            string jsonStr = this.AfdianClient.QueryOrder().Result;

            Output.WriteLine($"{nameof(AfdianApiTest.QueryOrder)}:");
            Output.WriteLine(jsonStr);

            Assert.NotEmpty(jsonStr);
        }

        [Fact]
        public void QuerySponsor()
        {
            string jsonStr = this.AfdianClient.QuerySponsor().Result;

            Output.WriteLine($"{nameof(AfdianApiTest.QuerySponsor)}:");
            Output.WriteLine(jsonStr);

            Assert.NotEmpty(jsonStr);
        }

    }
}