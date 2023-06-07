public sealed class AddressController
{
    /// <summary>
    /// The singleton instance of the main menu controller. Used for accessing the controller. Thread safe.
    /// </summary>
    private static readonly AddressController instance = new AddressController();

    static AddressController()
    {
    }
    private AddressController()
    {
    }

    /// <summary>
    /// The singleton instance of the main menu controller. Used for accessing the controller. Thread safe.
    /// </summary>
    public static AddressController Instance
    {
        get
        {
            return instance;
        }
    }

    public Address NewAddress()
    {
        return EditAddress(new Address("", "", "", ""));
    }

    private Address EditAddress(Address addr)
    {
        Menu menu = new Menu("Wat wilt u aanpassen?", new string[] { "Straatnaam", "Huisnummer", "Plaats", "Land", "-", "Sla op" });
        menu.DisplayOptions();
        int choice = menu.Run();
        switch(choice)
        {
            case 0:
                StringInputMenu streetMenu = new StringInputMenu("Vul de straatnaam in: ");
                string? street = streetMenu.Run();
                if (street == null || street.ToLower() == "terug")
                {
                    return EditAddress(addr);
                }
                if (street.Length < 3)
                {
                    ConsoleUtils.Error("De ingevoerde straatnaam is te kort.");
                    MainMenuController.Instance.ShowMainMenu();
                    return EditAddress(addr);
                }
                addr.Street = street;
                return EditAddress(addr);
            case 1:
                StringInputMenu streetNumberMenu = new StringInputMenu("Vul het huisnummer in: ");
                string? streetNumber = streetNumberMenu.Run();
                if (streetNumber == null || streetNumber.ToLower() == "terug")
                {
                    return EditAddress(addr);
                }
                if (streetNumber.Length < 1)
                {
                    ConsoleUtils.Error("Het ingevoerde huisnummer is te kort.");
                    MainMenuController.Instance.ShowMainMenu();
                    return EditAddress(addr);
                }
                addr.StreetNumber = streetNumber;
                return EditAddress(addr);
            case 2:
                StringInputMenu cityMenu = new StringInputMenu("Vul de plaats in: ");
                string? city = cityMenu.Run();
                if (city == null || city.ToLower() == "terug")
                {
                    return EditAddress(addr);
                }
                if (city.Length < 3)
                {
                    ConsoleUtils.Error("De ingevoerde plaatsnaam is te kort.");
                    MainMenuController.Instance.ShowMainMenu();
                    return EditAddress(addr);
                }
                addr.City = city;
                return EditAddress(addr);
            case 3:
                StringInputMenu countryMenu = new StringInputMenu("Vul het land in: ");
                string? country = countryMenu.Run();
                if (country == null || country.ToLower() == "terug")
                {
                    return EditAddress(addr);
                }
                if (country.Length < 3)
                {
                    ConsoleUtils.Error("Het ingevoerde land is te kort.");
                    MainMenuController.Instance.ShowMainMenu();
                    return EditAddress(addr);
                }
                addr.Country = country;
                return EditAddress(addr);
            case 5:
                if (addr.Street == null || addr.StreetNumber == null || addr.City == null || addr.Country == null)
                {
                    ConsoleUtils.Error("U heeft niet alle velden ingevuld.");
                    return EditAddress(addr);
                }
                return addr;
        }
        return addr;
    }

    // NOTE! This method is only used for while editing a passenger's address.
    public void ShowAddressEditingMenu(Passenger passenger)
    {
        AddressEditorView.Instance.PopulateViewBag(passenger.Address);
        AddressEditorView.Instance.Display();
        int choice = int.Parse((string)AddressEditorView.Instance.ViewBag["MainMenuSelection"]);
        switch(choice)
        {
            case 0:
                StringInputMenu streetMenu = new StringInputMenu("Vul de straatnaam in: ");
                string street = streetMenu.Run()!;
                if (street.ToLower() == "terug")
                {
                    ShowAddressEditingMenu(passenger);
                    return;
                }
                if (street.Length < 3)
                {
                    ConsoleUtils.Error("De ingevoerde straatnaam is te kort.");
                    MainMenuController.Instance.ShowMainMenu();
                    ShowAddressEditingMenu(passenger);
                    return;
                }
                passenger.Address.Street = street;
                ShowAddressEditingMenu(passenger);
                break;
            case 1:
                StringInputMenu streetNumberMenu = new StringInputMenu("Vul het huisnummer in: ");
                string streetNumber = streetNumberMenu.Run()!;
                if (streetNumber.ToLower() == "terug")
                {
                    ShowAddressEditingMenu(passenger);
                    return;
                }
                if (streetNumber.Length < 1)
                {
                    ConsoleUtils.Error("Het ingevoerde huisnummer is te kort.");
                    MainMenuController.Instance.ShowMainMenu();
                    ShowAddressEditingMenu(passenger);
                    return;
                }
                passenger.Address.StreetNumber = streetNumber;
                ShowAddressEditingMenu(passenger);
                break;
            case 2:
                StringInputMenu cityMenu = new StringInputMenu("Vul de plaats in: ");
                string city = cityMenu.Run()!;
                if (city.ToLower() == "terug")
                {
                    ShowAddressEditingMenu(passenger);
                    return;
                }
                if (city.Length < 3)
                {
                    ConsoleUtils.Error("De ingevoerde plaatsnaam is te kort.");
                    MainMenuController.Instance.ShowMainMenu();
                    ShowAddressEditingMenu(passenger);
                    return;
                }
                passenger.Address.City = city;
                ShowAddressEditingMenu(passenger);
                break;
            case 3:
                StringInputMenu countryMenu = new StringInputMenu("Vul het land in: ");
                string country = countryMenu.Run()!;
                if (country.ToLower() == "terug")
                {
                    ShowAddressEditingMenu(passenger);
                    return;
                }
                if (country.Length < 3)
                {
                    ConsoleUtils.Error("Het ingevoerde land is te kort.");
                    MainMenuController.Instance.ShowMainMenu();
                    ShowAddressEditingMenu(passenger);
                    return;
                }
                passenger.Address.Country = country;
                ShowAddressEditingMenu(passenger);
                break;
            case 5:
                PassengerOverviewView.Instance.ThePassenger = passenger;
                ReservationController.Instance.ShowPassengerEditor(passenger);
                break;
        }
    }
}