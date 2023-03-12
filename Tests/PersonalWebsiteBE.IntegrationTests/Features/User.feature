Feature: User
As a user I want to be able to manage my session

Scenario: Logging in with valid credentials
	Given An existing user with username <username>
	When I login with username <username> and password <password>
	Then AuthData should not be null
	Examples:
		| username  | password     |
		| CorbynG98 | P@ssword123# |
