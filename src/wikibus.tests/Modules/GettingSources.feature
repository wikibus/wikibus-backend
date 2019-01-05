Feature: Getting Sources

Scenario Outline: GETting sources
   Given Accept header is 'text/turtle'
   And existing <type> 'https://wikibus.org<path>'
   When I GET resource '<path>' 
   Then response should have status 200
   And <type> 'https://wikibus.org<path>' should have been retrieved
   Examples:
   | type     | path                   |
   | brochure | /brochure/12345        |
   | book     | /book/123456abc        |
   | magazine | /magazine/Bus%20Kurier |

Scenario: GET inexistent brochure
   Given brochure 'https://wikibus.org/brochure/12345' doesn't exist
   When I GET resource 'https://wikibus.org/brochure/12345'
   Then response should have status 404

Scenario Outline: GET collection first page
   Given Accept header is 'text/turtle'
     And exisiting <type> collection
    When I GET resource '<path>'
    Then response should have status 200
     And page 1 of <type> collection should have been retrieved
Examples: 
    | type     | path       |
    | book     | /books     |
    | brochure | /brochures |
    | magazine | /magazines |

Scenario Outline: GET collection negative page
   Given Accept header is 'text/turtle'
	 And query string is
		| key  | value |
		| page | -1    |
    When I GET resource '<path>'
    Then response should have status 400
Examples: 
    | type     | path       |
    | book     | /books/-1  |
    | brochure | /brochures |
    | magazine | /magazines |

Scenario Outline: GET source collection Nth page
   Given Accept header is 'text/turtle'
     And exisiting <type> collection
	 And query string is
		| key  | value |
		| page | 25    |
    When I GET resource '<path>'
    Then response should have status 200
     And page 25 of <type> collection should have been retrieved
Examples: 
    | type     | path       |
    | book     | /books/25  |
    | brochure | /brochures |
    | magazine | /magazines |

Scenario Outline: GET images
   Given Accept header is '*/*'
     And exisiting image <id>
    When I GET resource '<url>'
    Then response should have status 200
     And content type should be 'image/jpeg'
     And image <id> should have been retrieved
Examples: 
     | url                      | id |
     | /book/10/image           | 10 |
     | /brochure/15/image       | 15 |
     | /book/10/image/small     | 10 |
     | /brochure/15/image/small | 15 |

Scenario Outline: GET issue image
   Given Accept header is '*/*'
     And exisiting image for Buses 66
    When I GET resource '<url>'
    Then response should have status 200
     And content type should be 'image/jpeg'
     And image Buses 66 should have been retrieved
Examples: 
     | url                                  |
     | /magazine/Buses/issue/66/image       |
     | /magazine/Buses/issue/66/image/small |

Scenario Outline: GET missing image
   Given Accept header is '*/*'
    When I GET resource '<url>'
    Then response should have status 404
Examples: 
     | url                                  |
     | /book/10/image/large                 |
     | /brochure/15/image/large             |
     | /magazine/Buses/issue/66/image/large |
     | /book/10/image/small                 |
     | /brochure/15/image/small             |
     | /magazine/Buses/issue/66/image/small |
