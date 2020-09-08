@CoursesManagement @UpdateLecturesOrders
Feature: Updating the orders of a module lectures
  In order to reorganize a module content,
  As a Instructor,
  I want to be able to order the modules lectures

  Background:
    Given I'm an authenticated instructor within the organization

  Scenario Outline: The module Id is mandatory
    When I update a module lectures with an empty or a null Id <ModuleId>
    Then I should get a bad request error

    Examples:
      | ModuleId |
      | ""       |
      | "null"   |

  Scenario: The module lectures orders are mandatory
    When I update a module lectures without specifying lectures orders
    Then I should get a bad request error

  Scenario: The module Id should be valid
    When I update a module lectures with an invalid module Id
    Then I should get a not found error

  Scenario: The module course should not be published while updating the lectures orders
    When I update lectures orders of a published course
    Then I should get a bad request error

  Scenario: Updating a valid module lectures orders with valid properties should be successful
    Given A draft course and a module having the following lectures orders
      | Lecture Id | Order |
      | lectureId1 | 1     |
      | lectureId2 | 2     |
    When I update the lectures orders as
      | Module Id  | Order |
      | lectureId1 | 2     |
      | lectureId2 | 1     |
    Then The lectures orders should be successfully updated with the new orders 