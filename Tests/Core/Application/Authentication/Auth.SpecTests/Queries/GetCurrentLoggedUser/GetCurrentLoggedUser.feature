@Authentication @GetCurrentLoggedUser @TechnicalFeature
Feature: Checking a validity of an authentication token et getting info about its bearer
  In order to check the validity of a authentication token and get info about its bearer,
  The system should be able to validate such a token

  Scenario: The authentication token is mandatory
    When The system is called by an unauthenticated user
    Then The system should return a unauthorised error

  Scenario: The token bearer should be active
    When The token bearer has been blocked
    Then The system should return a locked error

  Scenario: The authentication token is valid and the bearer is an active user
    Given An active user of the system with the following info bearing a valid token
      | User Id | User name   | Learning Path |
      | user123 | Jean Dupont | Developer     |
    When He calls the system using the token
    Then The request should be successful
    And The user info should be returned as
      | User Id | User name   | Learning Path |
      | user123 | Jean Dupont | Developer     |