package date.slightcold.magneticnote.service;

import org.json.JSONObject;

import java.io.BufferedReader;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;

import date.slightcold.magneticnote.config.AppConfig;
import date.slightcold.magneticnote.model.User;

/**
 * Created by 29749 on 2017-12-13.
 */

public class UserService {
    public User GetByUserEmail(String email) {
        try {
            String url = AppConfig.ServerURL + "/User/Get?requestKey=" + AppConfig.RequestKey + "&Email=" + email;
            HttpURLConnection connection = (HttpURLConnection) new URL(url).openConnection();
            connection.setRequestMethod("POST");

            int code = connection.getResponseCode();
            if(code == 200){
                InputStream iStream = connection.getInputStream();
                BufferedReader redaer = new BufferedReader(new InputStreamReader(iStream));
                String returnCode = redaer.readLine();
                JSONObject user = new JSONObject(returnCode).getJSONObject("User");
                User value = new User();
                value.setId(user.getString("Id"));
                value.setEmail(user.getString("Email"));
                value.setPassword(user.getString("Password"));
                value.setAccount(user.getString("Account"));
                return value;
            }
            else{
                return null;
            }

        } catch (Exception e) {
            e.printStackTrace();
        }
        return null;
    }
}
