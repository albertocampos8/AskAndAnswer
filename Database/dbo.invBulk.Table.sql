USE [AskAndAnswer]
GO
/****** Object:  Table [dbo].[invBulk]    Script Date: 4/3/2017 10:20:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[invBulk](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[keyLocationBulk] [int] NOT NULL,
	[keyBulkItem] [bigint] NOT NULL,
	[intQty] [int] NOT NULL,
	[keyOwner] [bigint] NOT NULL,
 CONSTRAINT [PK_invBulk] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[invBulk] ADD  CONSTRAINT [DF_invBulk_intQty]  DEFAULT ((0)) FOR [intQty]
GO
ALTER TABLE [dbo].[invBulk]  WITH CHECK ADD  CONSTRAINT [FK_invBulk_keyBulkItem_otsVendorPN_ID] FOREIGN KEY([keyBulkItem])
REFERENCES [dbo].[otsVendorPN] ([ID])
GO
ALTER TABLE [dbo].[invBulk] CHECK CONSTRAINT [FK_invBulk_keyBulkItem_otsVendorPN_ID]
GO
ALTER TABLE [dbo].[invBulk]  WITH CHECK ADD  CONSTRAINT [FK_invBulk_keyLocation_invLocation_ID] FOREIGN KEY([keyLocationBulk])
REFERENCES [dbo].[locLocation] ([ID])
GO
ALTER TABLE [dbo].[invBulk] CHECK CONSTRAINT [FK_invBulk_keyLocation_invLocation_ID]
GO
ALTER TABLE [dbo].[invBulk]  WITH CHECK ADD  CONSTRAINT [FK_invBulk_keyOwner_otsUsers_ID] FOREIGN KEY([keyOwner])
REFERENCES [dbo].[otsUsers] ([ID])
GO
ALTER TABLE [dbo].[invBulk] CHECK CONSTRAINT [FK_invBulk_keyOwner_otsUsers_ID]
GO
