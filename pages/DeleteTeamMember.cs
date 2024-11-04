using Microsoft.Playwright;
using System.Threading.Tasks;

public class DeleteTeamMemberPage : BaseTest
{
    private string DeleteLinkSelector = "button.btn-danger:has-text('Delete')";
    private string ConfirmationModalSelector = "div.modal-content";
    private string ConfirmationDeleteButtonSelector = "div.modal-content button.btn-danger:has-text('Delete')";
    private string ManageTeamMembersLinkSelector = "a:has-text('Manage Team Members')";
    private string SaveButtonSelector = "div.modal-content confirmation-popup div.modal-body button.btn.btn-w-110.btn-primary:has-text('Save')";

    // Constructor to initialize the page object
    public DeleteTeamMemberPage(IPage page) : base(page) { }

    public async Task ClickDeleteLink()
    {
        await Task.Delay(1000);

        await _page.WaitForSelectorAsync(DeleteLinkSelector, new() { Timeout = 20000 });

        await GetElement(nameof(DeleteLinkSelector)).ClickAsync();

        await _page.WaitForSelectorAsync(ConfirmationModalSelector, new() { Timeout = 20000 });

        await GetElement(nameof(ConfirmationDeleteButtonSelector)).ClickAsync();

        await GetElement(nameof(ManageTeamMembersLinkSelector)).ClickAsync();

        // Optional delay for demonstration or if needed for the modal to appear
        await Task.Delay(1000);

        var saveButton = GetElement(nameof(SaveButtonSelector));

        // Check if the save button is visible and click it
        if (await saveButton.IsVisibleAsync())
        {
            await saveButton.ClickAsync();
        }
    }
}
