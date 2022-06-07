Feature: CommentsServiceTest
	As a user
  	I want to be able to comment on a post
  	So that I can share my opinion with others

	Background: 
		Given a user has a post
		And the user has a comment
		When the user deletes the comment
		Then the comment should be deleted