var express = require('express');
var session = require('express-session');
var bodyParser = require('body-parser');
var axios = require('axios');
var fs = require('fs');
var http = require('http');
var https = require('https');
var querystring = require('querystring');

var app = express();

const options = {
    pfx: fs.readFileSync(__dirname+'/ssl/214280259270360.pfx'),
    passphrase: '214280259270360'
};

http.createServer(app).listen(80);
https.createServer(options, app).listen(443);

var requestAddress = "https://note.snkdev.top:453";
var requestKey = "?RequestKey=e07fbbb9fbcdc86589667d8774bf11d5f70d23d1";

app.use(function(req, res, next) {
    if(req.protocol == 'http') {
        var v = "https://" + req.hostname + req.url;
        res.writeHead(302,{"Location":v,"Content-Encoding":"UTF-8"})
        res.end();
    }else {
        next();
    }
});

app.use("/static", express.static("static"));
app.use(session({
    secret: 'keyboard cat',
    resave: false,
    saveUninitialized: true,
    cookie: { secure: true }
}));
app.use(bodyParser.urlencoded({ extended: true }));
app.use(bodyParser.json());

app.get('/', function (req, res) {
    fs.readFile(__dirname + "/static/Index.html", function (err, data) {
        res.writeHead(200,{'Content-Type': 'text/html', "Content-Encoding":"UTF-8"})
        res.write(data)
		res.end()
    })
});
app.get("/favicon.ico", function(req,res){
	fs.readFile(__dirname + "/favicon.ico", function(err, data){
		res.writeHead(200, {'Content-Type': 'image/x-icon'})
		res.write(data)
		res.end()
	})
});
app.get("/Login", function (req,res) {
	if(req.session.User==null)
	{
        fs.readFile(__dirname + "/static/Login.html", function(err, data){
            res.writeHead(200,{'Content-Type': 'text/html', "Content-Encoding":"UTF-8" })
            res.write(data)
            res.end()
        })
	}else{
        res.writeHead(302,{'Location': "https://" + req.hostname + "/Home" })
        res.end()
	}
});
app.post("/Login", function (req,res,next){
    if(typeof(req.body.Email) != undefined && typeof(req.body.Password) != undefined){
        var key = requestKey;
        var email = req.body.Email;
        var password = req.body.Password;
        axios.get(requestAddress + "/User/Get" + key + "&Email=" + email).then(function (response) {
            var user = response.data.User;
            if(user != undefined && user.Password == password){
                req.session.User = user;
                res.json({ Result: 1, "Location" : "Home"});
            }else{
                res.json({ Result : 2});
            }
        }).catch(function (error) {
            res.writeHead(500)
            res.json({ Result : "error"});
        })
    }
    else {
        next()
    }
})
app.post("/Register", function(req,res){
	var email = req.body.Email;
	var password = req.body.Password;
    if(email != undefined && password != undefined){
        axios.post(requestAddress + "/Home/SendRegisterEmail" + requestKey, querystring.stringify({Email:email,Password:password})).then(function (response) {
            if(response.data.Result == true)
			{
                res.end("1");
			}else if(response.data.Result == false){
                res.end("2");
			}else {
			    res.end("3");
            }
        }).catch(function (error) {
            res.writeHead(500)
            res.end()
        })
    }
})
app.use("/Register/Validate",function (req,res,next) {
    if(req.url != "/Validate") {

        axios.post(requestAddress + "/Home/RegisterValidate" + requestKey, querystring.stringify({ValidateKey : req.url.substring(1,17)})).then(function (response) {
            if(response.data.Result == true) {
                res.send("注册成功");
            }else {
                next();
            }
        }).catch(function (error) {
            next();
        })
    }
    else {
        next();
    }
})
app.get("/Home", function (req,res,next) {
	if(req.session.User != null){
        fs.readFile(__dirname + "/static/Content.html", function(err, data){
            res.writeHead(200,{'Content-Type': 'text/html', "Content-Encoding":"UTF-8" })
            res.write(data)
            res.end()
        })
    }
    else {
	    next();
    }
})

