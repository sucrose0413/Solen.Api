@Authentication @ForgotPassword
Feature: Password Forgot
  In order to reset my password,
  As a user of the system,
  I want to be able to submit a password reset request when I forgot my password


  Scenario Outline: The email address should be valid
    When I submit a forgot password request with an invalid <Email>
    Then I should get a bad request error
    Examples:
      | Email          |
      | ""             |
      | "null"         |
      | "invalidEmail" |

  Scenario: The email address should be a known address
    When I submit a forgot password request with an unknown email
    Then I should get a not found error

  Scenario: The user should be active
    Given I am a blocked user
    When I submit a forgot password request
    Then I should get locked error

  Scenario: An active user should be able to submit a forgot password request
    Given I am an active user of the system
    When I submit a forgot password request
    Then The request should be successful
    And A password rest token associated to the user should be created 