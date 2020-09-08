@CoursesManagement @GetCourse
Feature: Get info about a training course
  In order to get information about a training course,
  As a Instructor,
  I want to be able to retrieve such info

  Background:
    Given I'm an authenticated instructor within the organization


  Scenario: The course Id should be valid
    When I get an invalid course
    Then I should get a not found error

  Scenario: The course errors list should not be empty where there are errors
    Given A course that has no module
    When I retrieve the course info
    Then The errors list should not be empty

  Scenario: The course errors list should be empty where there is no error
    Given A course that has no errors
    When I retrieve the course info
    Then The errors list should be empty


  Scenario: The returned data should correspond to the course info
    Given An existing training course
    When I retrieve the course info
    Then The following course info should be correctly returned
      | Property            | Description                                                            |
      | Id                  | The course Id                                                          |
      | Title               | The course title                                                       |
      | Subtitle            | The course subtitle                                                    |
      | Creator             | The name of the course creator                                         |
      | CreationDate        | The course creation date                                               |
      | Status              | The course status                                                      |
      | Duration            | The course duration                                                    |
      | LectureCount        | The number of lectures that the course contains title                  |
      | CourseLearnedSkills | The skills that a learner will have after the completion of the course |
      | IsEditable          | Indicates whether the course is editable                               |
      | IsPublished         | Indicates whether the course is published                              |
  
  