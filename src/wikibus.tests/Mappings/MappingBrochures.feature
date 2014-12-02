Feature: Mapping Brochures from SQL to RDF
	Make sure that correct RDF is returned for SQL rows

@mytag
Scenario: Mapping brochure row
	Given source table with data:
		| Id | Type     | Language | Language2 | Pages | Year | Month | Day  | Notes | FolderCode | FolderName              | BookTitle | BookAuthor | BookISBN | MagIssueMagazine | MagIssueNumber | FileMimeType | Url  | FileName |
		| 1  | Brochure | tr       | en        | 2     | NULL | NULL  | NULL |       |            | Türkkar City Angel E.D. | NULL      | NULL       | NULL     | NULL             | NULL           | NULL         | NULL | NULL     |
	When retrieve all triples
	Then resulting graph should be equal to:
		"""
		@base <http://wikibus.org/> .
		@prefix wbo: <http://wikibus.org/ontology#> .
		@prefix bibo: <http://purl.org/ontology/bibo/> .
		@prefix dcterms: <http://purl.org/dc/terms/> .

		<brochure/1> 
			a wbo:Brochure ;
			bibo:pages 2 ;
			dcterms:title "Türkkar City Angel E.D." ;
			dcterms:language <http://www.lexvo.org/page/iso639-1/tr>, <http://www.lexvo.org/page/iso639-1/en> .
		"""
