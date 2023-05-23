using System.Text.RegularExpressions;

public class ReservationMenuController
{
    /// <summary>
    /// The main menu for reserving a table. This initializes the menu and calls the recursive method.
    /// </summary>
    public static void ShowReserveMenu()
    {
        ReserveMenu menu = new ReserveMenu();
        if (Program.UserManager.LoggedInUser.Email != "")
        {
            menu.TheReservation.User = Program.UserManager.LoggedInUser;
            menu.TheReservation.Name = Program.UserManager.LoggedInUser.Name;
            menu.TheReservation.Email = Program.UserManager.LoggedInUser.Email;
        }
        ShowReserveMenu(menu);
    }

    /// <summary>
    /// The recursive method for reserving a table
    /// </summary>
    /// <param name="menu">The main reserve menu-object. Passed to store visual data</param>
    public static void ShowReserveMenu(ReserveMenu menu)
    {
        int selection = menu.Run(true);
        switch (selection)
        {
            case 0:
                ShowNameInputMenu(menu);
                break;
            case 1:
                ShowDateInputMenu(menu);
                break;
            case 2:
                ShowGroupSizeInputMenu(menu);
                break;
            case 3:
                ShowPhoneInputMenu(menu);
                break;
            case 4:
                ShowEmailInputMenu(menu);
                break;
            case 6:
                ShowCourseArrangementInputMenu(menu);
                break;
            case 7:
                ShowWineArrangementInputMenu(menu);
                break;
            case 9:
                if (menu.IsReservationReady)
                {
                    ShowConfirmationMenu(menu);
                }
                if (!menu.IsReservationReady)
                {
                    ConsoleUtils.Error("Uw reservering is nog niet compleet.");
                    ShowReserveMenu(menu);
                }
                break;
            case 10:
                System.Console.BackgroundColor = ConsoleColor.Black;
                System.Console.ForegroundColor = ConsoleColor.White;
                MainMenuController.MainMenu();
                break;
        }
    }

    private static void ShowCourseArrangementInputMenu(ReserveMenu menu)
    {
        Dictionary<string, string> options = new Dictionary<string, string>();
        ArrangementManager am = Program.ArrangementManager;
        options.Add("Geen", "");
        foreach (Arrangement arrangement in am.Arrangements)
        {
            if (arrangement.id == 0)
                continue;
            bool plural = arrangement.number_of_courses > 1;
            if (plural)
                options.Add($"{arrangement.number_of_courses} gangen ({arrangement.price} Euro per persoon)", "");
            else
                options.Add($"{arrangement.number_of_courses} gang ({arrangement.price} Euro per persoon)", "");

        }   
        options.Add("Terug", "");
        InputMenu inputmenu = new InputMenu("Kies het arrangement:", options);
        int selection = inputmenu.Run(true);
        if (selection == options.Count - 1)
        {
            ShowReserveMenu(menu);
            return;
        }
        if (selection == 0)
        {
            menu.TheReservation.CourseArrangement = null;
            ShowReserveMenu(menu);
            return;
        }
        menu.TheReservation.CourseArrangement = am.GetArrangement(selection);
        ShowReserveMenu(menu);
    }

    private static void ShowWineArrangementInputMenu(ReserveMenu menu)
    {
        Dictionary<string, string> options = new Dictionary<string, string>();
        bool hasWine = menu.TheReservation.WineArrangement != null;
        options.Add("Wijnarrangement", hasWine ? "Ja" : "Nee");
        options.Add("Terug", "");
        InputMenu inputmenu = new InputMenu("Voer het aantal gangen in: ", options);
        int selection = inputmenu.Run(true);
        switch (selection)
        {
            case 0:
                if(hasWine)
                    menu.TheReservation.WineArrangement = null;
                else
                    menu.TheReservation.WineArrangement = Program.ArrangementManager.GetArrangement(0);
                break;
        }
        ShowReserveMenu(menu);
    }

