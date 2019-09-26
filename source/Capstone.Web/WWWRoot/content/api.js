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

function signIn(value) {
    var ajaxOptions = {
        type: 'POST',
        data: value
    };

    return ajax('api/users/signin', ajaxOptions);
}

function signUp(value) {
    var ajaxOptions = {
        type: 'POST',
        data: value
    };

    return ajax('api/users/signup', ajaxOptions);
}

function changePassword(value) {
    var ajaxOptions = {
        type: 'POST',
        data: value
    };

    return ajax('api/users/updatePassword', ajaxOptions);
}

function postQuestion(value) {
	var ajaxOptions = {
		type: 'POST',
		data: value
	};

	return ajax('api/questions/post', ajaxOptions);
}

function postQuestionReply(questionKey, value) {
	var ajaxOptions = {
		type: 'POST',
		data: value
	}

	return ajax('api/questions/' + questionKey + '/post', ajaxOptions);
}

function postFeedback(value) {
	var ajaxOptions = {
		type: 'POST',
		data: {
			content: value
		}
	};

	return ajax('api/feedback/post', ajaxOptions);
}
