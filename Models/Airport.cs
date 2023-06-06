public class Airport {
    private int _id;
    private string _name;
    private string _city;
    private string _country;
    private string _code;
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

    public void AddFacility(string facility) {
        _facilities.Add(facility);
    }

    public void RemoveFacility(string facility) {
        _facilities.Remove(facility);
    }

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
    
    public string AirportOverviewString()
    {
        return $"Luchthaven {_name} ({_code})\n{_city}, {_country}\n\nFaciliteiten:\n{FacilitiesToString()}";
    }

    public override string ToString() {
        return $"{_name} ({_code})";
    }
}