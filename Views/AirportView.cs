public static class AirportView
{
    Menu menu = new Menu("Airport Menu", new string[] {
        "Naam van het land",
        "Naam van de stad",
        "Prijzen van de vluchten",
        "Overzicht",
        "-",
        "Confirm"
    });

    public void Run()
    {
        int selectedIndex = menu.Run();
        switch(selectedIndex)
        {
            case 0:
                StringInputMenu LandMenu = new StringInputMenu("Naam van het land");
                string? VliegveldLand = LandMenu.Run();	
                break;
            case 1:
                StringInputMenu StadMenu = new StringInputMenu("Naam van de stad");	
                string? VliegveldStad = StadMenu.Run();
                break;
            case 2:
                // Prijzen van de vluchten
                break;
            case 3:
                // Overzicht
                break;
            case 5:
                // Confirm
                break;
        }
    }

    public void Overzicht()
    {
        clear();
        Console.WriteLine("Overzicht:");
        Console.WriteLine("Land: " + VliegveldLand);
        Console.WriteLine("Stad: " + VliegveldStad);
        Console.WriteLine("Prijzen van de vluchten: " + VliegveldPrijzen);
        Console.WriteLine("Druk op een toets om terug te gaan.");
    }
    
}