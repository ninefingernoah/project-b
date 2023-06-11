public class AddressEditorView : IView
{

    private static readonly AddressEditorView instance = new AddressEditorView();

    static AddressEditorView()
    {
    }

    private AddressEditorView()
    {
    }

    public static AddressEditorView Instance
    {
        get
        {
            return instance;
        }
    }

    public Dictionary<string, object> ViewBag = new Dictionary<string, object>();

    Address TheAddress;

    public void Display()
    {
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

    public void PopulateViewBag(Address address)
    {
       TheAddress = address;
    }

    public void ClearViewBag()
    {
        ViewBag.Clear();
    }
}