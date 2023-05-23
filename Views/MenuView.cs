public class MenuView
{
    private static readonly MenuView instance = new MenuView();
    public Dictionary<string, string> ViewBag = new Dictionary<string, string>();
    public int LastChoice { get; set; }
    static MenuView()
    {
    }
    private MenuView()
    {
    }

    /// <summary>
    /// Displays the menu.
    /// </summary>
    public void Display(string prompt, List<string> optionsList, string? TopString = null, string? BotString = null)
    {
        string[] options = optionsList.ToArray();
        Menu Menu = new Menu(prompt, options, TopString, BotString);
        int choice = Menu.Run();
        LastChoice = choice;
        ViewBag["Selection"] = choice.ToString();
    }

    public void ClearViewBag()
    {
        ViewBag.Clear();
    }

    /// <summary>
    /// The singleton instance of the main menu controller. Used for accessing the controller. Thread safe.
    /// </summary>
    public static MenuView Instance
    {
        get
        {
            return instance;
        }
    }
}
