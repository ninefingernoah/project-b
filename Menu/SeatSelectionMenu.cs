using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class SeatSelectionMenu {
    private Flight _flight;
    public static List<Seat> SelectedSeats = new List<Seat>();
    public static Seat Cursor;

    public Flight Flight {
        get {
            return _flight;
        }
    }

    public SeatSelectionMenu(Flight flight) {
        _flight = flight;
    }

    public void Run() {
        List<Seat> allSeats = _flight.Airplane.Seats;

        int maxRow = GetMaxRow(allSeats);
        int maxColumn = GetMaxColumn(allSeats);

        Cursor = allSeats[0]; // set initial cursor position to the first seat

        Console.CursorVisible = false; // hide the cursor

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Selecteer een stoel met de pijltjestoetsen en druk op enter om te selecteren.");
            Console.WriteLine("Druk op C om alle selecties te verwijderen.");
            Console.WriteLine("Druk op S of escape om uw selectie op te slaan.");
            PrintSeatLayout(allSeats, maxRow, maxColumn);

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            switch (keyInfo.Key)
            {
                case ConsoleKey.LeftArrow:
                    MoveCursorLeft(allSeats, maxRow);
                    break;
                case ConsoleKey.RightArrow:
                    MoveCursorRight(allSeats, maxRow);
                    break;
                case ConsoleKey.UpArrow:
                    MoveCursorUp(allSeats);
                    break;
                case ConsoleKey.DownArrow:
                    MoveCursorDown(allSeats, maxColumn);
                    break;
                case ConsoleKey.Enter:
                    ToggleSeatSelection(Cursor);
                    break;
                case ConsoleKey.Escape:
                case ConsoleKey.S:
                    // TODO: return user to booking menu, add selected seats to booking in database
                    return;
                case ConsoleKey.C:
                    // clear all selected seats
                    ClearSeatSelection();
                    break;
            }
        }
    }

    static void PrintSeatLayout(List<Seat> seats, int maxRow, int maxColumn){
        for (int column = 1; column <= maxColumn; column++)
        {
            for (int row = 1; row <= maxRow; row++)
            {
                Seat seat = GetSeatAtPosition(seats, row, column);

                if (seat == null)
                {
                    PrintEmptyBox();
                }
                else if (seat == Cursor)
                {
                    PrintHighlightedSeatBox(seat.Color);
                }
                else if (SelectedSeats.Contains(seat))
                {
                    PrintSelectedSeatBox();
                }
                else
                {
                    PrintSeatBox(seat.Color);
                }
            }
            if (column % 3 == 0) { Console.WriteLine();} // print a new line after every 3 columns
            Console.WriteLine(); // Move to the next row
        }
    }

    static Seat GetSeatAtPosition(List<Seat> seats, int row, int column)
    {
        foreach (var seat in seats)
        {
            if (seat.Row == row && GetColumnIndex(seat.Column) == column)
            {
                return seat;
            }
        }

        return null;
    }

    static void MoveCursorUp(List<Seat> seats)
    {
        int columnIndex = GetColumnIndex(Cursor.Column);

        if (columnIndex > 1)
        {
            int newRow = Cursor.Row;
            string newColumn = GetColumnLetter(columnIndex - 1);

            if (GetSeatAtPosition(seats, newRow, GetColumnIndex(newColumn)) != null)
            {
                Cursor = GetSeatAtPosition(seats, newRow, GetColumnIndex(newColumn));
            }
        }
    }

    static void MoveCursorDown(List<Seat> seats, int maxColumn)
    {
        int columnIndex = GetColumnIndex(Cursor.Column);

        if (columnIndex < maxColumn)
        {
            int newRow = Cursor.Row;
            string newColumn = GetColumnLetter(columnIndex + 1);

            if (GetSeatAtPosition(seats, newRow, GetColumnIndex(newColumn)) != null)
            {
                Cursor = GetSeatAtPosition(seats, newRow, GetColumnIndex(newColumn));
            }
        }
    }

    static void MoveCursorLeft(List<Seat> seats, int maxRow)
    {
        int newRow = Cursor.Row - 1;
        string newColumn = Cursor.Column;

        if (newRow >= 1 && GetSeatAtPosition(seats, newRow, GetColumnIndex(newColumn)) != null)
        {
            Cursor = GetSeatAtPosition(seats, newRow, GetColumnIndex(newColumn));
        }
    }

    static void MoveCursorRight(List<Seat> seats, int maxRow)
    {
        int newRow = Cursor.Row + 1;
        string newColumn = Cursor.Column;

        if (newRow <= maxRow && GetSeatAtPosition(seats, newRow, GetColumnIndex(newColumn)) != null)
        {
            Cursor = GetSeatAtPosition(seats, newRow, GetColumnIndex(newColumn));
        }
    }

    static void ToggleSeatSelection(Seat seat)
    {
        if (SelectedSeats.Contains(seat))
        {
            SelectedSeats.Remove(seat);
        }
        else
        {
            SelectedSeats.Add(seat);
        }
    }

    static void ClearSeatSelection()
    {
        SelectedSeats.Clear();
    }

    static void PrintSeatBox(string color)
    {
        Console.ForegroundColor = GetConsoleColor(color);
        Console.Write("■"); // Box icon
        Console.ResetColor();
    }

    static void PrintEmptyBox()
    {
        Console.Write(" ");
    }

    static string GetColumnLetter(int columnIndex)
    {
        return ((char)('A' + columnIndex - 1)).ToString();
    }

    static void PrintHighlightedSeatBox(string color)
    {
        Console.BackgroundColor = ConsoleColor.Gray;
        Console.ForegroundColor = GetConsoleColor(color);
        Console.Write("■"); // Box icon
        Console.ResetColor();
    }

    static void PrintSelectedSeatBox()
    {
        Console.BackgroundColor = ConsoleColor.Magenta;
        Console.Write("■"); // Box icon
        Console.ResetColor();
    }


    static int GetMaxRow(List<Seat> seats)
    {
        int maxRow = 0;

        foreach (var seat in seats)
        {
            if (seat.Row > maxRow)
                maxRow = seat.Row;
        }

        return maxRow;
    }

    static int GetMaxColumn(List<Seat> seats)
    {
        int maxColumn = 0;

        foreach (var seat in seats)
        {
            int columnIndex = GetColumnIndex(seat.Column);
            if (columnIndex > maxColumn)
                maxColumn = columnIndex;
        }

        return maxColumn;
    }

    static int GetColumnIndex(string column)
    {
        return column[0] - 'A' + 1;
    }

    static ConsoleColor GetConsoleColor(string color)
    {
        switch (color)
        {
            case "blue":
                return ConsoleColor.Blue;
            case "yellow":
                return ConsoleColor.Yellow;
            default:
                return ConsoleColor.White;
        }
    }
}