// script.js

$("#login-form").on("submit",
	function (e) {
		// We handle submission manually.
		e.preventDefault();

		authenticate(
			$('#userName').val(),
			$('#userPassword').val());
	});
