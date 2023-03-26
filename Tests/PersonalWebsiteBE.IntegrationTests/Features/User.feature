Feature: User
As a user I want to authenticate to the API, and manage my data

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

Scenario: Logging out with a valid session token
	Given An existing session with sessionToken as <sessionToken>
	When I logout with sessionToken <sessionToken>
	Then No existing session with sessionToken as <sessionToken>
	Examples:
		| sessionToken                                                     |
		| DC10F6CB77B5962456E553E1334BFA309DC1E80DAEF543017577C28172BD9653 |

Scenario: Logging out with a invalid session token
	Given No existing session with sessionToken as <sessionToken>
	When I logout with sessionToken <sessionToken>
	Then No existing session with sessionToken as <sessionToken>
	Examples:
		| sessionToken        |
		| invalidSessionToken |
		|                     |