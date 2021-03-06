USE [AskAndAnswer]
GO
/****** Object:  Table [dbo].[locLocation]    Script Date: 4/4/2017 9:21:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[locLocation](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[keyCountry] [int] NOT NULL,
	[keyStateProvince] [int] NOT NULL,
	[keyCity] [int] NOT NULL,
	[keyAddress] [int] NOT NULL,
	[keyDetail] [int] NOT NULL,
	[keyDefinedBy] [bigint] NOT NULL,
	[dtDefined] [datetime] NOT NULL,
	[strFloor] [varchar](50) NOT NULL,
	[keyPostalCode] [int] NOT NULL,
 CONSTRAINT [PK_locLocation] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[locLocation] ADD  CONSTRAINT [DF_locLocation_dtDefined]  DEFAULT (getdate()) FOR [dtDefined]
GO
ALTER TABLE [dbo].[locLocation]  WITH CHECK ADD  CONSTRAINT [FK_locLocation_keyAddress_locAddress_ID] FOREIGN KEY([keyAddress])
REFERENCES [dbo].[locAddress] ([ID])
GO
ALTER TABLE [dbo].[locLocation] CHECK CONSTRAINT [FK_locLocation_keyAddress_locAddress_ID]
GO
ALTER TABLE [dbo].[locLocation]  WITH CHECK ADD  CONSTRAINT [FK_locLocation_keyCity_locCity_ID] FOREIGN KEY([keyCity])
REFERENCES [dbo].[locCity] ([ID])
GO
ALTER TABLE [dbo].[locLocation] CHECK CONSTRAINT [FK_locLocation_keyCity_locCity_ID]
GO
ALTER TABLE [dbo].[locLocation]  WITH CHECK ADD  CONSTRAINT [FK_locLocation_keyCountry_locCountry_ID] FOREIGN KEY([keyCountry])
REFERENCES [dbo].[locCountry] ([ID])
GO
ALTER TABLE [dbo].[locLocation] CHECK CONSTRAINT [FK_locLocation_keyCountry_locCountry_ID]
GO
ALTER TABLE [dbo].[locLocation]  WITH CHECK ADD  CONSTRAINT [FK_locLocation_keyDetail_locDetail_ID] FOREIGN KEY([keyDetail])
REFERENCES [dbo].[locDetail] ([ID])
GO
ALTER TABLE [dbo].[locLocation] CHECK CONSTRAINT [FK_locLocation_keyDetail_locDetail_ID]
GO
ALTER TABLE [dbo].[locLocation]  WITH CHECK ADD  CONSTRAINT [FK_locLocation_keyPostalCode_locPostalCode_ID] FOREIGN KEY([keyPostalCode])
REFERENCES [dbo].[locPostalCode] ([ID])
GO
ALTER TABLE [dbo].[locLocation] CHECK CONSTRAINT [FK_locLocation_keyPostalCode_locPostalCode_ID]
GO
ALTER TABLE [dbo].[locLocation]  WITH CHECK ADD  CONSTRAINT [FK_locLocation_keyStateProvince_locStateProvince_ID] FOREIGN KEY([keyStateProvince])
REFERENCES [dbo].[locStateProvince] ([ID])
GO
ALTER TABLE [dbo].[locLocation] CHECK CONSTRAINT [FK_locLocation_keyStateProvince_locStateProvince_ID]
GO
