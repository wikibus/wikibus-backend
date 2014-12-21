Feature: Mapping Magazines from SQL to RDF
   Make sure that correct RDF is returned for SQL rows

Scenario: Mapping complete magazine
   Given  table Sources.Magazine with data:
         | Id | Name      |
         | 1  | Bus Kurier |
    When retrieve all triples
    Then resulting dataset should contain '3' triples
     And resulting dataset should match query:
         """
          base <http://wikibus.org/>
          prefix wbo: <http://wikibus.org/ontology#>
          prefix bibo: <http://purl.org/ontology/bibo/>
          prefix dcterms: <http://purl.org/dc/terms/>
          prefix xsd: <http://www.w3.org/2001/XMLSchema#>
          prefix opus: <http://lsdis.cs.uga.edu/projects/semdis/opus#>
          prefix langIso: <http://www.lexvo.org/page/iso639-1/>
          prefix sch: <http://schema.org/>

          ASK
          {
             <magazine/Bus%20Kurier> a sch:Periodical, wbo:Magazine ;
                dcterms:title "Bus Kurier" .
          }
         """
