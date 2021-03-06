﻿CREATE TABLE [dbo].[PlayerShipModel](
	[ModelId] [int] IDENTITY(1,1) NOT NULL,
	[ModelName] [varchar](25) NOT NULL,
	[PlayerId] [int] NOT NULL,
 CONSTRAINT [PK_PlayerShipModel] PRIMARY KEY CLUSTERED 
(
	[ModelId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

