USE [AskAndAnswer]
GO

/****** Object:  Table [dbo].[webDisplayFields]    Script Date: 10/9/2016 11:46:50 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[webDisplayFields](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[fkAppID] [bigint] NOT NULL,
	[intDisplayOrder] [smallint] NOT NULL,
	[fkControlType] [tinyint] NOT NULL,
	[lblPrompt] [nvarchar](1000) NOT NULL,
	[lblHelpMessage] [nvarchar](max) NOT NULL,
	[txtDefaultValue] [nvarchar](100) NOT NULL,
	[fkKVPGroupID] [bigint] NOT NULL,
	[blInitVisible] [bit] NOT NULL,
	[blInitEnabled] [bit] NOT NULL,
	[blSelectionRequired] [bit] NOT NULL,
	[intIDCode] [smallint] NOT NULL,
 CONSTRAINT [PK_webDisplayFields] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[webDisplayFields] ADD  CONSTRAINT [DF_Table_1_intControlType]  DEFAULT ((1)) FOR [fkControlType]
GO

ALTER TABLE [dbo].[webDisplayFields] ADD  CONSTRAINT [DF_Table_1_intKVPIGroupID]  DEFAULT ((1)) FOR [fkKVPGroupID]
GO

ALTER TABLE [dbo].[webDisplayFields] ADD  CONSTRAINT [DF_webDisplayFields_blInitVisible]  DEFAULT ((1)) FOR [blInitVisible]
GO

ALTER TABLE [dbo].[webDisplayFields] ADD  CONSTRAINT [DF_webDisplayFields_blInitEnabled]  DEFAULT ((1)) FOR [blInitEnabled]
GO

ALTER TABLE [dbo].[webDisplayFields] ADD  CONSTRAINT [DF_webDisplayFields_blSelectionRequired]  DEFAULT ((0)) FOR [blSelectionRequired]
GO

ALTER TABLE [dbo].[webDisplayFields] ADD  CONSTRAINT [DF_webDisplayFields_intAssignedID]  DEFAULT ((0)) FOR [intIDCode]
GO

ALTER TABLE [dbo].[webDisplayFields]  WITH CHECK ADD  CONSTRAINT [FK_webDisplayFields_fkAppID_webAppIDs_ID] FOREIGN KEY([fkAppID])
REFERENCES [dbo].[webAppIDs] ([ID])
GO

ALTER TABLE [dbo].[webDisplayFields] CHECK CONSTRAINT [FK_webDisplayFields_fkAppID_webAppIDs_ID]
GO

ALTER TABLE [dbo].[webDisplayFields]  WITH CHECK ADD  CONSTRAINT [FK_webDisplayFields_fkControlType_webControlTypes_ID] FOREIGN KEY([fkControlType])
REFERENCES [dbo].[webControlTypes] ([ID])
GO

ALTER TABLE [dbo].[webDisplayFields] CHECK CONSTRAINT [FK_webDisplayFields_fkControlType_webControlTypes_ID]
GO

ALTER TABLE [dbo].[webDisplayFields]  WITH CHECK ADD  CONSTRAINT [FK_webDisplayFields_fkKVPGroupID_webKVP_ID] FOREIGN KEY([fkKVPGroupID])
REFERENCES [dbo].[webKVP] ([ID])
GO

ALTER TABLE [dbo].[webDisplayFields] CHECK CONSTRAINT [FK_webDisplayFields_fkKVPGroupID_webKVP_ID]
GO

