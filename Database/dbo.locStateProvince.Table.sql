USE [AskAndAnswer]
GO
/****** Object:  Table [dbo].[locStateProvince]    Script Date: 4/3/2017 10:20:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[locStateProvince](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[strStateProvince] [varchar](50) NOT NULL,
 CONSTRAINT [PK_locStateProvince] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
