public sealed class RegistrationView {
    private static readonly RegistrationView instance;

    static RegistrationView(){
    }
    private RegistrationView(){
    }

    public static RegistrationView Instance {
        get {
            return instance;
        }
    }
}