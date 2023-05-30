using System.Data;
public static class PassengerManager
{
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

    public static void AddPassenger(Passenger pass)
    {
        // TODO: Address
        DatabaseManager.QueryNonResult($"INSERT INTO passengers (id, email, first_name, last_name, document_number) VALUES ('{pass.Id}','{pass.Email}','{pass.FirstName}','{pass.LastName}','{pass.DocumentNumber}');");

    }

    public static void DeletePassenger(Passenger pass)
    {
        // TODO: Delete address
        DatabaseManager.QueryNonResult($"DELETE FROM passengers WHERE id = {pass.Id}");
    }

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
}