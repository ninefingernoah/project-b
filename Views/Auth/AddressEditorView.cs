/// <summary>
/// View for editing an address. Singleton.
/// </summary>
public class AddressEditorView : IView
{

    /// <summary>
    /// Singleton instance
    /// </summary>
    private static readonly AddressEditorView instance = new AddressEditorView();

    static AddressEditorView()
    {
    }

    private AddressEditorView()
    {
    }

    /// <summary>
    /// Singleton instance property
    /// </summary>
    public static AddressEditorView Instance
    {
        get
        {
            return instance;
        }
    }

    /// <summary>
    /// The viewbag. Holds temporary data for the view.
    /// </summary>
    public Dictionary<string, object> ViewBag = new Dictionary<string, object>();

    /// <summary>
    /// The address that is being edited.
    Address? TheAddress;

    /// <summary>
    /// Displays the view.
    /// </summary>
    public void Display()
    {
        if (TheAddress == null)
        {
            ConsoleUtils.Error("Er is iets fout gegaan. Er is geen adres om te bewerken.");
            MainMenuController.Instance.ShowMainMenu();
            return;
        }
        List<string> optionsList = new List<string>() {
            $"Straat: {TheAddress.Street}",
            $"Straat nummer: {TheAddress.HouseNumber}",
            $"Stad: {TheAddress.City}",
            $"Land: {TheAddress.Country}",
            "-",
            "Terug"
        };
        Menu menu = new Menu("Passagier bewerken", optionsList.ToArray());
        int choice = menu.Run();
        ViewBag["MainMenuSelection"] = choice.ToString();
    }

    /// <summary>
    /// Populates the viewbag with the address that is being edited.
    /// </summary>
    public void PopulateViewBag(Address address)
    {
       TheAddress = address;
    }

    /// <summary>
    /// Clears the viewbag.
    /// </summary>
    public void ClearViewBag()
    {
        ViewBag.Clear();
        TheAddress = null;
    }
}