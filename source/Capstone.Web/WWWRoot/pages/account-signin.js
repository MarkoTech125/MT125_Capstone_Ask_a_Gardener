$(document).one('click', '#account-signin-form-submit', onSigninFormSubmit);

function onSigninFormSubmit(e) {
	e.preventDefault();

	var signinDetails = {
		name: $('#account-signin-form-name').val(),
		password: $('#account-signin-form-password').val()
	};

	signIn(signinDetails)
		.then(function (result) {
			localStorage['user-key'] = result.userKey;
			localStorage['user-token'] = result.userToken;

			$.mobile.changePage('/pages/home.html');
		})
		.catch(function () {
			alert("Failed to authenticate, please try again.");
		});
}
