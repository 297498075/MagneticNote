var content = new Vue({
    el:"#content",
    data:{
        loading:false,
        single:false,
        formValidate: {
            Email: '',
            Password: ''
        },
        ruleValidate: {
            Email: [
                { required: true, message: '邮箱不能为空', trigger: 'blur' },
                { type: 'email', message: '邮箱格式不正确', trigger: 'blur' }
            ],
            Password: [
                { required: true, message: '密码不能为空', trigger: 'blur' },
                { type: 'string', min: 6, message: '密码最少6位', trigger: 'blur' }
            ]
        }
    },
    methods:{
        Login: function (e) {
            this.loading = true;
            this.$refs["formValidate"].validate((valid) => {
                if (valid) {
                    axios.post('../Login', { "Email" : this.formValidate.Email, "Password" : this.formValidate.Password }).then(function (response) {
                        if(response.data.Result == 1){
                            loading=false;
                            window.location.href=response.data.Location;
                        }else{
                            content.loading=false;
                            modal_error.show('邮箱或密码错误!')
                        }
                    }).catch(function (error) {
                        content.loading=false;
                        modal_error.show('服务器繁忙,请稍后再试!')
                    })
                } else {
                    content.loading=false;
                    modal_error.show('表单格式错误!')
                }
            })
        }
    }
})

var modal_error = new Vue({
    el:"#modal_error",
    data:{
        isshow:false,
        modalContent:""
    },
    methods:{
        show: function (p) {
            this.modalContent = p
            this.isshow = true;
        },
        close: function () {
            this.isshow = false;
        }
    }
})