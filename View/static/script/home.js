var app = new Vue({
    el:"#app",
    data: {
        nav_isshow:true,
        section_create_isshow:false,
        section_search_isshow:false,
        section_note_isshow:true,
        section_book_isshow:false,
        select_option_isshow:true,
        button_isloading:false,
        addNoteBookModal:false,
        addGroupModal:false,
        note_width:500,
        add_note_title:"",
        add_note_content:"",
        addNoteBook_Name:"",
        addGroup_Name:"",
        select_option:1,
        select_group_option:1,
        select_placeholder:"请选择笔记本",
        search_content:"",
        noteList:[],
        default_bookList:[],
        bookList:[],
        groupList:[],
        activeNote:{
            Id:0,
            Title:"",
            Content:"",
            CreateDate:"",
            NoteBookId:0
        }
    },
    methods: {
        select:function (name) {
            if(name == "add") {
                app.nav_isshow=false;
                app.section_search_isshow=false;
                app.section_note_isshow=false;
                app.section_book_isshow=false;
                app.section_create_isshow=true;
            }else if(name == "search"){
                app.section_create_isshow=false;
                app.section_note_isshow=false;
                app.section_book_isshow=false;
                app.nav_isshow=true;
                app.section_search_isshow=true;
            }else if(name == "note"){
                app.section_create_isshow=false;
                app.section_search_isshow=false;
                app.section_book_isshow=false;
                app.nav_isshow=true;
                app.section_note_isshow=true;
                if(app.noteList != []){
                    var k = 0;
                    while (app.noteList[k] == undefined){
                        k++;
                    }
                    app.activeNote = app.noteList[k];
                    app.select_option = app.noteList[k].NoteBookId;
                }
            }else if(name == "book"){
                app.section_create_isshow=false;
                app.section_search_isshow=false;
                app.section_note_isshow=false;
                app.nav_isshow=true;
                app.section_book_isshow=true;
            }
        }, //ok
        select_change : function (Id) {
            app.select_option_isshow = false;
        }, //ok
        cancel:function (e) {
            app.section_create_isshow=false;
            app.nav_isshow=true;
            app.section_note_isshow=true;
            app.add_note_title = "";
            app.add_note_content = "";
        }, //ok
        addNote:function (e) {
            app.button_isloading = true;
            if(app.select_option == 1 || app.select_option == "") {
                app.$Message.info({
                    content:"请选择笔记本"
                })
                app.button_isloading = false;
            }else if(app.add_note_title == ""){
                app.$Message.info({
                    content:"请输入标题"
                })
                document.getElementById("input_add_title").focus();
                app.button_isloading = false;
            }else {
                axios.post("/Add/Note", { Title : app.add_note_title, Content : app.add_note_content, NoteBookId : app.select_option}).then(function (response) {
                    if(response.data == 1){
                        app.$Message.success({
                            content:"保存成功",
                            onClose:function (e) {
                                app.cancel(e)
                                app.button_isloading = false;
                            }
                        })
                        renovateNote();
                    }
                    else {
                        window.location.href="./Login"
                    }
                }).catch(function (e) {
                    app.$modal.error({
                        title:"保存失败",
                        content:"服务器异常,请稍后再试",
                        onOk:function (e) {
                            app.$modal.remove()
                            window.location.href="./Login";
                        },
                        onCancel:function (e) {
                            app.$modal.remove()
                            window.location.href="./Login"
                        }
                    })
                })
            }
        }, //ok
        addNoteBook:function (e) {
            app.addNoteBookModal = true;
        }, //ok
        addNoteBook_Ok:function (e) {
            if(app.addNoteBook_Name == "") {
                app.$Message.info({
                    content:"请输入笔记本的名字"
                })
                app.button_isloading = false;
            }else {
                if (app.select_group_option == 1) {
                    axios.post("/Add/NoteBook", {Name: app.addNoteBook_Name}).then(function (response) {
                        if (response.data == 1) {
                            app.$Message.success({
                                content: "保存成功",
                                onClose: function (e) {
                                    app.button_isloading = false;
                                }
                            })
                            renovateGroup()
                            app.addNoteBook_Name = ""
                            app.addNoteBookModal = false;
                        }
                        else {
                            window.location.href = "./Login"
                        }
                    }).catch(function (e) {
                        app.$modal.error({
                            title: "保存失败",
                            content: "服务器异常,请稍后再试",
                            onOk: function (e) {
                                window.location.href = "./Login";
                            },
                            onCancel: function (e) {
                                window.location.href = "./Login"
                            }
                        })
                    })
                } else {
                    axios.post("/Add/NoteBook", {Name: app.addNoteBook_Name, GroupId: app.select_group_option}).then(function (response) {
                        if (response.data == 1) {
                            app.$Message.success({
                                content: "保存成功",
                                onClose: function (e) {
                                    app.button_isloading = false;
                                }
                            })
                            renovateGroup()
                            app.addNoteBook_Name=""
                            app.addNoteBookModal = false;
                        }
                        else {
                            window.location.href = "./Login"
                        }
                    }).catch(function (e) {
                        app.$modal.error({
                            title: "保存失败",
                            content: "服务器异常,请稍后再试",
                            onOk: function (e) {
                                app.$modal.remove()
                                window.location.href = "./Login";
                            },
                            onCancel: function (e) {
                                app.$modal.remove()
                                window.location.href = "./Login"
                            }
                        })
                    })
                }
            }
        }, //ok
        addGroup:function (e) {
            app.addGroupModal = true;
        }, //ok
        addGroup_Ok:function (e) {
            app.button_isloading = true;
            if(app.addGroup_Name == "") {
                app.$Message.info({
                    content:"请输入分组的名字"
                })
                app.button_isloading = false;
            }else {
                axios.post("/Add/Group", {Name: app.addGroup_Name}).then(function (response) {
                    if (response.data == 1) {
                        app.$Message.success({
                            content: "保存成功",
                            onClose: function (e) {
                                app.button_isloading = false;
                            }
                        })
                        renovateGroup()
                        app.addGroupModal = false;
                    }
                    else {
                        window.location.href = "./Login"
                    }
                }).catch(function (e) {
                    app.$modal.error({
                        title: "保存失败",
                        content: "服务器异常,请稍后再试",
                        onOk: function (e) {
                            window.location.href = "./Login";
                        },
                        onCancel: function (e) {
                            window.location.href = "./Login"
                        }
                    })
                })
            }
        }, //ok
        saveNote:function (e) {
            if(app.activeNote.Id != 0){
                app.button_isloading = true;
                axios.post("/Update/Note",{ Note : app.activeNote }).then(function (response) {
                    if(response.data == 1){
                        app.button_isloading = false;
                        app.$Message.success({
                            content:"保存成功"
                        })
                    }else {
                        app.button_isloading = false;
                        app.$Message.error({
                            content:"保存失败,请联系系统管理员"
                        })
                    }
                }).catch(function (error) {
                    app.$modal.error({
                        title:"登录异常",
                        content:"登录异常,请稍后再试",
                        onOk:function (e) {
                            app.$modal.remove()
                            window.location.href="./Login";
                        },
                        onCancel:function (e) {
                            app.$modal.remove()
                            window.location.href="./Login"
                        }
                    })
                })
            }
        }, //ok
        search:function (e) {
            if(app.search_content == "") {
                app.$Message.info({
                    content:"请输入搜索内容"
                })
            }else {
                if(app.select_option == 1){
                    axios.post("/Get/NoteByCondition",{Condition:app.search_content}).then(function (response) {
                        app.noteList = response.data.NoteList;
                        app.section_create_isshow=false;
                        app.section_search_isshow=false;
                        app.section_book_isshow=false;
                        app.nav_isshow=true;
                        app.section_note_isshow=true;
                    }).catch(function (error) {
                        app.$modal.error({
                            title:"搜索失败",
                            content:"服务器异常,请稍后再试",
                            onOk:function (e) {
                                app.$modal.remove()
                                window.location.href="./Login";
                            },
                            onCancel:function (e) {
                                app.$modal.remove()
                                window.location.href="./Login"
                            }
                        })
                    })
                }else {
                    axios.post("/Get/NoteByCondition",{Condition:app.search_content, NoteBookId : app.select_option}).then(function (response) {
                        app.noteList = response.data.NoteList;
                        app.section_create_isshow=false;
                        app.section_search_isshow=false;
                        app.section_book_isshow=false;
                        app.nav_isshow=true;
                        app.section_note_isshow=true;
                    }).catch(function (error) {
                        app.$modal.error({
                            title:"搜索失败",
                            content:"服务器异常,请稍后再试",
                            onOk:function (e) {
                                app.$modal.remove()
                                window.location.href="./Login";
                            },
                            onCancel:function (e) {
                                app.$modal.remove()
                                window.location.href="./Login"
                            }
                        })
                    })
                }
            }
        }, //ok
        active:function (e) {
            var btn = e.target;
            while (btn.tagName != "BUTTON"){
                btn = btn.parentElement;
            }
            var attr = btn.attributes;
            app.select_option = parseInt(attr["notebookid"].value);
            var id = parseInt(attr["noteid"].value)
            axios.post("/Get/Note", { Id : id }).then(function (response) {
                app.activeNote = response.data.Note;
            }).catch(function (error) {
                app.$modal.error({
                    title:"登录异常",
                    content:"登录异常,请稍后再试",
                    onOk:function (e) {
                        app.$modal.remove()
                        window.location.href="./Login";
                    },
                    onCancel:function (e) {
                        app.$modal.remove()
                        window.location.href="./Login"
                    }
                })
            })
        }, //ok
        deleteActiveNote:function (e) {
            app.button_isloading = true;
            if(app.activeNote.Id != 0){
                axios.post("/Delete/Note",{Id:app.activeNote.Id}).then(function (response) {
                    if(response.data == 1){
                        for(var k = 0; k < app.noteList.length; k++){
                            if(app.noteList[k].Id == app.activeNote.Id){
                                app.noteList.splice(k,1)
                                break
                            }
                        }
                        app.$Message.success({
                            content : "删除成功"
                        })
                        app.button_isloading = false;
                        if(app.noteList != []){
                            var k = 0;
                            while (app.noteList[k] == undefined){
                                k++;
                            }
                            app.activeNote = app.noteList[k];
                            app.select_option = app.noteList[k].NoteBookId;
                        }
                    }else {
                        app.$modal.error({
                            title:"删除失败",
                            content:"登录异常,请稍后再试",
                            onOk:function (e) {
                                app.$modal.remove()
                                window.location.href="./Login";
                            },
                            onCancel:function (e) {
                                app.$modal.remove()
                                window.location.href="./Login"
                            }
                        })
                    }
                }).catch(function (error) {
                    app.$modal.error({
                        title:"删除失败",
                        content:"服务器异常,请稍后再试",
                        onOk:function (e) {
                            app.$modal.remove()
                            window.location.href="./Login";
                        },
                        onCancel:function (e) {
                            app.$modal.remove()
                            window.location.href="./Login"
                        }
                    })
                })
            }
        },//ok
        deleteNoteBook:function (e) {
            app.button_isloading = true;
            var bookId = e.target.attributes["bookid"].value;
            if(bookId != undefined && bookId != 0){
                axios.post("/Delete/NoteBook",{Id:bookId}).then(function (response) {
                    if(response.data == 1){
                        for(var k = 0; k < app.default_bookList.length; k++){
                            if(app.default_bookList[k].Id == bookId){
                                app.default_bookList.splice(k,1)
                                break
                            }
                        }
                        for(var k = 0; k < app.bookList.length; k++){
                            if(app.bookList[k].Id == bookId){
                                app.bookList.splice(k,1)
                                break
                            }
                        }
                        for(var k = 0; k < app.groupList.length; k++){
                            for(var j; j < app.groupList[k].NoteBookList.length; j++){
                                if(app.groupList[k].NoteBookList[j].Id == bookId){
                                    app.groupList[k].NoteBookList.splice(k,1)
                                    break
                                }
                            }
                        }
                        app.$Message.success({
                            content : "删除成功"
                        })
                        app.button_isloading = false;
                        app.section_book_isshow = false;
                        app.select("book");
                    }else {
                        app.$modal.error({
                            title:"删除失败",
                            content:"登录异常,请稍后再试",
                            onOk:function (e) {
                                app.$modal.remove()
                                window.location.href="./Login";
                            },
                            onCancel:function (e) {
                                app.$modal.remove()
                                window.location.href="./Login"
                            }
                        })
                    }
                }).catch(function (error) {
                    app.$modal.error({
                        title:"删除失败",
                        content:"服务器异常,请稍后再试",
                        onOk:function (e) {
                            app.$modal.remove()
                            window.location.href="./Login";
                        },
                        onCancel:function (e) {
                            app.$modal.remove()
                            window.location.href="./Login"
                        }
                    })
                })
            }
        }, //ok
        deleteGroup:function (e) {
            app.button_isloading = true;
            var groupId = e.target.attributes["groupid"].value;
            if(groupId != undefined && groupId != 0){
                axios.post("/Delete/Group",{Id:groupId}).then(function (response) {
                    if(response.data == 1){
                        for(var k = 0; k < app.groupList.length; k++){
                            if(app.groupList[k].Id == groupId){
                                for(var j; j < app.groupList[k].NoteBookList; j++){
                                    app.default_bookList.push(app.groupList[k].NoteBookList[j])
                                }
                                app.groupList.splice(k,1)
                                break
                            }
                        }
                        app.$Message.success({
                            content : "删除成功"
                        })
                        app.button_isloading = false;
                        app.section_book_isshow = false;
                        app.select("book");
                    }else {
                        app.$modal.error({
                            title:"删除失败",
                            content:"登录异常,请稍后再试",
                            onOk:function (e) {
                                window.location.href="./Login";
                            },
                            onCancel:function (e) {
                                window.location.href="./Login"
                            }
                        })
                    }
                }).catch(function (error) {
                    app.$modal.error({
                        title:"删除失败",
                        content:"服务器异常,请稍后再试",
                        onOk:function (e) {
                            window.location.href="./Login";
                        },
                        onCancel:function (e) {
                            window.location.href="./Login"
                        }
                    })
                })
            }
        }, //ok
    }
})


