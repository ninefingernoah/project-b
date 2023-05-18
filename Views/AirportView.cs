public class AirportView : IView
{
    Menu menu = new Menu("Airport Menu", new string[] {
        "Naam van het vliegveld",
        "Locatie van het vliegveld",
        "Prijzen van vluchten",
        "Overzicht",
        "-",
        "Confirm",
        "Cancel"
    });

    public void Display()
    {
        int choice = menu.Run();
        switch (choice)
        {
            case 0: // Naam van het vliegveld
                AirportLocationView.Display();
                break;
            case 1: // Locatie van het vliegveld
                break;
            case 2: // Prijzen van vluchten
                break;
            case 3: // Overzicht
                break;
            case 4: // Confirm
                break;
            case 5: // Cancel
                break;
            default: // Should never happen.
                break;
        }
    }

    public void AirportNameMenu()
    {
        StringInputMenu menu = new("De naam van het vliegveld: ");
        string? Vliegveldnaam = menu.Run();
    }
}