app.post("/Add/Note",function (req,res,next) {
    if(req.session.User == null){
        next();
    }else {
        var key = requestKey;
        var userId = req.session.User.Id;
        var title = req.body.Title;
        var content = req.body.Content;
        var noteBookId = req.body.NoteBookId;
        if(title != "" && noteBookId != ""){
            var note = JSON.stringify({Title : title, Content : content, NoteBookId : noteBookId})
            axios.post(requestAddress + "/Note/Add" + key, querystring.stringify({Note : note})).then(function (response) {
                if(response.data.Result == true){
                    res.end("1")
                }else {
                    res.end("2")
                }
            }).catch(function (error) {
                res.writeHead(500)
                res.json({Result: "error"});
            })
        }
        else {
            res.end("2")
        }
    }
})
app.post("/Add/NoteBook",function (req,res,next) {
    if(req.session.User == null){
        next();
    }else {
        var key = requestKey;
        var userId = req.session.User.Id;
        var name = req.body.Name;
        var groupId = req.body.GroupId;
        if(name != ""){
            if(groupId != undefined && groupId != 1 && groupId != 0){
                var notebook = JSON.stringify({ Name : name, BookGroupId : groupId})
                axios.post(requestAddress + "/NoteBook/Add" + key, querystring.stringify({NoteBook : notebook})).then(function (response) {
                    if(response.data.Result == true){
                        res.end("1")
                    }else {
                        res.end("2")
                    }
                }).catch(function (error) {
                    res.writeHead(500)
                    res.json({Result: "error"});
                })
            }else {
                var notebook = JSON.stringify({ Name : name, UserId : userId})
                axios.post(requestAddress + "/NoteBook/Add" + key, querystring.stringify({NoteBook : notebook})).then(function (response) {
                    if(response.data.Result == true){
                        res.end("1")
                    }else {
                        res.end("2")
                    }
                }).catch(function (error) {
                    res.writeHead(500)
                    res.json({Result: "error"});
                })
            }
        }
        else {
            res.end("2")
        }
    }
})
app.post("/Add/Group",function (req,res,next) {
    if(req.session.User == null){
        next();
    }else {
        var key = requestKey;
        var userId = req.session.User.Id;
        var name = req.body.Name;
        if(name != ""){
            var group = JSON.stringify({ Name : name, UserId : userId})
            axios.post(requestAddress + "/BookGroup/Add" + key, querystring.stringify({BookGroup : group})).then(function (response) {
                if(response.data.Result == true){
                    res.end("1")
                }else {
                    res.end("2")
                }
            }).catch(function (error) {
                res.writeHead(500)
                res.json({Result: "error"});
            })
        }
        else {
            res.end("2")
        }
    }
})

app.post("/Delete/Note", function (req,res,next) {
    if(req.session.User == null){
        next();
    }else {
        var key = requestKey;
        var id = req.body.Id;
        if(id != undefined && id != 0){
            axios.post(requestAddress + "/Note/Delete" + key, querystring.stringify({Id:id})).then(function (response) {
                if(response.data.Result == true){
                    res.end("1")
                }else {
                    res.end("2")
                }
            }).catch(function (error) {
                res.writeHead(500)
                res.json({Result: "error"});
            })
        }
        else {
            res.end("2")
        }
    }
})
app.post("/Delete/NoteBook", function (req,res,next) {
    if(req.session.User == null){
        next();
    }else {
        var key = requestKey;
        var id = req.body.Id;
        if(id != undefined && id != 0){
            axios.post(requestAddress + "/NoteBook/Delete" + key, querystring.stringify({Id : id})).then(function (response) {
                if(response.data.Result == true){
                    res.end("1")
                }else {
                    res.end("2")
                }
            }).catch(function (error) {
                res.writeHead(500)
                res.json({Result: "error"});
            })
        }
        else {
            res.end("2")
        }
    }
})
app.post("/Delete/Group", function (req,res,next) {
    if(req.session.User == null){
        next();
    }else {
        var key = requestKey;
        var id = req.body.Id;
        if(id != undefined && id != 0){
            axios.post(requestAddress + "/BookGroup/Delete" + key, querystring.stringify({Id:id})).then(function (response) {
                if(response.data.Result == true){
                    res.end("1")
                }else {
                    res.end("2")
                }
            }).catch(function (error) {
                res.writeHead(500)
                res.json({Result: "error"});
            })
        }
        else {
            res.end("2")
        }
    }
})

