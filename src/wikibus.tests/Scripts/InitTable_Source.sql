IF not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sources].[Source]') AND type in (N'U'))

CREATE TABLE [Sources].[Source](
	[Id] [int] NOT NULL,
	[SourceType] [nvarchar](8) NOT NULL,
	[Language] [nvarchar](10) NULL,
	[Language2] [nvarchar](10) NULL,
	[Pages] [int] NULL,
	[Year] [smallint] NULL,
	[Month] [tinyint] NULL,
	[Day] [tinyint] NULL,
	[Notes] [nvarchar](512) NULL,
	[Image] [image] NULL,
	[FileCabinet] [int] NULL,
	[FileOffset] [int] NULL,
	[FolderCode] [nvarchar](128) NULL,
	[FolderName] [nvarchar](512) NULL,
	[BookTitle] [nvarchar](128) NULL,
	[BookAuthor] [nvarchar](64) NULL,
	[BookISBN] [nchar](13) NULL,
	[MagIssueMagazine] [int] NULL,
	[MagIssueNumber] [int] NULL,
	[FileMimeType] [nvarchar](32) NULL,
	[FileContents] [varbinary](max) NULL,
	[Url] [nvarchar](1024) NULL,
	[FileName] [nvarchar](256) NULL
) ON [PRIMARY] 
