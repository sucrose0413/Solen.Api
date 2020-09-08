@Authentication @CheckPasswordToken @TechnicalFeature
Feature: Checking a password reset token
  In order to check the validity of a password reset token,
  The system should be able to check such a token

  Scenario: Password reset token is mandatory
    When The password reset token is null
    Then The system should return a bad request error

  Scenario: Handling the case where the token is invalid
    When The password reset token is invalid
    Then The system should return a not found error

  Scenario: Handling the case where the token is valid
    When The password reset token is valid
    Then The request should be successful