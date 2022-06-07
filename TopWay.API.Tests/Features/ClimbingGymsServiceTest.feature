Feature: ClimbingGymsServiceTest
A feature is a collection of scenarios.
I want to test the ClimbingGymsService
So that I can see if the service is working

    Background: the Endpoint https://topway-api.azurewebsites.net is available

    @user-adding
    Scenario: Add a new user
        Given a user with the following details
          | username | password |
          | testuser | testpass |
        When I add the user
        Then the user should be added
          | username | password |
          | testuser | testpass |
        Then the response should be a 201
        And the response should be a JSON object
        And the response should contain the following details