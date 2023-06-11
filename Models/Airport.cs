/// <summary>
/// An airport.
/// </summary>
public class Airport {
    /// <summary>The ID of the airport.</summary>
    private int _id;
    /// <summary>The name of the airport.</summary>
    private string _name;
    /// <summary>The city where the airport is located.</summary>
    private string _city;
    /// <summary>The country where the airport is located.</summary>
    private string _country;
    /// <summary>The IATA code of the airport.(e.g. SCH</summary>
    private string _code;
    /// <summary>The facilities that the airport has.</summary>
    private List<string> _facilities;

    public int Id { get => _id; set => _id = value; }
    public string Name { get => _name; set => _name = value; }
    public string City { get => _city; set => _city = value; }
    public string Country { get => _country; set => _country = value; }
    public string Code { get => _code; set => _code = value; }
    public List<string> Facilities { get => _facilities; set => _facilities = value; }

    public Airport(int id, string name, string city, string country, string code) {
        _id = id;
        _name = name;
        _city = city;
        _country = country;
        _code = code;
        _facilities = new List<string>();
    }

    /// <summary>
    /// Adds a facility to the airport.
    /// </summary>
    /// <param name="facility">The facility to add.</param> 
    public void AddFacility(string facility) {
        _facilities.Add(facility);
    }

    /// <summary>
    /// Removes a facility from the airport.
    /// </summary>
    /// <param name="facility">The facility to remove.</param>
    public void RemoveFacility(string facility) {
        _facilities.Remove(facility);
    }

    /// <summary>
    /// Created a formatted string of all the facilities in the airport.
    /// </summary>
    /// <returns>The formatted string.</returns>
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
    /// Creates a formatted string of the airport and its facilities.
    /// </summary>
    /// <returns>The formatted string.</returns>
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
    /// Creates a formatted string of the airport.
    /// </summary>
    /// <returns>The formatted string.</returns>
    public override string ToString() {
        return $"{_name} ({_code})";
    }
}