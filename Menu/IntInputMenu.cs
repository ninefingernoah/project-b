/// <summary>
/// The menu for selecting an integer.
/// </summary>
public class IntInputMenu
{
    /// <summary>
    /// The title of the menu.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Whether an option to go back should be displayed.
    /// </summary>
    private bool backoption { get; }

    /// <summary>
    /// Creates a new instance of the IntInputMenu class.
    /// </summary>
    /// <param name="title">The title of the menu.</param>
    /// <param name="backoption">Whether an option to go back should be displayed.</param>
    public IntInputMenu(string title, bool backoption = true)
    {
        Title = title;
        this.backoption = backoption;
    }

    /// <summary>
    /// Runs the menu.
    /// </summary>
    /// <returns>The selected integer. Can be null if the user wants to go back.</returns>
    public int? Run()
    {
        Console.Clear();
        Console.WriteLine(Title);
        if (backoption)
            Console.WriteLine("Typ 'terug' om terug te gaan.");
        Console.WriteLine();

        string input;
        int number;
        do
        {

            Console.Write(">> ");
            input = Console.ReadLine()!;
            if (input == "terug" || input == "Terug" || input == null)
            {
                return null;
            }
        } while (!int.TryParse(input, out number));

        return number;
    }
}