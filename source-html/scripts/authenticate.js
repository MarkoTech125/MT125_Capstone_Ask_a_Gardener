// Ensures the user token is appended to every AJAX request.
$.ajaxSetup(
	{
		beforeSend: onBeforeSend
	});

// Appends the user token to an AJAX request.
function onBeforeSend(xhr) {
	var tokenValue = localStorage.getItem('user-token');

	if (tokenValue != null) {
		xhr.setRequestHeader(
			'Authorization',
			'Bearer ' + tokenValue);
	}
}

// Requests a user token using the specified name and password.
function authenticate(name, password) {
	var authenticateResponse =
		function (data, status, xhr) {
			localStorage.setItem('user-token', data);
		};

	var request = {
		'name': name,
		'password': password
	};

	return $.post("https://localhost:5001/api/users/authenticate", request, authenticateResponse);
}
