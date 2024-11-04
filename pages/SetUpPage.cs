using Microsoft.Playwright;

namespace RethinkEd_Automation.Pages
{
    public class SetupPage : BaseTest
    {
        // Selectors for the Setup page
        private string SetupLinkSelector = "a[aria-label='Setup']";
        private string PageTitleSelector = "h1.pageTitle";
        private string ManageStudentsSelector = "text=Manage Students";
        private string ManageTeamMembersSelector = "text=Manage Team Members";
        private string ManageRolesSelector = "text=Manage Roles";
        private string AccountSettingsSelector = "text=Account Settings";

        public SetupPage(IPage page) : base(page) { }

        public async Task GoToSetupPage()
        {
            await _page.Locator(SetupLinkSelector).Nth(0).ClickAsync();
            await _page.WaitForLoadStateAsync(LoadState.NetworkIdle); // Wait for the page to load
        }

        public async Task<bool> VerifySetupPageContents()
        {
            // Wait for the "Account Setup" title to ensure we are on the correct page
            await _page.WaitForSelectorAsync(PageTitleSelector, new() { Timeout = 10000 });

            // Check if the necessary texts are available on the page
            var manageStudentsVisible = await GetElement(nameof(ManageStudentsSelector)).IsVisibleAsync();
            var manageTeamMembersVisible = await GetElement(nameof(ManageTeamMembersSelector)).IsVisibleAsync();
            var manageRolesVisible = await GetElement(nameof(ManageRolesSelector)).IsVisibleAsync();
            var accountSettingsVisible = await GetElement(nameof(AccountSettingsSelector)).IsVisibleAsync();

            return manageStudentsVisible && manageTeamMembersVisible && manageRolesVisible && accountSettingsVisible;
        }
    }
}
