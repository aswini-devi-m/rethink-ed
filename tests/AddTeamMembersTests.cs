using RethinkEd_Automation.Pages;
using RethinkEd_Automation.Utilities;

namespace RethinkEd_Automation.Tests
{

    using NUnit.Framework;
    using Bogus;
    using Microsoft.Playwright;

    namespace RethinkEd_Automation.Tests
    {
        public class AddTeamMemberTests
        {

            private readonly AddTeamMemberPage _addTeamMemberPage;

            public AddTeamMemberTests(AddTeamMemberPage page)
            {
                _addTeamMemberPage = page;
            }
            public async Task Add_Team_Member(bool invalid = false)
            {
                var confirmPassword = "";
                int iteration = 1;
                if (!invalid)
                {
                    iteration = int.Parse(GlobalAppSettings.iterations());
                }

                for (int i = 0; i < iteration; i++)
                {

                    // Define the CSV file path
                    var csvFilePath = GlobalAppSettings.GetCsvfilePath();

                    // Generate fake data
                    var faker = new Faker();
                    var firstName = faker.Name.FirstName();
                    var lastName = faker.Name.LastName();
                    var email = faker.Internet.Email();
                    var cellPhone = faker.Phone.PhoneNumber();
                    var username = faker.Internet.UserName();
                    var password = faker.Internet.Password();
                    if (!invalid)
                    {
                        confirmPassword = password; // Confirm password should match the password
                        // Write the data to the CSV file
                        using (var writer = new StreamWriter(csvFilePath, true))
                        {
                            if (new FileInfo(csvFilePath).Length == 0)
                            {
                                // Write the header
                                await writer.WriteLineAsync("FirstName,LastName,Email,CellPhone,Username,Password");
                            }
                            // Write the fake data as a new row
                            await writer.WriteLineAsync($"{firstName},{lastName},{email},{cellPhone},{username},{password}");
                        }
                    }
                    else
                    {
                        confirmPassword = faker.Internet.Password(); ;
                    }

                    await _addTeamMemberPage.ClickAddTeamMemberButton();

                    // Fill the form with fake data


                    await _addTeamMemberPage.FillForm(firstName, lastName, email, cellPhone, username, password, confirmPassword);

                    // Click on the Save button
                    await _addTeamMemberPage.ClickSaveButton();
                }
            }
        }
    }
}
