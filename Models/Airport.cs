/// <summary>
/// An airport
/// </summary>
public class Airport {
    /// <summary>The id of the airport</summary>
    private int _id;
    /// <summary>The name of the airport</summary>
    private string _name;
    /// <summary>The city of the airport</summary>
    private string _city;
    /// <summary>The country of the airport</summary>
    private string _country;
    /// <summary>The code of the airport</summary>
    private string _code;
    /// <summary>The facilities of the airport</summary>
    private List<string> _facilities;

    /// <summary>The id of the airport</summary>
    public int Id { get => _id; set => _id = value; }
    /// <summary>The name of the airport</summary>
    public string Name { get => _name; set => _name = value; }
    /// <summary>The city of the airport</summary>
    public string City { get => _city; set => _city = value; }
    /// <summary>The country of the airport</summary>
    public string Country { get => _country; set => _country = value; }
    /// <summary>The code of the airport</summary>
    public string Code { get => _code; set => _code = value; }
    /// <summary>The facilities of the airport</summary>
    public List<string> Facilities { get => _facilities; set => _facilities = value; }

    /// <summary>
    /// Constructor for Airport
    /// </summary>
    /// <param name="id">The id of the airport</param>
    /// <param name="name">The name of the airport</param>
    /// <param name="city">The city of the airport</param>
    /// <param name="country">The country of the airport</param>
    /// <param name="code">The code of the airport</param>
    public Airport(int id, string name, string city, string country, string code) {
        _id = id;
        _name = name;
        _city = city;
        _country = country;
        _code = code;
        _facilities = new List<string>();
    }

    /// <summary>
    /// Adds a facility to the airport
    /// </summary>
    /// <param name="facility">The facility to add</param>
    public void AddFacility(string facility) {
        _facilities.Add(facility);
    }

    /// <summary>
    /// Removes a facility from the airport
    /// </summary>
    /// <param name="facility">The facility to remove</param>
    public void RemoveFacility(string facility) {
        _facilities.Remove(facility);
    }

    /// <summary>
    /// Returns a string of all the facilities
    /// </summary>
    /// <returns>A string of all the facilities</returns>
    public string FacilitiesToString() {
        string facilities = "";
        for(int i = 0; i < _facilities.Count; i++) {
            if(i == _facilities.Count - 1)
                facilities += "- " + _facilities[i];
            else
                facilities += "- " + _facilities[i] + "\n";
        }
        return facilities;
    }
    
    /// <summary>
    /// Returns a string of the airport
    /// </summary>
    /// <returns>A string of the airport</returns>
    public string AirportOverviewString()
    {
        var res = $"Luchthaven {_name} ({_code})\n{_city}, {_country}";
        if (_facilities.Count > 0)
        {
            res += $"\n\nFaciliteiten:\n{FacilitiesToString()}";
        }
        return res;
    }

    /// <summary>
    /// Returns a string of the airport
    /// </summary>
    /// <returns>A string of the airport</returns>
    public override string ToString() {
        return $"{_name} ({_code})";
    }
}