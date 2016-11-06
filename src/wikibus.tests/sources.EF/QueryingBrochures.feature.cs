﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:2.0.0.0
//      SpecFlow Generator Version:2.0.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace wikibus.tests.Sources_EF
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.0.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("Retrieving data with SQL repository")]
    [NUnit.Framework.CategoryAttribute("SQL")]
    [NUnit.Framework.CategoryAttribute("EF")]
    public partial class RetrievingDataWithSQLRepositoryFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "QueryingBrochures.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Retrieving data with SQL repository", null, ProgrammingLanguage.CSharp, new string[] {
                        "SQL",
                        "EF"});
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
        [NUnit.Framework.DescriptionAttribute("Retrieving brochure row with two languages")]
        [NUnit.Framework.CategoryAttribute("Brochure")]
        public virtual void RetrievingBrochureRowWithTwoLanguages()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Retrieving brochure row with two languages", new string[] {
                        "Brochure"});
#line 5
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "Id",
                        "SourceType",
                        "Language",
                        "Language2",
                        "Pages",
                        "FolderName"});
            table1.AddRow(new string[] {
                        "1",
                        "folder",
                        "tr",
                        "en",
                        "2",
                        "Türkkar City Angel E.D."});
#line 6
   testRunner.Given("table Sources.Source with data:", ((string)(null)), table1, "Given ");
#line 9
  testRunner.And("data is inserted", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 10
   testRunner.When("getting Brochure <http://wikibus.org/brochure/1>", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 11
   testRunner.Then("Id should be <http://wikibus.org/brochure/1>", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "Property",
                        "Value"});
            table2.AddRow(new string[] {
                        "Pages",
                        "2"});
            table2.AddRow(new string[] {
                        "Title",
                        "Türkkar City Angel E.D."});
#line 12
   testRunner.And("Brochure should match", ((string)(null)), table2, "And ");
#line 16
 testRunner.And("Languages should contain tr", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 17
 testRunner.And("Languages should contain en", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Retrieving brochure which doesn\'t exist")]
        [NUnit.Framework.CategoryAttribute("Brochure")]
        public virtual void RetrievingBrochureWhichDoesnTExist()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Retrieving brochure which doesn\'t exist", new string[] {
                        "Brochure"});
#line 20
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                        "Id",
                        "SourceType",
                        "Language",
                        "Language2",
                        "Pages",
                        "FolderName"});
            table3.AddRow(new string[] {
                        "1",
                        "folder",
                        "tr",
                        "en",
                        "2",
                        "Türkkar City Angel E.D."});
#line 21
   testRunner.Given("table Sources.Source with data:", ((string)(null)), table3, "Given ");
