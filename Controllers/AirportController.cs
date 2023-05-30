public class AirportController {
    private static readonly AirportController instance = new AirportController();
    private AirportController() {
    }
    static AirportController() {
    }
    public static AirportController Instance {
        get {
            return instance;
        }
    }

    public void ShowAirportDetails(Airport airport) {
        AirportDetailsView.Instance.Display(airport);
        int choice = int.Parse(AirportDetailsView.Instance.ViewBag["AirportDetailsSelection"]);
        ShowAirportList();
    }

    public void ShowAirportList() {
        AirportListView.Instance.Display(AirportManager.GetAirports());
        int choice = int.Parse(AirportListView.Instance.ViewBag["AirportListSelection"]);
        if (choice == AirportManager.GetAirports().Count) {
            MainMenuController.Instance.ShowMainMenu();
        } else {
            Airport airport = AirportManager.GetAirports()[choice];
            ShowAirportDetails(airport);
        }
    }
}