@CoursesManagement @UpdateCourse
Feature: Updating a training course
  In order to update the content of a training course,
  As a Instructor,
  I want to be able to perform such an operation

  Background:
    Given I'm an authenticated instructor within the organization

  Scenario Outline: The course Id is mandatory
    When I update a course with an empty or a null Id <CourseId>
    Then I should get a bad request error

    Examples:
      | CourseId |
      | ""       |
      | "null"   |

  Scenario Outline: The course title is mandatory
    When I update a course with an empty or a null title <Title>
    Then I should get a bad request error

    Examples:
      | Title  |
      | ""     |
      | "null" |

  Scenario: The course title should be less than or equal to 60 characters
    When I update a course with a title length over 60 characters
    Then I should get a bad request error

  Scenario: The course subtitle should be less than or equal to 120 characters
    When I update a course with a subtitle length over 120 characters
    Then I should get a bad request error

  Scenario: The course skill should be less than or equal to 150 characters
    When I update a course with a skill length over 150 characters
    Then I should get a bad request error

  Scenario: The course Id should be valid
    When I update an invalid course
    Then I should get a not found error

  Scenario: The course should be belonging to the same organization as the instructor
    When I update a course belonging to an other organization
    Then I should get a not found error

  Scenario: The course should not be published while updating it
    When I update a published course
    Then I should get a bad request error

  Scenario: Updating a valid course with valid properties should be successful
    Given A draft course with
      | Title     | Subtitle     | Description     | Skills                   |
      | old title | old subtitle | old description | old skill 1, old skill 2 |
    When I update the course with
      | Title     | Subtitle     | Description     | Skills                   |
      | new title | new subtitle | new description | new skill 1, new skill 2 |
    Then The course should be successfully updated with the new data 