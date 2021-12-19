using FluentAssertions;
using RestSharp;
using System.Configuration;
using TechTalk.SpecFlow;

namespace BDD_Automation.Steps
{
    [Binding]
    public sealed class ShareApiSteps
    {

        public static RestClient api = new RestClient(ConfigurationManager.AppSettings["RestAPI"]);
        public static RestRequest request;
        public static IRestResponse apiResult;


        [Given(@"Create Request ""(.*)"" with ""(.*)"" method")]
        public void GivenCreateRequestWithMethod(string _request, Method _method)
        {
            request = new RestRequest(_request, _method);
            request.RequestFormat = DataFormat.Json;
        }

        [When(@"Create URL segment for ""(.*)"" with parameter (.*)")]
        public void ThenCreateURLSegmentForWithParameter(string _segment, string _parameter)
        {
            request.AddUrlSegment(_segment, _parameter);
        }

        [When(@"Execute API")]
        public void ThenExecuteAPI()
        {
            apiResult = api.Execute(request);
        }

        [Then(@"returned status code will be ""(.*)""")]
        public void ThenReturnedStatusCodeWillBe(int _status)
        {
            var code = (int)apiResult.StatusCode;
            code.Should().Be(_status);
        }

    }
}
