# Covid19TrackerTests
## An API Test Framework to Test VACCOVID - coronavirus, vaccine and treatment tracker API

API: https://rapidapi.com/vaccovidlive-vaccovidlive-default/api/vaccovid-coronavirus-vaccine-and-treatment-tracker/

## Requirements

1. Install the following from Nuget Package Manager after cloning the repsitory
	- Microsoft.Playwright
	- Fluent Assertions
	- Microsoft.Playwright
	- Nancy
	- System.Configuration.ConfigurationManager
2. SignUp to rapidApi to obtain the ApiKey:https://rapidapi.com/

## Running Tests Locally
1. Ensure you replace <APIKey> and <APIHost> values with yours in appsettings.json
2. Ensure 'appsettings.json' file's Copy To Output Directory property is set to Copy Always
3. In visual studio, right click on project, expand the project and rightClick on CovidTrackerTests.cs and select Run Tests.
