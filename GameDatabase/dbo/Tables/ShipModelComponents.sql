CREATE TABLE [dbo].[ShipModelComponents] (
    [ComId]       INT IDENTITY (1, 1) NOT NULL,
    [ComponentId] INT NULL,
    [ShipId]      INT NULL,
    CONSTRAINT [PK_ShipModelComponents] PRIMARY KEY CLUSTERED ([ComId] ASC)
);

