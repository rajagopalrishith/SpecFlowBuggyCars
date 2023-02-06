Feature: EndtoEndFeature

Feature validating a registration, login and voting
Background:
Given I open the buggy application home page


@EndtoEnd
Scenario: Validate successful registration for an user
	Given I navigate to application registration
	Then I fill registration fields with random values
	Then I click register button
	And I Validate registration is successfull
	Then I open the buggy application home page
	Then I login to the application with credentials from context
	Then I click login button
	And I validate successfull login
	And I Select category Popular Make
    And I vote for car model Guilia Quadrifoglio
    And I validate successful voting message is shown
