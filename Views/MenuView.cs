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
    public void Display(string prompt, List<string> optionsList, string? TopString = null, string? BotString = null, bool ViewBagBool = false)
    {
        if (ViewBagBool)
        {
            PopulateViewBag(optionsList);
            List<string> newList = new List<string>();
            foreach (string option in optionsList)
            {
                if (option != "-")
                {
                    newList.Add($"{option}: {ViewBag[option]}");
                }
            }
            optionsList = newList;
        }
        var options = optionsList.ToArray();

        Menu Menu = new Menu(prompt, options, TopString: TopString, BotString: BotString);
        int choice = Menu.Run();
        LastChoice = choice;
        ViewBag["Selection"] = choice.ToString();
    }

    private void PopulateViewBag(List<string> options)
    {
        foreach (string option in options)
        {
            if (!ViewBag.ContainsKey(option))
            {
                ViewBag[option] = "<vul in>";
            }
        }
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
