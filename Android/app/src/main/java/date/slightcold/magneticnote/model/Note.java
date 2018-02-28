package date.slightcold.magneticnote.model;

/**
 * Created by 29749 on 2017-12-13.
 */

public class Note {
    private int id;
    private String title;
    private String content;
    private String CreateDate;
    private String UpdateDate;
    private int NoteBookId;

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getTitle() {
        return title;
    }

    public void setTitle(String title) {
        this.title = title;
    }

    public String getContent() {
        return content;
    }

    public void setContent(String content) {
        this.content = content;
    }

    public String getCreateDate() {
        return CreateDate;
    }

    public void setCreateDate(String createDate) {
        CreateDate = createDate;
    }

    public String getUpdateDate() {
        return UpdateDate;
    }

    public void setUpdateDate(String updateDate) {
        UpdateDate = updateDate;
    }

    public int getNoteBookId() {
        return NoteBookId;
    }

    public void setNoteBookId(int noteBookId) {
        NoteBookId = noteBookId;
    }

    @Override
    public String toString() {
        return this.title;
    }
}
