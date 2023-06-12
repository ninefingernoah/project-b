/// <summary>
/// The view for editing a passenger. Singleton.
public class PassengerView
{
    /// <summary>
    /// The singleton instance.
    /// </summary>
    private static readonly PassengerView instance = new PassengerView();

    /// <summary>
    /// The viewbag. Holds temporary data for the view.
    /// </summary>
    public Dictionary<string, string> ViewBag = new Dictionary<string, string>();

    /// <summary>
    /// List that holds temporary data for the view.
    /// </summary>
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
    /// Displays the menu for creating a passenger.
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
        Menu passengerMenu;
        string[] options = optionsList.ToArray();
        if (ViewBag.ContainsKey("MainMenuSelection"))
        {
            passengerMenu = new Menu("Nieuwe reiziger", options, int.Parse((string)ViewBag["MainMenuSelection"]));
        } else
        {
            passengerMenu = new Menu("Nieuwe reiziger", options);
        }
        int choice = passengerMenu.Run();
        ViewBag["MainMenuSelection"] = choice.ToString();
    }

    /// <summary>
    /// Displays the menu for editing a passenger.
    /// </summary>
    /// <param name="passenger">The passenger that has been edited.</param>
    public Passenger? Run()
    {
        bool loop = true;
        string? voornaam = "";
        string? achternaam = "";
        string? email = "";
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
                    if(voornaam != null)
                        views[0] = voornaam;
                    break;
                case 1:
                    achternaam = new StringInputMenu("Vul uw achternaam in:").Run();
                    if(achternaam != null)
                        views[1] = achternaam;
                    break;
                case 2:
                    email = new StringInputMenu("Vul uw email in:").Run();
                    if(email == null || !StringUtils.CheckValidEmail(email))
                    {
                        ConsoleUtils.Error("Dit is geen geldig email adres.");
                        break;
                    }
                    views[2] = email;
                    break;
                case 3:
                    docnummer = new StringInputMenu("Vul uw Document nummer in:").Run();
                    if(docnummer != null)
                        views[3] = docnummer;
                    break;
                case 4:
                    date = new DateTimeInputMenu("Vul uw geboorte datum in:").Run();
                    if(date != null)
                        views[4] = ((DateTime)date).ToString("dd/MM/yyyy"); // Forces the datetime to be displayed in the correct format.
                    break;
                case 5:
                    street = new StringInputMenu("Vul uw straatnaam in:").Run();
                    if (street != null)
                        views[5] = street;
                    break;
                case 6:
                    huisnummer = new IntInputMenu("Vul uw huisnummer in:").Run();
                    if (huisnummer != null && huisnummer > 0)
                        views[6] = huisnummer.ToString()!;
                    break;
                case 7:
                    city = new StringInputMenu("Vul uw plaats in:").Run();
                    if (city != null)
                        views[7] = city;
                    break;
                case 8:
                    country = new StringInputMenu("Vul uw land in:").Run();
                    if (country != null)
                        views[8] = country;
                    break;
                case 10:
                    if (!views.Contains("<vul in>") && !views.Contains(""))
                    {
                        loop = false;
                    }
                    break;
                case 11:
                    return null;

            }
        } while (loop);

        Address address = new Address(city!, country!, street!, huisnummer.ToString()!); // All the parameters are checked to be not null in the loop above.
        return new Passenger(PassengerManager.GetNextId(), email!, voornaam!, achternaam!, docnummer!, date, address); // Same here.
    }

    /// <summary>
    /// Clears the viewbag.
    /// </summary>
    public void ClearViewBag()
    {
        ViewBag.Clear();
    }

    /// <summary>
    /// Clears the views.
    /// </summary>
    public void ClearView()
    {
        views.Clear();
    }
}
