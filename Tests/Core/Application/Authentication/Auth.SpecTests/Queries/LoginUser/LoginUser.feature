@Authentication @LoginUser
Feature: User Login
  In order to log in,
  As a user of the system,
  I want to be able to enter my credentials
  

  Scenario Outline: The Email address is mandatory
    When I enter an empty or a null <Email> address
    Then I should get bad request error

    Examples:
      | Email  |
      | ""     |
      | "null" |

  Scenario: The Email address should be valid
    When I enter an invalid Email address like 'invalidemail.com'
    Then I should get bad request error

  Scenario Outline: The password is mandatory
    When I enter an empty or a null <Password>
    Then I should get bad request error

    Examples:
      | Password |
      | ""       |
      | "null"   |


  Scenario: The credentials should be valid
    When I enter a bad Email address or a bad password
    Then I should get unauthorised error
    
  Scenario: The user should be active
    Given I'm a blocked user
    When I enter my valid credentials
    Then I should get locked error

  Scenario: Login as an active user and with valid credentials should be successful
    Given I'm an active user with
      | OrganizationId | User Id | User name   | Email          | Learning Path | LearningPathId | Roles               |
      | org123         | usr123  | Jean Dupont | user@email.com | Developer     | path123        | Instructor, Learner |
    When I enter my valid credentials
    Then I should get logged in successfully
    And My info should be returned as
      | User Id | User name   | Learning Path |
      | usr123  | Jean Dupont | Developer     |
    And The following claims should be set correctly in the authentication Token
      | nameid | unique_name | email          | organizationId | learningPathId | role                  |
      | usr123 | Jean Dupont | user@email.com | org123         | path123        | [Learner, Instructor] |
    And A refresh token should be set and returned as well
