var myBody = new Vue({
	el:"#myBody",
	data:{
	    Email:"",
        Password:"",
        header_menu: false,
		timer:""
	},
	methods:{
		MoveToEnd:function(e){
			this.header_menu = false;
            timer=setInterval("runToBottom()",5);
        },
        Register:function (e) {
            if(checkEmail(this.Email,this.Password)){
                var User = { "Email" : this.Email, "Password" : this.Password }
                axios.post('/Register', User).then(function (response) {
                    if(response.data == 1){
                        showMessage("已经验证信息下发至您的邮箱,请注意查收...");
                    }else if(response.data == 2){
                        showMessage("此邮箱已被注册,请换一个试试...")
                    }else {
                        showMessage("服务器异常，请稍后再试！")
                    }
                }).catch(function (error) {
                    showMessage("服务器繁忙,请稍后再试!");
                })
            }
            else {
                alert("请输入有效的Email");
            }
        }
	}
})

function runToBottom(){
    h = document.getElementById("register_bottom").scrollHeight
    currentPosition=document.documentElement.scrollTop || document.body.scrollTop;
    currentPosition+=15;
    if(currentPosition<document.body.scrollHeight && (document.documentElement.scrollHeight - document.documentElement.clientHeight - h + 80> currentPosition))
    {
		document.documentElement.scrollTop = currentPosition;
    }
    else {
        clearInterval(timer);
    }
}
function checkEmail(email,password)
{
    var myreg = /^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+(.[a-zA-Z0-9_-])+/;
    if (email == null || !myreg.test(email))
    {
        return false;
    }
    return true;
}
function showMessage(message) {
    alert(message);
}



