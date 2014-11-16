Feature: Retrieve sources from repository

Scenario: Get brochure 'VanHool T8' from repository
	Given In-memory query processor
	And Rdf from 'samplebrochure.ttl'
	When brochure <http://wikibus.org/brochure/VanHool+T8> is fetched
	Then the brochure should have title 'VanHool T8 - New Look'
	
Scenario: Get brochure '12345' from repository
	Given In-memory query processor
	And Rdf from 'samplebrochure.ttl'
	When brochure <http://wikibus.org/brochure/12345> is fetched
	Then the brochure should have title 'Jelcz M11 - nowość'
