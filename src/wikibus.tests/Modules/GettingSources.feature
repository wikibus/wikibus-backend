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
