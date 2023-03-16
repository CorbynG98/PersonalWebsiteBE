Feature: User
As a user I want to be able to manage my session

Scenario: Logging in with valid credentials
	Given An existing user with username <username>
	When I login with username <username> and password <password>
	Then AuthData should not be null
	Examples:
		| username  | password     |
		| CorbynG98 | P@ssword123# |

Scenario: Logging in with a valid username and invalid password
	Given An existing user with username <username>
	When I login with username <username> and password <password>
	Then Auth should fail
	Examples:
		| username        | password         |
		| CorbynG98       | fakePasswordHere |

Scenario: Logging in with an invalid username
	Given No existing user with username <username>
	When I login with username <username> and password <password>
	Then Auth should fail
	Examples:
		| username        | password         |
		| totallyFakeUser | fakePasswordHere |
		| totallyFakeUser | P@ssword123#     |

Scenario: Logging in with no username
	Given No existing user with username <username>
	When I login with no username and password <password>
	Then Auth should fail
	Examples:
		| password         |
		| P@ssword123#     |
		| fakePasswordHere |
		|                  |

Scenario: Logging in with no password and an invalid username
	Given No existing user with username <username>
	When I login with username <username> and no password
	Then Auth should fail
	Examples:
		| username  |
		|           |

Scenario: Logging in with no password and a valid username
	Given An existing user with username <username>
	When I login with username <username> and no password
	Then Auth should fail
	Examples:
		| username  |
		| CorbynG98 |