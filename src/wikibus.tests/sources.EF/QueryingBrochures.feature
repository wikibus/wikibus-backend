@SQL @EF
Feature: Retrieving data with SQL repository
   
@Brochure
Scenario: Retrieving brochure row with two languages
   Given table Sources.Source with data:
      | Id | SourceType | Language | Language2 | Pages | FolderName              |
      | 1  | folder     | tr       | en        | 2     | Türkkar City Angel E.D. |
	 And data is inserted
   When getting Brochure <http://wikibus.org/brochure/1> 
   Then Id should be <http://wikibus.org/brochure/1>
   And Brochure should match
      | Property | Value                   |
      | Pages    | 2                       |
      | Title    | Türkkar City Angel E.D. |
	And Languages should contain tr
	And Languages should contain en

@Brochure
Scenario: Retrieving brochure which doesn't exist
   Given table Sources.Source with data:
      | Id | SourceType | Language | Language2 | Pages | FolderName              |
      | 1  | folder     | tr       | en        | 2     | Türkkar City Angel E.D. |
	 And data is inserted
   When getting Brochure <http://wikibus.org/brochure/2> 
   Then Brochure should be null
	
@Book
Scenario: Retrieving brochure with image
   Given table Sources.Source with data:
      | Id  | SourceType | Image    |
      | 2   | folder     | 3qAAAA== |
	 And data is inserted
   When getting Brochure <http://wikibus.org/brochure/2> 
   Then Should have image http://wikibus.org/brochure/2/image
	
@Book
Scenario: Retrieving book with image
   Given table Sources.Source with data:
      | Id  | SourceType | Image    |
      | 407 | book       | 3qAAAA== |
	 And data is inserted
   When getting Book <http://wikibus.org/book/407>
   Then Should have image http://wikibus.org/book/407/image
      
@Brochure
Scenario: Getting brochure row with date
   Given table Sources.Source with data:
      | Id | SourceType | Language | Language2 | Pages | Year | Month | Day | FolderCode                         | FolderName                                                |
      | 6  | folder     | pl       | NULL      | 2     | 2006 | 9     | 21  | BED 81419 2006-09-21 POL Version 2 | Fakty: Autobus turystyczny Volvo B9r/Sunsundegui Elegance |
	 And data is inserted
   When getting Brochure <http://wikibus.org/brochure/6>
   Then Brochure should match
      | Property | Value                                                     |
      | Pages    | 2                                                         |
      | Year     | 2006                                                      |
      | Month    | 9                                                         |
      | Code     | BED 81419 2006-09-21 POL Version 2                        |
      | Title    | Fakty: Autobus turystyczny Volvo B9r/Sunsundegui Elegance |
   And Date should be 2006-09-21

@Book
Scenario: Getting complete book row
   Given table Sources.Source with data:
         | Id  | SourceType | Language | Language2 | Pages | Year | BookTitle                                       | BookAuthor        | BookISBN      | 
         | 407 | book       | pl       | NULL      | 140   | 2010 | Pojazdy samochodowe i przyczepy Jelcz 1952-1970 | Wojciech Polomski | 9788320617412 |
	 And data is inserted
   When getting Book <http://wikibus.org/book/407>
   Then Book should match
      | Property | Value                                           |
      | Pages    | 140                                             |
      | Year     | 2010                                            |
      | ISBN     | 9788320617412                                   |
      | Title    | Pojazdy samochodowe i przyczepy Jelcz 1952-1970 |
	And Languages should contain pl
	And Author should be 'Wojciech Polomski'

@MagazineIssue
Scenario: Getting complete magazine issue
   Given table Sources.Source with data:
         | Id  | SourceType | Language | Pages | Year | Month | MagIssueMagazine | MagIssueNumber | Image    |
         | 324 | magissue   | pl       | 16    | 2007 | 3     | 1                | 13             | 3qAAAA== |
     And table Sources.Magazine with data:
         | Id | Name       |
         | 1  | Bus Kurier |
	 And data is inserted
    When Getting issue <http://wikibus.org/magazine/Bus%20Kurier/issue/13>
    Then Languages should contain pl
     And Issue should match
      | Property | Value |
      | Number   | 13    |
      | Year     | 2007  |
      | Pages    | 16    |
      | Month    | 3     |
    And Magazine is <http://wikibus.org/magazine/Bus%20Kurier>