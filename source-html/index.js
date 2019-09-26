$(document).on('pagebeforeshow', '#question-page',
	function () {
		questionRepository.getAll().then(
			function (questions) {
				$('#question-page-content-listview').empty();

				for (var i = 0; i < questions.length; i++) {
					$('#question-page-content-listview').append(
						'<li class=\"ul-li-static ui-body-inherit\">' +
						'	<a href=\"#\" class=\"ui-btn ui-btn-icon-right ui-icon-carat-r\" onclick=\"showQuestionDetails(' + questions[i].key + ');\">' +
						'		<h3>' + questions[i].content + '</h3>' +
						'		<p>' + questions[i].contentExtended + '</p>' +
						'	</a>' +
						'</li>');
				}

				if (questions.length == 0) {
					$('#question-page-content-listview').append(
						'<p>No questions found.</p>');
				}
			});
	});

$(document).on('pagebeforeshow', '#question-detail-page',
	function () {
		$
	});

function showQuestionDetails(key) {
	questionRepository.get(i).then(
		function (question) {
			$('#question-detail-content')
				.val(question.content);
			$('#question-detail-content-extended')
				.val(question.contentExtended);
		});

	$.mobile.changePage('#question-detail-page');
}
