@Authentication @RefreshToken @TechnicalFeature
Feature: Refresh Token Implementation
  In order to support short time expiry of an authentication token and keep control on users access,
  The system should implement refresh token

  Scenario: The given refresh token should be issued by the system
    When The given refresh token is an unknown token
    Then The system should return an unauthorized error

  Scenario: The given refresh token should be valid
    When The given refresh token is expired
    Then The system should return an unauthorized error

  Scenario: The refresh token bearer should be an active user
    When The given refresh token bearer has been blocked
    Then The system should return a locked error

  Scenario: Refreshing a valid token that is associated to an active bearer should be successful
    When The given refresh token is valid and the bearer is active
    Then The request should be successful
    And A new refresh token and a valid authentication token should be returned