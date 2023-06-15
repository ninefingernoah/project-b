public class StringInputMenu
{
    public string Title { get; set; }

    public StringInputMenu(string title)
    {
        Title = title;
    }

    public string? Run()
    {
        Console.Clear();
        Console.WriteLine(Title);
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