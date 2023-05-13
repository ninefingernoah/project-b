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
    /// Clears the console and displays the given message.
    /// </summary>
    /// <param name="message">The message to display.</param>
    public static void Warn(string msg)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(msg);
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine("Druk op een toets om door te gaan...");
        Console.ReadKey();
        Console.Clear();
    }
}
