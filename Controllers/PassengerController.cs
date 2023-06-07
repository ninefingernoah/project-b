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


    public Passenger? NewPassenger()
    {
        PassengerView.Instance.ClearView();
        return PassengerView.Instance.Run();
    }
}