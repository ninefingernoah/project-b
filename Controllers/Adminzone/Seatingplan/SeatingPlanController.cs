public class SeatingPlanController
{
    public static void ShowMainMenu()
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.Clear();
        string[] options = new string[] { "Voeg toe", "Verander", "Terug" };
        Menu menu = new Menu("Zitplan", options);
        int selectedIndex = menu.Run(true);
        switch (selectedIndex)
        {
            case 0:
                // Add seating
                AddTableController.ShowAddTableMenu();
                break;
            case 1:
                // Change seating
                ChangeSeatingPlanController.ShowChangeSeatingPlanMenu();
                break;
            case 2:
                AdminMenuController.ShowAdminMainMenu();
                break;
            default:
                // This should never happen
                throw new Exception("Impossible menu selection");
        }
    }
}