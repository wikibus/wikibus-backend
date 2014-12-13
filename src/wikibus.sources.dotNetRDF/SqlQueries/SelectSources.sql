SELECT [Id]
      ,CASE [SourceType]
        WHEN 'folder' THEN 'Brochure'
        WHEN 'book' THEN 'Book'
        WHEN 'file' THEN 'File'
        WHEN 'magissue' THEN 'Issue'
       END as [Type]
      ,CASE [SourceType]
        WHEN 'folder' THEN 'brochure'
        WHEN 'book' THEN 'book'
        WHEN 'file' THEN 'file'
        WHEN 'magissue' THEN 'issue'
       END as [TypeLower]
      ,[Language]
      ,[Language2]
      ,[Pages]
      ,[Year]
      ,[Month]
      ,[Day]
      ,[Notes]
      ,[FolderCode]
      ,[FolderName]
      ,[BookTitle]
      ,[BookAuthor]
      ,RTRIM([BookISBN]) as [BookISBN]
      ,[MagIssueMagazine]
      ,[MagIssueNumber]
      ,[FileMimeType]
      ,[Url]
      ,[FileName]
  FROM [Sources].[Source]