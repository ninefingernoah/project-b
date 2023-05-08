using System.Text.RegularExpressions;
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
            ConsoleUtils.Success("U bent succesvol ingelogd!", MainMenuController.Instance.ShowMainMenu);
            return;
        }
        ConsoleUtils.Error("De combinatie van email en wachtwoord waren niet correct. Probeer het nog een keer!", ShowLoginMenu);
    }

    /*
    *   REGISTER SECTION
    */
    public void ShowRegisterMenu()
    {
        RegisterView.Instance.Display();
        int choice = int.Parse(RegisterView.Instance.ViewBag["MainMenuSelection"]);
        switch (choice)
        {
            case 0:
                ShowFirstNameInputMenu();
                break;
            case 1:
                ShowLastNameInputMenu();
                break;
            case 2:
                ShowEmailInputMenu();
                break;
            case 3:
                ShowRegisterPasswordInputMenu();
                break;
            case 4:
                ShowRegisterConfirmPasswordInputMenu();
                break;
            case 6:
                var fname = RegisterView.Instance.ViewBag["firstname"];
                var lname = RegisterView.Instance.ViewBag["lastname"];
                var email = RegisterView.Instance.ViewBag["email"];
                var password = RegisterView.Instance.ViewBag["password"];
                if (password != RegisterView.Instance.ViewBag["cpassword"])
                {
                    ConsoleUtils.Error("De wachtwoorden komen niet overeen. Probeer het nog een keer!", ShowRegisterMenu);
                    return;
                }
                if (fname == "" || lname == "" || email == "" || password == "")
                {
                    ConsoleUtils.Error("U heeft niet alle velden ingevuld. Probeer het nog een keer!", ShowRegisterMenu);
                    return;
                }
                UserManager.Register(fname, lname, email, password);
                RegisterView.Instance.ClearViewBag();
                ConsoleUtils.Success("U bent succesvol geregistreerd!", MainMenuController.Instance.ShowMainMenu);
                break;
            case 7:
                RegisterView.Instance.ClearViewBag();
                MainMenuController.Instance.ShowMainMenu();
                break;
        }
    }

    private void ShowRegisterPasswordInputMenu()
    {
        StringInputMenu menu = new StringInputMenu("Vul uw wachtwoord in:");
        string? password = menu.Run();
        if (password == null)
        {
            ShowRegisterMenu();
            return;
        }

        RegisterView.Instance.ViewBag["password"] = password!;
        RegisterView.Instance.ViewBag["displaypassword"] = new string('*', password!.Length);
        ShowRegisterMenu();
    }

    private void ShowRegisterConfirmPasswordInputMenu()
    {
        StringInputMenu menu = new StringInputMenu("Vul uw wachtwoord in:");
        string? password = menu.Run();
        if (password == null)
        {
            ShowRegisterMenu();
            return;
        }

        if(password != RegisterView.Instance.ViewBag["password"])
        {
            ConsoleUtils.Error("De wachtwoorden komen niet overeen. Probeer het nog een keer!", ShowRegisterMenu);
            return;
        }

        RegisterView.Instance.ViewBag["cpassword"] = password!;
        RegisterView.Instance.ViewBag["cdisplaypassword"] = new string('*', password!.Length);
        ShowRegisterMenu();
    }

    private void ShowFirstNameInputMenu()
    {
        StringInputMenu menu = new StringInputMenu("Vul uw voornaam in:");
        string? firstname = menu.Run();
        if (firstname == null)
        {
            ShowRegisterMenu();
            return;
        }

        RegisterView.Instance.ViewBag["firstname"] = firstname!;
        ShowRegisterMenu();
    }

    private void ShowLastNameInputMenu()
    {
        StringInputMenu menu = new StringInputMenu("Vul uw achternaam in:");
        string? lastname = menu.Run();
        if (lastname == null)
        {
            ShowRegisterMenu();
            return;
        }

        RegisterView.Instance.ViewBag["lastname"] = lastname!;
        ShowRegisterMenu();
    }

    private void ShowEmailInputMenu()
    {
        StringInputMenu menu = new StringInputMenu("Vul uw emailadres in:");
        string? email = menu.Run();
        if (email == null)
        {
            ShowRegisterMenu();
            return;
        }
        if (!CheckValidEmail(email))
        {
            ConsoleUtils.Error("Dit is geen geldig emailadres. Probeer het nog een keer!", ShowEmailInputMenu);
            return;
        }

        RegisterView.Instance.ViewBag["email"] = email!;
        ShowRegisterMenu();
    }

    private bool CheckValidEmail(string email)
    {
        Regex rx = new Regex("^[^@\\s]+@[^@\\s]+\\.(com|net|org|gov|nl|be|de|co.uk|uk)$");
        return rx.IsMatch(email);
    }
}