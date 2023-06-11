/// <summary>
/// The controller for the passenger.
/// </summary>
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


    /// <summary>
    /// Shows the menu for creating a new passenger and returns the passenger.
    /// </summary>
    /// <returns>The passenger that was created.</returns>
    public Passenger? NewPassenger()
    {
        PassengerView.Instance.ClearView();
        return PassengerView.Instance.Run();
    }
}