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
    
}