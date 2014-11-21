using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace wikibus.tests.Nancy
{
    [Binding]
    public class SerializingToRdfSteps
    {
        private object _model;

        [Given(@"A model of type '(.*)':")]
        public void GivenAModelOfType(string modelTypeName, Table table)
        {
            table.CreateInstance()
        }
        
        [When(@"model is serialized to '(.*)'")]
        public void WhenModelIsSerializedTo(string p0)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"json object should contain key '(.*)' with value '(.*)'")]
        public void ThenJsonObjectShouldContainKeyWithValue(string p0, string p1)
        {
            ScenarioContext.Current.Pending();
        }
    }
}
