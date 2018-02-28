package date.slightcold.magneticnote.service;

import org.json.JSONArray;
import org.json.JSONObject;

import java.io.BufferedReader;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.ArrayList;
import java.util.List;

import date.slightcold.magneticnote.config.AppConfig;
import date.slightcold.magneticnote.model.NoteBook;

/**
 * Created by admin on 2017-12-27.
 */

public class NoteBookService {
    public List<NoteBook> GetByUserId(String id) {
        try {
            String url = AppConfig.ServerURL + "/NoteBook/Get?requestKey=" + AppConfig.RequestKey + "&UserId=" + id;

            HttpURLConnection connection = (HttpURLConnection) new URL(url).openConnection();
            connection.setRequestMethod("POST");

            int code = connection.getResponseCode();
            if(code == 200){
                InputStream iStream = connection.getInputStream();
                BufferedReader redaer = new BufferedReader(new InputStreamReader(iStream));
                String returnCode = redaer.readLine();

                JSONArray array = new JSONObject(returnCode).getJSONArray("NoteBookList");
                List<NoteBook> listValue = new ArrayList<NoteBook>();
                for(int i = 0; i < array.length(); i++)
                {
                    NoteBook note = new NoteBook();
                    note.setBookGroupId(array.getJSONObject(i).getInt("BookGroupId"));
                    note.setName(array.getJSONObject(i).getString("Name"));
                    note.setId(array.getJSONObject(i).getInt("Id"));
                    note.setUserId(array.getJSONObject(i).getInt("UserId"));
                    listValue.add(note);
                }
                return listValue;
            }
            else
            {
                return null;
            }
        } catch (Exception e) {
            e.printStackTrace();
        }

        return null;
    }
}
