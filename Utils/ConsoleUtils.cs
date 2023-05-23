public static class ConsoleUtils
{
    public static void Info(string message, ConsoleColor textColor = ConsoleColor.White)
    {
        Console.Clear();
        Console.ResetColor();
        Console.ForegroundColor = textColor;
        Console.WriteLine(message);
        Console.WriteLine();
        Console.WriteLine("Druk op een toets om door te gaan...");
        Console.ReadKey();
        Console.ResetColor();
    }

    public static string ReadString(string prompt, ConsoleColor textColor = ConsoleColor.White)
    {
        Console.Clear();
        Console.ResetColor();
        Console.ForegroundColor = textColor;
        Console.Write(prompt);
        string? input = Console.ReadLine();
        if (input == null)
        {
            return "";
        }
        else
        {
            return input;
        }
    }

    /// <summary>
    /// The error method. This method is called when an error occurs. It clears the console and prints the error message.
    /// </summary>
    /// <param name="message">The error message</param>
    public static void Error(string message)
    {
        System.Console.BackgroundColor = ConsoleColor.Black;
        System.Console.ForegroundColor = ConsoleColor.Red;
        System.Console.Clear();
        System.Console.WriteLine(message);
        System.Console.WriteLine("Druk op een toets om door te gaan...");
        System.Console.ReadKey();
        System.Console.BackgroundColor = ConsoleColor.Black;
        System.Console.ForegroundColor = ConsoleColor.White;
    }

    public static int ReadInt(string prompt, ConsoleColor textColor = ConsoleColor.White)
    {
        Console.Clear();
        Console.ResetColor();
        Console.ForegroundColor = textColor;
        Console.Write(prompt);
        string? input = Console.ReadLine();
        if (input == null)
        {
            return 0;
        }
        else
        {
            try
            {
                return int.Parse(input);
            }
            catch (Exception)
            {
                Info("Ongeldige invoer", ConsoleColor.Red);
                return 0;
            }
        }
    }

    public static void Success(string text)
    {
        Console.Clear();
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(text);
        Console.WriteLine();
        Console.WriteLine("Druk op een toets om door te gaan...");
        Console.ReadKey();
        Console.ResetColor();
    }
}