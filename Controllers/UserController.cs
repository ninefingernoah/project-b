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

    /*
    *   LOGIN SECTION
    */

    /// <summary>
    /// Shows the login menu and handles the user input.
    /// </summary>
    public void ShowLoginMenu() {
        LoginView.Instance.Display();
        int choice = int.Parse(LoginView.Instance.ViewBag["MainMenuSelection"]);
        switch(choice) {
            case 0:
                ShowLoginEmailInputMenu();
                break;
            case 1:
                ShowLoginPasswordInputMenu();
                break;
            case 3:
                // TODO: Implement login
                // Clear viewbag to prevent the user from showing the wrong info when logging in again.
                // Navigate to main menu (make sure the user sees the authenticated version)
                Login(LoginView.Instance.ViewBag["email"], LoginView.Instance.ViewBag["password"]);
                break;
            case 4:
                LoginView.Instance.ClearViewBag();
                MainMenuController.Instance.ShowMainMenu();
                break;
        }
    }

    private void ShowLoginEmailInputMenu() {
        StringInputMenu menu = new StringInputMenu("Vul uw emailadres in:");
        string? email = menu.Run();
        if (email == null)
        {
            ShowLoginMenu();
            return;
        }

        LoginView.Instance.ViewBag["email"] = email!;
        ShowLoginMenu();
    }

    private void ShowLoginPasswordInputMenu() {
        StringInputMenu menu = new StringInputMenu("Vul uw wachtwoord in:");
        string? password = menu.Run();
        if (password == null)
        {
            ShowLoginMenu();
            return;
        }
        
        LoginView.Instance.ViewBag["password"] = password!;
        LoginView.Instance.ViewBag["displaypassword"] = new string('*', password!.Length);
        ShowLoginMenu();
    }

    private void Login(string email, string password)
    {
        bool success = UserManager.Login(email, password);
        if (success)
        {
            MainMenuController.Instance.ShowMainMenu();
            return;
        }
        ConsoleUtils.Error("Inloggen mislukt.", ShowLoginMenu);
    }

    /*
    *   REGISTER SECTION
    */
}