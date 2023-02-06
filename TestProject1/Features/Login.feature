Feature: Login

Background:


@login
Scenario: Validate logging in application with incorrect username and password combinations
  Given I open the buggy application home page
  When I fill in login Information
	|   login        |    password         |  
	|  <login>       |   <password>        |  
  Then I click login button
  And I Validate error message for invalid login

  Examples:
  	| login          | password                  |  
	|  johnderek     |  Specialpassword_2        |  
    |  johnderek2    |  Specialpassword_1        |  
	|  johnderek2    |  Specialpassword_2        |
	

@login
Scenario: Validate logging in application with correct credentials
  Given I open the buggy application home page
  When I fill in login Information
	|   login        |    password         |  
	|  <login>       |   <password>        |  
  Then I click login button
  And I validate successfull login

  Examples:
  	| login          | password                  |  
	|  johnderek     |  Specialpassword_1        |  