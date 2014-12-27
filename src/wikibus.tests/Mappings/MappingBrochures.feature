Feature: Mapping Brochures from SQL to RDF
   Make sure that correct RDF is returned for SQL rows
   
Scenario: Mapping brochure row
   Given table Sources.Source with data:
      | Id | SourceType | Language | Language2 | Pages | FolderName              |
      | 1  | folder     | tr       | en        | 2     | Türkkar City Angel E.D. |
   When retrieve all triples
   Then resulting dataset should contain '5' triples
   And resulting dataset should match query:
      """
      base <http://wikibus.org/>
      prefix wbo: <http://wikibus.org/ontology#>
      prefix bibo: <http://purl.org/ontology/bibo/>
      prefix dcterms: <http://purl.org/dc/terms/>

      ASK
      {
         <brochure/1> 
            a wbo:Brochure ;
            bibo:pages 2 ;
            dcterms:title "Türkkar City Angel E.D." ;
            dcterms:language <http://www.lexvo.org/page/iso639-1/tr>, 
                             <http://www.lexvo.org/page/iso639-1/en> .
      }
      """

Scenario: Mapping brochure and book with image
   Given table Sources.Source with data:
      | Id  | SourceType | Image    |
      | 1   | folder     | 3qAAAA== |
      | 407 | book       | 3qAAAA== |
   When retrieve all triples
   Then resulting dataset should contain '8' triples
   And resulting dataset should match query:
      """
      base <http://wikibus.org/>
      prefix sch: <http://schema.org/>

      ASK
      {
         <brochure/1> 
            sch:image [
                a sch:ImageObject ;
                sch:contentUrl "http://wikibus.org/brochure/1/image"^^sch:URL
            ].
            
         <book/407> 
            sch:image [
                a sch:ImageObject ;
                sch:contentUrl "http://wikibus.org/book/407/image"^^sch:URL
            ].
      }
      """

Scenario: Mapping brochure row with date
   Given table Sources.Source with data:
      | Id | SourceType | Language | Language2 | Pages | Year | Month | Day | FolderCode                         | FolderName                                                |
      | 6  | folder     | pl       | NULL      | 2     | 2006 | 9     | 21  | BED 81419 2006-09-21 POL Version 2 | Fakty: Autobus turystyczny Volvo B9r/Sunsundegui Elegance |
   When retrieve all triples
   Then resulting dataset should contain '8' triples
   And resulting dataset should match query:
      """
      base <http://wikibus.org/>
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
            dcterms:title "Fakty: Autobus turystyczny Volvo B9r/Sunsundegui Elegance" ;
            opus:year "2006"^^xsd:gYear ;
            opus:month "9"^^xsd:gMonth ;
            dcterms:date "2006-9-21"^^xsd:date ;
            dcterms:language langIso:pl ;
            dcterms:identifier "BED 81419 2006-09-21 POL Version 2" .
      }
      """

Scenario: Mapping brochure row with incomplete date
   Given table Sources.Source with data:
      | Id | SourceType | Language | Language2 | Pages | Year | FolderCode                         | FolderName                                                |
      | 6  | folder     | pl       | NULL      | 2     | 2006 | BED 81419 2006-09-21 POL Version 2 | Fakty: Autobus turystyczny Volvo B9r/Sunsundegui Elegance |
   When retrieve all triples
   Then resulting dataset should contain '6' triples
   And resulting dataset should not match query:
      """
      base <http://wikibus.org/>
      prefix dcterms: <http://purl.org/dc/terms/>

      ASK
      {
         <brochure/6> dcterms:date ?date
      }
      """

Scenario: Mapping complete book row
   Given table Sources.Source with data:
         | Id  | SourceType | Language | Language2 | Pages | Year | BookTitle                                       | BookAuthor        | BookISBN      | 
         | 407 | book       | pl       | NULL      | 140   | 2010 | Pojazdy samochodowe i przyczepy Jelcz 1952-1970 | Wojciech Polomski | 9788320617412 |
    When retrieve all triples
    Then resulting dataset should contain '8' triples
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
             <book/407> a wbo:Book ;
                dcterms:title "Pojazdy samochodowe i przyczepy Jelcz 1952-1970" ;
                sch:isbn "9788320617412" ;
                sch:author [ sch:name "Wojciech Polomski" ] ;
                bibo:pages 140 ;
                opus:year "2010"^^xsd:gYear ;
                dcterms:language langIso:pl .
          }
         """

Scenario: Mapping complete magazine issue
   Given table Sources.Source with data:
         | Id  | SourceType | Language | Pages | Year | Month | MagIssueMagazine | MagIssueNumber | Image    |
         | 324 | magissue   | pl       | 16    | 2007 | 3     | 1                | 13             | 3qAAAA== |
     And table Sources.Magazine with data:
         | Id | Name      |
         | 1  | Bus Kurier |
    When retrieve all triples
    Then resulting dataset should contain '13' triples
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
             <magazine/Bus%20Kurier/issue/13> a sch:PublicationIssue ;
                sch:image [
                    a sch:ImageObject ;
                    sch:contentUrl "http://wikibus.org/magazine/Bus Kurier/issue/13/image"^^sch:URL 
                ] ;
                sch:issueNumber "13"^^xsd:string ;
                sch:isPartOf <magazine/Bus%20Kurier> ;
                bibo:pages 16 ;
                opus:year "2007"^^xsd:gYear ;
                opus:month "3"^^xsd:gMonth ;
                dcterms:language langIso:pl .
          }
         """