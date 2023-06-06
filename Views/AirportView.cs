public class AirportView : IView
{
    private static readonly AirportView instance = new AirportView();
    
    public Dictionary<string, string> ViewBag = new Dictionary<string, string>();
    public List<string> facilities = new List<string>();

    public static AirportView Instance
    {
        get { return instance; }
    }

    public void Display()
    {
        populateViewBag();

        List<string> optionsList = new List<string>()
        {
            $"Land waar het vliegveld zich bevindt: {ViewBag["AirportCountry"]}",
            $"Stad waar het vliegveld zich bevindt: {ViewBag["AirportCity"]}",
            $"Naam van het vliegveld: {ViewBag["AirportName"]}",
            $"Code van het vliegveld: {ViewBag["AirportCode"]}",
            "Vluchten en prijzen",
            "Faciliteiten",
            "-",
            "Confirm",
            "Annuleren"
        };
        string[] options = optionsList.ToArray();
        Menu airportMenu = new Menu("Vliegveld aanmaken", options);
        int choice = airportMenu.Run();
        ViewBag["AirportMenuSelection"] = choice.ToString();
    }

    private void populateViewBag()
    {
        if(!ViewBag.ContainsKey("AirportCountry"))
            ViewBag["AirportCountry"] = "<Vul In>";
        if(!ViewBag.ContainsKey("AirportCity"))
            ViewBag["AirportCity"] = "<Vul In>";
        if(!ViewBag.ContainsKey("AirportName"))
            ViewBag["AirportName"] = "<Vul In>";
        if(!ViewBag.ContainsKey("AirportCode"))
            ViewBag["AirportCode"] = "<Vul In>";
    }

    public void ClearViewBag()
    {
        ViewBag.Clear();
        facilities = new List<string>();
    }
}