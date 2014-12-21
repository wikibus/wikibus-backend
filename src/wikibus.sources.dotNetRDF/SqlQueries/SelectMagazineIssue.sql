SELECT i.[Id]
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
  FROM [Sources].[Source] i
  JOIN [Sources].[Magazine] m
    ON i.[MagIssueMagazine] = m.[Id]
  WHERE [SourceType] = 'magissue'