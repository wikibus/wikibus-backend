Feature: Getting Sources

Scenario Outline: GETting sources
   Given Accept header is 'text/turtle'
   And existing <type> 'http://wikibus.org<path>'
   When I GET resource '<path>' 
   Then response should have status 200
   And <type> 'http://wikibus.org<path>' should have been retrieved
   Examples:
   | type     | path            |
   | brochure | /brochure/12345 |
   | book     | /book/123456abc |

Scenario: GET inexistent brochure
   Given brochure 'http://wikibus.org/brochure/12345' doesn't exist
   When I GET resource '/brochure/12345'
   Then response should have status 404

Scenario: GET books collection first page
   Given Accept header is 'text/turtle'
     And exisiting book collection
    When I GET resource '/books'
    Then response should have status 200
     And page 1 of book collection should have been retrieved

Scenario: GET books collection Nth page
   Given Accept header is 'text/turtle'
     And exisiting book collection
	 And query string is
		| key  | value |
		| page | 25    |
    When I GET resource '/books'
    Then response should have status 200
     And page 25 of book collection should have been retrieved
