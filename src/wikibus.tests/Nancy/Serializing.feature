Feature: Serializing to Rdf
	Test serializing models to various RDF serializations

@mytag
Scenario: Serialize simple model to JSON-LD
	Given A model of type 'Brochure':
	| Title | 'Jelcz M11 - mały, stary autobus'
	When model is serialized to 'JsonLd'
	Then json object should contain key 'title' with value 'Jelcz M11 - mały, stary autobus'
