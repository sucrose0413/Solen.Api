@CoursesManagement @CreateModule
Feature: Creation of a module
  In order to add some content to a training course,
  As a Instructor,
  I want to be able to create modules

  Background:
    Given I'm an authenticated instructor within the organization

  Scenario Outline: The module name is mandatory
    When I create a module with an empty or a null <Name>
    Then I should get a bad request error

    Examples:
      | Name   |
      | ""     |
      | "null" |

  Scenario: The module name should be less than or equal to 100 characters
    When I create a module with a name length over 100 characters
    Then I should get a bad request error

  Scenario Outline: The module course is mandatory
    When I create a module with an empty or a null course Id <CourseId>
    Then I should get a bad request error

    Examples:
      | CourseId |
      | ""       |
      | "null"   |

  Scenario: The course Id should be valid
    When I create a module with an invalid course Id
    Then I should get a not found error

  Scenario: The module course should not be published while creating the module
    When I create a module while the course is published
    Then I should get a bad request error

  Scenario: Creation of a module with valid properties should be successful
    When I create a module with valid properties as
      | Name         | Course Id | Order |
      | module title | courseId  | 1     |
    Then The module Id should be returned
    And The module should be created