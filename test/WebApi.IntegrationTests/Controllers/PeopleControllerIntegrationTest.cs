using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using WebApi.IntegrationTests.Configuration;
using WebApi.Models;
using Xunit;

namespace WebApi.IntegrationTests.Controllers
{
    public class PeopleControllerIntegrationTest : BaseIntegrationTest
    {
        private const string BaseUrl = "/api/people";

        public PeopleControllerIntegrationTest(BaseTestFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task GetShouldReturnValues()
        {
            var person = await CreatePerson();

            var response = await Client.GetAsync(BaseUrl);
            response.EnsureSuccessStatusCode();

            var dataString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<Person>>(dataString);

            Assert.Equal(data.Count, 1);
            Assert.Contains(data, x => x.Name == person.Name);
        }

        [Fact]
        public async Task GetSinglePersonNoIdReturnsNotFound()
        {
            var response = await Client.GetAsync($"{BaseUrl}/99");
            Assert.Equal(response.StatusCode, HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetSinglePersonWithIdReturnsPerson()
        {
            var person = await CreatePerson();

            var response = await Client.GetAsync($"{BaseUrl}/{person.Id}");
            response.EnsureSuccessStatusCode();

            var dataString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<Person>(dataString);

            Assert.Equal(data.Name, person.Name);
        }

        private async Task<Person> CreatePerson()
        {
            var person = new Person
            {
                Name = "Rafael dos Santos",
                Phone = "+5527900000000",
                BirthDay = new DateTime(1988, 09, 08),
                Salary = 1000
            };

            await TestDbContext.AddAsync(person);
            await TestDbContext.SaveChangesAsync();
            return person;
        }
    }
}
