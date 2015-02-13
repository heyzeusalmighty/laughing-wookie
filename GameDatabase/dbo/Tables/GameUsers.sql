CREATE TABLE [dbo].[GameUsers] (
    [UserId]       INT          IDENTITY (1, 1) NOT NULL,
    [UserName]     VARCHAR (50) NULL,
    [EmailAddress] VARCHAR (50) NULL,
    CONSTRAINT [PK_GameUsers] PRIMARY KEY CLUSTERED ([UserId] ASC)
);

