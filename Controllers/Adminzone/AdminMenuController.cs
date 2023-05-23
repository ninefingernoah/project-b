public class AdminMenuController
{
    public static void ShowAdminMainMenu()
    {
        string[] options = new string[] { "Reserveringen", "Menukaart wijzigen", "Restaurant info wijzigen", "Zitplan wijzigen", "Uitloggen" };
        Menu menu = new Menu(Program.RestaurantManager.Restaurant.Name, options);
        int selectedIndex = menu.Run(true);
        switch (selectedIndex)
        {
            case 0:
                // Show reservations
                break;
            case 1:
                // Show dish menu
                break;
            case 2:
                // Show restaurant info
                break;
            case 3:
                SeatingPlanController.ShowMainMenu();
                break;
            
            default:
                // This should never happen
                throw new Exception("Impossible menu selection");
        }
    }
}