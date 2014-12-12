Feature: Mapping Brochures from SQL to RDF
   Make sure that correct RDF is returned for SQL rows

Scenario: Mapping brochure row
   Given table source with data:
      | Id | Type     | TypeLower | Language | Language2 | Pages | Year | Month | Day  | Notes | FolderCode | FolderName              | BookTitle | BookAuthor | BookISBN | MagIssueMagazine | MagIssueNumber | FileMimeType | Url  | FileName |
      | 1  | Brochure | brochure  | tr       | en        | 2     | NULL | NULL  | NULL | NULL  | NULL       | Türkkar City Angel E.D. | NULL      | NULL       | NULL     | NULL             | NULL           | NULL         | NULL | NULL     |
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

Scenario: Mapping brochure row with date
   Given table source with data:
      | Id | Type     | TypeLower | Language | Language2 | Pages | Year | Month | Day | Notes | FolderCode                         | FolderName                                                | BookTitle | BookAuthor | BookISBN | MagIssueMagazine | MagIssueNumber | FileMimeType | Url  | FileName |
      | 6  | Brochure | brochure  | pl       | NULL      | 2     | 2006 | 9     | 21  | NULL  | BED 81419 2006-09-21 POL Version 2 | Fakty: Autobus turystyczny Volvo B9r/Sunsundegui Elegance | NULL      | NULL       | NULL     | NULL             | NULL           | NULL         | NULL | NULL     |
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
   Given table source with data:
      | Id | Type     | TypeLower | Language | Language2 | Pages | Year | Month | Day  | Notes | FolderCode                         | FolderName                                                | BookTitle | BookAuthor | BookISBN | MagIssueMagazine | MagIssueNumber | FileMimeType | Url  | FileName |
      | 6  | Brochure | brochure  | pl       | NULL      | 2     | 2006 | NULL  | NULL | NULL  | BED 81419 2006-09-21 POL Version 2 | Fakty: Autobus turystyczny Volvo B9r/Sunsundegui Elegance | NULL      | NULL       | NULL     | NULL             | NULL           | NULL         | NULL | NULL     |
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