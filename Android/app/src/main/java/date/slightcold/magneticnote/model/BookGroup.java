package date.slightcold.magneticnote.model;

/**
 * Created by admin on 2017-12-27.
 */

public class BookGroup {
    public int id;
    public String name;
    public int userId;

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public int getUserId() {
        return userId;
    }

    public void setUserId(int userId) {
        this.userId = userId;
    }

    @Override
    public String toString() {
        return this.name;
    }
}
