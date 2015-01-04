SELECT Id, BookAuthor
      ,CASE 
        WHEN [Image] is null THEN 'image'
       END as [HasImage]
      ,'book' as [SourceType]
from [Sources].[Source] 
where BookAuthor is not null