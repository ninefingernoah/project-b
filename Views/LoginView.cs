public sealed class LoginView {
    private static readonly LoginView instance = new LoginView();
    public Dictionary<string, string> ViewBag = new Dictionary<string, string>();

    static LoginView(){
    }
    private LoginView(){
    }

    public static LoginView Instance {
        get {
            return instance;
        }
    }

    public User Display() {
        string[] options = new string[] { "E-mail: <vul in>", "Wachtwoord: <vul in>", "Terug" };
        Menu menu = new Menu("Log in", options);
        int selection = menu.Run();
        //if selection == 2 mainmenu
        return null;
    }
}