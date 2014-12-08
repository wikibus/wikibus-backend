﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:1.9.0.77
//      SpecFlow Generator Version:1.9.0.0
//      Runtime Version:4.0.30319.18444
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace wikibus.tests.SourcesRepository
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.9.0.77")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("Retrieve sources from repository")]
    public partial class RetrieveSourcesFromRepositoryFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "RetrieveSources.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Retrieve sources from repository", "Verify that models are correctly deserialized from RDF", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.TestFixtureTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Get simple brochure")]
        public virtual void GetSimpleBrochure()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Get simple brochure", ((string[])(null)));
#line 4
this.ScenarioSetup(scenarioInfo);
#line 5
   testRunner.Given("In-memory query processor", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 6
   testRunner.And("RDF data:", "@base <http://wikibus.org/> .\r\n@prefix dcterms: <http://purl.org/dc/terms/>.\r\n\r\n{" +
                    "\r\n   <brochure/VanHool+T8> a <ontology#Brochure> ;\r\n      dcterms:title \"VanHool" +
                    " T8 - New Look\" .\r\n}", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 16
   testRunner.When("brochure <http://wikibus.org/brochure/VanHool+T8> is fetched", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 17
   testRunner.Then("\'Title\' should be string equal to \'VanHool T8 - New Look\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Get brochure with Polish diacritics")]
        public virtual void GetBrochureWithPolishDiacritics()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Get brochure with Polish diacritics", ((string[])(null)));
#line 19
this.ScenarioSetup(scenarioInfo);
#line 20
   testRunner.Given("In-memory query processor", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 21
   testRunner.And("RDF data:", "@base <http://wikibus.org/> .\r\n@prefix dcterms: <http://purl.org/dc/terms/>.\r\n\r\n{" +
                    "\r\n   <brochure/12345> a <ontology#Brochure> ;\r\n      dcterms:title \"Jelcz M11 - " +
                    "nowość\" .\r\n}", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 31
   testRunner.When("brochure <http://wikibus.org/brochure/12345> is fetched", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 32
   testRunner.Then("\'Title\' should be string equal to \'Jelcz M11 - nowość\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Get complete brochure")]
        public virtual void GetCompleteBrochure()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Get complete brochure", ((string[])(null)));
#line 34
this.ScenarioSetup(scenarioInfo);
#line 35
    testRunner.Given("In-memory query processor", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 36
    testRunner.And("RDF data:", @"@base <http://wikibus.org/>.
@prefix wbo: <http://wikibus.org/ontology#>.
@prefix bibo: <http://purl.org/ontology/bibo/>.
@prefix dcterms: <http://purl.org/dc/terms/>.
@prefix xsd: <http://www.w3.org/2001/XMLSchema#>.
@prefix opus: <http://lsdis.cs.uga.edu/projects/semdis/opus#>.
@prefix langIso: <http://www.lexvo.org/page/iso639-1/>.
@prefix rdfs: <http://www.w3.org/2000/01/rdf-schema#>.

{
    <brochure/6> 
        a wbo:Brochure ;
        bibo:pages 2 ;
        dcterms:title ""Fakty: Autobus turystyczny Volvo B9r/Sunsundegui Elegance"" ;
        opus:year ""2006""^^xsd:gYear ;
        opus:month ""9""^^xsd:gMonth ;
        dcterms:date ""2006-9-21""^^xsd:date ;
        dcterms:language langIso:pl ;
        dcterms:identifier ""BED 81419 2006-09-21 POL Version 2"" ;
        rdfs:comment ""Some description about brochure"".
}", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 60
    testRunner.When("brochure <http://wikibus.org/brochure/6> is fetched", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 61
    testRunner.Then("\'Title\' should be string equal to \'Fakty: Autobus turystyczny Volvo B9r/Sunsundeg" +
                    "ui Elegance\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 62
     testRunner.And("\'Pages\' should be integer equal to \'2\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 63
     testRunner.And("\'Date\' should be DateTime equal to \'2006-09-21\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 64
     testRunner.And("\'Month\' should be integer equal to \'9\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 65
     testRunner.And("\'Code\' should be string equal to \'BED 81419 2006-09-21 POL Version 2\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 66
     testRunner.And("Languages should contain \'pl\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 67
     testRunner.And("\'Description\' should be string equal to \'Some description about brochure\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Get brochure without data")]
        public virtual void GetBrochureWithoutData()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Get brochure without data", ((string[])(null)));
#line 69
this.ScenarioSetup(scenarioInfo);
#line 70
    testRunner.Given("In-memory query processor", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 71
    testRunner.And("RDF data:", @"@base <http://wikibus.org/>.
@prefix wbo: <http://wikibus.org/ontology#>.
@prefix bibo: <http://purl.org/ontology/bibo/>.
@prefix dcterms: <http://purl.org/dc/terms/>.
@prefix xsd: <http://www.w3.org/2001/XMLSchema#>.
@prefix opus: <http://lsdis.cs.uga.edu/projects/semdis/opus#>.
@prefix langIso: <http://www.lexvo.org/page/iso639-1/>.
@prefix rdfs: <http://www.w3.org/2000/01/rdf-schema#>.

{
    <brochure/6> 
        a wbo:Brochure ;
}", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 87
    testRunner.When("brochure <http://wikibus.org/brochure/6> is fetched", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 88
    testRunner.Then("\'Title\' should be null", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 89
     testRunner.And("\'Pages\' should be null", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 90
     testRunner.And("\'Date\' should be null", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 91
     testRunner.And("\'Month\' should be null", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 92
     testRunner.And("\'Code\' should be null", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 93
     testRunner.And("\'Languages\' should be empty", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 94
     testRunner.And("\'Description\' should be null", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
