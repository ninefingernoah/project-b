using Newtonsoft.Json.Linq;
public static class JSONManager {
    public static JObject GetJSON(string path) {
        string json = "";
        try {
            using (StreamReader r = new StreamReader(path)) {
                json = r.ReadToEnd();
            }
        }
        catch (Exception e) {
            Console.WriteLine("Er is iets fout gegaan.");
        }
        return JObject.Parse(json);
    }
    
}