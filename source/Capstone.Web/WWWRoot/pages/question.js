$(document).one('pagebeforeshow', '#question-page',
	function () {
		questionRepository.getAll()
			.then(function (questions) {
				$('#question-page-content-listview').empty();

				for (var i = 0; i < questions.length; i++) {
					$('#question-page-content-listview').append(
						'<li class=\"ul-li-static ui-body-inherit\">' +
						'	<a href=\"#\" class=\"ui-btn ui-btn-icon-right ui-icon-carat-r\" onclick=\"showQuestionDetails(' + questions[i].key + ');\">' +
						'		<h3>' + questions[i].content + '</h3>' +
						'	</a>' +
						'</li>');
				}

				if (questions.length == 0) {
					$('#question-page-content-listview').append(
						'<p>No questions found.</p>');
				}
			})
			.catch(function () {
				alert('Failed to load questions.');
			});
	});

function showQuestionDetails(key) {
	localStorage['question-key'] = key;
	$.mobile.changePage('/pages/question-details.html');
}
