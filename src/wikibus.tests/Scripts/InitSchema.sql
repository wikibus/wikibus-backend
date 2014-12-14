IF NOT EXISTS (SELECT [schema_id] FROM [sys].[schemas] WHERE [name] = N'Sources')
EXECUTE ('CREATE SCHEMA [Sources]')