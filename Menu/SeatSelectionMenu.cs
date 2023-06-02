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
                    MoveCursorUp(allSeats, maxColumn);
                    break;
                case ConsoleKey.DownArrow:
                    MoveCursorDown(allSeats);
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

    void PrintSeatLayout(List<Seat> seats, int maxRow, int maxColumn){
        for (int column = maxColumn; column > 0; column--)
        {
            Console.Write($"{GetColumnLetter(column)} ");
            for (int row = 1; row <= maxRow; row++)
            {
                Seat seat = GetSeatAtPosition(seats, row, column);

                if (seat == null)
                {
                    PrintEmptyBox();
                }
                else if (_flight.TakenSeats.Contains(seat))
                {
                    PrintTakenSeatBox(seat.Color);
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
            if (column % 3 == 1) { Console.WriteLine();} // print a new line after every 3 columns
            Console.WriteLine(); // Move to the next row
        }
        // print the cursor position
        Console.WriteLine($"{Cursor.Number}");
        // print the selected seats
        Console.WriteLine("\nGeselecteerde stoelen:");
        foreach (var seat in SelectedSeats)
        {
            Console.WriteLine($"Stoel {seat.Number}");
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

    //TODO: if GetSeatAtPosition returns null, check if theres seats further down the row/column

    static void MoveCursorDown(List<Seat> seats)
    {
        int columnIndex = GetColumnIndex(Cursor.Column);
        string newColumn = GetPreviousColumn(seats, Cursor.Column, Cursor.Row);
        if (GetSeatAtPosition(seats, Cursor.Row, GetColumnIndex(newColumn)) != null)
        {
            Cursor = GetSeatAtPosition(seats, Cursor.Row, GetColumnIndex(newColumn));
        }
    }

    static void MoveCursorUp(List<Seat> seats, int maxColumn)
    {
        int columnIndex = GetColumnIndex(Cursor.Column);
        string newColumn = GetNextColumn(seats, Cursor.Column, Cursor.Row);
        if (GetSeatAtPosition(seats, Cursor.Row, GetColumnIndex(newColumn)) != null)
        {
            Cursor = GetSeatAtPosition(seats, Cursor.Row, GetColumnIndex(newColumn));
        }
    }

    static void MoveCursorLeft(List<Seat> seats, int maxRow)
    {
        int newRow = GetPreviousRow(seats, Cursor.Row, Cursor.Column);
        string newColumn = Cursor.Column;
        if (GetSeatAtPosition(seats, newRow, GetColumnIndex(newColumn)) != null)
        {
            Cursor = GetSeatAtPosition(seats, newRow, GetColumnIndex(newColumn));
        }
    }

    static void MoveCursorRight(List<Seat> seats, int maxRow)
    {
        int newRow = GetNextRow(seats, Cursor.Row, Cursor.Column);
        string newColumn = Cursor.Column;
        if (GetSeatAtPosition(seats, newRow, GetColumnIndex(newColumn)) != null)
        {
            Cursor = GetSeatAtPosition(seats, newRow, GetColumnIndex(newColumn));
        }
    }

    void ToggleSeatSelection(Seat seat)
    {
        if (_flight.TakenSeats.Contains(seat))
        {
            return;
        }
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

    static void PrintTakenSeatBox(string color)
    {
        Console.BackgroundColor = ConsoleColor.Red;
        Console.ForegroundColor = GetConsoleColor(color);
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

    /// <summary>
    /// Returns the next row number or 1 if there is no next row
    /// </summary>
    /// <param name="seats">The list of seats in the airplane</param>
    /// <param name="row">The current row from which we are trying to move to the next</param>
    static int GetNextRow(List<Seat> seats, int row, string column)
    {
        int maxRow = GetMaxRow(seats);
        int columnIndex = GetColumnIndex(column);
        if (row < maxRow) {
            List<Seat> seatsToTheRight = seats.Where(s => s.Row > row && GetColumnIndex(s.Column) == columnIndex).ToList();
            if (seatsToTheRight.Count > 0)
            {
                return seatsToTheRight.Min(s => s.Row);
            }
        }
        return seats.Where(s => GetColumnIndex(s.Column) == columnIndex).Min(s => s.Row);
    }

    /// <summary>
    /// Returns the previous row number or GetMaxRow() if there is no previous row
    /// </summary>
    /// <param name="seats">The list of seats in the airplane</param>
    /// <param name="row">The current row from which we are trying to move to the previous</param>
    static int GetPreviousRow(List<Seat> seats, int row, string column)
    {
        int minRow = 1;
        int columnIndex = GetColumnIndex(column);
        if (row > minRow)
        {
            List<Seat> seatsToTheLeft = seats.Where(s => s.Row < row && GetColumnIndex(s.Column) == columnIndex).ToList();
            if (seatsToTheLeft.Count > 0)
            {
                return seatsToTheLeft.Max(s => s.Row);
            }
        }
        return seats.Where(s => GetColumnIndex(s.Column) == columnIndex).Max(s => s.Row);
    }

    /// <summary>
    /// Returns the next column letter or the first column letter if there is no next column
    /// </summary>
    /// <param name="seats">The list of seats in the airplane</param>
    /// <param name="column">The current column from which we are trying to move to the next</param>
    static string GetNextColumn(List<Seat> seats, string column, int row)
    {
        int maxColumn = GetMaxColumn(seats);
        int columnIndex = GetColumnIndex(column);
        if (columnIndex < maxColumn)
        {
            List<Seat> seatsUp = seats.Where(s => GetColumnIndex(s.Column) > columnIndex && s.Row == row).ToList();
            if (seatsUp.Count > 0)
            {
                return GetColumnLetter(seatsUp.Min(s => GetColumnIndex(s.Column)));
            }
        }
        return GetColumnLetter(seats.Where(s => s.Row == row).Min(s => GetColumnIndex(s.Column)));
    }

    /// <summary>
    /// Returns the previous column letter or the last column letter if there is no previous column
    /// </summary>
    /// <param name="seats">The list of seats in the airplane</param>
    /// <param name="column">The current column from which we are trying to move to the previous</param>
    static string GetPreviousColumn(List<Seat> seats, string column, int row)
    {
        int minColumn = 1;
        if (GetColumnIndex(column) > minColumn)
        {
            List<Seat> seatsDown = seats.Where(s => GetColumnIndex(s.Column) < GetColumnIndex(column) && s.Row == row).ToList();
            if (seatsDown.Count > 0)
            {
                return GetColumnLetter(seatsDown.Max(s => GetColumnIndex(s.Column)));
            }
        }
        return GetColumnLetter(seats.Where(s => s.Row == row).Max(s => GetColumnIndex(s.Column)));
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
            case "orange_(business_class)":
                return ConsoleColor.DarkYellow;
            default:
                return ConsoleColor.White;
        }
    }
}