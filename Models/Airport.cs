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

    public string ToFacilitiesString() {
        string facilities = " ";
        foreach (string facility in _facilities) {
            facilities += facility + ",\n";
        }
        string facilitiesToString = facilities.Substring(0, facilities.Length - 1);
        return $"Luchthaven {_name} ({_code}) in {_city}, {_country} met faciliteiten:\n{facilitiesToString}";
    }

    public override string ToString() {
        return $"{_name} ({_code})";
    }
}