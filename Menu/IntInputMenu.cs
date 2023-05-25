public class IntInputMenu
{
    public string Title { get; set; }

    public IntInputMenu(string title)
    {
        Title = title;
    }

    public int? Run()
    {
        Console.Clear();
        Console.WriteLine(Title);
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