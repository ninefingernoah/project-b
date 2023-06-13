using System.Data.SQLite;
using System.Data;

/// <summary>
/// Manages all the database related logic
/// </summary>
public static class DatabaseManager
{
    /// <summary>
    /// The name of the database file
    /// </summary>
    private const string _dbName = "airline.db";

    /// <summary>
    /// Creates the tables, if they do not already exist.
    /// </summary>
    public static void CreateDatabase()
    {
        QueryNonResult(@"
                CREATE TABLE IF NOT EXISTS users (
                    id INTEGER NOT NULL PRIMARY KEY,
                    role TEXT NOT NULL,
                    first_name TEXT NOT NULL,
                    last_name TEXT NOT NULL,
                    email TEXT NOT NULL,
                    password TEXT NOT NULL
                );
            ");
        QueryNonResult(@"
                CREATE TABLE IF NOT EXISTS airplanes (
	                id INTEGER NOT NULL PRIMARY KEY,
	                name TEXT NOT NULL,
	                total_capacity INTEGER NOT NULL
                );
            ");
        QueryNonResult(@"
                CREATE TABLE IF NOT EXISTS addresses (
                    id INTEGER NOT NULL PRIMARY KEY,
                    city TEXT NOT NULL,
                    country TEXT NOT NULL,
                    street TEXT NOT NULL,
                    street_number TEXT NOT NULL
                );
        ");
        QueryNonResult(@"
                CREATE TABLE IF NOT EXISTS airports (
                    id INTEGER NOT NULL PRIMARY KEY,
                    name TEXT NOT NULL,
                    city TEXT NOT NULL,
                    country TEXT NOT NULL,
                    code TEXT NOT NULL
                );
            ");
        QueryNonResult(@"
                CREATE TABLE IF NOT EXISTS passengers (
                    id INTEGER NOT NULL PRIMARY KEY,
                    email TEXT NOT NULL,
                    first_name TEXT NOT NULL,
                    last_name TEXT NOT NULL,
                    document_number TEXT NOT NULL,
                    date_of_birth TEXT NOT NULL,
                    address_id INTEGER NOT NULL,
                    letters_changed INTEGER DEFAULT 0,
                    FOREIGN KEY(address_id) REFERENCES addresses(id)
                );
            ");
        QueryNonResult(@"
                CREATE TABLE IF NOT EXISTS seats (
                    seat_number TEXT NOT NULL,
                    airplane_id INTEGER NOT NULL,
                    color TEXT NOT NULL,
                    price INTEGER NOT NULL,
                    FOREIGN KEY(airplane_id) REFERENCES airplanes(id),
                    PRIMARY KEY(seat_number, airplane_id)
                );
            ");
        QueryNonResult(@"
                CREATE TABLE IF NOT EXISTS flights (
                    id INTEGER NOT NULL PRIMARY KEY,
                    departure_id INTEGER NOT NULL,
                    destination_id INTEGER NOT NULL,
                    departure_time TEXT NOT NULL,
                    arrival_time TEXT NOT NULL,
                    airplane_id INTEGER NOT NULL,
                    FOREIGN KEY(departure_id) REFERENCES airports(id),
                    FOREIGN KEY(destination_id) REFERENCES airports(id),
                    FOREIGN KEY(airplane_id) REFERENCES airplanes(id)
                );
            ");
        QueryNonResult(@"
                CREATE TABLE IF NOT EXISTS flight_takenseats (
                    flight_id INTEGER NOT NULL,
                    seat_number TEXT NOT NULL,
                    reservation_number INTEGER NOT NULL,
                    FOREIGN KEY(flight_id) REFERENCES flights(id),
                    FOREIGN KEY(reservation_number) REFERENCES reservations(number),
                    PRIMARY KEY(flight_id, seat_number, reservation_number)
                );
            ");
        QueryNonResult(@"
                CREATE TABLE IF NOT EXISTS reservations (
                    number TEXT NOT NULL PRIMARY KEY,
                    outward_flight_id INTEGER NOT NULL,
                    inward_flight_id INTEGER,
                    user_id INTEGER,
                    email TEXT NOT NULL,
                    price REAL NOT NULL,
                    made_on TEXT NOT NULL,
                    is_paid INTEGER NOT NULL,
                    FOREIGN KEY(outward_flight_id) REFERENCES flights(id),
                    FOREIGN KEY(inward_flight_id) REFERENCES flights(id),
                    FOREIGN KEY(user_id) REFERENCES users(id)
                );
              ");
        QueryNonResult(@"
                CREATE TABLE IF NOT EXISTS reservation_passengers (
                    reservation_number TEXT NOT NULL,
                    passenger_id INTEGER NOT NULL,
                    outward_seat_number TEXT,
                    inward_seat_number TEXT,
                    FOREIGN KEY(reservation_number) REFERENCES reservations(number),
                    FOREIGN KEY(passenger_id) REFERENCES passengers(id),
                    PRIMARY KEY(reservation_number, passenger_id)
                );
            ");
        QueryNonResult(@"
                CREATE TABLE IF NOT EXISTS airport_facilities (
                    airport_id INTEGER NOT NULL,
                    facility TEXT NOT NULL,
                    FOREIGN KEY(airport_id) REFERENCES airports(id)
                );
        ");
        QueryNonResult(@"
                CREATE TABLE IF NOT EXISTS reservations_seats (
                    reservation_number TEXT NOT NULL,
                    seat_number TEXT NOT NULL,
                    airplane_id INTEGER NOT NULL,
                    flight_id INTEGER NOT NULL,
                    FOREIGN KEY(flight_id) REFERENCES flights(id),
                    FOREIGN KEY(reservation_number) REFERENCES reservations(number),
                    FOREIGN KEY(airplane_id) REFERENCES airplanes(id),
                    PRIMARY KEY(reservation_number, seat_number, flight_id)
                );
            ");
    }

    /// <summary>
    /// Queries for a result from the database
    /// </summary>
    /// <param name="query">The query to be executed</param>
    public static DataTable QueryResult(string query)
    {
        DataTable dt = new DataTable();
        using (SQLiteConnection conn = OpenConnection())
        {
            SQLiteCommand command = new SQLiteCommand(query, conn);
            SQLiteDataReader reader = command.ExecuteReader();
            dt.Load(reader);
        }
        return dt;
    }

    /// <summary>
    /// Queries for a non-result from the database
    /// </summary>
    /// <param name="query">The query to be executed</param>
    /// <return>
    public static bool QueryNonResult(string query)
    {
        bool result = true;
        using (SQLiteConnection conn = OpenConnection())
        {
            SQLiteCommand command = new SQLiteCommand(query, conn);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (SQLiteException)
            {
                result = false;
            }
        }
        return result;
    }

    // LOCKS UP THE DATABASE, DO NOT USE
    // private static bool TableExists(string tablename)
    // {
    //     bool result = false;
    //     using(SQLiteConnection conn = OpenConnection())
    //     {
    //         SQLiteCommand command = new SQLiteCommand($"SELECT name FROM sqlite_master WHERE type='table' AND name='{tablename}';", conn);
    //         SQLiteDataReader reader = command.ExecuteReader();
    //         result = reader.HasRows;
    //     }
    //     return result;
    // }

    /// <summary>
    /// Opens a connection to the database
    /// </summary>
    private static SQLiteConnection OpenConnection()
    {
        SQLiteConnection sqlite_conn;
        sqlite_conn = new SQLiteConnection($"Data Source={_dbName};Version=3;");
        sqlite_conn.Open();
        return sqlite_conn;
    }

    /// <summary>
    /// Closes a connection to the database
    /// </summary>
    // private static void CloseConnection(SQLiteConnection conn) {
    //     try {
    //         conn.Close();
    //     } catch (Exception ex) {
    //         Console.WriteLine(ex.Message);
    //     }
    // }
}