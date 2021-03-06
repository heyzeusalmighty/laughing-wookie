﻿CREATE TABLE [dbo].[ShipModelComponents](
	[ComId] [int] IDENTITY(1,1) NOT NULL,
	[ComponentId] [int] NULL,
	[ShipId] [int] NULL,
 CONSTRAINT [PK_ShipModelComponents] PRIMARY KEY CLUSTERED 
(
	[ComId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

