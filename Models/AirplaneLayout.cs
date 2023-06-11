/// <summary>
/// The layout of an airplane.
/// </summary>
public class AirplaneLayout {
    /// <summary> The name of the airplane. </summary>
    public string Name;
    /// <summary> The layout of the seats divided into a list of Sections. </summary>
    public List<Section> SeatLayout { get; set; }
}