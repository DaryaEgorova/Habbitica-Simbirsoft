function showLoginForm() {
$('#loginModal .registerBox').fadeOut('fast', function () {
            $('.loginBox').fadeIn('fast');
            $('.register-footer').fadeOut('fast', function () {
                $('.login-footer').fadeIn('fast');
            });
            $('.modal-title').html('Login');
        });
        $('.error').removeClass('alert alert-danger').html('');
        setTimeout(function () {
            $('#loginModal').modal({ backdrop: 'static', keyboard: false });
            $('#loginModal').modal('show');
        }, 230);
    });
