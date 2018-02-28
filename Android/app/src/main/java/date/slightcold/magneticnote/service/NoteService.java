package date.slightcold.magneticnote.service;

import org.json.*;

import java.net.HttpURLConnection;
import java.net.URL;
import java.util.ArrayList;
import java.util.List;
import java.io.*;

import date.slightcold.magneticnote.config.AppConfig;
import date.slightcold.magneticnote.model.Note;

/**
 * Created by 29749 on 2017-12-13.
 */

public class NoteService {
    public List<Note> GetByUserId(String id) {
        try {
            String url = AppConfig.ServerURL + "/Note/Get?requestKey=" + AppConfig.RequestKey + "&UserId=" + id;

            HttpURLConnection connection = (HttpURLConnection) new URL(url).openConnection();
            connection.setRequestMethod("POST");

            int code = connection.getResponseCode();
            if(code == 200){
                InputStream iStream = connection.getInputStream();
                BufferedReader redaer = new BufferedReader(new InputStreamReader(iStream));
                String returnCode = redaer.readLine();

                JSONArray array = new JSONObject(returnCode).getJSONArray("NoteList");
                List<Note> listValue = new ArrayList<Note>();
                for(int i = 0; i < array.length(); i++)
                {
                    Note note = new Note();
                    note.setContent(array.getJSONObject(i).getString("Content"));
                    note.setCreateDate(array.getJSONObject(i).getString("CreateDate"));
                    note.setId(array.getJSONObject(i).getInt("Id"));
                    note.setNoteBookId(array.getJSONObject(i).getInt("NoteBookId"));
                    note.setTitle(array.getJSONObject(i).getString("Title"));
                    note.setUpdateDate(array.getJSONObject(i).getString("UpdateDate"));
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
