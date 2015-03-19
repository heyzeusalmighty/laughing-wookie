CREATE TABLE [dbo].[Player](
	[PlayerId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[GameId] [int] NOT NULL,
	[DiscColor] [varchar](12) NOT NULL,
	[CurrentOrange] [int] NOT NULL,
	[CurrentBrown] [int] NOT NULL,
	[CurrentPink] [int] NOT NULL,
	[OrangeIncome] [int] NOT NULL,
	[BrownIncome] [int] NOT NULL,
	[PinkIncome] [int] NOT NULL,
	[Pass] [bit] NOT NULL,
	[TurnOrder] [int] NOT NULL,
	[TotalDiscs] [int] NOT NULL,
	[AvailableDiscs] [int] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_Player] PRIMARY KEY CLUSTERED 
(
	[PlayerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


GO
ALTER TABLE [dbo].[Player] ADD  CONSTRAINT [DF_Player_Pass]  DEFAULT ((0)) FOR [Pass]