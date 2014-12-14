IF not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Sources].[Magazine]') AND type in (N'U'))

CREATE TABLE [Sources].[Magazine](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[SubName] [nvarchar](512) NULL
) ON [PRIMARY]

