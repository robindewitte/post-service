using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using twatter_postservice;
using twatter_postservice.Controllers;
using Xunit;

namespace UnitTest
{
    public class UnitTest
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public IConfigurationRoot Configuration { get; private set; }

        public UnitTest()
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder()
               .UseStartup<Startup>().ConfigureAppConfiguration(config =>
               {
                   Configuration = new ConfigurationBuilder()
                     .AddJsonFile("appsettings.json")
                     .Build();

                   config.AddConfiguration(Configuration);
               }));
            _client = _server.CreateClient();
        }

        [Theory]
        [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void TestLengthValidatorFalse(string value)
        {
            bool result = PostController.ValidateMessageLength(value);
            Assert.False(result);
            
        }
        [Theory]
        [InlineData("kloppend")]
        public void TestLengthValidatorTrue(string value)
        {
            bool result = PostController.ValidateMessageLength(value);
            Assert.True(result);
        }

        [Theory]
        [InlineData("kloppend")]
        public void TestComplianceValidatorTrue(string value)
        {
            bool result = PostController.ValidateMessageCompliance(value);
            Assert.True(result);
        }

        [Theory]
        [InlineData("Hitler")]
        [InlineData("fuck")]
        public void TestComplianceValidatorFalse(string value)
        {
            bool result = PostController.ValidateMessageCompliance(value);
            Assert.False(result);
        }

        [Fact]
        public async Task ValidateTokenBlock()
        {
            var response = await _client.GetAsync("https://localhost:5005/api/post/getUserMessages/robintest");
            var responseString = response.StatusCode;
            Assert.Equal(System.Net.HttpStatusCode.Unauthorized, responseString);

            string token = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJyb2JpbnRlc3QiLCJleHAiOjE2MjMwNTc3MjUsImlzcyI6IlRlc3QuY29tIiwiYXVkIjoiVGVzdC5jb20ifQ.qf61Bn90DYvIVFNlUlqDfiI7Xw3O_ZdYHG1bjbEyu8U";

            _client.DefaultRequestHeaders.Add("Authorization", token);
            var responseWithToken = await _client.GetAsync("https://localhost:5005/api/post/getUserMessages/robintest");
            _client.DefaultRequestHeaders.Clear();
            responseString = responseWithToken.StatusCode;
            Assert.Equal(System.Net.HttpStatusCode.OK, responseString);
        }

    }
}
