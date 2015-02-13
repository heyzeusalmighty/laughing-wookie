CREATE TABLE [dbo].[ScienceTrack] (
    [Id]        INT IDENTITY (1, 1) NOT NULL,
    [GameId]    INT NOT NULL,
    [PlayerId]  INT NOT NULL,
    [ScienceId] INT NOT NULL,
    CONSTRAINT [PK_ScienceTrack] PRIMARY KEY CLUSTERED ([Id] ASC)
);

