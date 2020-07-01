Feature: Validate funcionality on login page of Application
 #In order to Search Carers
 #As a type of Client
 #I want login
 #So that reason
 
 @mytag
 Scenario: Validate button login
    Given Open the Chrome and launch the application
    When Enter the Email and Password
    Then the result should be the user logged
  