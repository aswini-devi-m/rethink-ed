
using NUnit.Framework;
using RethinkEd_Automation.Pages;
using Microsoft.Playwright;
using RethinkEd_Automation.Utilities;
using RethinkEd_Automation.Tests.RethinkEd_Automation.Tests;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;


namespace RethinkEd_Automation.Tests
{
    public class MasterSuite
    {
        protected IPage page;
        protected IPlaywright playwright;
        protected IBrowser browser;
        protected LoginPage loginPage;
        protected SetupPage setupPage;
        protected ManageTeamMembersPage manageTeamMembersPage;
        protected AddTeamMemberPage addTeamMemberPage;
        private ExtentReports extent;
        private ExtentTest test;
        private string reportPath;


        [OneTimeSetUp]
        public async Task OneTimeSetup()
        {
            // Get the current directory
            var currentDirectory = Directory.GetCurrentDirectory();

            // Find the index of the "bin" folder
            var binIndex = currentDirectory.IndexOf("bin", StringComparison.OrdinalIgnoreCase);

            // If "bin" is found, take the path before it
            string rootPath;
            if (binIndex >= 0)
            {
                rootPath = currentDirectory.Substring(0, binIndex);
            }
            else
            {
                rootPath = currentDirectory; // Fallback to current if "bin" not found
            }

            // Define the folder path (e.g., "Data" in the root project folder)
            reportPath = Path.Combine(rootPath, "extent_reports");

            if (!Directory.Exists(reportPath))
            {
                Directory.CreateDirectory(reportPath);
            }

            extent = new ExtentReports();
            var spark = new ExtentSparkReporter(Path.Combine(reportPath, "rethink.html"));
            extent.AttachReporter(spark);



            playwright = await Playwright.CreateAsync();
            browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
            var context = await browser.NewContextAsync();
            page = await context.NewPageAsync();
            loginPage = new LoginPage(page);
            setupPage = new SetupPage(page);

            // Perform login once
            await page.GotoAsync(GlobalAppSettings.GetBaseUrl());
            string username = GlobalAppSettings.GetUsername();
            string password = GlobalAppSettings.GetPassword();
            await loginPage.Login(username, password);
            setupPage = new SetupPage(page);
            await setupPage.GoToSetupPage();
            await setupPage.VerifySetupPageContents();

            manageTeamMembersPage = new ManageTeamMembersPage(page);
            await manageTeamMembersPage.GoToManageTeamMembers();
            await manageTeamMembersPage.VerifyManageTeamMembersPageContents();
        }


        [SetUp]
        public async Task Setup()
        {
            setupPage = new SetupPage(page);
            manageTeamMembersPage = new ManageTeamMembersPage(page);
            await setupPage.GoToSetupPage();
            await manageTeamMembersPage.GoToManageTeamMembers();
        }

        [Test, Order(1)]
        [Retry(3)] // Retry up to 3 times on failure
        public async Task Test_Add_Team_Member()
        {
            try
            {
                test = extent.CreateTest("Add Team Member Test");

                addTeamMemberPage = new AddTeamMemberPage(page);
                var addTeamMember = new AddTeamMemberTests(addTeamMemberPage);
                await addTeamMember.Add_Team_Member();
                test.Pass("Add Team Member test passed.");
            }
            catch (Exception ex)
            {
                test.Fail($"Test failed: {ex.Message}");
            }
        }

        [Test, Order(2)]
        [Retry(2)] // Retry up to 2 times on failure
        public async Task Test_Edit_Team_Member()
        {
            try
            {
                test = extent.CreateTest("Edit Team Member Test");
                var searchPage = new SearchPage(page);
                var editPage = new EditTeamMemberPage(page);
                var editTeamMember = new EditTeamMemberTests(searchPage, editPage);
                await editTeamMember.Edit_Team_Member();
                test.Pass("Test passed!");

            }
            catch (Exception ex)
            {
                test.Fail($"Test failed: {ex.Message}");
            }
        }

        [Test, Order(3)]
        public async Task Test_Delete_Team_Member()
        {
            try
            {
                test = extent.CreateTest("Delete Team Member Test");
                var searchPage = new SearchPage(page);
                var deletePage = new DeleteTeamMemberPage(page);
                var deleteTeamMember = new DeleteTeamMemberTests(searchPage, deletePage);
                await deleteTeamMember.Delete_Team_Member();
                test.Pass("Test passed!");
            }
            catch (Exception ex)
            {
                test.Fail($"Test failed: {ex.Message}");
            }
        }

        [Test]
        public async Task Test_Exception_Raising()
        {
            test = extent.CreateTest("Test Exception Raising");

            try
            {
                addTeamMemberPage = new AddTeamMemberPage(page);
                var addTeamMember = new AddTeamMemberTests(addTeamMemberPage);
                await addTeamMember.Add_Team_Member(true);
                RaiseException("This is a simulated exception for testing purposes.");
            }
            catch (Exception ex)
            {
                await CaptureScreenshotAsync("Test_Exception_Raising_Failure.png");
                test.Fail($"Exception raised: {ex.Message}");
            }
        }

        private void RaiseException(string message)
        {
            throw new Exception(message); // Raise an exception with the provided message
        }

        private async Task CaptureScreenshotAsync(string fileName)
        {
            string filePath = Path.Combine(reportPath, fileName); // Full path for the screenshot file
            await page.ScreenshotAsync(new PageScreenshotOptions { Path = filePath });
            test.AddScreenCaptureFromPath(filePath); // Add the screenshot to the Extent report
        }


        [TearDown]
        public async Task TearDown()
        {
            //await _browser.CloseAsync();
        }

        [OneTimeTearDown]
        public async Task OneTimeTearDown()
        {
            extent.Flush();
            await browser.CloseAsync();
        }
    }
}
