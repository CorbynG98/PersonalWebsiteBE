Feature: Session
As a user I want to be able to manage and see my sessions

Scenario: Verifying a valid session should return true
	Given An existing session with sessionToken as <sessionToken>
	When I verify the session with token <sessionToken>
	Then The verify result should be true
	Examples:
		| sessionToken                                                     |
		| E35B00DFC5492A94637EAC2F686DFE58FFA699D0C4DD2195127C02A8AEC9E6D9 |
		
Scenario: Verifying an invalid session should return false
	Given No existing session with sessionToken as <sessionToken>
	When I verify the session with token <sessionToken>
	Then The verify result should be false
	Examples:
		| sessionToken        |
		| invalidSessionToken |
		
Scenario: Verifying with no sessionToken should be false
	Given No existing session with no sessionToken
	When I verify the session with no token
	Then The verify result should be false
		
Scenario: Getting active sessions for logged in user
	Given An existing user with Id <userId>
	When I get all the sessions for user with Id <userId>
	Then The count of session should be more than 1
	Examples:
		| userId               |
		| 8GuVX9H9fx5WwCB8HVeQ |

Scenario: Getting active sessions for an invalid logged in user
	Given No existing user with Id <userId>
	When I get all the sessions for user with Id <userId>
	Then The count of session should be 0
	Examples:
		| userId        |
		| invalidUserId |
		|               |