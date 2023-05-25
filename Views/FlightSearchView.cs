public class FlightSearchView
{
    public static FlightSearchView Instance { get; } = new FlightSearchView();

    public Dictionary<string, string> ViewBag { get; set; } = new Dictionary<string, string>();

    static FlightSearchView()
    {
    }
    private FlightSearchView()
    {
        ResetViewBag();
    }

    public void Display()
    {
        Menu menu = new Menu("Zoek een vlucht", new string[] { "Filters", "Vlucht ID", "Terug" });
        int choice = menu.Run();
        ViewBag["FlightSearchSelection"] = choice.ToString();
    }

    public void ResetViewBag()
    {
        ViewBag.Clear();
        ViewBag.Add("FlightSearchSelection", "");
    }

}