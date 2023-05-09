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

    public void Start(){
        var flight = FlightController.Instance.GetFlightType();
        int amount = 0;
    }
}
