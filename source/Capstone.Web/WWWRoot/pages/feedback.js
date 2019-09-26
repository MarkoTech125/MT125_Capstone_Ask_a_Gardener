$(document).one('click', '#feedback-form-submit', onFeedbackFormSubmit);

function onFeedbackFormSubmit(e) {
	e.preventDefault();

	var content = $('#feedback-form-content').val();
	postFeedback(content)
		.then(function () {

			$.mobile.changePage('/pages/home.html');
		})
		.catch(function () {
			alert('Failed to submit feedback.');
		});
}
