@CoursesManagement @UpdateModule
Feature: Updating a module
  In order to update the info about a module,
  As a Instructor,
  I want to be able to perform such an operation

  Background:
    Given I'm an authenticated instructor within the organization

  Scenario Outline: The module Id is mandatory
    When I update a module with an empty or a null Id <ModuleId>
    Then I should get a bad request error

    Examples:
      | ModuleId |
      | ""       |
      | "null"   |

  Scenario Outline: The module name is mandatory
    When I update a module with an empty or a null name <Name>
    Then I should get a bad request error

    Examples:
      | Name   |
      | ""     |
      | "null" |

  Scenario: The module name should be less than or equal to 100 characters
    When I update a module with a name length over 100 characters
    Then I should get a bad request error

  Scenario: The module Id should be valid
    When I update an invalid module Id
    Then I should get a not found error

  Scenario: The module course should not be published while updating the module
    When I update a module while the course is published
    Then I should get a bad request error

  Scenario: Updating a valid module with valid data should be successful
    Given A module belonging to a draft course with
      | Module Id | Module Name |
      | moduleId  | old name    |
    When I update the module with
      | Module Id | Module Name |
      | moduleId  | new name    |
    Then The module should be successfully updated with the new data