document.ready = function (callback) {
    ///兼容FF,Google
    if (document.addEventListener) {
        document.addEventListener('DOMContentLoaded', function () {
            document.removeEventListener('DOMContentLoaded', arguments.callee, false);
            callback();
        }, false)
    }
    //兼容IE
    else if (document.attachEvent) {
        document.attachEvent('onreadystatechange', function () {
            if (document.readyState == "complete") {
                document.detachEvent("onreadystatechange", arguments.callee);
                callback();
            }
        })
    }
    else if (document.lastChild == document.body) {
        callback();
    }
}

document.ready(function () {
    renovateNote();
    app.note_width = document.body.scrollWidth - 500;
})

window.onload = function () {
    renovateGroup();
    window.onresize = function(){
        app.note_width = document.body.scrollWidth - 500;
    }
}

function renovateNote() {
    axios.post("/Get/Note").then(function (response) {
        app.noteList = response.data.NoteList;
        if(response.data.NoteList != null && response.data.NoteList != [])
        {
            app.activeNote = response.data.NoteList[0];
            app.select_option = response.data.NoteList[0].NoteBookId;
        }
    }).catch(function (e) {
        alert("服务器异常!");
    })
}

function renovateGroup() {
    axios.post("/Get/NoteBook").then(function (response) {
        app.default_bookList = response.data.NoteBookList;
    }).catch(function () {
        alert("服务器异常!");
    })
    axios.post("/Get/AllNoteBook").then(function (response) {
        app.bookList = response.data.NoteBookList;
    }).catch(function () {
        alert("服务器异常!");
    })
    axios.post("/Get/BookGroup").then(function (response) {
        app.groupList = response.data.BookGroupList;
    }).catch(function () {
        alert("服务器异常!");
    })
}