    /// <summary>
    /// The menu for entering the name of the reservation
    /// </summary>
    /// <param name="menu">The main reserve menu-object. Passed to store visual data</param>
    private static void ShowNameInputMenu(ReserveMenu menu)
    {
        System.Console.Clear();
        System.Console.Write("Voer uw naam in: ");
        string? name = System.Console.ReadLine();
        if (name != null)
        {
            menu.TheReservation.Name = name;
            ShowReserveMenu(menu);	
        }
        else 
            ShowNameInputMenu(menu);
    }

    /// <summary>
    /// The menu for entering the group size of the reservation
    /// </summary>
    /// <param name="menu">The main reserve menu-object. Passed to store visual data</param>
    private static void ShowGroupSizeInputMenu(ReserveMenu menu)
    {
        System.Console.Clear();
        System.Console.Write("Voer uw groepsgrootte in: ");
        string? groupSize;
        try
        {
            groupSize = System.Console.ReadLine();
        }
        catch (Exception)
        {
            ConsoleUtils.Error("De groepsgrootte moet een getal zijn.");
            ShowGroupSizeInputMenu(menu);
            return;
        }
        if (groupSize != null)
        {
            if (int.Parse(groupSize) < 1)
            {
                ConsoleUtils.Error("De groepsgrootte moet minimaal 1 zijn.");
                ShowGroupSizeInputMenu(menu);
            }
            else if (int.Parse(groupSize) > 18)
            {
                ConsoleUtils.Error("De groepsgrootte mag maximaal 18 zijn.");
                ShowGroupSizeInputMenu(menu);
            }
            else
            {
                menu.TheReservation.GroupSize = int.Parse(groupSize);
                ShowReserveMenu(menu);
            }
        }
        else
            ShowGroupSizeInputMenu(menu);
    }

    /// <summary>
    /// The menu for entering the phonenumber of the reservation
    /// </summary>
    /// <param name="menu">The main reserve menu-object. Passed to store visual data</param>
    private static void ShowPhoneInputMenu(ReserveMenu menu)
    {
        System.Console.Clear();
        System.Console.Write("Voer uw telefoonnummer in: ");
        string? phonenumber = System.Console.ReadLine();
        if (phonenumber == null)
        {
            ShowPhoneInputMenu(menu);
            return;
        }
        // Regex for checking if the phonenumber is valid
        // Valid formats: 0123456789, 012-345-6789, and (012)-345-6789.
        // Source: https://www.abstractapi.com/guides/c-validate-phone-number
        string regex = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";
        if (Regex.IsMatch(phonenumber, regex))
        {
            menu.TheReservation.PhoneNumber = phonenumber;
            ShowReserveMenu(menu);
        }
        else
        {
            ConsoleUtils.Error("Voer een geldig telefoonnummer in. Bijvoorbeeld 0123456789 of 012-345-6789.");
            ShowPhoneInputMenu(menu);
        }
    }

