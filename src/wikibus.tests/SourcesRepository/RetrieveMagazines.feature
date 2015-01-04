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

Scenario: Get single issue
    Given RDF data:
        """
        @base <http://wikibus.org/> .
        @prefix xsd: <http://www.w3.org/2001/XMLSchema#>.

        {
            <magazine/Motor/issue/161> <http://lsdis.cs.uga.edu/projects/semdis/opus#month> "5"^^xsd:gMonth;
                                       <http://lsdis.cs.uga.edu/projects/semdis/opus#year> "1955"^^xsd:gYear;
                                       <http://purl.org/dc/terms/date> "1955-5-22"^^xsd:date;
                                       <http://purl.org/dc/terms/language> <http://lexvo.org/id/iso639-1/pl>;
                                       <http://purl.org/ontology/bibo/pages> 16 ;
                                       <http://schema.org/isPartOf> <magazine/Motor>;
                                       <http://schema.org/issueNumber> "161"^^xsd:string;
                                       a <http://schema.org/PublicationIssue>.
        }
        """
     When issue <http://wikibus.org/magazine/Motor/issue/161> is fetched
     Then 'Magazine.Id' should be Uri 'http://wikibus.org/magazine/Motor'
      And 'Number' should be string equal to '161'
