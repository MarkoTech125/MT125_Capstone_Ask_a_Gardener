$(document).one('click', '#account-signup-form-submit', onSignupFormSubmit);

function onSignupFormSubmit(e) {
	e.preventDefault();

	if ($('#account-signup-form-password').val() !=
		$('#account-signup-form-password-repeat').val()) {

		alert('Passwords do not match.');
		return;
	}

	var signupDetails = {
		name: $('#account-signup-form-name').val(),
		password: $('#account-signup-form-password').val()
	};

	signUp(signupDetails)
		.then(function (result) {
			$.mobile.changePage('/pages/account-signin.html');
		})
		.catch(function () {
			alert("Failed to create an account.");
		});
}
