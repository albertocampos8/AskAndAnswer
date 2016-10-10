USE [AskAndAnswer]
GO

/****** Object:  Table [dbo].[webKVP]    Script Date: 10/9/2016 11:47:08 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[webKVP](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[fkGroupID] [bigint] NOT NULL,
	[intDisplayOrder] [int] NOT NULL,
	[keyActualValue] [nvarchar](100) NOT NULL,
	[valDisplayedValue] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_webKVP] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[webKVP]  WITH CHECK ADD  CONSTRAINT [FK_webKVP_fkGroupID_webKVPGroupIDDecoder_ID] FOREIGN KEY([fkGroupID])
REFERENCES [dbo].[webKVPGroupIDDecoder] ([ID])
GO

ALTER TABLE [dbo].[webKVP] CHECK CONSTRAINT [FK_webKVP_fkGroupID_webKVPGroupIDDecoder_ID]
GO

