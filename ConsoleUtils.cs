public static class ConsoleUtils
{
    /// <summary>
    /// Clears the console and displays the given message.
    /// </summary>
    /// <param name="message">The message to display.</param>
    /// <param name="func">The function to call after the user presses a key.</param>
    public static void Error(string message, Action func)
    {
        Error(message);
        func();
    }

    /// <summary>
    /// Clears the console and displays the given message.
    /// </summary>
    /// <param name="message">The message to display.</param>
    public static void Error(string message)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine("Druk op een toets om door te gaan...");
        Console.ReadKey();
        Console.Clear();
    }

    /// <summary>
    /// Clears the console and displays the given message.
    /// </summary>
    /// <param name="message">The message to display.</param>
    /// <param name="func">The function to call after the user presses a key.</param>
    public static void Success(string message, Action func)
    {
        Success(message);
        func();
    }

    /// <summary>
    /// Clears the console and displays the given message.
    /// </summary>
    /// <param name="message">The message to display.</param>
    public static void Success(string message)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine("Druk op een toets om door te gaan...");
        Console.ReadKey();
        Console.Clear();
    }

    /// <summary>
    /// Clears the console, displays the given message and returns a bool based on the user's input (y/n).
    /// </summary>
    /// <param name="message">The message to display.</param>
    /// <returns>True if the user pressed y, false if the user pressed n.</returns>
    public static bool Confirm(string message)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(message);
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine("Druk op y om door te gaan, of een andere toets om te annuleren...");
        ConsoleKeyInfo key = Console.ReadKey();
        Console.Clear();
        return key.Key == ConsoleKey.Y;
    }
}