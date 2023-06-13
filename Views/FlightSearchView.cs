/// <summary>
/// The view for the flight search menu. Singleton.
/// </summary>
public class FlightSearchView
{
    /// <summary>
    /// The singleton instance.
    /// </summary>
    public static FlightSearchView Instance { get; } = new FlightSearchView();

    /// <summary>
    /// The viewbag. Holds temporary data for the view.
    /// </summary>
    public Dictionary<string, string> ViewBag { get; set; } = new Dictionary<string, string>();

    static FlightSearchView()
    {
    }
    private FlightSearchView()
    {
        ResetViewBag();
    }

    /// <summary>
    /// Displays the view.
    /// </summary>
    public void Display()
    {
        Menu menu = new Menu("Kies een manier om een vlucht te zoeken", new string[] { "Filters", "Vlucht ID", "Terug" });
        int choice = menu.Run();
        ViewBag["FlightSearchSelection"] = choice.ToString();
    }

    /// <summary>
    /// Resets the ViewBag to default values. Clears the ViewBag first.
    /// </summary>
    public void ResetViewBag()
    {
        ViewBag.Clear();
        ViewBag.Add("FlightSearchSelection", "");
    }

}