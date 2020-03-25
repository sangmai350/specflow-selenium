Feature: Login functionality 
	Test the Login functionality 

@regressiontest
Scenario: Verify if the login functionality is working
	Given I have navigated to the application
	And I have typed username and password
	When I click login button
	Then I should see the HomePage page
	


