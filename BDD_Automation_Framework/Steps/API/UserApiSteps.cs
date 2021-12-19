using BDD_Automation_Framework.Model;
using FluentAssertions;
using RestSharp.Serialization.Json;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace BDD_Automation.Steps
{
    [Binding]
    public sealed class UserApiSteps
    {
        private static UserResponse userInfo;
        private static CreateUser newUser;
        string jsonBody;
        private readonly JsonDeserializer deserializer = new JsonDeserializer();

        [When(@"I create a request body with the following values")]
        public void WhenICreateRequestBodyWithFollowingValues(Table table)
        {
            newUser = table.CreateInstance<CreateUser>();
            var obj = new CreateUser()
            {
                name = newUser.name,
                job = newUser.job,
            };
            JsonSerializer serializer = new JsonSerializer();
            jsonBody = serializer.Serialize(obj);
            ShareApiSteps.request.AddJsonBody(jsonBody);
        }

        [Then(@"The new user should have the following values")]
        public void ThenTheNewUserShouldHaveTheFollowingValues(Table table)
        {
            newUser = deserializer.Deserialize<CreateUser>(ShareApiSteps.apiResult);
            var expectedUserData = table.CreateInstance<CreateUser>();
            newUser.Should().BeEquivalentTo(expectedUserData);
        }

        [Then(@"The User should have the following values")]
        public void ThenTheUserShouldHaveTheFollowingValues(Table table)
        {
            userInfo = deserializer.Deserialize<UserResponse>(ShareApiSteps.apiResult);
            var expectedUserData = table.CreateInstance<UserData>();
            userInfo.data.Should().BeEquivalentTo(expectedUserData);
        }
    }
}
