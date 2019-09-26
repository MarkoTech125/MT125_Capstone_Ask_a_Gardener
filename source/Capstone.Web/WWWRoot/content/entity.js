/* == ENTITY == */

// Creates a new entity.
function Entity(key) {
	this.key = key;
}

/* == ENTITY (USER) == */
function UserEntity(key, name, role) {
	Entity.call(key);

	this.name = name;
	this.role = role;
}

UserEntity.prototype = Object.create(Entity.prototype);

/* == ENTITY (QUESTION) == */

function QuestionEntity(key, content, contentExtended, timestamp, authorKey) {
	Entity.call(key);

	this.content = content;
	this.contentExtended = contentExtended;
	this.timestamp = timestamp;
	this.authorKey = authorKey;
}

QuestionEntity.prototype = Object.create(Entity.prototype);

/* == ENTITY (QUESTION REPLY) == */

function QuestionReplyEntity(key, questionKey, content, timestamp, authorKey) {
	Entity.call(key);

	this.questionKey = questionKey;
    this.content = content;
	this.timestamp = timestamp;
	this.authorKey = authorKey;
}

QuestionReplyEntity.prototype = Object.create(Entity.prototype);
