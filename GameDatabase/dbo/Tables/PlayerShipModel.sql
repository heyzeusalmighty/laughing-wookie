CREATE TABLE [dbo].[PlayerShipModel] (
    [ModelId]   INT          IDENTITY (1, 1) NOT NULL,
    [ModelName] VARCHAR (25) NOT NULL,
    [PlayerId]  INT          NOT NULL,
    CONSTRAINT [PK_PlayerShipModel] PRIMARY KEY CLUSTERED ([ModelId] ASC)
);

