CREATE TABLE [dbo].[Game] (
    [GameId]         INT          IDENTITY (1, 1) NOT NULL,
    [Round]          INT          NOT NULL,
    [Status]         VARCHAR (50) NOT NULL,
    [CurrentPlayer]  INT          NULL,
    [GameIdentifier] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Game] PRIMARY KEY CLUSTERED ([GameId] ASC)
);

