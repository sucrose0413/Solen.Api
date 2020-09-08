@CoursesManagement @CreateCourse
Feature: Creation of a training  course
  In order to create and share a training course,
  As a Instructor,
  I want to be able to create such a course

  Background:
    Given I'm an authenticated instructor within the organization
    
    
  Scenario Outline: The course title is mandatory
    When I create a course with an empty or a null <Title>
    Then I should get a bad request error
    
    Examples:
      | Title  |
      | ""     |
      | "null" |

  Scenario: The course title should be less than or equal to 60 characters
    When I create a course with a title length over 60 characters
    Then I should get a bad request error

  Scenario: Creation of a course with a valid title should be successful
    When I create a course with a valid title
    Then The course Id should be returned
    And The course should be created