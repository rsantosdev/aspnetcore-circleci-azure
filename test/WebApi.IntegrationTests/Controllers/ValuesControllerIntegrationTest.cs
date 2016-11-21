using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using WebApi.IntegrationTests.Configuration;
using Xunit;
using System.Text;
using System.Net;

namespace WebApi.IntegrationTests.Controllers
{
    public class ValuesControllerIntegrationTest : BaseIntegrationTest
    {
        private const string BaseUrl = "/api/values";

        public ValuesControllerIntegrationTest(BaseTestFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task GetShouldReturnValues()
        {
            var response = await Client.GetAsync(BaseUrl);
            response.EnsureSuccessStatusCode();

            var dataString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<string>>(dataString);

            Assert.Equal(data.Count, 2);
            Assert.Contains(data, x => x == "value1");
            Assert.Contains(data, x => x == "value2");
        }

        [Fact]
        public async Task GetSingleShouldReturnValue()
        {
            var response = await Client.GetAsync($"{BaseUrl}/5");
            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadAsStringAsync();

            Assert.Equal(data, "value");
        }

        [Fact]
        public async Task PostShouldReturnOk()
        {
            var body = new { value = "my-value" };
            var content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
            var response = await Client.PostAsync(BaseUrl, content);
            response.EnsureSuccessStatusCode();

            Assert.Equal(response.StatusCode, HttpStatusCode.OK);
        }

        [Fact]
        public async Task PutShouldReturnOk()
        {
            var body = new { value = "my-value" };
            var content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
            var response = await Client.PutAsync($"{BaseUrl}/5", content);
            response.EnsureSuccessStatusCode();

            Assert.Equal(response.StatusCode, HttpStatusCode.OK);
        }

        [Fact]
        public async Task DeleteShouldReturnOk()
        {
            var response = await Client.DeleteAsync($"{BaseUrl}/5");
            response.EnsureSuccessStatusCode();

            Assert.Equal(response.StatusCode, HttpStatusCode.OK);
        }
    }
}