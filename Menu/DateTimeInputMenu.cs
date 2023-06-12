/// <summary>
/// The menu for selecting a date and time.
/// </summary>
public class DateTimeInputMenu
{

    //TODO: pas datetime format aan naar dd-mm-yyyy
    /// <summary>
    /// The title of the menu.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Creates a new instance of the DateTimeInputMenu class.
    /// </summary>
    /// <param name="title">The title of the menu.</param>
    public DateTimeInputMenu(string title)
    {
        Title = title;
    }

    /// <summary>
    /// Runs the menu.
    /// </summary>
    /// <returns>The selected date and time. Can be null if the user wants to go back.</returns>
    public DateTime? Run()
    {
        Console.Clear();
        Console.WriteLine(Title);
        Console.WriteLine("Typ 'terug' om terug te gaan.");
        Console.WriteLine();

        string input;
        DateTime date;
        do
        {
            Console.Write(">> ");
            input = Console.ReadLine()!;
            if (input == "terug" || input == "Terug" || input == null)
            {
                return null;
            }
        } while (!IsValidDate(input, out date));

        return date;
    }

    /// <summary>
    /// Checks if the input is a valid date.
    /// </summary>
    /// <param name="input">The input to check.</param>
    /// <param name="date">The date to return.</param>
    /// <returns>True if the input is a valid date, false if not.</returns>
    private bool IsValidDate(string input, out DateTime date)
    {
        if (DateTime.TryParseExact(input, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out date))
        {
            return true;
        }
        else
        {
            Console.WriteLine("Verkeerde input. Type de datum als volgt: dd/mm/yyyy.");
            return false;
        }
    }
}