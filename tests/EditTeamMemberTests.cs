
using NUnit.Framework;
using RethinkEd_Automation.Pages;
using Microsoft.Playwright;
using RethinkEd_Automation.Utilities;
using System.Globalization;
using CsvHelper;
using Bogus;


namespace RethinkEd_Automation.Tests
{
    public class EditTeamMemberTests
    {
        protected SearchPage _searchPage;
        protected EditTeamMemberPage _editPage;

        public EditTeamMemberTests(SearchPage searchPage, EditTeamMemberPage editPage)
        {
            _searchPage = searchPage;
            _editPage = editPage;
        }

        public class SearchData
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string CellPhone { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
        }

        public async Task Edit_Team_Member()
        {
            var filePath = GlobalAppSettings.GetCsvfilePath();
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var searchDataList = csv.GetRecords<SearchData>().ToList();
            foreach (var searchData in searchDataList)
            {
                var faker = new Faker();
                var cellPhone = faker.Phone.PhoneNumber();
                await _searchPage.SearchTeamMemberAsync($"{searchData.LastName}, {searchData.FirstName}");
                await _searchPage.ClickEditLinkAsync();
                await _editPage.EditTeamMemberPhoneNumber(cellPhone);
            }
        }
    }
}
