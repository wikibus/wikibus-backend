﻿   Feature: Retrieve sources from repository
   Verify that models are correctly deserialized from RDF
   
Scenario: Get simple brochure
   Given In-memory query processor
   And RDF data:
      """
      @base <http://wikibus.org/> .
      @prefix dcterms: <http://purl.org/dc/terms/>.

      {
         <brochure/VanHool+T8> a <ontology#Brochure> ;
            dcterms:title "VanHool T8 - New Look" .
      }
      """
   When brochure <http://wikibus.org/brochure/VanHool+T8> is fetched
   Then 'Title' should be string equal to 'VanHool T8 - New Look'
   
Scenario: Get brochure with Polish diacritics
   Given In-memory query processor
   And RDF data:
      """
      @base <http://wikibus.org/> .
      @prefix dcterms: <http://purl.org/dc/terms/>.

      {
         <brochure/12345> a <ontology#Brochure> ;
            dcterms:title "Jelcz M11 - nowość" .
      }
      """
   When brochure <http://wikibus.org/brochure/12345> is fetched
   Then 'Title' should be string equal to 'Jelcz M11 - nowość'

Scenario: Get complete brochure
    Given In-memory query processor
    And RDF data:
        """
        @base <http://wikibus.org/>.
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
                dcterms:title "Fakty: Autobus turystyczny Volvo B9r/Sunsundegui Elegance" ;
                opus:year "2006"^^xsd:gYear ;
                opus:month "9"^^xsd:gMonth ;
                dcterms:date "2006-9-21"^^xsd:date ;
                dcterms:language langIso:pl ;
                dcterms:identifier "BED 81419 2006-09-21 POL Version 2" ;
                rdfs:comment "Some description about brochure".
        }
        """
    When brochure <http://wikibus.org/brochure/6> is fetched
    Then 'Title' should be string equal to 'Fakty: Autobus turystyczny Volvo B9r/Sunsundegui Elegance'
     And 'Pages' should be integer equal to '2'
     And 'Date' should be DateTime equal to '2006-09-21'
     And 'Month' should be integer equal to '9'
     And 'Code' should be string equal to 'BED 81419 2006-09-21 POL Version 2'
     And Languages should contain 'pl'
     And 'Description' should be string equal to 'Some description about brochure'

Scenario: Get brochure without data
    Given In-memory query processor
    And RDF data:
        """
        @base <http://wikibus.org/>.
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
        }
        """
    When brochure <http://wikibus.org/brochure/6> is fetched
    Then 'Title' should be null
     And 'Pages' should be null
     And 'Date' should be null
     And 'Month' should be null
     And 'Code' should be null
     And 'Languages' should be empty
     And 'Description' should be null

Scenario: Get complete book
    Given In-memory query processor
    And RDF data:
        """
        @base <http://wikibus.org/>.
        @prefix wbo: <http://wikibus.org/ontology#>.
        @prefix dcterms: <http://purl.org/dc/terms/>.
        @prefix sch: <http://schema.org/>.

        {
            <book/6> 
                a wbo:Book ;
                dcterms:title "Strassenbahnen in Schlesien" ;
                sch:isbn "3879434247" ;
                sch:author [ sch:name "Siegfried Bufe" ] .
        }
        """
    When book <http://wikibus.org/book/6> is fetched
    Then 'Title' should be string equal to 'Strassenbahnen in Schlesien'
     And 'Author' should be string equal to 'Siegfried Bufe'
     And 'ISBN' should be string equal to '3879434247'

Scenario: Get book without author
    Given In-memory query processor
    And RDF data:
        """
        @base <http://wikibus.org/>.
        @prefix wbo: <http://wikibus.org/ontology#>.
        @prefix dcterms: <http://purl.org/dc/terms/>.
        @prefix sch: <http://schema.org/>.

        {
            <book/6> 
                a wbo:Book ;
                dcterms:title "Strassenbahnen in Schlesien" ;
                sch:isbn "3879434247" .
        }
        """
    When book <http://wikibus.org/book/6> is fetched
     Then 'Author' should be null