    /// <summary>
    /// The menu for entering the phonenumber of the reservation
    /// </summary>
    /// <param name="menu">The main reserve menu-object. Passed to store visual data</param>
    private static void ShowEmailInputMenu(ReserveMenu menu)
    {
        System.Console.Clear();
        System.Console.Write("Voer uw email in: ");
        string? email = System.Console.ReadLine();
        if (email == null)
        {
            ShowEmailInputMenu(menu);
            return;
        }
        if (!Regex.IsMatch(email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
        {
            ConsoleUtils.Error("Voer een geldig emailadres in. Bijvoorbeeld email@service.com");
            ShowEmailInputMenu(menu);
        }
        else
        {
            menu.TheReservation.Email = email;
            ShowReserveMenu(menu);
        }
    }

    

    /// <summary>
    /// The menu for entering the date of the reservation. This initialises the menu and calls the recursive method
    /// </summary>
    /// <param name="menu">The main reserve menu-object. Passed to store visual data</param>
    private static void ShowDateInputMenu(ReserveMenu menu)
    {
        Dictionary<string, string> options = new Dictionary<string, string>();
        if (menu.TheReservation.Date != null)
        {
            options.Add("Datum", menu.TheReservation.Date.Value.ToString("dd-MM-yyyy"));
            options.Add("Tijd", menu.TheReservation.Date.Value.ToString("HH:mm"));
        }
        else
        {
            options.Add("Datum", "<vul in>");
            options.Add("Tijd", "<vul in>");
        }
        options.Add("-", "");
        options.Add("Terug", "");
        InputMenu dateMenu = new InputMenu("Voer een datum in", options);
        ShowDateInputMenu(menu, dateMenu);
    }


    /// <summary>
    /// The recursive method for entering the date of the reservation
    /// </summary>
    /// <param name="menu">The main reserve menu-object. Passed to store visual data</param>
    /// <param name="dateMenu">The datemenu-object. Passed to store visual data</param>
    private static void ShowDateInputMenu(ReserveMenu menu, InputMenu dateMenu)
    {
        int selection = dateMenu.Run(true);
        switch(selection) {
            // The user selected to change the date
            case 0:
                System.Console.Clear();
                System.Console.Write("Voer een datum in: ");
                string? date = System.Console.ReadLine();
                if (date != null)
                {
                    DateTime now = DateTime.Now;
                    DateTime dateInput;
                    try
                    {
                        dateInput = DateTime.Parse(date);
                    }
                    catch (Exception)
                    {
                        ConsoleUtils.Error("De datum die u heeft ingevoerd is ongeldig. Probeer het opnieuw.");
                        ShowDateInputMenu(menu, dateMenu);
                        return;
                    }
                    if (dateInput < now)
                    {
                        ConsoleUtils.Error("De datum die u heeft ingevoerd is in het verleden. Probeer het opnieuw.");
                        ShowDateInputMenu(menu, dateMenu);
                    }
                    else
                    {
                        menu.TheReservation.Date = DateTime.Parse(date);
                        dateMenu.SetOption(0, date);
                        ShowDateInputMenu(menu, dateMenu);
                    }
                }
                else
                    ShowDateInputMenu(menu);
                break;
            // The user selected to change the time
            case 1:
                System.Console.Clear();
                System.Console.Write("Voer een tijd in: ");
                string? time = System.Console.ReadLine();
                if (time != null)
                {
                    DateTime old = menu.TheReservation.Date ?? DateTime.Today + TimeSpan.FromHours(24);
                    DateTime newDate;
                    try{
                        newDate = new DateTime(old.Year, old.Month, old.Day, int.Parse(time.Split(':')[0]), int.Parse(time.Split(':')[1]), 0);
                        newDate = DateTimeUtils.RoundToHourOrHalfHour(newDate);
                    }
                    catch (Exception)
                    {
                        ConsoleUtils.Error("De tijd die u heeft ingevoerd is ongeldig. Probeer het opnieuw.");
                        ShowDateInputMenu(menu, dateMenu);
                        return;
                    }
                    TimeOnly reservatonTime = new TimeOnly(newDate.Hour, newDate.Minute);
                    if (reservatonTime < Program.RestaurantManager.Restaurant.OpeningTime || reservatonTime > Program.RestaurantManager.Restaurant.ClosingTime)
                    {
                        ConsoleUtils.Error($"De tijd die u heeft ingevoerd is niet binnen de openingstijden. We zijn open van {Program.RestaurantManager.Restaurant.OpeningTime} tot {Program.RestaurantManager.Restaurant.ClosingTime}. Probeer het opnieuw.");
                        ShowDateInputMenu(menu, dateMenu);
                        return;
                    }
                    menu.TheReservation.Date = newDate;
                    dateMenu.SetOption(1, newDate.TimeOfDay.ToString());
                    ShowDateInputMenu(menu, dateMenu);
                }
                else
                    ShowDateInputMenu(menu);
                break;
            case 3:
                ShowReserveMenu(menu);
                break;
        }
        
    }

    /// <summary>
    /// The menu for confirming the users reservation
    /// </summary>
    /// <param name="menu">The main reserve menu-object. Passed to store visual data</param>
    private static void ShowConfirmationMenu(ReserveMenu menu)
    {
        // This looks like a lot of code. But really it only displays the details of the reservation in a nice way.
        // For questions, please consult Rens. He wrote this and it is his trashy code.
        List<string> reservationInfo = new List<string>();
        reservationInfo.Add("Naam: " + menu.TheReservation.Name);
        reservationInfo.Add("Groepsgrootte: " + menu.TheReservation.GroupSize);
        reservationInfo.Add("Datum: " + menu.TheReservation.Date?.ToString("dd-MM-yyyy"));
        reservationInfo.Add("Tijd: " + menu.TheReservation.Date?.ToString("HH:mm"));
        reservationInfo.Add("Telefoonnummer: " + menu.TheReservation.PhoneNumber);
        reservationInfo.Add("Email: " + menu.TheReservation.Email);
        reservationInfo.Add("Gerechts arrangement: " + menu.TheReservation.CourseArrangement);
        reservationInfo.Add("Wijn arrangement: " + menu.TheReservation.WineArrangement);
        if (menu.TheReservation.CourseArrangement != null || menu.TheReservation.WineArrangement != null)
        {
            reservationInfo.Add("\n");
            reservationInfo.Add("Prijs overzicht:");
            reservationInfo.Add("");
            if (menu.TheReservation.CourseArrangement != null)
                reservationInfo.Add($"{menu.TheReservation.CourseArrangement.number_of_courses} gangen menu ({menu.TheReservation.CourseArrangement.price} euro) x {menu.TheReservation.GroupSize} personen = {menu.TheReservation.CourseArrangement.price * menu.TheReservation.GroupSize} euro");
            if (menu.TheReservation.WineArrangement != null)
                reservationInfo.Add($"Wijnarrangement ({menu.TheReservation.WineArrangement.price} euro) = {menu.TheReservation.WineArrangement.price} euro");
            double total = 0;
            if (menu.TheReservation.CourseArrangement != null)
                total += menu.TheReservation.CourseArrangement.price * menu.TheReservation.GroupSize;
            if (menu.TheReservation.WineArrangement != null)
                total += menu.TheReservation.WineArrangement.price;
            reservationInfo.Add("");
            reservationInfo.Add("----------------------------- +");
            reservationInfo.Add($"Totaal: {total} euro");
        }

        string[] options = new string[] {"Bevestig", "Terug"};
        string prompt = "Bevestig uw reservering" + "\n" + "\n" + string.Join("\n", reservationInfo);
        Menu confirmationMenu = new Menu(prompt, options);
        int selection = confirmationMenu.Run();
        switch(selection) {
            case 0:
                bool success; // = Program.ReservationManager.PlaceReservation(menu.TheReservation);
                success = Program.ReservationManager.PlaceReservation(menu.TheReservation);
                if (success)
                    ShowConfirmationMessage(menu.TheReservation);
                else
                {
                    ConsoleUtils.Error("Helaas kunnen we deze hoeveelheid mensen op het gekozen tijdstip geen plaats toewijzien. \nKies een andere datum of tijd.");
                    ShowConfirmationMenu(menu);
                }
                break;
            case 1:
                ShowReserveMenu(menu);
                break;
        }
    }

    private static void ShowConfirmationMessage(Reservation reservation)
    {
        System.Console.Clear();
        System.Console.WriteLine("Uw reservering is bevestigd. Uw reserveringscode: " + reservation.ReservationCode);
        System.Console.WriteLine("Druk op een toets om door te gaan...");
        System.Console.ReadKey();
        MainMenuController.MainMenu();
    }

}