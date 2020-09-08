@CoursesManagement @UnpublishCourse
Feature: Unpublishing a training course
  In order to edit a training course or to prevent the learners to access it,
  As a Instructor,
  I want to be able to unpublish the course

  Background:
    Given I'm an authenticated instructor within the organization

  Scenario Outline: The course Id is mandatory
    When I unpublish a course with an empty or a null <CourseId>
    Then I should get a bad request error

    Examples:
      | CourseId |
      | ""       |
      | "null"   |

  Scenario: The course Id should be valid
    When I unpublish an invalid course Id
    Then I should get a not found error

  Scenario: The course should be belonging to the same organization as the instructor
    When I unpublish a course belonging to an other organization
    Then I should get a not found error

  Scenario: Unpublishing a valid course should be successful
    Given A existing published course
    When I unpublish the course
    Then The course should be unpublished