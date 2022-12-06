using Covid19TrackerTests.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using Nancy.Json;
using NUnit.Framework;

namespace Covid19TrackerTests
{
    public class CovidTrackerTests : PlaywrightTest
    {


        private IAPIRequestContext Request = null;


        [SetUp]
        public async Task SetUpApiTests()
        {
            await CreateAPIRequestContext();
        }

        private async Task CreateAPIRequestContext()
        {

            var configurationBuilder = new ConfigurationManager()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
            string BaseUri = configurationBuilder["BaseUri"];
            string ApiKey = configurationBuilder["ApiKey"];



            var headers = new Dictionary<string, string>();
            headers.Add("X-RapidAPI-Key", ApiKey);

            Request = await Playwright.APIRequest.NewContextAsync(new()
            {
                BaseURL = BaseUri,
                ExtraHTTPHeaders = headers,
                IgnoreHTTPSErrors = true
            });

        }


        [Test]
        public async Task ShouldGetHighestRecordedCovidDeathsInEurope()
        {
            var response = await Request.GetAsync("api/npm-covid-data/europe");
            var result = await response.JsonAsync();
            Assert.That(200, Is.EqualTo(response.Status));


            var data = result.ToString();
            JavaScriptSerializer js = new();
            CountryCovidData[] countryData = js.Deserialize<CountryCovidData[]>(data);
            var highest = countryData.OrderByDescending(x => x.TotalDeaths).ToArray();


            Console.WriteLine("-----------------Highest Death Ranking For Europe---------------------");
            Console.WriteLine("1." + highest[0].Country + ", Total Deaths: " + highest[0].TotalDeaths);
            Console.WriteLine("2." + highest[1].Country + ", Total Deaths: " + highest[1].TotalDeaths);
            Console.WriteLine("3." + highest[2].Country + ", Total Deaths: " + highest[2].TotalDeaths);
            Console.WriteLine("----------------------------------------------------------------------");

        }


        [Test]
        public async Task ShouldGetHighestRecordedCovidDeathsInNorthAmerica()
        {
            var response = await Request.GetAsync("api/npm-covid-data/northamerica");
            var result = await response.JsonAsync();
            Assert.That(200, Is.EqualTo(response.Status));

            var data = result.ToString();
            JavaScriptSerializer js = new();
            CountryCovidData[] countryData = js.Deserialize<CountryCovidData[]>(data);
            var highest = countryData.OrderByDescending(x => x.TotalDeaths).ToArray();


            Console.WriteLine("----------Highest Death Ranking For Northern America------------------");
            Console.WriteLine("1." + highest[0].Country + ", Total Deaths: " + highest[0].TotalDeaths);
            Console.WriteLine("2." + highest[1].Country + ", Total Deaths: " + highest[1].TotalDeaths);
            Console.WriteLine("3." + highest[2].Country + ", Total Deaths: " + highest[2].TotalDeaths);
            Console.WriteLine("----------------------------------------------------------------------");
        }

        [TearDown]
        public async Task TearDownApiTesting()
        {
            await Request.DisposeAsync();
        }
    }
}
