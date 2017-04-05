USE [AskAndAnswer]
GO
/****** Object:  Table [dbo].[locAddress]    Script Date: 4/4/2017 9:21:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[locAddress](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[strAddress] [varchar](200) NOT NULL,
 CONSTRAINT [PK_locAddress] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
