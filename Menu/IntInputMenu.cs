public class IntInputMenu
{
    public string Title { get; set; }
    private bool backoption { get; }

    public IntInputMenu(string title, bool backoption = true)
    {
        Title = title;
        this.backoption = backoption;
    }

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