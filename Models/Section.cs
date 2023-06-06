public class Section
{
    private string _section { get; set; }
    private List<Seat> _seats { get; set; }

    public string SectionName { get => _section; set => _section = value; }
    public List<Seat> Seats { get => _seats; set => _seats = value; }
}