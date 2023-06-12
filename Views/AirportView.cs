/// <summary>
/// View for creating an airport. Singleton.
/// </summary>
public class AirportView : IView
{
    private static readonly AirportView instance = new AirportView();
    
    public Dictionary<string, string> ViewBag = new Dictionary<string, string>();
    public List<string> facilities = new List<string>();

    public static AirportView Instance
    {
        get { return instance; }
    }

    /// <summary>
    /// Displays the creation menu of an airport.
    /// </summary>
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
            "Opslaan",
            "Annuleren"
        };
        string[] options = optionsList.ToArray();
        Menu airportMenu = new Menu("Vliegveld aanmaken", options);
        int choice = airportMenu.Run();
        ViewBag["AirportMenuSelection"] = choice.ToString();
    }

    /// <summary>
    /// Populates the viewbag with the needed data.
    /// </summary>
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

    /// <summary>
    /// Clears the viewbag. Used for when the user is finished with the view.
    /// </summary>
    public void ClearViewBag()
    {
        ViewBag.Clear();
        facilities = new List<string>();
    }

    /// <summary>
    /// Checks if all fields are filled in.
    /// </summary>
    public bool AllFieldsFilledIn()
    {
        foreach(KeyValuePair<string, string> entry in ViewBag)
        {
            if(entry.Value == "<Vul In>")
                return false;
        }
        return true;
    }
}