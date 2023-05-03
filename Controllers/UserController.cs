/// <summary>
/// The controller for the user menus.
/// </summary>
public sealed class UserController {

    /// <summary>
    /// The singleton instance of the user menus controller. Used for accessing the controller. Thread safe.
    /// </summary>
    private static readonly UserController instance = new UserController();

    static UserController() {
    }
    private UserController() {
    }

    /// <summary>
    /// The singleton instance of the user menus controller. Used for accessing the controller. Thread safe.
    /// </summary>
    public static UserController Instance {
        get {
            return instance;
        }
    }

    public void ShowLoginMenu() {
        LoginView.Instance.Display();
        int choice = int.Parse(LoginView.Instance.ViewBag["MainMenuSelection"]);
        switch(choice) {
            case 0:
            ShowEmailInputMenu();
                break;
            case 2:
                // TODO: Implement login
                // Clear viewbag to prevent the user from showing the wrong info when logging in again.
                // Navigate to main menu (make sure the user sees the authenticated version)
                break;
            case 3:
                LoginView.Instance.ClearViewBag();
                MainMenuView.Instance.Display();
                break;
        }
    }

    public void ShowEmailInputMenu() {
        
    }
}