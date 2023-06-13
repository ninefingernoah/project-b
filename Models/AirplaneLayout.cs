/// <summary>
/// This class is used to store the layout of an airplane. Gets deserialized from a JSON file.
/// </summary>
public class AirplaneLayout {
    /// <summary>The name of the airplane.</summary>
    public string Name;
    /// <summary>The list of sections on the plane which holds the seats.</summary>
    public List<Section> SeatLayout { get; set; }
}