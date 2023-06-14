public class NewFlightView
{
    private static readonly NewFlightView instance = new NewFlightView();
    public Airport Departure;
    public Airport Arrival;
    public Airplane Plane;
    public DateTime depTime;
    public DateTime ArrTime;

    static NewFlightView()
    {
    }

    /// <summary>
    /// Displays the flight editor view.
    /// </summary>
    public int Display()
    {
        List<string> optionsList = new List<string>() {
            $"Vertrek vanaf: {Departure}",
            $"Aankomst op: {Arrival}",
            $"Vliegtuig: {Plane}",
            $"Vertrekdatum: {depTime.ToString("dd-MM-yyyy HH:mm")}",
            $"Aankomstdatum: {ArrTime.ToString("dd-MM-yyyy HH:mm")}",
            "-",
            "Opslaan",
            "-",
            "Terug"
        };
        string[] options = optionsList.ToArray();
        Menu editorMenu;
        editorMenu = new Menu("Nieuwe vlucht", options);
        return editorMenu.Run();

    }

    /// <summary>
    /// The singleton instance of the main menu controller. Used for accessing the controller. Thread safe.
    /// </summary>
    public static NewFlightView Instance
    {
        get
        {
            return instance;
        }
    }

    public void ClearViewBag()
    {
        Departure = null;
        Arrival = null;
        Plane = null;
        depTime = DateTime.MinValue;
        ArrTime = DateTime.MinValue;
    }

    public bool CheckValid()
    {
        try
        {
            if (Departure == null)
            {
                return false;
            }
            else if (Arrival == null)
            {
                return false;
            }
            else if (Plane == null)
            {
                return false;
            }
            else if (depTime == DateTime.MinValue)
            {
                return false;
            }
            else if (ArrTime == DateTime.MinValue)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        catch (System.Exception)
        {

            return false;
        }

    }
}
