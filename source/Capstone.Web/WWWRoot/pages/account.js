$(document).one('pagebeforeshow', '#account-page',
	function () {
		var key = localStorage.getItem('user-key');

		userRepository.get(key).then(setUser);
	});

function setUser(user) {
	$('#account-name').text(user.name);
	$('#account-role').text(user.role);
}