app.post("/Update/Note", function (req,res,next) {
    if(req.session.User == null){
        next();
    } else {
        if(req.body.Note != undefined && req.body.Note.Id != 0){
            var key = requestKey;
            var note = JSON.stringify(req.body.Note);
            var nurl = requestAddress + "/Note/Update"+key;
            axios.post(nurl,querystring.stringify({ Note:note})).then(function (response) {
                    if(response.data.Result == true){
                        res.end("1")
                    }else {
                        res.end("2")
                    }
                }).catch(function (error) {
                    res.writeHead(500)
                    res.json({Result: "error"})
                })
        }
    }
})

app.post("/Get/Note", function (req,res,next) {
    if(req.session.User == null){
        next();
    } else {
        var key = requestKey;
        var userId = req.session.User.Id;
        if(req.body.Id != undefined && req.body.Id != ""){
            axios.get(requestAddress + "/Note/Get" + key + "&Id=" + req.body.Id).then(function (response) {
                res.json(response.data)
            }).catch(function (error) {
                res.writeHead(500)
                res.json({Result: "error"});
            })
        }else{
            axios.get(requestAddress + "/Note/Get" + key + "&UserId=" + userId).then(function (response) {
                res.json(response.data)
            }).catch(function (error) {
                res.writeHead(500)
                res.json({Result: "error"});
            })
        }
    }
})
app.post("/Get/NoteByCondition", function (req,res,next) {
    if(req.session.User == null){
        next();
    } else {
        var key = requestKey;
        var userId = req.session.User.Id;
        if(req.body.Condition != undefined && req.body.Condition != "" && req.body.NoteBookId){
            axios.get(requestAddress + "/Note/Get" + key + "&Condition=" + req.body.Condition + "&NoteBookId=" + req.body.NoteBookId).then(function (response) {
                res.json(response.data)
            }).catch(function (error) {
                res.writeHead(500)
                res.json({Result: "error"});
            })
        }else if(req.body.Condition != undefined && req.body.Condition != ""){
            axios.post(requestAddress + "/Note/Get" + key, querystring.stringify({Condition:req.body.Condition})).then(function (response) {
                res.json(response.data)
            }).catch(function (error) {
                res.writeHead(500)
                res.json({Result: "error"});
            })
        }else {
            res.json({NoteList:[]});
        }
    }
})
app.post("/Get/NoteBook", function (req,res,next) {
    if(req.session.User == null){
        next();
    }else {
        var key = requestKey;
        var userId = req.session.User.Id;
        axios.get(requestAddress + "/NoteBook/Get" + key + "&UserId=" + userId).then(function (response) {
            res.json(response.data)
        }).catch(function (error) {
            res.writeHead(500)
            res.json({Result: "error"});
        })
    }
})
app.post("/Get/AllNoteBook", function (req,res,next) {
    if(req.session.User == null){
        next();
    }else {
        var key = requestKey;
        var userId = req.session.User.Id;
        axios.get(requestAddress + "/Home/GetAllNoteBook" + key + "&UserId=" + userId).then(function (response) {
            res.json(response.data)
        }).catch(function (error) {
            res.writeHead(500)
            res.json({Result: "error"});
        })
    }
})
app.post("/Get/BookGroup", function (req,res,next) {
    if(req.session.User == null){
        next();
    }else {
        var key = requestKey;
        var userId = req.session.User.Id;
        axios.get(requestAddress + "/Home/GetBookGroupContainsBook" + key + "&UserId=" + userId).then(function (response) {
            res.json(response.data);
        }).catch(function (error) {
            res.writeHead(500)
            res.json({ Result : "error"});
        })
    }
})

app.use(function (req,res) {
	res.writeHead(404,{ "Content-Encoding":"UTF-8"})
    res.write("没有这个页面")
	res.end()
}) //404
