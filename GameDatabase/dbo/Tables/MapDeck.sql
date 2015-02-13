CREATE TABLE [dbo].[MapDeck] (
    [MapDeckId] INT          IDENTITY (1, 1) NOT NULL,
    [MapId]     INT          NOT NULL,
    [Division]  INT          NOT NULL,
    [SortOrder] INT          NULL,
    [Revealed]  BIT          CONSTRAINT [DF_MapDeck_Revealed] DEFAULT ((0)) NOT NULL,
    [XCoords]   INT          NULL,
    [YCoords]   INT          NULL,
    [Occupied]  VARCHAR (25) NULL,
    [GameId]    INT          NOT NULL,
    CONSTRAINT [PK_MapDeck] PRIMARY KEY CLUSTERED ([MapDeckId] ASC)
);

