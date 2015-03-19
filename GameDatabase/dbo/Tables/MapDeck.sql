CREATE TABLE [dbo].[MapDeck](
	[MapDeckId] [int] IDENTITY(1,1) NOT NULL,
	[MapId] [int] NOT NULL,
	[Division] [int] NOT NULL,
	[SortOrder] [int] NULL,
	[Revealed] [bit] NOT NULL,
	[XCoords] [int] NULL,
	[YCoords] [int] NULL,
	[Occupied] [varchar](25) NULL,
	[GameId] [int] NOT NULL,
 CONSTRAINT [PK_MapDeck] PRIMARY KEY CLUSTERED 
(
	[MapDeckId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


GO
ALTER TABLE [dbo].[MapDeck] ADD  CONSTRAINT [DF_MapDeck_Revealed]  DEFAULT ((0)) FOR [Revealed]