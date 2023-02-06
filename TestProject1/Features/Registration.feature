Feature: Registration

Feature to test successfull registration in buggy application
Background:
Given I open the buggy application home page


@registration
Scenario: Validate registration with password length not satisfied
	Given I navigate to application registration
	Then I fill registration fields as below
	| login        | FirstName        |   LastName        |   Password   | ConfirmPassword |
	|  rishith     |  rishith         |  rajagopal        |   1234       |     1234        |
	Then I click register button
	And I validate the following error message as below
	| errormessage                                                                                                |
	| InvalidParameter: 1 validation error(s) found. - minimum field size of 6, SignUpInput.Password.             |




@registration
Scenario: Validate registration with passwords not matching
	Given I navigate to application registration
	Then I fill registration fields as below
	| login        | FirstName        |   LastName        |   Password   | ConfirmPassword |
	|  rishith     |  rishith         |  rajagopal        |   1234       |     123         |
	And I validate error message for the condition Passwords do not match



@registration
Scenario: Validate successful registration for an user
	Given I navigate to application registration
	Then I fill registration fields with random values
	Then I click register button
	And I Validate registration is successfull



@registration
Scenario: Validate registration with existing username
	Given I navigate to application registration
	Then I fill registration fields as below
	| login        | FirstName        |   LastName        |   Password                | ConfirmPassword              |
	|  john        |  john            |    derek          |   Specialpassword_1       |     Specialpassword_1        |
	Then I click register button
	And I validate the following error message as below
	| errormessage                                             |
	| UsernameExistsException: User already exists             |


@registration
Scenario: Validate registration with no Uppercase in password field
	Given I navigate to application registration
	Then I fill registration fields as below
	| login        | FirstName        |   LastName        |   Password                | ConfirmPassword              |
	|  john        |  john            |    derek          |   specialpassword_1       |     specialpassword_1        |
	Then I click register button
	And I validate the following error message as below
	| errormessage                                                                                                       |
	| InvalidPasswordException: Password did not conform with policy: Password must have uppercase characters            |


@registration
Scenario: Validate registration with no Lowercase in password field
	Given I navigate to application registration
	Then I fill registration fields as below
	| login        | FirstName        |   LastName        |   Password                |    ConfirmPassword              |
	|  john        |  john            |    derek          |   ABCDEFGH_1              |     ABCDEFGH_1                  |
	Then I click register button
	And I validate the following error message as below
	| errormessage                                                                                                       |
	| InvalidPasswordException: Password did not conform with policy: Password must have lowercase characters            |


@registration
Scenario: Validate registration with no special characters in password field
	Given I navigate to application registration
	Then I fill registration fields as below
	| login        | FirstName        |   LastName        |   Password                |    ConfirmPassword              |
	|  john        |  john            |    derek          |   ABCDEFGHe1              |     ABCDEFGHe1                  |
	Then I click register button
	And I validate the following error message as below
	| errormessage                                                                                                    |
	| InvalidPasswordException: Password did not conform with policy: Password must have symbol characters            |
	







