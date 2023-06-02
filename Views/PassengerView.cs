public class PassengerView
{
    private static readonly PassengerView instance = new PassengerView();
    public Dictionary<string, string> ViewBag = new Dictionary<string, string>();

    public List<string> views = new List<string>();

    static PassengerView()
    {
    }
    private PassengerView()
    {
    }

    /// <summary>
    /// The singleton instance of the main menu controller. Used for accessing the controller. Thread safe.
    /// </summary>
    public static PassengerView Instance
    {
        get
        {
            return instance;
        }
    }


    /// <summary>
    /// Displays the login menu.
    /// </summary>
    private void Display()
    {
        List<string> optionsList = new List<string>() {
            $"Voornaam: {views[0]}",
            $"Achternaam: {views[1]}",
            $"Email: {views[2]}",
            $"Document nummer: {views[3]}",
            $"Geboorte datum: {views[4]}",
            $"Straatnaam: {views[5]}",
            $"Huisnummer: {views[6]}",
            $"Plaats: {views[7]}",
            $"Land: {views[8]}",
            "-",
            "Sla op",
            "Ga terug"
        };
        string[] options = optionsList.ToArray();
        Menu loginMenu = new Menu("Nieuwe reiziger", options);
        int choice = loginMenu.Run();
        ViewBag["MainMenuSelection"] = choice.ToString();
    }

    public Passenger? Run()
    {
        bool loop = true;
        string? voornaam = "";
        string? achternaam = "";
        string email = "";
        string? docnummer = "";
        DateTime? date = DateTime.MinValue;
        string? street = "";
        int? huisnummer = -1;
        string? city = "";
        string? country = "";

        for (int i = 0; i < 9; i++)
        {
            views.Add("<vul in>");
        }
        do
        {
            Display();
            int choice = int.Parse(PassengerView.Instance.ViewBag["MainMenuSelection"]);
            switch (choice)
            {
                case 0:
                    voornaam = new StringInputMenu("Vul uw voornaam in:").Run();
                    views[0] = voornaam;
                    break;
                case 1:
                    achternaam = new StringInputMenu("Vul uw achternaam in:").Run();
                    views[1] = achternaam;
                    break;
                case 2:
                    email = new StringInputMenu("Vul uw email in:").Run();
                    views[2] = email;
                    break;
                case 3:
                    docnummer = new StringInputMenu("Vul uw Document nummer in:").Run();
                    views[3] = docnummer;
                    break;
                case 4:
                    date = new DateTimeInputMenu("Vul uw geboorte datum in:").Run();
                    views[4] = date.ToString();
                    break;
                case 5:
                    street = new StringInputMenu("Vul uw straatnaam in:").Run();
                    views[5] = street;
                    break;
                case 6:
                    huisnummer = new IntInputMenu("Vul uw straatnummer in:").Run();
                    views[6] = huisnummer.ToString();
                    break;
                case 7:
                    city = new StringInputMenu("Vul uw plaats in:").Run();
                    views[7] = city;
                    break;
                case 8:
                    country = new StringInputMenu("Vul uw land in:").Run();
                    views[8] = country;
                    break;
                case 10:
                    if (!views.Contains("<vul in>"))
                    {
                        loop = false;
                    }
                    break;
                case 11:
                    return null;

            }
        } while (loop);

        Address address = new Address(city, country, street, huisnummer.ToString());
        return new Passenger(PassengerManager.GetNextId(), email, voornaam, achternaam, docnummer, date, address);
    }

    public void ClearViewBag()
    {
        ViewBag.Clear();
    }
    public void ClearView()
    {
        views.Clear();
    }
}
