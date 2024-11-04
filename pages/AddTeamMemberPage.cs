using Microsoft.Playwright;

public class AddTeamMemberPage : BaseTest
{
    private readonly IPage _page;

    // Constructor
    public AddTeamMemberPage(IPage page) : base(page) { }

    // Define selectors as private fields
    private string FirstNameSelector = "#firstName";
    private string LastNameSelector = "#lastName";
    private string EmailSelector = "#email";
    private string CellPhoneSelector = "#phone";
    private string UsernameSelector = "#username";
    private string PasswordSelector = "#password";
    private string ConfirmPasswordSelector = "#confirmPassword";
    private string SaveButtonSelector = "button.btn-primary";
    private string AddMemberButtonSelector = "a.action-link.dark.action-link-sm.has-icon.me-2";

    public async Task ClickAddTeamMemberButton()
    {
        await GetElement(nameof(AddMemberButtonSelector)).ClickAsync();
    }

    public async Task FillForm(string firstName, string lastName, string email, string cellPhone, string username, string password, string confirmPassword)
    {
        await GetElement(nameof(FirstNameSelector)).FillAsync(firstName);
        await GetElement(nameof(LastNameSelector)).FillAsync(lastName);
        await GetElement(nameof(EmailSelector)).FillAsync(email);
        await GetElement(nameof(CellPhoneSelector)).FillAsync(cellPhone);
        await GetElement(nameof(UsernameSelector)).FillAsync(username);
        await GetElement(nameof(PasswordSelector)).FillAsync(password);
        await GetElement(nameof(ConfirmPasswordSelector)).FillAsync(confirmPassword);
    }

    public async Task ClickSaveButton()
    {
        await GetElement(nameof(SaveButtonSelector)).ClickAsync();
    }
}
