﻿CREATE TABLE [dbo].[Game](
	[GameId] [int] IDENTITY(1,1) NOT NULL,
	[Round] [int] NOT NULL,
	[Status] [varchar](50) NOT NULL,
	[CurrentPlayer] [int] NULL,
	[GameIdentifier] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Game] PRIMARY KEY CLUSTERED 
(
	[GameId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

