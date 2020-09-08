@CoursesManagement @UpdateModulesOrders
Feature: Updating the orders of a training course modules
  In order to reorganize a training course content,
  As a Instructor,
  I want to be able to order the course modules

  Background:
    Given I'm an authenticated instructor within the organization

  Scenario Outline: The course Id is mandatory
    When I update a course modules with an empty or a null Id <CourseId>
    Then I should get a bad request error

    Examples:
      | CourseId |
      | ""       |
      | "null"   |

  Scenario: The course modules orders are mandatory
    When I update a course modules without specifying modules orders
    Then I should get a bad request error

  Scenario: The course Id should be valid
    When I update a course modules with an invalid course Id
    Then I should get a not found error

  Scenario: The course should be belonging to the same organization as the instructor
    When I update a course belonging to an other organization
    Then I should get a not found error

  Scenario: The course should not be published while updating its modules orders
    When I update modules orders of a published course
    Then I should get a bad request error

  Scenario: Updating a valid course modules orders with valid properties should be successful
    Given A draft course having the following modules orders
      | Module Id | Order |
      | moduleId1 | 1     |
      | moduleId2 | 2     |
    When I update the modules orders as
      | Module Id | Order |
      | moduleId1 | 2     |
      | moduleId2 | 1     |
    Then The modules orders should be successfully updated with the new orders 