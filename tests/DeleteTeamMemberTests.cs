
using NUnit.Framework;
using RethinkEd_Automation.Pages;
using Microsoft.Playwright;
using RethinkEd_Automation.Utilities;
using System.Globalization;
using CsvHelper;


namespace RethinkEd_Automation.Tests
{
    public class DeleteTeamMemberTests
    {
        protected SearchPage _searchPage;
        protected DeleteTeamMemberPage _deletePage;


        public DeleteTeamMemberTests(SearchPage searchPage, DeleteTeamMemberPage deletePage)
        {
            _searchPage = searchPage;
            _deletePage = deletePage;
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

        public async Task Delete_Team_Member()
        {
            var filePath = GlobalAppSettings.GetCsvfilePath();
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var searchDataList = csv.GetRecords<SearchData>().ToList();
            foreach (var searchData in searchDataList)
            {
                await _searchPage.SearchTeamMemberAsync($"{searchData.LastName}, {searchData.FirstName}");
                await _searchPage.ClickEditLinkAsync();
                await _deletePage.ClickDeleteLink();

            }
        }
    }
}
