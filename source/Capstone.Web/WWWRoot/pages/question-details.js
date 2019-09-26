$('#question-details-form-submit').off('click').on('click', onPostAnswerSubmit);

$(document).one('pagebeforeshow', '#question-details-page',
	function () {
		var key = localStorage['question-key'];

		questionRepository.get(key)
			.then(function (question) {
				prepareQuestion(question);

				questionReplyRepository.getAll()
					.then(function (questionReplies) {
						$('#question-details-page-content-listview').empty();

						for (var i = 0; i < questionReplies.length; i++) {

							if (questionReplies[i].questionKey == key) {
								appendReply(questionReplies[i]);
							}
						}

						if (questionReplies.length == 0) {
							$('#question-page-content-listview').append(
								'<p>No replies yet.</p>');
						}
					})
					.catch(function () {
						alert("Failed to load question details.");
					});

			})
			.catch(function () {
				alert("Failed to load question details.");
			});
	});

function prepareQuestion(question) {
	userRepository.get(question.authorKey)
		.then(function (questionAuthor) {
			$('#question-details-content-header').html(
				'<strong>' + questionAuthor.name + '</strong> (' + question.timestamp + ')');

			$('#question-details-title').text(question.content);
			$('#question-details-content').text(question.contentExtended);
		});
}

function appendReply(reply) {
	userRepository.get(reply.authorKey)
		.then(function (replyAuthor) {
			$('#question-details-page-content-listview').append(
				'<div>' +
				'	<p><strong>' + replyAuthor.name + '</strong> (' + reply.timestamp + ')</p>' +
				'	<p>' + reply.content + '</p>' + 
				'</div>');
		});
}

function onPostAnswerSubmit(e) {
	e.preventDefault();

	var replyDetails = {
		content: $('#question-details-form-content').val(),
	}

	postQuestionReply(localStorage['question-key'], replyDetails)
		.then(function (result) {
			alert('Question reply posted!');
		})
		.catch(function () {
			alert('Failed to post question reply.');
		});
}
