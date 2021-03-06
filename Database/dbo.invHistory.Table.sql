USE [AskAndAnswer]
GO
/****** Object:  Table [dbo].[invHistory]    Script Date: 4/4/2017 9:21:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[invHistory](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[keyBulkItem] [bigint] NOT NULL,
	[keyChangedBy] [bigint] NOT NULL,
	[dtTransaction] [datetime] NOT NULL,
	[intDelta] [int] NOT NULL,
	[strComment] [varchar](500) NOT NULL,
	[keyTransactionType] [tinyint] NOT NULL,
	[keyLocationBulk] [int] NOT NULL,
	[keyOwner] [bigint] NOT NULL,
	[keySubInv] [int] NOT NULL,
 CONSTRAINT [PK_invHistory] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[invHistory] ADD  CONSTRAINT [DF_invHistory_dtTransaction]  DEFAULT (getdate()) FOR [dtTransaction]
GO
ALTER TABLE [dbo].[invHistory]  WITH CHECK ADD  CONSTRAINT [FK_invHistory_keyBulkItem_otsVendorPN_ID] FOREIGN KEY([keyBulkItem])
REFERENCES [dbo].[otsVendorPN] ([ID])
GO
ALTER TABLE [dbo].[invHistory] CHECK CONSTRAINT [FK_invHistory_keyBulkItem_otsVendorPN_ID]
GO
ALTER TABLE [dbo].[invHistory]  WITH CHECK ADD  CONSTRAINT [FK_invHistory_keyChangedBy_otsUsers_ID] FOREIGN KEY([keyChangedBy])
REFERENCES [dbo].[otsUsers] ([ID])
GO
ALTER TABLE [dbo].[invHistory] CHECK CONSTRAINT [FK_invHistory_keyChangedBy_otsUsers_ID]
GO
ALTER TABLE [dbo].[invHistory]  WITH CHECK ADD  CONSTRAINT [FK_invHistory_keyLocationBulk_locLocation_ID] FOREIGN KEY([keyLocationBulk])
REFERENCES [dbo].[locLocation] ([ID])
GO
ALTER TABLE [dbo].[invHistory] CHECK CONSTRAINT [FK_invHistory_keyLocationBulk_locLocation_ID]
GO
ALTER TABLE [dbo].[invHistory]  WITH CHECK ADD  CONSTRAINT [FK_invHistory_keyOwner_otsUsers_ID] FOREIGN KEY([keyOwner])
REFERENCES [dbo].[otsUsers] ([ID])
GO
ALTER TABLE [dbo].[invHistory] CHECK CONSTRAINT [FK_invHistory_keyOwner_otsUsers_ID]
GO
ALTER TABLE [dbo].[invHistory]  WITH CHECK ADD  CONSTRAINT [FK_invHistory_keySubInv_invSubInv_ID] FOREIGN KEY([keySubInv])
REFERENCES [dbo].[invSubInv] ([ID])
GO
ALTER TABLE [dbo].[invHistory] CHECK CONSTRAINT [FK_invHistory_keySubInv_invSubInv_ID]
GO
ALTER TABLE [dbo].[invHistory]  WITH CHECK ADD  CONSTRAINT [FK_invHistory_keyTransactionType_traType_ID] FOREIGN KEY([keyTransactionType])
REFERENCES [dbo].[traType] ([ID])
GO
ALTER TABLE [dbo].[invHistory] CHECK CONSTRAINT [FK_invHistory_keyTransactionType_traType_ID]
GO
