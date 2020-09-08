@CoursesManagement @DraftCourse
Feature: Change course status to Draft
  In order to edit a course,
  As a Instructor,
  I want to be able to change the course status to Draft

  Background:
    Given I'm an authenticated instructor within the organization

  Scenario Outline: The course Id is mandatory
    When I edit a course with an empty or a null <Id>
    Then I should get a bad request error

    Examples:
      | Id     |
      | ""     |
      | "null" |

  Scenario: The course Id should be valid
    When I edit an invalid course Id
    Then I should get a not found error

  Scenario: The course should be belonging to the same organization as the instructor
    When I edit a course belonging to an other organization
    Then I should get a not found error

  Scenario: Editing a valid course should be successful
    Given A course belonging to my organization
    When I edit the course
    Then The course status should be Draft