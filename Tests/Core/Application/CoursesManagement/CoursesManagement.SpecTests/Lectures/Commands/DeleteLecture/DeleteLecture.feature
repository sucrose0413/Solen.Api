@CoursesManagement @DeleteLecture
Feature: Deletion of a lecture
  In order to delete a lecture,
  As a Instructor,
  I want to be able to perform such an operation

  Background:
    Given I'm an authenticated instructor within the organization


  Scenario: The lecture Id should be valid
    When I delete an invalid lecture Id
    Then I should get a not found error

  Scenario: The lecture course should not be published while deleting the lecture
    When I delete a lecture while the course is published
    Then I should get a bad request error

  Scenario: Deletion of a valid lecture should be successful
    Given A lecture belonging to a draft course
    When I delete the lecture
    Then The deleted lecture Id should be returned
    And The lecture should be deleted