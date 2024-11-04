using Microsoft.Playwright;

namespace RethinkEd_Automation.Pages
{
    public class LoginPage : BaseTest
    {
        // Selectors for the input fields and buttons
        private string UsernameSelector = "input#signInName";
        private string PasswordSelector = "input#password";
        private string NextButtonSelector = "button#next";
        private string DashboardSelector = "a[aria-label='Dashboard']";

        public LoginPage(IPage page) : base(page) { }

        public async Task Login(string username, string password)
        {
            await GetElement(nameof(UsernameSelector)).FillAsync(username);
            await GetElement(nameof(PasswordSelector)).FillAsync(password);
            await GetElement(nameof(NextButtonSelector)).ClickAsync();

            // Wait until the network is idle
            await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            // Wait for the Dashboard link to appear with a custom timeout
            await _page.WaitForSelectorAsync(DashboardSelector, new()
            {
                Timeout = 20000 // Sets the timeout to 20 seconds
            });
        }
    }
}
