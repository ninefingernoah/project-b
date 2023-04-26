using System.Data.SQLite;
using System.Data;
public static class DatabaseManager {
    private const string _dbName = "airline.db";

    public static void CreateDatabase() {
        // TODO: Implement logic
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
