﻿CREATE TABLE [dbo].[MangaRatings]
(
	[Id] INT NOT NULL IDENTITY PRIMARY KEY,
    [Name] NVARCHAR(MAX) NOT NULL,
    [OriginalName] NVARCHAR(MAX) NULL,
    [Rating] INT NOT NULL CHECK ([Rating] BETWEEN 0 AND 10), 
    [Link] NVARCHAR(MAX) NULL
)
