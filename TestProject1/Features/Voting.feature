Feature: Voting

A short summary of the feature

@login
Scenario: Validate voting for a car model
  Given I open the buggy application home page
  When I fill in login Information
	|   login        |    password         |  
	|  <login>       |   <password>        |  
  Then I click login button
  And I validate successfull login
  And I Select category Popular Make
  And I vote for car model Guilia Quadrifoglio
  And I validate successful voting message is shown

  Examples:
  	| login          | password                  |  
	|  johnderek     |  Specialpassword_1        |  