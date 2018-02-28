package date.slightcold.magneticnote;

import android.annotation.SuppressLint;
import android.os.Bundle;
import android.os.Handler;
import android.os.Looper;
import android.os.Message;
import android.support.constraint.ConstraintLayout;
import android.support.design.widget.FloatingActionButton;
import android.support.design.widget.NavigationView;
import android.support.v4.view.GravityCompat;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBarDrawerToggle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.text.Html;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.webkit.WebView;
import android.widget.Adapter;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.LinearLayout;
import android.widget.ListView;
import android.widget.ScrollView;
import android.widget.SimpleAdapter;
import android.widget.TextView;
import android.widget.Toast;

import org.json.JSONArray;
import org.json.JSONObject;
import org.w3c.dom.Text;
import org.xutils.x;

import java.io.BufferedReader;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import date.slightcold.magneticnote.model.Note;
import date.slightcold.magneticnote.model.User;
import date.slightcold.magneticnote.service.NoteBookService;
import date.slightcold.magneticnote.service.NoteService;
import date.slightcold.magneticnote.service.UserService;

import static date.slightcold.magneticnote.R.layout.activity_main;
import static date.slightcold.magneticnote.R.layout.edit_note;

public class MainActivity extends AppCompatActivity
        implements NavigationView.OnNavigationItemSelectedListener {

    private static UserService userService = new UserService();
    private static NoteService noteService = new NoteService();
    private static NoteBookService noteBookService = new NoteBookService();
    private static User currentUser = new User();
    private static Note currentNote = new Note();
    private static List<?> currentValueList = null;
    private static Handler handler = null;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        handler = new Handler(MainActivity.this.getMainLooper());

        super.onCreate(savedInstanceState);

        if (!isLogin()) {
            x.view().inject(this);
            setContentView(R.layout.login_main);

            Button button = (Button) findViewById(R.id.action_login);
            button.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View v) {
                    EditText text_username = (EditText) findViewById(R.id.input_username);
                    EditText text_password = (EditText) findViewById(R.id.input_password);
                    currentUser.setEmail(text_username.getText().toString().trim());
                    currentUser.setPassword(text_password.getText().toString().trim());

                    if(!currentUser.getEmail().equals("") || !currentUser.getPassword().equals("")) {
                        new Thread(){
                            @Override
                            public void run() {
                                Login(null);
                            }
                        }.start();
                    }
                    else {
                        Toast.makeText(MainActivity.this, "请输入正确的账号和秘密", Toast.LENGTH_SHORT).show();
                    }
                }
            });
        }
    }

    private void Login(User user) {
        if(user == null)
        {
            user = userService.GetByUserEmail(currentUser.getEmail());
        }

        if(user == null) {
            Toast.makeText(MainActivity.this, "网络错误，请检查网络连接", Toast.LENGTH_SHORT).show();
        }
        else if(user.getPassword().equals(currentUser.getPassword())) {
            currentUser = user;

            handler.post(new Runnable(){
                @Override
                public void run() {
                    MainActivity.this.setContentView(activity_main);

                    Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
                    setSupportActionBar(toolbar);

                    FloatingActionButton fab = (FloatingActionButton) findViewById(R.id.fab);
                    fab.setOnClickListener(new View.OnClickListener() {
                        @Override
                        public void onClick(View view) {
                            Toast.makeText(MainActivity.this, "开发中，静待下一个版本发布!", Toast.LENGTH_LONG).show();
                        }
                    });

                    DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
                    ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(
                            MainActivity.this, drawer, toolbar, R.string.navigation_drawer_open, R.string.navigation_drawer_close);
                    drawer.setDrawerListener(toggle);
                    toggle.syncState();

                    NavigationView navigationView = (NavigationView) findViewById(R.id.nav_view);
                    navigationView.setNavigationItemSelectedListener(MainActivity.this);

                    new Thread(){
                        @Override
                        public void run() {
                            currentValueList = noteService.GetByUserId(currentUser.getId());
                            if(currentValueList != null)
                            {
                                handler.post(new Runnable() {
                                    @Override
                                    public void run() {
                                        LinearLayout listView = (LinearLayout) findViewById(R.id.Content_List);

                                        for (int i = 0; i < currentValueList.size(); i++) {
                                            Button button = new Button(MainActivity.this);
                                            button.setPadding(30,20,30,0);
                                            button.setText(currentValueList.get(i).toString());
                                            button.setHeight(120);
                                            button.setTag(currentValueList.get(i));
                                            button.setWidth(listView.getWidth());
                                            button.setBackgroundColor(0);
                                            button.setOnClickListener(new View.OnClickListener() {
                                                @Override
                                                public void onClick(View view) {
                                                    handler.post(new Runnable() {
                                                        @Override
                                                        public void run() {
                                                            currentNote = (Note)view.getTag();
                                                            MainActivity.this.setContentView(edit_note);
                                                            TextView title = findViewById(R.id.edit_note_title);
                                                            title.setText(currentNote.getTitle());
                                                            WebView  content = findViewById(R.id.edit_note_content);
                                                            content.getSettings().setDefaultTextEncodingName("UTF -8");
                                                            content.loadData(currentNote.getContent(), "text/html; charset=UTF-8", null);
                                                        }
                                                    });
                                                }
                                            });

                                            listView.addView(button);
                                        }
                                    }
                                });
                            }
                        }
                    }.start();

                }
            });
        }
        else {
            Toast.makeText(MainActivity.this, "账号或密码错误", Toast.LENGTH_SHORT).show();
        }
    }

    private boolean isLogin() {
        String email = getString(R.string.email);
        if(email.equals("")) {
            return false;
        }
        else {
            User user = userService.GetByUserEmail(email);
            Login(user);
            return true;
        }
    }

    @Override
    public void onBackPressed() {
        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        LinearLayout edit_note =  (LinearLayout) findViewById(R.id.edit_note);
        if (drawer != null && drawer.isDrawerOpen(GravityCompat.START)) {
            drawer.closeDrawer(GravityCompat.START);
        }else if(edit_note != null){
            handler.post(new Runnable() {
                @Override
                public void run() {
                    MainActivity.this.setContentView(activity_main);
                }
            });
        } else {
            super.onBackPressed();
        }
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.main, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();

        //noinspection SimplifiableIfStatement
        if (id == R.id.action_setting) {
            return true;
        }

        return super.onOptionsItemSelected(item);
    }

    @SuppressWarnings("StatementWithEmptyBody")
    @Override
    public boolean onNavigationItemSelected(MenuItem item) {
        // Handle navigation view item clicks here.
        int id = item.getItemId();

        if (id == R.id.nav_allNote) {

        } else if (id == R.id.nav_notebook) {
            new Thread(new Runnable() {
                @Override
                public void run() {
                    currentValueList = noteBookService.GetByUserId(currentUser.getId());

                    handler.post(new Runnable() {
                        @Override
                        public void run() {
                            LinearLayout listView = (LinearLayout) findViewById(R.id.Content_List);
                            listView.removeAllViews();
                            for (int i = 0; i < currentValueList.size(); i++) {
                                Button button = new Button(MainActivity.this);
                                button.setPadding(30,20,30,0);
                                button.setText(currentValueList.get(i).toString());
                                button.setHeight(120);
                                button.setTag(currentValueList.get(i));
                                button.setWidth(listView.getWidth());
                                button.setBackgroundColor(0);

                                listView.addView(button);
                            }
                        }
                    });
                }
            }).start();

        } else if (id == R.id.nav_shared) {

        } else if (id == R.id.nav_workGroup) {

        } else if (id == R.id.nav_info) {

        } else if (id == R.id.nav_setting) {

        }

        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        drawer.closeDrawer(GravityCompat.START);
        return true;
    }
}
