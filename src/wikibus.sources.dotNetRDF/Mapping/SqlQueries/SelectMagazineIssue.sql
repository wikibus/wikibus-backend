SELECT i.[Id]
      ,[SourceType]
      ,[Language]
      ,[Language2]
      ,[Pages]
      ,[Year]
      ,[Month]
      ,[Day]
      ,[Notes]
      ,[MagIssueMagazine]
      ,[MagIssueNumber]
      ,m.[Name] as [Magazine]
      ,CASE 
        WHEN [Image] is not null THEN cast(1 as bit)
       END as [HasImage]
  FROM [Sources].[Source] i
  JOIN [Sources].[Magazine] m
    ON i.[MagIssueMagazine] = m.[Id]
  WHERE [SourceType] = 'magissue' and [MagIssueNumber] is not null