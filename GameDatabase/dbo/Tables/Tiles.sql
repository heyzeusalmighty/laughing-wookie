CREATE TABLE [dbo].[Tiles] (
    [Id]           INT          IDENTITY (1, 1) NOT NULL,
    [XCoords]      INT          NOT NULL,
    [YCoords]      INT          NOT NULL,
    [TileId]       INT          NOT NULL,
    [ControlledBy] VARCHAR (50) NULL,
    CONSTRAINT [PK_Tiles] PRIMARY KEY CLUSTERED ([Id] ASC)
);

