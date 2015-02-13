CREATE TABLE [dbo].[PlayerTrack] (
    [TrackId]  INT          IDENTITY (1, 1) NOT NULL,
    [PlayerId] INT          NOT NULL,
    [Track]    VARCHAR (25) NOT NULL,
    [TileId]   INT          NOT NULL,
    [Position] INT          NOT NULL,
    CONSTRAINT [PK_PlayerTrack] PRIMARY KEY CLUSTERED ([TrackId] ASC)
);

