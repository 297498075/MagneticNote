package date.slightcold.magneticnote;

import android.app.Application;

import org.xutils.x;

/**
 * Created by admin on 2018-01-01.
 */

public class MagneticNoteApplication extends Application {
    @Override
    public void onCreate() {
        super.onCreate();
        x.Ext.init(this);
        x.Ext.setDebug(true);
    }
}
