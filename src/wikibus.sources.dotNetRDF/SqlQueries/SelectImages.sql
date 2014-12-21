SELECT s.Id
      ,m.Name as Magazine
      ,s.MagIssueNumber
      ,CASE [SourceType]
        WHEN 'folder' THEN 'brochure'
        WHEN 'book' THEN 'book'
        WHEN 'file' THEN 'file'
       END as [TypeLower]
from [Sources].[Source] s
left join [Sources].[Magazine] m on m.Id = s.MagIssueMagazine
where Image is not null