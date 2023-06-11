/// <summary>
/// A section of an airplane.
/// </summary>
public class Section
{
    /// <summary> The name of the section. </summary>
    private string _section { get; set; }
    /// <summary> The seats inside of the section. </summary>
    private List<Seat> _seats { get; set; }

    public string SectionName { get => _section; set => _section = value; }
    public List<Seat> Seats { get => _seats; set => _seats = value; }
}