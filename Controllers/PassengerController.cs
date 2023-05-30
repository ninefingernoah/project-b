public class PassengerController
{
    private static readonly PassengerController instance = new PassengerController();

    static PassengerController()
    {
    }
    private PassengerController()
    {
    }

    /// <summary>
    /// The singleton instance of the main menu controller. Used for accessing the controller. Thread safe.
    /// </summary>
    public static PassengerController Instance
    {
        get
        {
            return instance;
        }
    }


    public Passenger NewPassenger()
    {
        int id = PassengerManager.GetNextId();
        StringInputMenu menu = new StringInputMenu("Wat is de email van de reiziger?");
        string? email = menu.Run();

        menu = new StringInputMenu("Wat is de voornaam van de reiziger?");
        string? FirstName = menu.Run();

        menu = new StringInputMenu("Wat is de achternaam van de reiziger?");
        string? LastName = menu.Run();

        menu = new StringInputMenu("Wat is het document nummer van de reiziger?");
        string? DocNumber = menu.Run();

        DateTimeInputMenu DTmenu = new DateTimeInputMenu("Wat is de geboorte datum van de reiziger?");
        DateTime? date = DTmenu.Run();

        Address addr = AddressController.Instance.NewAddress();
        return new Passenger(id, email, FirstName, LastName, DocNumber, date, addr);
    }
}