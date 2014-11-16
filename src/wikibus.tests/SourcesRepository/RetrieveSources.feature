Feature: Retrieve sources from repository

Scenario: Get brochure from repository
	Given Rdf from 'samplebrochure.ttl'
	When brochure <http://wikibus.org/brochure/12345> is fetched
	Then the brochure should have title 'Jelcz M11 - nowość'
