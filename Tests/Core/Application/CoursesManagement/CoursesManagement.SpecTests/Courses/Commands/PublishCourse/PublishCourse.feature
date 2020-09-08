@CoursesManagement @PublishCourse
Feature: Publishing a training course
  In order to allow the learners to access a course,
  As a Instructor,
  I want to be able to publish the course

  Background:
    Given I'm an authenticated instructor within the organization

  Scenario Outline: The course Id is mandatory
    When I publish a course with an empty or a null <CourseId>
    Then I should get a bad request error

    Examples:
      | CourseId     |
      | ""     |
      | "null" |

  Scenario: The course Id should be valid
    When I publish an invalid course Id
    Then I should get a not found error

  Scenario: The course should be belonging to the same organization as the instructor
    When I publish a course belonging to an other organization
    Then I should get a not found error

  Scenario: The course should have at least one module
    Given A draft course that has no module
    When I publish the course
    Then I should get a bad request error

  Scenario: Every module of the course should have at least one lecture
    Given A draft course that one of its modules has no lecture
    When I publish the course
    Then I should get a bad request error

  Scenario: Every article of the course should have some content
    Given A draft course that one of its articles has no content
    When I publish the course
    Then I should get a bad request error

  Scenario:Every article of the course should have a URL
    Given A draft course that one of its medias has no Url
    When I publish the course
    Then I should get a bad request error

  Scenario: Publishing a course with a valid content should be successful
    Given A draft course with a valid content
    When I publish the course
    Then The course should be published