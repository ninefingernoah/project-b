public class Address
{
    public string City { get; set; }
    public string Country { get; set; }
    public string Street { get; set; }
    public string StreetNumber { get; set; }

    public Address(string city, string country, string street, string streetNumber)
    {
        City = city;
        Country = country;
        Street = street;
        StreetNumber = streetNumber;
    }
}