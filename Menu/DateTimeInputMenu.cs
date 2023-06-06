public class DateTimeInputMenu
{

    //TODO: pas datetime format aan naar dd-mm-yyyy
    public string Title { get; set; }

    public DateTimeInputMenu(string title)
    {
        Title = title;
    }

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