#line 24
  testRunner.And("data is inserted", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 25
   testRunner.When("getting Brochure <http://wikibus.org/brochure/2>", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 26
   testRunner.Then("Brochure should be null", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Retrieving brochure with image")]
        [NUnit.Framework.CategoryAttribute("Book")]
        public virtual void RetrievingBrochureWithImage()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Retrieving brochure with image", new string[] {
                        "Book"});
#line 29
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table4 = new TechTalk.SpecFlow.Table(new string[] {
                        "Id",
                        "SourceType",
                        "Image"});
            table4.AddRow(new string[] {
                        "2",
                        "folder",
                        "3qAAAA=="});
#line 30
   testRunner.Given("table Sources.Source with data:", ((string)(null)), table4, "Given ");
#line 33
  testRunner.And("data is inserted", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 34
   testRunner.When("getting Brochure <http://wikibus.org/brochure/2>", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 35
   testRunner.Then("Should have image http://wikibus.org/brochure/2/image", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Retrieving book with image")]
        [NUnit.Framework.CategoryAttribute("Book")]
        public virtual void RetrievingBookWithImage()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Retrieving book with image", new string[] {
                        "Book"});
#line 38
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table5 = new TechTalk.SpecFlow.Table(new string[] {
                        "Id",
                        "SourceType",
                        "Image"});
            table5.AddRow(new string[] {
                        "407",
                        "book",
                        "3qAAAA=="});
#line 39
   testRunner.Given("table Sources.Source with data:", ((string)(null)), table5, "Given ");
#line 42
  testRunner.And("data is inserted", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 43
   testRunner.When("getting Book <http://wikibus.org/book/407>", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 44
   testRunner.Then("Should have image http://wikibus.org/book/407/image", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Getting brochure row with date")]
        [NUnit.Framework.CategoryAttribute("Brochure")]
        public virtual void GettingBrochureRowWithDate()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Getting brochure row with date", new string[] {
                        "Brochure"});
#line 47
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table6 = new TechTalk.SpecFlow.Table(new string[] {
                        "Id",
                        "SourceType",
                        "Language",
                        "Language2",
                        "Pages",
                        "Year",
                        "Month",
                        "Day",
                        "FolderCode",
                        "FolderName"});
            table6.AddRow(new string[] {
                        "6",
                        "folder",
                        "pl",
                        "NULL",
                        "2",
                        "2006",
                        "9",
                        "21",
                        "BED 81419 2006-09-21 POL Version 2",
                        "Fakty: Autobus turystyczny Volvo B9r/Sunsundegui Elegance"});
#line 48
   testRunner.Given("table Sources.Source with data:", ((string)(null)), table6, "Given ");
#line 51
  testRunner.And("data is inserted", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 52
   testRunner.When("getting Brochure <http://wikibus.org/brochure/6>", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table7 = new TechTalk.SpecFlow.Table(new string[] {
                        "Property",
                        "Value"});
            table7.AddRow(new string[] {
                        "Pages",
                        "2"});
            table7.AddRow(new string[] {
                        "Year",
                        "2006"});
            table7.AddRow(new string[] {
                        "Month",
                        "9"});
            table7.AddRow(new string[] {
                        "Code",
                        "BED 81419 2006-09-21 POL Version 2"});
            table7.AddRow(new string[] {
                        "Title",
                        "Fakty: Autobus turystyczny Volvo B9r/Sunsundegui Elegance"});
#line 53
   testRunner.Then("Brochure should match", ((string)(null)), table7, "Then ");
#line 60
   testRunner.And("Date should be 2006-09-21", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Getting complete book row")]
        [NUnit.Framework.CategoryAttribute("Book")]
        public virtual void GettingCompleteBookRow()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Getting complete book row", new string[] {
                        "Book"});
#line 63
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table8 = new TechTalk.SpecFlow.Table(new string[] {
                        "Id",
                        "SourceType",
                        "Language",
                        "Language2",
                        "Pages",
                        "Year",
                        "BookTitle",
                        "BookAuthor",
                        "BookISBN"});
            table8.AddRow(new string[] {
                        "407",
                        "book",
                        "pl",
                        "NULL",
                        "140",
                        "2010",
                        "Pojazdy samochodowe i przyczepy Jelcz 1952-1970",
                        "Wojciech Polomski",
                        "9788320617412"});
#line 64
   testRunner.Given("table Sources.Source with data:", ((string)(null)), table8, "Given ");
#line 67
  testRunner.And("data is inserted", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 68
   testRunner.When("getting Book <http://wikibus.org/book/407>", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table9 = new TechTalk.SpecFlow.Table(new string[] {
                        "Property",
                        "Value"});
            table9.AddRow(new string[] {
                        "Pages",
                        "140"});
            table9.AddRow(new string[] {
                        "Year",
                        "2010"});
            table9.AddRow(new string[] {
                        "ISBN",
                        "9788320617412"});
            table9.AddRow(new string[] {
                        "Title",
                        "Pojazdy samochodowe i przyczepy Jelcz 1952-1970"});
#line 69
   testRunner.Then("Book should match", ((string)(null)), table9, "Then ");
#line 75
 testRunner.And("Languages should contain pl", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 76
 testRunner.And("Author should be \'Wojciech Polomski\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Getting complete magazine issue")]
        [NUnit.Framework.CategoryAttribute("MagazineIssue")]
        public virtual void GettingCompleteMagazineIssue()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Getting complete magazine issue", new string[] {
                        "MagazineIssue"});
#line 79
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table10 = new TechTalk.SpecFlow.Table(new string[] {
                        "Id",
                        "SourceType",
                        "Language",
                        "Pages",
                        "Year",
                        "Month",
                        "MagIssueMagazine",
                        "MagIssueNumber",
                        "Image"});
            table10.AddRow(new string[] {
                        "324",
                        "magissue",
                        "pl",
                        "16",
                        "2007",
                        "3",
                        "1",
                        "13",
                        "3qAAAA=="});
#line 80
   testRunner.Given("table Sources.Source with data:", ((string)(null)), table10, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table11 = new TechTalk.SpecFlow.Table(new string[] {
                        "Id",
                        "Name"});
            table11.AddRow(new string[] {
                        "1",
                        "Bus Kurier"});
#line 83
     testRunner.And("table Sources.Magazine with data:", ((string)(null)), table11, "And ");
#line 86
  testRunner.And("data is inserted", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 87
    testRunner.When("Getting issue <http://wikibus.org/magazine/Bus%20Kurier/issue/13>", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 88
    testRunner.Then("Languages should contain pl", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            TechTalk.SpecFlow.Table table12 = new TechTalk.SpecFlow.Table(new string[] {
                        "Property",
                        "Value"});
            table12.AddRow(new string[] {
                        "Number",
                        "13"});
            table12.AddRow(new string[] {
                        "Year",
                        "2007"});
            table12.AddRow(new string[] {
                        "Pages",
                        "16"});
            table12.AddRow(new string[] {
                        "Month",
                        "3"});
#line 89
     testRunner.And("Issue should match", ((string)(null)), table12, "And ");
#line 95
    testRunner.And("Magazine is <http://wikibus.org/magazine/Bus%20Kurier>", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
