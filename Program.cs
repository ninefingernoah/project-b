public class Program
{

    /// <summary>
    /// Main entry point for the application.
    /// </summary>
    /// <param name="args">Command line arguments.</param>
    public static void Main(string[] args)
    {
        DatabaseManager.CreateDatabase();
        PopulateTestData();
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
                VALUES ('Boeing_787', 0)
            ");
        DatabaseManager.QueryNonResult($@"
                INSERT INTO airplanes (name, total_capacity)
                VALUES ('Boeing_737', 0)
            ");
        // Insert flights
        DatabaseManager.QueryNonResult($@"
                INSERT INTO flights (airplane_id, departure_id, destination_id, departure_time, arrival_time)
                VALUES (1, 1, 2, '2025-01-01 12:00', '2025-01-01 14:00:00')
            ");
        DatabaseManager.QueryNonResult($@"
                INSERT INTO flights (airplane_id, departure_id, destination_id, departure_time, arrival_time)
                VALUES (1, 1, 2, '2025-01-06 14:00', '2025-01-06 16:00:00')
            ");
        // Insert reservations
        DatabaseManager.QueryNonResult($@"
                INSERT INTO reservations (number, outward_flight_id, inward_flight_id, user_id, email, price, made_on, is_paid)
                VALUES (1234, 1, 2, 2, 'johndoe@email.com', 400, '2020-01-01 12:00:00', 1)
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

}
