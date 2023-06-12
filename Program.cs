public class Program {

    /// <summary>
    /// Main entry point for the application.
    /// </summary>
    /// <param name="args">Command line arguments.</param>
    public static void Main(string[] args) {
        var mainMenuController = MainMenuController.Instance;
        mainMenuController.ShowMainMenu();
        // FlightController.Instance.ShowFlightEditor(FlightManager.GetFlight(1));
    }

    private static void PopulateTestData()
    {
        DatabaseManager.QueryNonResult($@"
                INSERT INTO users (role, first_name, last_name, email, password)
                VALUES ('ADMIN', 'Admin', 'Admin', 'admin', '{BCrypt.Net.BCrypt.HashPassword("admin")}')
            ");
        DatabaseManager.QueryNonResult($@"
                INSERT INTO users (role, first_name, last_name, email, password)
                VALUES ('USER', 'John', 'Doe', 'johndoe@email.com', '{BCrypt.Net.BCrypt.HashPassword("password")}')
            ");
        // Insert airports
        DatabaseManager.QueryNonResult($@"
                INSERT INTO airports (name, city, country, code)
                VALUES ('Schiphol', 'Amsterdam', 'Nederland', 'AMS')
            ");
        DatabaseManager.QueryNonResult($@"
                INSERT INTO airports (name, city, country, code)
                VALUES ('Heathrow', 'Londen', 'Verenigd Koninkrijk', 'LHR')
            ");
        // Insert airplanes
        DatabaseManager.QueryNonResult($@"
                INSERT INTO airplanes (name, total_capacity)
                VALUES ('Boeing_787', 500)
            ");
        DatabaseManager.QueryNonResult($@"
                INSERT INTO airplanes (name, total_capacity)
                VALUES ('Boeing_737', 300)
            ");
        // Insert flights
        DatabaseManager.QueryNonResult($@"
                INSERT INTO flights (airplane_id, departure_id, destination_id, departure_time, arrival_time)
                VALUES (1, 1, 2, '2025-01-01 12:00', '2025-01-01 14:00:00')
            ");
        // Insert reservations
        DatabaseManager.QueryNonResult($@"
                INSERT INTO reservations (number, flight_id, user_id, email, price, made_on, is_paid)
                VALUES (1234, 1, 2, 'johndoe@email.com', 400, '2020-01-01 12:00:00', 1)
            ");
        // Insert addresses
        DatabaseManager.QueryNonResult($@"
                INSERT INTO addresses (city, country, street, street_number)
                VALUES ('Amsterdam', 'Nederland', 'Straat', '1')
            ");
        DatabaseManager.QueryNonResult($@"
                INSERT INTO addresses (city, country, street, street_number)
                VALUES ('Rotterdam', 'Nederland', 'Dijk', '12')
            ");
        // Insert passengers
        DatabaseManager.QueryNonResult($@"
                INSERT INTO passengers (email, first_name, last_name, document_number, date_of_birth, address_id)
                VALUES ('johndoe@email.com', 'John', 'Doe', '123456789', '1990-01-01', 1)
            ");
        DatabaseManager.QueryNonResult($@"
                INSERT INTO passengers (email, first_name, last_name, document_number, date_of_birth, address_id)
                VALUES ('janedoe@email.com', 'Jane', 'Doe', '987654321', '1990-01-01', 2)
            ");
        // Insert reservation_passengers
        DatabaseManager.QueryNonResult($@"
                INSERT INTO reservation_passengers (reservation_number, passenger_id)
                VALUES (1234, 1)
            ");
        DatabaseManager.QueryNonResult($@"
                INSERT INTO reservation_passengers (reservation_number, passenger_id)
                VALUES (1234, 2)
            ");
    }

    public static bool IsTestDataPresent()
{
    // Check if users table has the test data
    string query = "SELECT COUNT(*) FROM users WHERE email = 'admin'";
    int userCount = Convert.ToInt32(DatabaseManager.QueryResult(query).Rows[0][0]);
    if (userCount == 0)
    {
        return false;
    }

    query = "SELECT COUNT(*) FROM users WHERE email = 'johndoe@email.com'";
    userCount = Convert.ToInt32(DatabaseManager.QueryResult(query).Rows[0][0]);
    if (userCount == 0)
    {
        return false;
    }

    // Check if airports table has the test data
    query = "SELECT COUNT(*) FROM airports WHERE name = 'Schiphol'";
    int airportCount = Convert.ToInt32(DatabaseManager.QueryResult(query).Rows[0][0]);
    if (airportCount == 0)
    {
        return false;
    }

    query = "SELECT COUNT(*) FROM airports WHERE name = 'Heathrow'";
    airportCount = Convert.ToInt32(DatabaseManager.QueryResult(query).Rows[0][0]);
    if (airportCount == 0)
    {
        return false;
    }

    // Check if airplanes table has the test data
    query = "SELECT COUNT(*) FROM airplanes WHERE name = 'Boeing_787'";
    int airplaneCount = Convert.ToInt32(DatabaseManager.QueryResult(query).Rows[0][0]);
    if (airplaneCount == 0)
    {
        return false;
    }

    query = "SELECT COUNT(*) FROM airplanes WHERE name = 'Boeing_737'";
    airplaneCount = Convert.ToInt32(DatabaseManager.QueryResult(query).Rows[0][0]);
    if (airplaneCount == 0)
    {
        return false;
    }

    // Check if flights table has the test data
    query = "SELECT COUNT(*) FROM flights WHERE airplane_id = 1";
    int flightCount = Convert.ToInt32(DatabaseManager.QueryResult(query).Rows[0][0]);
    if (flightCount == 0)
    {
        return false;
    }

    // Check if reservations table has the test data
    query = "SELECT COUNT(*) FROM reservations WHERE number = '1234'";
    int reservationCount = Convert.ToInt32(DatabaseManager.QueryResult(query).Rows[0][0]);
    if (reservationCount == 0)
    {
        return false;
    }

    // Check if addresses table has the test data
    query = "SELECT COUNT(*) FROM addresses WHERE street_number = '1'";
    int addressCount = Convert.ToInt32(DatabaseManager.QueryResult(query).Rows[0][0]);
    if (addressCount == 0)
    {
        return false;
    }

    query = "SELECT COUNT(*) FROM addresses WHERE street_number = '12'";
    addressCount = Convert.ToInt32(DatabaseManager.QueryResult(query).Rows[0][0]);
    if (addressCount == 0)
    {
        return false;
    }

    // Check if passengers table has the test data
    query = "SELECT COUNT(*) FROM passengers WHERE email = 'johndoe@email.com' AND first_name = 'John' AND last_name = 'Doe' AND document_number = '123456789'";
    int passengerCount = Convert.ToInt32(DatabaseManager.QueryResult(query).Rows[0][0]);
    if (passengerCount == 0)
    {
        return false;
    }

    query = "SELECT COUNT(*) FROM passengers WHERE email = 'janedoe@email.com' AND first_name = 'Jane' AND last_name = 'Doe' AND document_number = '987654321'";
    passengerCount = Convert.ToInt32(DatabaseManager.QueryResult(query).Rows[0][0]);
    if (passengerCount == 0)
    {
        return false;
    }

    // Check if reservation_passengers table has the test data
    query = "SELECT COUNT(*) FROM reservation_passengers WHERE reservation_number = '1234'";
    int resPassengerCount = Convert.ToInt32(DatabaseManager.QueryResult(query).Rows[0][0]);
    if (resPassengerCount == 0)
    {
        return false;
    }

    return true;
}


}
