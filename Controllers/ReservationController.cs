public sealed class ReservationController
{
    private static readonly ReservationController instance = new ReservationController();

    static ReservationController()
    {
    }
    private ReservationController()
    {
    }

    /// <summary>
    /// The singleton instance of the main menu controller. Used for accessing the controller. Thread safe.
    /// </summary>
    public static ReservationController Instance
    {
        get
        {
            return instance;
        }
    }

    public void AskReservation()
    {
        StringInputMenu menu = new StringInputMenu("Vul uw reserveringscode in: ");
        int reservationCode;
        try
        {
            reservationCode = int.Parse(menu.Run()!);
        }
        catch (Exception)
        {
            ConsoleUtils.Error("De ingevoerde reserveringscode is ongeldig.");
            MainMenuController.Instance.ShowMainMenu();
            return;
        }
        Reservation reservation = ReservationManager.GetReservation(reservationCode);
        if (reservation == null)
        {
            ConsoleUtils.Error("De ingevoerde reserveringscode is ongeldig.");
            MainMenuController.Instance.ShowMainMenu();
            return;
        }
        StringInputMenu emailMenu = new StringInputMenu("Vul uw emailadres in: ");
        string email = emailMenu.Run()!;
        if (reservation.User.Email != email)
        {
            ConsoleUtils.Error("Het ingevoerde emailadres is ongeldig.");
            MainMenuController.Instance.ShowMainMenu();
            return;
        }
        ShowReservation(reservation);
    }

    public void ShowReservation(Reservation reservation)
    {
        // TODO: Show reservation
    }

}