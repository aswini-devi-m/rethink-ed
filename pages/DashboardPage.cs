using Microsoft.Playwright;

namespace RethinkEd_Automation.Pages
{
    public class DashboardPage : BaseTest
    {
        // Selectors for the dashboard links
        private string DashboardLinkSelector = "text=Dashboard";
        private string SetupLinkSelector = "text=Setup";

        public DashboardPage(IPage page) : base(page) { }

        public async Task<bool> IsDashboardLinkVisible()
        {
            return await GetElement(nameof(DashboardLinkSelector)).IsVisibleAsync();
        }

        public async Task<bool> IsSetupLinkVisible()
        {
            return await GetElement(nameof(SetupLinkSelector)).IsVisibleAsync();
        }
    }
}
