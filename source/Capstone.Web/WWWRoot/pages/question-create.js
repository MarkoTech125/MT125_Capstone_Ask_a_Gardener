$(document).one('click', '#question-create-form-submit', onCreateFormSubmit);

function onCreateFormSubmit(e) {
	e.preventDefault();

	var questionDetails = {
		content: $('#question-create-form-content').val(),
		contentExtended: $('#question-create-form-content-extended').val()
	}

	postQuestion(questionDetails)
		.then(function (result) {
			alert('Question posted!');
		})
		.catch(function () {
			alert('Failed to post question.');
		});
}
