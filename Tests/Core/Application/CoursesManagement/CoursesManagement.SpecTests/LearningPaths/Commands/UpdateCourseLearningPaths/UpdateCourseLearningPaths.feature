@CoursesManagement @UpdateCourseLearningPaths
Feature: Updating a training course learning paths list
  In order to add/remove learning paths to/from a training course learning path list,
  As a Instructor,
  I want to be able to perform such an operation

  Background:
    Given I'm an authenticated instructor within the organization

  Scenario Outline: The course Id is mandatory
    When I update a course learning paths with an empty or a null Id <CourseId>
    Then I should get a bad request error

    Examples:
      | CourseId |
      | ""       |
      | "null"   |

  Scenario: The learning paths are mandatory
    When I update a course learning paths without specifying learning paths Ids
    Then I should get a bad request error

  Scenario: The course Id should be valid
    When I update a course learning paths with an invalid course Id
    Then I should get a not found error

  Scenario: The course should be belonging to the same organization as the instructor
    When I update a course belonging to an other organization
    Then I should get a not found error

  Scenario: The learning paths Ids should be valid
    When I update a course learning paths with an invalid learning paths Ids
    Then I should get a not found error

  Scenario: The course should not be published while updating its learning paths
    When I update the learning paths of a published course
    Then I should get a bad request error

  Scenario: Adding learning paths to a training course learning paths list
    Given A draft course that has an empty learning paths list
      | Course Id | Course name                            | Learning Paths |
      | courseId  | How to attach course to learning paths | x              |
    And The following learning paths
      | Learning path Id | Learning path  name |
      | learningPathId1  | Developer           |
      | learningPathId2  | Business Analyst    |
    When I update the course learning paths list as
      | Course Id | Learning Paths Ids               |
      | courseId  | learningPathId1, learningPathId2 |
    Then The learning paths should be successfully be added to the course learning paths list
      | Course Id | Course name                            | Learning Paths              |
      | courseId  | How to attach course to learning paths | Developer, Business Analyst |

  Scenario: Removing learning paths from a training course learning paths list
    Given A draft course that has a non-empty learning paths list
      | Course Id | Course name                            | Learning Paths                                                  |
      | courseId  | How to attach course to learning paths | Developer (learningPathId1), Business Analyst (learningPathId2) |
    When I update the course learning paths list as
      | Course Id | Learning Paths Ids |
      | courseId  | learningPathId1    |
    Then The 'Business Analyst' learning path should successfully be removed from the course learning paths list
      | Course Id | Course name                            | Learning Paths |
      | courseId  | How to attach course to learning paths | Developer      |
