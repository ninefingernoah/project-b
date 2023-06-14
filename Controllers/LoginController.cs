/// <summary>
/// The controller for logging a user in.
/// </summary>
public sealed class LoginController
{
    private static readonly LoginController instance = new LoginController();

    static LoginController()
    {
    }

    private LoginController()
    {
    }

    public static LoginController Instance
    {
        get
        {
            return instance;
        }
    }

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
                // Logs the user in and sends them to the main menu.
                Login(LoginView.Instance.ViewBag["email"], LoginView.Instance.ViewBag["password"]);
                break;
            case 4:
                LoginView.Instance.ClearViewBag();
                MainMenuController.Instance.ShowMainMenu();
                break;
        }
    }

    /// <summary>
    /// Shows the login email input menu and handles the user input.
    /// </summary>
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

    /// <summary>
    /// Shows the login password input menu and handles the user input.
    /// </summary>
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

    /// <summary>
    /// Logs the user in.
    /// </summary>
    private void Login(string email, string password)
    {
        bool success = UserManager.Login(email, password);
        if (success)
        {
            ConsoleUtils.Success("U bent succesvol ingelogd!", MainMenuController.Instance.ShowMainMenu);
            return;
        }
        ConsoleUtils.Error("De combinatie van email en wachtwoord waren niet correct. Probeer het nog een keer!", ShowLoginMenu);
    }
}