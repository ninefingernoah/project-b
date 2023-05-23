using System.Data.SQLite;
using System.Data;
public class DatabaseManager
{
    private SQLiteConnection _conn;

    public DatabaseManager(string databaseName = "database.db")
    {
        _conn = new SQLiteConnection($"Data Source={databaseName};Version=3;");
        _conn.Open();
        CreateDefaultTables();
    }

    public void CreateDefaultTables()
    {
        // Users
        if(!TableExists("Users"))
        {
            Query(@"CREATE TABLE IF NOT EXISTS `Users` (
            `email` string NOT NULL PRIMARY KEY,
            `name` TEXT,
            `password` TEXT NOT NULL,
            `role` TEXT
            );");
            Query(@"INSERT INTO Users (email, name, password, role) VALUES (
                'admin', 'Administrator', 'admin', 'ADMIN')");
        }
        
        // Dishes
        Query(@"CREATE TABLE IF NOT EXISTS 'Dishes' (
            `id` INT NOT NULL PRIMARY KEY,
            `name` TEXT,
            `description` TEXT,
            `ingredients` TEXT
            );");
        // Allergens
        Query(@"CREATE TABLE IF NOT EXISTS 'Allergens' (
            `name` TEXT
            );");
        // Allergen-dishes relationship
        Query(@"CREATE TABLE IF NOT EXISTS Dishes_Allergens (
            dish_id INT NOT NULL,
            allergen TEXT,
            FOREIGN KEY(dish_id) REFERENCES Dishes(id)
        );");
        // Reservations
        if(!TableExists("Arrangements"))
        {
            Query(@"CREATE TABLE IF NOT EXISTS 'Arrangements' (
            `id` INT NOT NULL PRIMARY KEY,
            `price` REAL NOT NULL,
            `rowtype` INT,
            `number_of_courses` INT NOT NULL
            );");
            Query(@"INSERT INTO Arrangements (id, price, rowtype, number_of_courses) VALUES (0, 7.5, 0, 0)");
            Query(@"INSERT INTO Arrangements (id, price, rowtype, number_of_courses) VALUES (1, 15, 0, 1)");
            Query(@"INSERT INTO Arrangements (id, price, rowtype, number_of_courses) VALUES (2, 22.5, 0, 2)");
            Query(@"INSERT INTO Arrangements (id, price, rowtype, number_of_courses) VALUES (3, 30, 0, 3)");
        }
        Query(@"CREATE TABLE IF NOT EXISTS 'Reservations' (
            `id` TEXT NOT NULL PRIMARY KEY,
            `user_email` TEXT,
            `name` TEXT NOT NULL,
            `email` TEXT NOT NULL,
            `phonenumber` TEXT NOT NULL,
            `groupsize` INT NOT NULL,
            `date_and_time` TEXT NOT NULL,
            `course_arrangement_id` INT,
            `wine_arrangement_id` INT,
            FOREIGN KEY(user_email) REFERENCES Users(email)
            );");
        // Seatings
        Query(@"CREATE TABLE IF NOT EXISTS 'Seatings' (
            `id` INT NOT NULL PRIMARY KEY,
            `available` INT NOT NULL,
            `area` TEXT NOT NULL,
            `amount_of_chairs` INT NOT NULL
            );");
        // Reservation-seating relationship
        Query(@"CREATE TABLE IF NOT EXISTS 'Reservations_seatings' (
            `reservation_id` TEXT NOT NULL,
            `seating_id` INT NOT NULL,
            FOREIGN KEY(reservation_id) REFERENCES Reservations(id),
            FOREIGN KEY(seating_id) REFERENCES Seatings(id)
            );");
    }

    public void Query(string sql)
    {
        SQLiteCommand command = new SQLiteCommand(sql, _conn);
        command.ExecuteNonQuery();
    }

    public bool TableExists(string tablename)
    {
        DataTable table = GetTable("sqlite_master");
        foreach (DataRow row in table.Rows)
        {
            if (row["name"].ToString() == tablename)
            {
                return true;
            }
        }
        return false;
    }

    // Get all the data from a table
    public DataTable GetTable(string tableName)
    {
        SQLiteCommand command = new SQLiteCommand($"SELECT * FROM {tableName}", _conn);
        SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
        DataTable table = new DataTable();
        adapter.Fill(table);
        return table;
    }

    public DataTable QueryDat(string query)
    {
        SQLiteCommand command = new SQLiteCommand(query, _conn);
        SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
        DataTable table = new DataTable();
        adapter.Fill(table);
        return table;
    }

    // Get a specific row from a table
    public DataRow GetFirstRow(string query)
    {
        SQLiteCommand command = new SQLiteCommand(query, _conn);
        SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
        DataTable table = new DataTable();
        adapter.Fill(table);
        return table.Rows[0];
    }

}