CREATE TABLE [dbo].[tblMovie]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Title] varchar(50) NOT NULL,
	[Description] varchar(250) NOT NULL, 
    [Cost] FLOAT NOT NULL,
	[RatingId] INT NOT NULL,
	[FormatId] INT NOT NULL,
	[DirectorId] INT NOT NULL,
	[InStkQty] INT NOT NULL,
	[ImagePath] VARCHAR(250)
	
)
