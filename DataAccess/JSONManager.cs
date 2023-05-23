using Newtonsoft.Json.Linq;
public static class JSONManager {
    public static JObject GetJSON(string path) {
        try {
            using (StreamReader r = new StreamReader("Data\\" + path)) {
                string json = r.ReadToEnd();
                return JObject.Parse(json);
            }
        }
        catch (Exception e) {
            Console.WriteLine(e.Message);
            Console.WriteLine("Er is iets fout gegaan.");
        }
        return null;
    }


    public static void AddCityToJSON(string path, JObject jObject, string? cityName)
    {
        // Read the existing JSON file
        string jsonContent = File.ReadAllText("Data\\" + path);

        // Parse the JSON content into a JObject
        JObject jsonObject = JObject.Parse(jsonContent);

        // Create a new property for the new city
        JProperty cityProperty = new JProperty(cityName, jObject);

        // Add the new property to the existing JObject
        jsonObject.Add(cityProperty);

        // Convert the JObject back to JSON string
        string updatedJsonContent = jsonObject.ToString();

        // Write the updated JSON content back to the file
        File.WriteAllText("Data\\" + path, updatedJsonContent);
    }

    public static void AddAirportToJson(Airport NewAirport)
    {
        JObject JSONClassPriceData = JSONManager.GetJSON("seatprices.json");
        if (JSONClassPriceData == null)
        {
            ConsoleUtils.Error("Er is iets fout gegaan bij het ophalen van de prijzen. Probeer het opnieuw.", AirportController.Instance.showAirportCreationMenu);
            return;
        }

       JObject newCity = new JObject
        {
                ["Boeing_737"] = new JObject
                {
                    ["blue"] = Int32.Parse(AirportSeatAndPricesView.Instance.ViewBag["Boeing737Blue"]),
                    ["yellow"] = Int32.Parse(AirportSeatAndPricesView.Instance.ViewBag["Boeing737Yellow"])
                },
                ["Airbus_330"] = new JObject
                {
                    ["white"] = Int32.Parse(AirportSeatAndPricesView.Instance.ViewBag["Airbus330White"]),
                    ["dark_blue"] = Int32.Parse(AirportSeatAndPricesView.Instance.ViewBag["Airbus330Darkblue"]),
                    ["purple"] = Int32.Parse(AirportSeatAndPricesView.Instance.ViewBag["Airbus330Purple"]),
                    ["pink"] = Int32.Parse(AirportSeatAndPricesView.Instance.ViewBag["Airbus330Pink"]),
                    ["grey_(business_class)"] = Int32.Parse(AirportSeatAndPricesView.Instance.ViewBag["Airbus330Grey"])
                },
                ["Boeing_787"] = new JObject
                {
                    ["white"] = Int32.Parse(AirportSeatAndPricesView.Instance.ViewBag["Boeing787White"]),
                    ["blue"] = Int32.Parse(AirportSeatAndPricesView.Instance.ViewBag["Boeing787Blue"]),
                    ["orange_(business_class)"] = Int32.Parse(AirportSeatAndPricesView.Instance.ViewBag["Boeing787Orange"])
                }
        };

        JSONManager.AddCityToJSON("seatprices.json", newCity, NewAirport.City);

    }
}
