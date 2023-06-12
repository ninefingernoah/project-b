using System.Data;

/// <summary>
/// Manages all the passenger related logic
/// </summary>
public static class PassengerManager
{
    /// <summary>
    /// Gathers a passenger from the database by ID.
    /// </summary>
    /// <param name="id">The ID of the passenger to get.</param>
    public static Passenger GetPassenger(int id)
    {
        DataRow dr = DatabaseManager.QueryResult($"SELECT * FROM passengers WHERE id = {id}").Rows[0];
        Address address = GetAddress((int)(long)dr["address_id"]);
        return new Passenger(
            (int)(long)dr["id"],
            dr["email"].ToString()!,
            dr["first_name"].ToString()!,
            dr["last_name"].ToString()!,
            dr["document_number"].ToString()!,
            Convert.ToDateTime(dr["date_of_birth"]),
            address
        );
    }

    /// <summary>
    /// Adds a  passenger to the database.
    /// </summary>
    /// <param name="pass">The passenger to add.</param>
    public static void AddPassenger(Passenger pass)
    {
        DatabaseManager.QueryNonResult($"INSERT INTO addresses (city, country, street, street_number) VALUES ('{pass.Address.City}','{pass.Address.Country}','{pass.Address.Street}','{pass.Address.HouseNumber}');");
        var address_id = (int)(long)DatabaseManager.QueryResult($"SELECT MAX(id) FROM addresses").Rows[0][0];
        DatabaseManager.QueryNonResult($"INSERT INTO passengers (id, email, first_name, last_name, document_number, date_of_birth, address_id) VALUES ('{pass.Id}','{pass.Email}','{pass.FirstName}','{pass.LastName}','{pass.DocumentNumber}','{pass.BirthDate.ToString()}', '{address_id}');");
        
    }

    /// <summary>
    /// Deletes a passenger from the database.
    /// </summary>
    /// <param name="pass">The passenger to delete.</param>
    public static void DeletePassenger(Passenger pass)
    {
        // TODO: TEST ADDRESS DELETION
        var address_id = (int)(long)DatabaseManager.QueryResult($"SELECT address_id FROM passengers WHERE id = {pass.Id}").Rows[0][0];
        DatabaseManager.QueryNonResult($"DELETE FROM passengers WHERE id = {pass.Id}");
        DatabaseManager.QueryNonResult($"DELETE FROM addresses WHERE id = {address_id}");
    }

    /// <summary>
    /// Gathers an address from the database by ID of the address.
    /// </summary>
    /// <param name="id">The ID of the address to get.</param>
    public static Address GetAddress(int id)
    {
        DataRow dr = DatabaseManager.QueryResult($"SELECT * FROM addresses WHERE id = {id}").Rows[0];
        return new Address(
            dr["city"].ToString()!,
            dr["country"].ToString()!,
            dr["street"].ToString()!,
            dr["street_number"].ToString()!
        );
    }

    /// <summary>
    /// Gets the next ID for a passenger.
    /// </summary>
    /// <returns>The next ID for a passenger. Max(id) of passengers + 1</returns>
    public static int GetNextId()
    {
        return (int)(long)DatabaseManager.QueryResult("SELECT MAX(id) FROM passengers").Rows[0][0] + 1;
    }
}