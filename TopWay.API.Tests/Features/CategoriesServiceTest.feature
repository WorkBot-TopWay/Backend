Feature: CategoriesServiceTest
    As a user
    I want to add a new category
    So that I can add a new category

    Background: the Endpoint https://topway-api.azurewebsites.net is available

    @user-adding
    Scenario: Add a new category
        When a user adds a new category
          | name |
          | test |
        Then the response should be a 200 OK
        And the response should be a json
          | Id | Name |
          | 1  | test |
        Then the response with Status 400 is received
        And An Error Message with value "Name is required" is received
        And the response with Status 400 is received