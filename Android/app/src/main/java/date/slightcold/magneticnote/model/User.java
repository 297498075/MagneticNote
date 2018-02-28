package date.slightcold.magneticnote.model;

/**
 * Created by 29749 on 2017-12-13.
 */

public class User{
    public String getId() {
        return Id;
    }

    public void setId(String id) {
        Id = id;
    }

    public String getEmail() {
        return Email;
    }

    public void setEmail(String email) {
        Email = email;
    }

    public String getAccount() {
        return Account;
    }

    public void setAccount(String account) {
        Account = account;
    }

    public String getPassword() {
        return Password;
    }

    public void setPassword(String password) {
        Password = password;
    }

    private String Id;
    private String Email;
    private String Account;
    private String Password;
}
