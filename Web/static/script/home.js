Vue.use(VueHtml5Editor,{
    // 全局组件名称，使用new VueHtml5Editor(options)时该选项无效
    // global component name
    name: "vue-html5-editor",
    // 是否显示模块名称，开启的话会在工具栏的图标后台直接显示名称
    // if set true,will append module name to toolbar after icon
    showModuleName: false,
    // 自定义各个图标的class，默认使用的是font-awesome提供的图标
    // custom icon class of built-in modules,default using font-awesome
    icons: {
        text: "fa fa-pencil",
        color: "fa fa-paint-brush",
        font: "fa fa-font",
        align: "fa fa-align-justify",
        list: "fa fa-list",
        link: "fa fa-chain",
        unlink: "fa fa-chain-broken",
        tabulation: "fa fa-table",
        image: "fa fa-file-image-o",
        hr: "fa fa-minus",
        eraser: "fa fa-eraser",
        undo: "fa-undo fa",
        "full-screen": "fa fa-arrows-alt",
        info: "fa fa-info",
    },
    // 配置图片模块
    // config image module
    image: {
        // 文件最大体积，单位字节  max file size
        sizeLimit: 512 * 1024,
        // 上传参数,默认把图片转为base64而不上传
        // upload config,default null and convert image to base64
        upload: {
            url: null,
            headers: {},
            params: {},
            fieldName: {}
        },
        // 压缩参数,默认使用localResizeIMG进行压缩,设置为null禁止压缩
        // compression config,default resize image by localResizeIMG (https://github.com/think2011/localResizeIMG)
        // set null to disable compression
        compress: {
            width: 1600,
            height: 1600,
            quality: 80
        },
        // 响应数据处理,最终返回图片链接
        // handle response data，return image url
        uploadHandler(responseText){
            //default accept json data like  {ok:false,msg:"unexpected"} or {ok:true,data:"image url"}
            var json = JSON.parse(responseText)
            if (!json.ok) {
                alert(json.msg)
            } else {
                return json.data
            }
        }
    },
    // 语言，内建的有英文（en-us）和中文（zh-cn）
    //default en-us, en-us and zh-cn are built-in
    language: "zh-cn",
    // 自定义语言
    i18n: {
        //specify your language here
        "zh-cn": {
            "align": "对齐方式",
            "image": "图片",
            "list": "列表",
            "link": "链接",
            "unlink": "去除链接",
            "table": "表格",
            "font": "文字",
            "full screen": "全屏",
            "text": "排版",
            "eraser": "格式清除",
            "info": "关于",
            "color": "颜色",
            "please enter a url": "请输入地址",
            "create link": "创建链接",
            "bold": "加粗",
            "italic": "倾斜",
            "underline": "下划线",
            "strike through": "删除线",
            "subscript": "上标",
            "superscript": "下标",
            "heading": "标题",
            "font name": "字体",
            "font size": "文字大小",
            "left justify": "左对齐",
            "center justify": "居中",
            "right justify": "右对齐",
            "ordered list": "有序列表",
            "unordered list": "无序列表",
            "fore color": "前景色",
            "background color": "背景色",
            "row count": "行数",
            "column count": "列数",
            "save": "确定",
            "upload": "上传",
            "progress": "进度",
            "unknown": "未知",
            "please wait": "请稍等",
            "error": "错误",
            "abort": "中断",
            "reset": "重置"
        }
    },
    // 隐藏不想要显示出来的模块
    // the modules you don't want
    hiddenModules: [],
    // 自定义要显示的模块，并控制顺序
    // keep only the modules you want and customize the order.
    // can be used with hiddenModules together
    visibleModules: [
        "text",
        "color",
        "font",
        "align",
        "list",
        "link",
        "unlink",
        "tabulation",
        "image",
        "hr",
        "eraser",
        "undo",
        "full-screen",
        "info",
    ],
    // 扩展模块，具体可以参考examples或查看源码
    // extended modules
    modules: {
        //omit,reference to source code of build-in modules
    }
})
var app = new Vue({
    el:"#app",
    data: {
        nav_isshow:true,
        section_screen_isshow:false,
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
        addNoteBook_Name:"",
        addGroup_Name:"",
        select_option:1,
        select_group_option:1,
        select_placeholder:"请选择笔记本",
        search_content:"",
        add_note_content:"",
        noteList:[],
        default_bookList:[],
        bookList:[],
        groupList:[],
        activeNote:{
            Id:0,
            Title:"",
            Content:"",
            CreateDate:"",
            NoteBookId:0,
            editorOption:{}
        }
    },
    methods: {
        select:function (name) {
            if(name == "add") {
                app.nav_isshow=false
                app.section_screen_isshow=false;
                app.section_note_isshow=false;
                app.section_search_isshow=false;
                app.section_book_isshow=false;
                app.section_create_isshow=true;
            }else if(name == "search"){
                app.section_create_isshow=false;
                app.section_book_isshow=false;
                app.nav_isshow=true;
                app.section_search_isshow=true;
                app.activeNote = {
                    Id:0,
                    Title:"",
                    Content:"",
                    CreateDate:"",
                    NoteBookId:0
                }
                if(app.noteList != undefined && app.noteList.length > 0){
                    var k = 0;
                    while (app.noteList[k] == undefined){
                        k++;
                    }
                    app.activeNote = app.noteList[k];
                    app.select_option = app.noteList[k].NoteBookId;
                }
                app.section_screen_isshow=true;
            }else if(name == "note"){
                app.section_create_isshow=false;
                app.section_search_isshow=false;
                app.section_screen_isshow=false;
                app.section_book_isshow=false;
                app.nav_isshow=true;
                app.section_note_isshow=true;
                app.activeNote = {
                    Id:0,
                    Title:"",
                    Content:"",
                    CreateDate:"",
                    NoteBookId:0
                }
                if(app.noteList != undefined && app.noteList.length > 0){
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
                app.nav_isshow=true;
                app.section_book_isshow=true;
                app.section_screen_isshow=true;
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
                app.section_create_isshow=false;
                app.section_search_isshow=false;
                app.section_book_isshow=false;
                app.section_screen_isshow=false
                app.nav_isshow=true;
                app.section_note_isshow=true;
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
        activeChange: function (data) {
            subContent(data);
        },
        addNoteContent: function (data) {
            app.add_note_content = data;
        }
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
        if(response.data.NoteList != null && response.data.NoteList.length > 0)
        {
            app.noteList = response.data.NoteList;
            app.activeNote = response.data.NoteList[0];
            app.select_option = response.data.NoteList[0].NoteBookId;
        }
    }).catch(function (e) {
        alert("服务器异常!");
    })
}

function renovateGroup() {
    app.bookList = [];
    axios.post("/Get/NoteBook").then(function (response) {
        app.default_bookList = response.data.NoteBookList;
        if(app.default_bookList != undefined && app.default_bookList.length > 0){
            for(i in app.default_bookList){
                var book = app.default_bookList[i]
                app.bookList.push(book)
            }
        }
    }).catch(function () {
        alert("服务器异常!");
    })
    axios.post("/Get/BookGroup").then(function (response) {
        app.groupList = response.data.BookGroupList;
        for(i in app.groupList){
            if(app.groupList[i].NoteBookList != undefined && app.groupList[i].NoteBookList.length > 0){
                for(j in app.groupList[i].NoteBookList){
                    var book = app.groupList[i].NoteBookList[j]
                    app.bookList.push(book)
                }
            }
        }
    }).catch(function () {
        alert("服务器异常!");
    })
}

function subContent(data) {
    var indexof1 = data.indexOf('>')
    if(indexof1 == -1){
        if(data.length > 10){
            app.activeNote.Content = data.substr(0,10) + "...";
        }
        else {
            app.activeNote.Content = data
        }
    }else {
        var indexof2 = data.indexOf('<')
        var str = data.substring(indexof1,indexof2)
        if(str.length > 10){
            app.activeNote.Content = str.substr(0,10) + "...";
        }else {
            app.activeNote.Content = str;
        }
    }
}


