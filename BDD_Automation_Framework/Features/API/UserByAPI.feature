Feature: Test The User API in reqres.i


@apitest
Scenario: Get a User Information By API Via RestSharp and reqres.in
	Given Create Request "/users/{id}" with "GET" method
	When Create URL segment for "id" with parameter 2
	And Execute API
	Then The User should have the following values
	| id | email                  | first_name | last_name | avatar                                  |
	| 2  | janet.weaver@reqres.in | Janet      | Weaver    | https://reqres.in/img/faces/2-image.jpg |

Scenario: Create a User Via RestSharp and reqres.in
	Given Create Request "users" with "POST" method
	When I create a request body with the following values
	| name    | job       |
	| SangMai | For_Mohan |
	And Execute API
	Then returned status code will be "201"
	Then The new user should have the following values
	| name    | job       |
	| SangMai | For_Mohan |
