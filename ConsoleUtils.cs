public static class ConsoleUtils {
    public static string ReadString(string message) {
        Console.Write(message + ": ");
        return Console.ReadLine();
    }

    public static int ReadInt(string message) {
        Console.Write(message + ": ");
        try {
            return Convert.ToInt32(Console.ReadLine());
        }
        catch (Exception e) {
            Console.WriteLine(e.Message);
            Console.WriteLine("Er is iets fout gegaan.");
        }
        return 0;
    }

    public static DateTime ReadDateTime(string message) {
        Console.Write(message + ": ");
        try {
            return Convert.ToDateTime(Console.ReadLine());
        }
        catch (Exception e) {
            Console.WriteLine(e.Message);
            Console.WriteLine("Er is iets fout gegaan.");
        }
        return DateTime.MinValue;
    }

    public static double ReadDouble(string message) {
        Console.Write(message + ": ");
        try {
            return Convert.ToDouble(Console.ReadLine());
        }
        catch (Exception e) {
            Console.WriteLine(e.Message);
            Console.WriteLine("Er is iets fout gegaan.");
        }
        return 0;
    }

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
}