SELECT Id, BookAuthor
      ,CASE 
        WHEN [Image] is null THEN 'image'
       END as [HasImage] 
from [Sources].[Source] 
where BookAuthor is not null