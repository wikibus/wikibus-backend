Feature: Getting Sources

Scenario: GET brochure
	Given Accept header is 'text/turtle'
	And existing brochure 'http://wikibus.org/brochure/12345'
	When I GET resource '/brochure/12345' 
	Then response should have status 200
	And brochure 'http://wikibus.org/brochure/12345' should have been retrieved

Scenario: GET inexistent brochure
	Given brochure 'http://wikibus.org/brochure/12345' doesn't exist
	When I GET resource '/brochure/12345'
	Then response should have status 404