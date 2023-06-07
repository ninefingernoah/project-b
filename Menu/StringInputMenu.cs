public class StringInputMenu
{
    public string Title { get; set; }
    private bool backoption { get; }

    /// <summary>
    /// The constructor for the StringInputMenu class.
    /// </summary>
    /// <param name="title">The title that will be displayed.</param>
    /// <param name="backoption">Whether the menu should tell the user about the option to go back</param>
    public StringInputMenu(string title, bool backoption = true)
    {
        Title = title;
        this.backoption = backoption;
    }

    /// <summary>
    /// Gathers user input and returns it. Returns null if the user wants to go back.
    /// </summary>
    /// <returns>The user input. Returns null if the users types 'terug'</returns>
    public string? Run()
    {
        Console.Clear();
        Console.WriteLine(Title);
        if(backoption)
            Console.WriteLine("Typ 'terug' om terug te gaan.");
        Console.WriteLine();

        string input = "";
        while (input == "")
        {
            Console.Write(">> ");
            input = Console.ReadLine()!;
            if (input == "terug" || input == "Terug" || input == null)
            {
                return null;
            }
        }

        return input;
    }
}