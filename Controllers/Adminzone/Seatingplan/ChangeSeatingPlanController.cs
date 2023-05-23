public class ChangeSeatingPlanController
{
    public static void ShowChangeSeatingPlanMenu()
    {
        Menu menu = new Menu("Zitplan", new string[] { "Overzicht", "Zoek tafel", "Terug" });
        int selectedIndex = menu.Run(true);
        switch (selectedIndex)
        {
            case 0:
                // Show seating plan
                ShowSeatingPlan();
                break;
            case 1:
                // Search seating
                // TODO: Search seating implementation
                ShowSearchMenu();
                break;
            case 2:
                // Back
                SeatingPlanController.ShowMainMenu();
                break;
            default:
                // This should never happen
                throw new Exception("Impossible menu selection");
        }
    }

    private static void ShowSearchMenu()
    {
        int seatingnum = ConsoleUtils.ReadInt("Enter the number of the seating: ");
        List<Seating> seatings = Program.SeatingManager.Seatings;
        Seating? seating = seatings.Find(s => s.SeatingID == seatingnum);
        if (seating == null)
        {

        }
    }
    
    private static void ShowSeatingPlan()
    {
        SeatingManager sm = Program.SeatingManager;
        if (sm.Seatings.Count == 0)
        {
            ConsoleUtils.Error("Er zijn geen tafels in het zitplan.");
            SeatingPlanController.ShowMainMenu();
            return;
        }
        ShowSeatingPlanPage(0);
    }

    private static void ShowSeatingPlanPage(int page)
    {
        SeatingManager sm = Program.SeatingManager;
        int maxPage = (int)Math.Ceiling((double)sm.Seatings.Count / 5);
        if (page < 0 || page > maxPage)
        {
            ConsoleUtils.Error("Ongeldige pagina.");
            SeatingPlanController.ShowMainMenu();
            return;
        }
        Console.Clear();
        Console.WriteLine("Zitplan");
        Console.WriteLine($"Pagina {page + 1} van {maxPage + 1}");
        Console.WriteLine();
        List<String> options = new List<string>();
        for (int i = 0; i < 5; i++)
        {
            int index = page * 5 + i;
            if (index >= sm.Seatings.Count)
            {
                break;
            }
            Seating s = sm.Seatings[index];
            options.Add($"Tafel {s.SeatingID}:\nAantal stoelen: {s.NumOfChairs}\nGedeelte: {s.Area}\nBeschikbaar: {s.Available} ? 'Ja' : 'Nee'");
            options.Add("-");
        }
        if (page > 0)
        {
            options.Add("Vorige pagina");
        }
        if ((page + 1) < maxPage)
        {
            options.Add("Volgende pagina");
        }
        options.Add("Terug");
        Menu menu = new Menu("Zitplan", options.ToArray());
        int selectedIndex = menu.Run(true);
        if (selectedIndex == options.Count - 1)
        {
            // Back
            SeatingPlanController.ShowMainMenu();
            return;
        }
        if (page > 0 && selectedIndex == options.Count - 2)
        {
            // Previous page
            ShowSeatingPlanPage(page - 1);
            return;
        }
        if (page < maxPage && selectedIndex == options.Count - 2)
        {
            // Next page
            ShowSeatingPlanPage(page + 1);
            return;
        }
        // selectedIndex is divided by 2 because of the "-" options. This avoids it.
        int seatingIndex = page * 5 + (selectedIndex / 2);
        Seating seating = sm.Seatings[seatingIndex];
        // Change logic
        // TODO: Change seating implementation
        // TODO: Give user error when trying to change table which houses reservations. And prompt them to change the reservations.
    }
}