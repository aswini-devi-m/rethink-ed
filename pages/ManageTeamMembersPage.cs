using Microsoft.Playwright;

namespace RethinkEd_Automation.Pages
{
    public class ManageTeamMembersPage : BaseTest
    {
        // Selectors for the Manage Team Members page
        private string ManageButtonSelector = "a[aria-label='Manage Team Members']";
        private string TitleSelector = "h2:has-text('Team Members')";
        private string HeaderSelector = "h2";

        public ManageTeamMembersPage(IPage page) : base(page) { }

        // Method to navigate to the Manage Team Members page
        public async Task GoToManageTeamMembers()
        {
            // Click the manage button under the Manage Team Members section
            await GetElement(nameof(ManageButtonSelector)).ClickAsync();
            // Wait for the new page to load
            await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        }

        // Method to verify that we are on the Manage Team Members page
        public async Task<bool> VerifyManageTeamMembersPageContents()
        {
            await _page.WaitForSelectorAsync(TitleSelector, new() { Timeout = 10000 });
            // After waiting, check if the title is present
            var titleLocator = GetElement(nameof(HeaderSelector));
            var titleText = await titleLocator.InnerTextAsync();

            // Validate that the title text is "Team Members"
            return titleText == "Team Members"; // Return true if the check passes
        }
    }
}
