function ajax(url, options) {
	const urlBase = 'https://domainname.ngrok.io/';

	return new Promise(
		function (resolve, reject) {
			$.ajax(urlBase + url, options).done(resolve).fail(reject);
		});
}

/* == ENTITY REPOSITORY == */

const userRepository = new EntityRepository('users');
const questionRepository = new EntityRepository('questions');
const questionReplyRepository = new EntityRepository('questionReplies');

function EntityRepository(route) {
	this.route = route;
}

EntityRepository.prototype.get = function (entityKey) {
	var ajaxOptions = {
		type: 'GET'
	};

	return ajax('api/' + this.route + '/' + entityKey, ajaxOptions);
}

EntityRepository.prototype.getAll = function () {
	var ajaxOptions = {
		type: 'GET'
	};

	return ajax('api/' + this.route, ajaxOptions);
}

EntityRepository.prototype.create = function (entity) {
	var ajaxOptions = {
		type: 'POST',
		data: entity
	};

	return ajax('api/' + this.route, ajaxOptions);
}

EntityRepository.prototype.update = function (entityKey, entity) {
	var ajaxOptions = {
		type: 'PUT',
		data: entity
	};

	return ajax('api/' + this.route + '/' + entityKey, ajaxOptions);
}

EntityRepository.prototype.delete = function (entityKey) {
	var ajaxOptions = {
		type: 'DELETE'
	};

	return ajax('api/' + entityKey, ajaxOptions);
}
