@CoursesManagement @DeleteCourse
Feature: Deletion of a training course
  In order to delete a training course,
  As a Instructor,
  I want to be able to perform such an operation

  Background:
    Given I'm an authenticated instructor within the organization


  Scenario: The course Id should be valid
    When I delete an invalid course Id
    Then I should get a not found error

  Scenario: The course should be belonging to the same organization as the instructor
    When I delete a course belonging to an other organization
    Then I should get a not found error

  Scenario: Deletion of a valid course should be successful
    Given A course belonging to my organization
    When I delete the course
    Then The deleted course Id should be returned
    And The course should be deleted