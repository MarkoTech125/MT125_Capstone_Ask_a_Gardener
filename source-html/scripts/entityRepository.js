function ajax(url, options) {
	return new Promise(
		function (resolve, reject) {
			$.ajax(url, options).done(resolve).fail(reject);
		});
}

/* == ENTITY REPOSITORY == */

const questionRepository = new EntityRepository('questions');
const questionReplyRepository = new EntityRepository('questionReplies');

function EntityRepository(route) {
	this.route = route;
}

EntityRepository.prototype.get = function (entityKey) {
	var ajaxOptions = {
		type: 'GET'
	};

	return ajax('https://localhost:5001/api/' + this.route + '/' + entityKey, ajaxOptions);
}

EntityRepository.prototype.getAll = function () {
	var ajaxOptions = {
		type: 'GET'
	};

	return ajax('https://localhost:5001/api/' + this.route, ajaxOptions);
}

EntityRepository.prototype.create = function (entity) {
	var ajaxOptions = {
		type: 'POST',
		data: entity
	};

	return ajax('https://localhost:5001/api/' + this.route, ajaxOptions);
}

EntityRepository.prototype.update = function (entityKey, entity) {
	var ajaxOptions = {
		type: 'PUT',
		data: entity
	};

	return ajax('https://localhost:5001/api/' + this.route + '/' + entityKey, ajaxOptions);
}

EntityRepository.prototype.delete = function (entityKey) {
	var ajaxOptions = {
		type: 'DELETE'
	};

	return ajax('https://localhost:5001/api/' + entityKey, ajaxOptions);
}
