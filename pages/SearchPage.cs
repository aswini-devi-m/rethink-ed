using Microsoft.Playwright;
using System.Threading.Tasks;

public class SearchPage : BaseTest
{
    // Selector for the input field
    private string InputSelector = "input[placeholder='Search team members']";
    private string EditLinkSelector = "table.studentsTable tbody tr:first-child td.text-center a.action-link.primary:has-text('Edit')";

    // Constructor to initialize the page object
    public SearchPage(IPage page) : base(page) { }

    // Asynchronous method to perform search
    public async Task SearchTeamMemberAsync(string searchText)
    {
        await _page.FillAsync(InputSelector, searchText); // Fill the search box asynchronously
        await Task.Delay(1000);
        await _page.Keyboard.PressAsync("Enter"); // Press Enter key asynchronously
    }

    public async Task ClickEditLinkAsync()
    {
        await _page.WaitForSelectorAsync(EditLinkSelector, new() { Timeout = 20000 }); // Wait for the Edit link to be visible
        await GetElement(nameof(EditLinkSelector)).ClickAsync(); // Click the Edit link
        //await _page.Locator(EditLinkSelector).Nth(0).ClickAsync();
    }
}
