using System.Data.SQLite;
using System.Data;
public static class DatabaseManager {
    private const string _dbName = "airline.db";

    public static void CreateDatabase() {
        // TODO: Implement logic
        if (!TableExists("users")) {
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
        }
    }

    /// <summary>
    /// Queries for a result from the database
    /// </summary>
    /// <param name="query">The query to be executed</param>
    public static DataTable QueryResult(string query) {
        SQLiteConnection conn = OpenConnection();
        SQLiteCommand command = new SQLiteCommand(query, conn);
        SQLiteDataReader reader = command.ExecuteReader();
        DataTable dt = new DataTable();
        dt.Load(reader);
        CloseConnection(conn);
        return dt;
    }

    /// <summary>
    /// Queries for a non-result from the database
    /// </summary>
    /// <param name="query">The query to be executed</param>
    /// <return>
    public static bool QueryNonResult(string query) {
        SQLiteConnection conn = OpenConnection();
        SQLiteCommand command = new SQLiteCommand(query, conn);
        try {
            command.ExecuteNonQuery();
        } catch (Exception ex) {
            Console.WriteLine(ex.Message);
            CloseConnection(conn);
            return false;
        }
        CloseConnection(conn);
        return true;
    }

    private static bool TableExists(string tablename)
    {
        SQLiteConnection conn = OpenConnection();
        SQLiteCommand command = new SQLiteCommand($"SELECT name FROM sqlite_master WHERE type='table' AND name='{tablename}';", conn);
        SQLiteDataReader reader = command.ExecuteReader();
        bool result = reader.HasRows;
        CloseConnection(conn);
        return result;
    }

    /// <summary>
    /// Opens a connection to the database
    /// </summary>
    private static SQLiteConnection OpenConnection() {
        SQLiteConnection sqlite_conn;
        sqlite_conn = new SQLiteConnection($"Data Source={_dbName};Version=3;New=True;Compress=True;");
        try {
            sqlite_conn.Open();
        } catch (Exception ex) {
            Console.WriteLine(ex.Message);
        }
        return sqlite_conn;
    }

    /// <summary>
    /// Closes a connection to the database
    /// </summary>
    private static void CloseConnection(SQLiteConnection conn) {
        try {
            conn.Close();
        } catch (Exception ex) {
            Console.WriteLine(ex.Message);
        }
    }
}
