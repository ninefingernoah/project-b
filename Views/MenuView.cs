/// <summary>
/// A multi-purpose menu view. Singleton.
/// </summary>
// Suggestion: Make this class not a singleton and maybe more generic?
public class MenuView
{
    /// <summary>
    /// The singleton instance.
    /// </summary>
    private static readonly MenuView instance = new MenuView();

    /// <summary>
    /// The getter for the singleton instance.
    /// </summary>
    public static MenuView Instance
    {
        get
        {
            return instance;
        }
    }

    /// <summary>
    /// The viewbag. Holds temporary data for the view.
    /// </summary>
    public Dictionary<string, string> ViewBag = new Dictionary<string, string>();

    /// <summary>
    /// The last choice made by the user.
    /// </summary>
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

    /// <summary>
    /// Populates all the items in the viewbag with the value "<vul in>" if they don't exist yet.
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

    /// <summary>
    /// Clears the viewbag.
    /// </summary>
    public void ClearViewBag()
    {
        ViewBag.Clear();
    }
}
