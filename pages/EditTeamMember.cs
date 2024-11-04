using Microsoft.Playwright;
using System.Threading.Tasks;

public class EditTeamMemberPage : BaseTest
{
    private string CellPhoneSelector = "#phone";
    private string SaveAndCloseButtonSelector = "button:has-text('Save and Close')";
    private string SaveButtonSelector = "div.modal-content confirmation-popup div.modal-body button.btn.btn-w-110.btn-primary:has-text('Save')";

    // Constructor to initialize the page object
    public EditTeamMemberPage(IPage page) : base(page) { }

    public async Task EditTeamMemberPhoneNumber(string cellPhone)
    {
        // Fill the phone number using the page factory
        await GetElement(nameof(CellPhoneSelector)).FillAsync(cellPhone);

        // Click the "Save and Close" button
        await GetElement(nameof(SaveAndCloseButtonSelector)).ClickAsync();

        // Optional delay for demonstration or if needed for the modal to appear
        await Task.Delay(1000);

        // Locate the save button in the confirmation popup
        var saveButton = GetElement(nameof(SaveButtonSelector));

        // Check if the button is visible and click it
        if (await saveButton.IsVisibleAsync())
        {
            await saveButton.ClickAsync();
        }
    }
}
