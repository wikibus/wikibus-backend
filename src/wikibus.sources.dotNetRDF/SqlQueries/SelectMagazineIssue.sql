SELECT [Language]
      ,[Language2]
      ,[Pages]
      ,[Year]
      ,[Month]
      ,[Day]
      ,[Notes]
      ,CASE 
        WHEN [Image] is not null THEN 'image'
       END as [HasImage]
      ,[MagIssueMagazine]
      ,[MagIssueNumber]
      ,m.[Name] as [Magazine]
  FROM [Sources].[Source] i
  JOIN [Sources].[Magazine] m
    ON i.[MagIssueMagazine] = m.[Id]
  WHERE [SourceType] = 'magissue'