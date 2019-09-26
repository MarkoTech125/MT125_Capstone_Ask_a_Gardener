/* == ENTITY == */

// Creates a new entity.
function Entity(key) {
	this.key = key;
}

/* == ENTITY (QUESTION) == */

function QuestionEntity(key, content, contentExtended, replies) {
	Entity.call(key);

	this.content = content;
	this.contentExtended = contentExtended;
	this.replies = replies;
}

QuestionEntity.prototype = Object.create(Entity.prototype);

/* == ENTITY (QUESTION REPLY) == */

function QuestionReplyEntity(key, content) {
	Entity.call(key);

	this.content = content;
}

QuestionReplyEntity.prototype = Object.create(Entity.prototype);
