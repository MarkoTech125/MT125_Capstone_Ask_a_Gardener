$(document).one('click', '#change-password-form-submit', onChangePasswordFormSubmit);

function onChangePasswordFormSubmit(e) {

    if ($('#change-password-form-password').val() !=
        $('#change-password-form-password-repeat').val()) {
        alert('Password and password repeat did not match.');
    }

    var details = {
        password: $('#change-password-form-old-password').val(),
        passwordNew: $('#change-password-form-password').val()
    }

    changePassword(details)
        .then(function (result) {
            $.mobile.changePage('/pages/account.html');
        })
        .catch(function () {
            alert('Failed to change password.');
            $.mobile.changePage('/pages/account.html');
        });
}
