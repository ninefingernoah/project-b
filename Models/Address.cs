/// <summary>
/// An address.
/// </summary>
public class Address
{
    /// <summary>The city of the address.</summary>
    public string City { get; set; }
    /// <summary>The country of the address.</summary>
    public string Country { get; set; }
    /// <summary>The street of the address.</summary>
    public string Street { get; set; }
    /// <summary>The house number of the address.</summary>
    public string HouseNumber { get; set; }

    /// <summary>
    /// The constructor for the Address class.
    /// </summary>
    /// <param name="city">The city of the address.</param>
    /// <param name="country">The country of the address.</param>
    /// <param name="street">The street of the address.</param>
    /// <param name="houseNumber">The house number of the address.</param>
    public Address(string city, string country, string street, string houseNumber)
    {
        City = city;
        Country = country;
        Street = street;
        HouseNumber = houseNumber;
    }
}