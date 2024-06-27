Feature: Activity
As a user I want to get and see my recent auth activities

Rule: I dunno what the fuck this is
	
Scenario: Getting activities for logged in user
	Given An existing user with Id <userId>
	When I get all the activities for user with Id <userId>
	Then The count of activities should be more than 1
	Examples:
		| userId               |
		| 8GuVX9H9fx5WwCB8HVeQ |

Scenario: Getting active sessions for an invalid logged in user
	Given No existing user with Id <userId>
	When I get all the activities for user with Id <userId>
	Then The count of activities should be 0
	Examples:
		| userId        |
		| invalidUserId |
		|               |