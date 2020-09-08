@CoursesManagement @CreateLecture
Feature: Creation of a lecture
  In order to add some content to a training course,
  As a Instructor,
  I want to be able to create lectures

  Background:
    Given I'm an authenticated instructor within the organization

  Scenario Outline: The lecture title is mandatory
    When I create a lecture with an empty or a null <Title>
    Then I should get a bad request error

    Examples:
      | Title  |
      | ""     |
      | "null" |

  Scenario: The lecture title should be less than or equal to 100 characters
    When I create a lecture with a title length over 100 characters
    Then I should get a bad request error

  Scenario Outline: The lecture module is mandatory
    When I create a lecture with an empty or a null module Id <ModuleId>
    Then I should get a bad request error

    Examples:
      | ModuleId |
      | ""       |
      | "null"   |

  Scenario Outline: The lecture type is mandatory
    When I create a lecture with an empty or a null type <LectureType>
    Then I should get a bad request error

    Examples:
      | LectureType |
      | ""          |
      | "null"      |

  Scenario: The module Id should be valid
    When I create a lecture with an invalid module Id
    Then I should get a not found error

  Scenario: The lecture course should not be published while creating the lecture
    When I create a lecture while the course is published
    Then I should get a bad request error

  Scenario: The lecture type should be valid
    Given The following valid lecture types
      | Lecture Type |
      | Article      |
      | Video        |
    When I create a lecture with an invalid type
    Then I should get a bad request error

  Scenario: Creation of a lecture with valid properties should be successful
    When I create a lecture with valid properties as
      | Title         | Module Id | Lecture Type | Content         | Order | Duration (seconds) |
      | lecture title | moduleId  | Article      | lecture content | 1     | 60                 |
    Then The lecture Id should be returned
    And The lecture should be created

