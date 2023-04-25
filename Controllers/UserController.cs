public sealed class UserController {
    private static readonly UserController instance;

    static UserController(){
    }
    private UserController(){
    }

    public static UserController Instance {
        get {
            return instance;
        }
    }

    public static void ShowLogin() {

    }

    public static void ShowRegister(){

    }

}