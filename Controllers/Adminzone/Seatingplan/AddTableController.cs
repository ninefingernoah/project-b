public class AddTableController
{
    public static void ShowAddTableMenu()
    {
        Seating seating = new Seating(false, 0, "", -1);
        Dictionary<string, string> options = new Dictionary<string, string>();
        options.Add("Tafelnummer", "<vul in>");
        options.Add("Aantal stoelen", "<vul in>");
        options.Add("Gedeelte", "<vul in>");
        options.Add("-", "");
        options.Add("Opslaan", "");
        options.Add("Annuleren", "");
        InputMenu menu = new InputMenu("Voeg een tafel toe", options);
        ShowAddTableMenu(menu, seating);
    }

    /// <summary>
    /// Shows the add table menu. This is a recursive function.
    /// </summary>
    /// <param name="menu">The menu to show.</param>
    /// <param name="seating">The seating to add.</param>
    public static void ShowAddTableMenu(InputMenu menu, Seating seating)
    {
        int selectedIndex = menu.Run(true);
        switch(selectedIndex)
        {
            case 0:
                // Tafelnummer
                AskTableNumber(menu, seating);
                break;
            case 1:
                // Aantal stoelen
                AskNumberOfChairs(menu, seating);
                break;
            case 2:
                // Gedeelte
                AskArea(menu, seating);
                break;
            case 4:
                // Opslaan
                SeatingManager sm = Program.SeatingManager;
                if(!seating.IsComplete())
                {
                    ConsoleUtils.Error("Niet alle velden zijn ingevuld.");
                    ShowAddTableMenu(menu, seating);
                    return;
                }
                // Dit wordt voorheen al gecheckt, maar gewoon voor de zekerheid.
                if(sm.SeatingExists(seating.SeatingID))
                {
                    ConsoleUtils.Error("Er bestaat al een tafel met dat tafelnummer.");
                    ShowAddTableMenu(menu, seating);
                    return;
                }
                seating.Available = true;
                sm.AddSeating(seating);
                ConsoleUtils.Success($"De tafel({seating.SeatingID}) is toegevoegd.");
                SeatingPlanController.ShowMainMenu();
                break;
            case 5:
                // Annuleren
                SeatingPlanController.ShowMainMenu();
                break;
        }
    }

    /// <summary>
    /// Asks the user for the number of chairs.
    /// </summary>
    /// <param name="menu">The menu to show.</param>
    /// <param name="seating">The seating to add.</param>
    public static void AskTableNumber(InputMenu menu, Seating seating)
    {
        Console.Clear();
        Console.WriteLine("Voer het tafelnummer in:");
        string input = Console.ReadLine()!;
        if (input == "")
        {
            ShowAddTableMenu(menu, seating);
            return;
        }
        int tableNumber;
        if (!int.TryParse(input, out tableNumber))
        {
            ConsoleUtils.Error("Dat is niet een geldig tafelnummer.");
            AskTableNumber(menu, seating);
            return;
        }
        if(tableNumber < 1)
        {
            ConsoleUtils.Error("Dat is niet een geldig tafelnummer.");
            AskTableNumber(menu, seating);
            return;
        }
        SeatingManager sm = Program.SeatingManager;
        if (sm.SeatingExists(tableNumber))
        {
            ConsoleUtils.Error("Er bestaat al een tafel met dat tafelnummer.");
            AskTableNumber(menu, seating);
            return;
        }
        seating.SeatingID = tableNumber;
        menu.SetOption(0, seating.SeatingID.ToString());
        ShowAddTableMenu(menu, seating);
    }

    /// <summary>
    /// Asks the user for the area.
    /// </summary>
    /// <param name="menu">The menu to show.</param>
    /// <param name="seating">The seating to add.</param>
    public static void AskNumberOfChairs(InputMenu menu, Seating seating)
    {
        Dictionary<string, string> options = new Dictionary<string, string>();
        options.Add("1", "");
        options.Add("2", "");
        options.Add("4", "");
        options.Add("-", "");
        options.Add("Annuleren", "");
        InputMenu chairsMenu = new InputMenu("Hoeveel stoelen?", options);
        int selectedIndex = chairsMenu.Run(true);
        if(selectedIndex >= 0 && selectedIndex <= 1)
        {
            seating.NumOfChairs = selectedIndex + 1;
            menu.SetOption(1, seating.NumOfChairs.ToString());
        }
        if (selectedIndex == 2)
        {
            seating.NumOfChairs = 4;
            menu.SetOption(1, seating.NumOfChairs.ToString());
        }
        ShowAddTableMenu(menu, seating);
    }

    public static void AskArea(InputMenu menu, Seating seating)
    {
        Console.Clear();
        Console.WriteLine("Voer het gedeelte in:");
        string input = Console.ReadLine()!;
        if (input == "")
        {
            ShowAddTableMenu(menu, seating);
            return;
        }
        if (input.Length > 50)
        {
            ConsoleUtils.Error("De naam van het gedeelte mag niet langer zijn dan 50 karakters.");
            AskArea(menu, seating);
            return;
        }
        seating.Area = input;
        menu.SetOption(2, seating.Area);
        ShowAddTableMenu(menu, seating);
    }
}