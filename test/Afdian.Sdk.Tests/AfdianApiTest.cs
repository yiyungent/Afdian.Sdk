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

        public AfdianApiTest(ITestOutputHelper output)
        {
            this.AfdianClient = new AfdianClient(userId: "", token: "");
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
    }
}