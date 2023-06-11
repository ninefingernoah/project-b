public class PassengerOverviewView : IView
{

    private static readonly PassengerOverviewView instance = new PassengerOverviewView();

    static PassengerOverviewView()
    {
    }

    public static PassengerOverviewView Instance
    {
        get
        {
            return instance;
        }
    }

    public Dictionary<string, object> ViewBag = new Dictionary<string, object>();

    public Passenger? ThePassenger;

    public void Display()
    {
        if (ThePassenger == null)
        {
            throw new Exception("Passenger is null");
        }
        List<string> optionsList = new List<string>() {
            $"Voornaam: {ThePassenger.FirstName}",
            $"Achternaam: {ThePassenger.LastName}",
            $"Email: {ThePassenger.Email}",
            $"Document nummer: {ThePassenger.DocumentNumber}",
            $"Geboortedatum: {((DateTime) ThePassenger.BirthDate!).ToString("dd-MM-yyyy")}",
            $"Adres: {ThePassenger.Address.Street} {ThePassenger.Address.HouseNumber}, {ThePassenger.Address.City}, {ThePassenger.Address.Country}",
            "-",
            "Terug"
        };
        Menu menu = new Menu("Passagier bewerken", optionsList.ToArray());
        int choice = menu.Run();
        ViewBag["MainMenuSelection"] = choice.ToString();
    }

    public void PopulateViewBag(Passenger passenger)
    {
       ThePassenger = passenger;
    }

    public void ClearViewBag()
    {
        ViewBag.Clear();
    }
}