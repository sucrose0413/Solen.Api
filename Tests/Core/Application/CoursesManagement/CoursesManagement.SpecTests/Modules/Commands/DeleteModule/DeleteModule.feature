@CoursesManagement @DeleteModule
Feature: Deletion of a module
  In order to delete a module,
  As a Instructor,
  I want to be able to perform such an operation

  Background:
    Given I'm an authenticated instructor within the organization


  Scenario: The module Id should be valid
    When I delete an invalid module Id
    Then I should get a not found error

  Scenario: The module course should not be published while deleting the module
    When I delete a module while the course is published
    Then I should get a bad request error

  Scenario: Deletion of a valid module should be successful
    Given A module belonging to a draft course
    When I delete the module
    Then The deleted module Id should be returned
    And The module should be deleted