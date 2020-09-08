@CoursesManagement @UpdateLecture
Feature: Updating a lecture
  In order to update the info about a lecture,
  As a Instructor,
  I want to be able to perform such an operation

  Background:
    Given I'm an authenticated instructor within the organization

  Scenario Outline: The lecture Id is mandatory
    When I update a lecture with an empty or a null Id <LectureId>
    Then I should get a bad request error

    Examples:
      | LectureId |
      | ""        |
      | "null"    |

  Scenario Outline: The lecture title is mandatory
    When I update a lecture with an empty or a null title <Title>
    Then I should get a bad request error

    Examples:
      | Title  |
      | ""     |
      | "null" |

  Scenario: The lecture title should be less than or equal to 100 characters
    When I update a lecture with a title length over 100 characters
    Then I should get a bad request error

  Scenario: The lecture Id should be valid
    When I update an invalid lecture Id
    Then I should get a not found error

  Scenario: The lecture course should not be published while updating the lecture
    When I update a lecture while the course is published
    Then I should get a bad request error

  Scenario: Updating a valid lecture with valid data should be successful
    Given A lecture belonging to a draft course with
      | Lecture Id | Title     | Content     | Duration (seconds) |
      | lectureId  | old title | old content | 60                 |
    When I update the lecture with
      | Lecture Id | Title     | Content     | Duration (seconds) |
      | lectureId  | new title | new content | 120                |
    Then The lecture should be successfully updated with the new data