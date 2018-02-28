package date.slightcold.magneticnote.model;

/**
 * Created by admin on 2017-12-27.
 */

public class NoteBook {
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

    public int getBookGroupId() {
        return bookGroupId;
    }

    public void setBookGroupId(int bookGroupId) {
        this.bookGroupId = bookGroupId;
    }

    public int id;
    public String name;
    public int userId;
    public int bookGroupId;

    @Override
    public String toString() {
        return this.name;
    }
}
