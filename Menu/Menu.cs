using System;
using static System.Console;
public class Menu
{
    /// <summary>The index of the option that the user currently has selected</summary>
    protected int _selectedIndex;

    /// <summary>The options that the user can choose from</summary>
    protected string[] _options;

    /// <summary>The prompt that will be displayed to the user</summary>
    protected string _prompt;

    protected string? _topString;
    protected string? _botString;

    public Menu(string prompt, string[] options, string? TopString = null, string? BotString = null)
    {
        _prompt = prompt;
        _options = options;
        _selectedIndex = 0;
        _topString = TopString;
        _botString = BotString;
    }

    /// <summary>
    /// Displays the menu   
    /// </summary>
    public virtual void DisplayOptions()
    {
        ForegroundColor = ConsoleColor.White;
        BackgroundColor = ConsoleColor.Black;
        if (_prompt != "") WriteLine(_prompt); Write("\n");
        if (_topString != null)
        {
            WriteLine(_topString + "\n");
        }
        for (int i = 0; i < _options.Length; i++)
        {
            if (_options[i] == "-")
            {
                WriteLine();
                continue;
            }
            if (i == _selectedIndex)
            {
                ForegroundColor = ConsoleColor.Black;
                BackgroundColor = ConsoleColor.White;
            }
            else
            {
                ForegroundColor = ConsoleColor.White;
                BackgroundColor = ConsoleColor.Black;
            }
            WriteLine(_options[i]);
        }

        if (_botString != null)
        {
            ForegroundColor = ConsoleColor.White;
            BackgroundColor = ConsoleColor.Black;
            WriteLine("\n" + _botString);
        }
        // Reset the color
        ForegroundColor = ConsoleColor.White;
        BackgroundColor = ConsoleColor.Black;
    }

    /// <summary>
    /// Runs the menu and returns the index of the selected option
    /// </summary>
    /// <param name="loggedIn">If the user is logged in</param>
    /// <returns>The index of the selected option</returns>
    public virtual int Run(bool loggedIn = false)
    {
        ConsoleKey keyPressed;
        // Run the menu until the user presses enter
        do
        {
            Clear();
            DisplayOptions();

            ConsoleKeyInfo keyInfo = ReadKey(true);
            keyPressed = keyInfo.Key;

            // Move the selection up or down
            if (keyPressed == ConsoleKey.DownArrow || keyPressed == ConsoleKey.S)
            {
                _selectedIndex++;
                if (_selectedIndex >= _options.Length)
                {
                    _selectedIndex = 0;
                }
                if (_options[_selectedIndex] == "-")
                {
                    _selectedIndex++;
                }
            }
            else if (keyPressed == ConsoleKey.UpArrow || keyPressed == ConsoleKey.W)
            {
                _selectedIndex--;
                if (_selectedIndex < 0)
                {
                    _selectedIndex = _options.Length - 1;
                }
                if (_options[_selectedIndex] == "-")
                {
                    _selectedIndex--;
                }
            }

        } while (keyPressed != ConsoleKey.Enter);

        return _selectedIndex;
    }



}
