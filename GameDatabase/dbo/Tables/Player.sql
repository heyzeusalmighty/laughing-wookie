CREATE TABLE [dbo].[Player] (
    [PlayerId]       INT          IDENTITY (1, 1) NOT NULL,
    [Username]       VARCHAR (50) NOT NULL,
    [GameId]         INT          NOT NULL,
    [DiscColor]      VARCHAR (12) NOT NULL,
    [CurrentOrange]  INT          NOT NULL,
    [CurrentBrown]   INT          NOT NULL,
    [CurrentPink]    INT          NOT NULL,
    [OrangeIncome]   INT          NOT NULL,
    [BrownIncome]    INT          NOT NULL,
    [PinkIncome]     INT          NOT NULL,
    [Pass]           BIT          CONSTRAINT [DF_Player_Pass] DEFAULT ((0)) NOT NULL,
    [TurnOrder]      INT          NOT NULL,
    [TotalDiscs]     INT          NOT NULL,
    [AvailableDiscs] INT          NOT NULL,
    [UserId]         INT          NOT NULL,
    CONSTRAINT [PK_Player] PRIMARY KEY CLUSTERED ([PlayerId] ASC)
);

