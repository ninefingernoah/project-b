using System.Data;

public static class PassengerManager
{
    public static void AddPassenger(Passenger pass)
    {
        DatabaseManager.QueryNonResult($"INSERT INTO passengers (id, email, first_name, last_name, document_number) VALUES ('{pass.Id}','{pass.Email}','{pass.FirstName}','{pass.LastName}','{pass.DocumentNumber}');");

    }

    public static void DeletePassenger(Passenger pass)
    {
        DatabaseManager.QueryNonResult($"DELETE FROM passengers WHERE id = {pass.Id}");
    }

    public static int GetNewestId()
    {
        DataRow dr = DatabaseManager.QueryResult($"SELECT id FROM passengers ORDER BY id ASC").Rows[0];
        return (int)dr["id"] + 1;
    }
}