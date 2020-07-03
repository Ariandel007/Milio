Feature: Validate funcionality of filter
 #In order to Search Carers
 #As a type of Client
 #I want login
 #So that reason
 
 @mytag
 Scenario: Search by fare
    Given The client press the button Order
    When Select fare
    Then the result should be the carers order by fare