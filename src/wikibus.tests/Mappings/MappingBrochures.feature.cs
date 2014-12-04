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
namespace wikibus.tests.Mappings
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.9.0.77")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("Mapping Brochures from SQL to RDF")]
    public partial class MappingBrochuresFromSQLToRDFFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "MappingBrochures.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Mapping Brochures from SQL to RDF", " Make sure that correct RDF is returned for SQL rows", ProgrammingLanguage.CSharp, ((string[])(null)));
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
        [NUnit.Framework.DescriptionAttribute("Mapping brochure row")]
        public virtual void MappingBrochureRow()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Mapping brochure row", ((string[])(null)));
#line 4
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "Id",
                        "Type",
                        "Language",
                        "Language2",
                        "Pages",
                        "Year",
                        "Month",
                        "Day",
                        "Notes",
                        "FolderCode",
                        "FolderName",
                        "BookTitle",
                        "BookAuthor",
                        "BookISBN",
                        "MagIssueMagazine",
                        "MagIssueNumber",
                        "FileMimeType",
                        "Url",
                        "FileName"});
            table1.AddRow(new string[] {
                        "1",
                        "Brochure",
                        "tr",
                        "en",
                        "2",
                        "NULL",
                        "NULL",
                        "NULL",
                        "NULL",
                        "NULL",
                        "Türkkar City Angel E.D.",
                        "NULL",
                        "NULL",
                        "NULL",
                        "NULL",
                        "NULL",
                        "NULL",
                        "NULL",
                        "NULL"});
#line 5
   testRunner.Given("table \'[Sources].[Source]\' with data:", ((string)(null)), table1, "Given ");
#line 8
   testRunner.When("retrieve all triples", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 9
   testRunner.Then("resulting dataset should contain \'5\' triples", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 10
   testRunner.And("resulting dataset should match query:", @"base <http://wikibus.org/>
prefix wbo: <http://wikibus.org/ontology#>
prefix bibo: <http://purl.org/ontology/bibo/>
prefix dcterms: <http://purl.org/dc/terms/>

ASK
{
   <brochure/1> 
      a wbo:Brochure ;
      bibo:pages 2 ;
      dcterms:title ""Türkkar City Angel E.D."" ;
      dcterms:language <http://www.lexvo.org/page/iso639-1/tr>, 
                       <http://www.lexvo.org/page/iso639-1/en> .
}", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Mapping brochure row with date")]
        public virtual void MappingBrochureRowWithDate()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Mapping brochure row with date", ((string[])(null)));
#line 28
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "Id",
                        "Type",
                        "Language",
                        "Language2",
                        "Pages",
                        "Year",
                        "Month",
                        "Day",
                        "Notes",
                        "FolderCode",
                        "FolderName",
                        "BookTitle",
                        "BookAuthor",
                        "BookISBN",
                        "MagIssueMagazine",
                        "MagIssueNumber",
                        "FileMimeType",
                        "Url",
                        "FileName"});
            table2.AddRow(new string[] {
                        "6",
                        "Brochure",
                        "pl",
                        "NULL",
                        "2",
                        "2006",
                        "9",
                        "21",
                        "NULL",
                        "BED 81419 2006-09-21 POL Version 2",
                        "Fakty: Autobus turystyczny Volvo B9r/Sunsundegui Elegance",
                        "NULL",
                        "NULL",
                        "NULL",
                        "NULL",
                        "NULL",
                        "NULL",
                        "NULL",
                        "NULL"});
#line 29
   testRunner.Given("table \'[Sources].[Source]\' with data:", ((string)(null)), table2, "Given ");
#line 32
   testRunner.When("retrieve all triples", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 33
   testRunner.Then("resulting dataset should contain \'8\' triples", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 34
   testRunner.And("resulting dataset should match query:", @"base <http://wikibus.org/>
prefix wbo: <http://wikibus.org/ontology#>
prefix bibo: <http://purl.org/ontology/bibo/>
prefix dcterms: <http://purl.org/dc/terms/>
prefix xsd: <http://www.w3.org/2001/XMLSchema#>
prefix opus: <http://lsdis.cs.uga.edu/projects/semdis/opus#>
prefix langIso: <http://www.lexvo.org/page/iso639-1/>

ASK
{
   <brochure/6> 
      a wbo:Brochure ;
      bibo:pages 2 ;
      dcterms:title ""Fakty: Autobus turystyczny Volvo B9r/Sunsundegui Elegance"" ;
      opus:year ""2006""^^xsd:gYear ;
      opus:month ""9""^^xsd:gMonth ;
      dcterms:date ""2006-9-21""^^xsd:date ;
      dcterms:language langIso:pl ;
      dcterms:identifier ""BED 81419 2006-09-21 POL Version 2"" .
}", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
