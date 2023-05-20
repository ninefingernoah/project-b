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
}
