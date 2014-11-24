Feature: Getting Sources

Scenario: GET brochure
	When I GET resource '/brochure/12345' with Accept 'text/turtle'
	Then response should have status 200
	And brochure 'http://wikibus.org/brochure/12345' should have been retrieved
