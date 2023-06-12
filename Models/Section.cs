/// <summary>
/// A section in a plane
/// </summary>
public class Section
{
    /// <summary>The name of the section</summary>
    private string _section { get; set; }
    /// <summary>The seats in the section</summary>
    private List<Seat> _seats { get; set; }

    /// <summary>
    /// The name of the section
    /// </summary>
    public string SectionName { get => _section; set => _section = value; }
    /// <summary>
    /// The seats in the section
    /// </summary>
    public List<Seat> Seats { get => _seats; set => _seats = value; }
}