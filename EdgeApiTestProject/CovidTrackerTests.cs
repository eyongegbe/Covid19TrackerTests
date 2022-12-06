using EdgeApiTestProject.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using Nancy.Json;
using NUnit.Framework;

namespace EdgeApiTestProject
{
    public class CovidTrackerTests:PlaywrightTest
    {


        private IAPIRequestContext Request = null;
        

       [SetUp]
        public async Task SetUpApiTests()
        {
            await CreateAPIRequestContext();
        }

        private async Task CreateAPIRequestContext()
        {

            var configurationBuilder = new ConfigurationBuilder()
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
            Console.WriteLine("1."+highest[0].Country + ", Total Deaths: " +highest[0].TotalDeaths);
            Console.WriteLine("2."+highest[1].Country + ", Total Deaths: " +highest[1].TotalDeaths);
            Console.WriteLine("3."+highest[2].Country + ", Total Deaths: " +highest[2].TotalDeaths);
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
            var highest = countryData.OrderByDescending(x => x.TotalDeaths).Take(3).ToArray();


            Console.WriteLine("----------Highest Death Ranking For Northern America---------------");
            Console.WriteLine($"1.{ highest[0].Country}, Total Deaths: {highest[0].TotalDeaths}");
            Console.WriteLine($"2.{highest[1].Country}, Total Deaths:  {highest[1].TotalDeaths}");
            Console.WriteLine($"3.{highest[2].Country}, Total Deaths: {highest[2].TotalDeaths}");
            Console.WriteLine("-------------------------------------------------------------------");

        }

        [Test]
        public async Task ShouldGetNewCasesForSpecificCountryAndDate()
        {
            var response = await Request.GetAsync("api/npm-covid-data/northamerica");
            var result = await response.JsonAsync();
            Assert.That(200, Is.EqualTo(response.Status));

            var data = result.ToString();
            JavaScriptSerializer js = new();
            CountryCovidData[] countryData = js.Deserialize<CountryCovidData[]>(data);
            var highest = countryData.OrderByDescending(x => x.TotalDeaths).Take(3).ToArray();


            foreach (var country in highest)
            {
                var response1 = await Request.GetAsync("api/covid-ovid-data/sixmonth/" + country.Country);
                var result1 = await response1.JsonAsync();
                Assert.That(200, Is.EqualTo(response1.Status));


                var data1 = result1.ToString();
                JavaScriptSerializer js1 = new();
                CovidCasesDataByDate[] casesByDate = js1.Deserialize<CovidCasesDataByDate[]>(data1);
                var dates = casesByDate.OrderBy(x => x.Country).ToArray();
                var recordedCases = 0;
                foreach (var date in dates)
                {
                    while (date.date.Equals("2022-08-15") && date.Country.Equals(country.Country))
                    {
                        recordedCases = date.new_cases;
                    }
                }

                Console.WriteLine($"{country.Country} recorded {recordedCases} new cases on 22-08-15");
            }

        }


        [TearDown]
        public async Task TearDownApiTesting()
        {
            await Request.DisposeAsync();
        }
    }
}
