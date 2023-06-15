using System.Text.RegularExpressions;
/// <summary>
/// The controller for the register menu.
/// </summary>
public sealed class RegisterController
{
    private static readonly RegisterController instance = new RegisterController();

    static RegisterController()
    {
    }

    private RegisterController()
    {
    }

    public static RegisterController Instance
    {
        get
        {
            return instance;
        }
    }

    /// <summary>
    /// Shows the register menu and handles the user input.
    /// </summary>
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

    /// <summary>
    /// Shows the register password input menu and handles the user input.
    /// </summary>
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

    /// <summary>
    /// Shows the register confirm password input menu and handles the user input.
    /// </summary>
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

    /// <summary>
    /// Shows the first name input menu and handles the user input.
    /// </summary>
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

    /// <summary>
    /// Shows the last name input menu and handles the user input.
    /// </summary>
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

    /// <summary>
    /// Shows the email input menu and handles the user input.
    /// </summary>
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

    /// <summary>
    /// Checks if a user exists.
    /// </summary>
    /// <param name="email">The email of the user.</param>
    private bool CheckValidEmail(string email)
    {
        Regex rx = new Regex("^[^@\\s]+@[^@\\s]+\\.(com|net|org|gov|nl|be|de|co.uk|uk)$");
        return rx.IsMatch(email);
    }
}