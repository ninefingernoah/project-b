/// <summary>
/// View for displaying airport details.
/// </summary>
public class AirportDetailsView {
    public Dictionary<string, string> ViewBag = new Dictionary<string, string>();
    private static readonly AirportDetailsView instance = new AirportDetailsView();

    static AirportDetailsView() {
    }
    private AirportDetailsView() {
    }

    /// <summary>
    /// The singleton instance of the main menu controller. Used for accessing the controller. Thread safe.
    /// </summary>
    public static AirportDetailsView Instance {
        get {
            return instance;
        }
    }

    /// <summary>
    /// Displays airport details.
    /// </summary>
    public void Display(Airport airport){
        string[] options = new string[] { "Terug" };
        string topString = "Luchthaven details:\n\n";
        topString += airport.AirportOverviewString();
        Menu menu = new Menu(topString, options);
        int choice = menu.Run();
        ViewBag["AirportDetailsSelection"] = choice.ToString();
    }

}