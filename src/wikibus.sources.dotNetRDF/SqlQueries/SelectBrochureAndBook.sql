SELECT [Id]
      ,CASE [SourceType]
        WHEN 'folder' THEN 'Brochure'
        WHEN 'book' THEN 'Book'
        WHEN 'file' THEN 'File'
       END as [Type]
      ,CASE [SourceType]
        WHEN 'folder' THEN 'brochure'
        WHEN 'book' THEN 'book'
        WHEN 'file' THEN 'file'
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
      ,[FileMimeType]
      ,[Url]
      ,[FileName]
      ,CASE 
        WHEN [Image] is null THEN 'image'
       END as [HasImage]
  FROM [Sources].[Source]
  WHERE [SourceType] <> 'magissue'