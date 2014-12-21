Feature: Retrieve magazines from repository

Scenario: Get simple brochure
   Given RDF data:
      """
      @base <http://wikibus.org/> .
      @prefix dcterms: <http://purl.org/dc/terms/>.

      {
         <magazine/Bus Kurier> a <ontology#Magazine> ;
            dcterms:title "Bus Kurier" .
      }
      """
   When magazine <http://wikibus.org/magazine/Bus Kurier> is fetched
   Then 'Title' should be string equal to 'Bus Kurier'
    And 'Issues' should be Uri 'http://wikibus.org/magazine/Bus%20Kurier/issues'