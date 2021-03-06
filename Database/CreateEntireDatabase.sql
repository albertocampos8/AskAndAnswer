
GO
CREATE DATABASE [AskAndAnswer]
 CONTAINMENT = NONE

GO
ALTER DATABASE [AskAndAnswer] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AskAndAnswer].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AskAndAnswer] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AskAndAnswer] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AskAndAnswer] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AskAndAnswer] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AskAndAnswer] SET ARITHABORT OFF 
GO
ALTER DATABASE [AskAndAnswer] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [AskAndAnswer] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [AskAndAnswer] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AskAndAnswer] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AskAndAnswer] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AskAndAnswer] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AskAndAnswer] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AskAndAnswer] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AskAndAnswer] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AskAndAnswer] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AskAndAnswer] SET  DISABLE_BROKER 
GO
ALTER DATABASE [AskAndAnswer] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AskAndAnswer] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AskAndAnswer] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AskAndAnswer] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AskAndAnswer] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AskAndAnswer] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [AskAndAnswer] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AskAndAnswer] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [AskAndAnswer] SET  MULTI_USER 
GO
ALTER DATABASE [AskAndAnswer] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AskAndAnswer] SET DB_CHAINING OFF 
GO
ALTER DATABASE [AskAndAnswer] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [AskAndAnswer] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [AskAndAnswer]
GO
/****** Object:  StoredProcedure [dbo].[spGetWebDisplayFieldInfo]    Script Date: 10/20/2016 1:19:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alberto Campos
-- Create date: 10/1/2016
-- Description:	Returns all information for Web Controls from webDisplayFields whose fkAppID matches the supplied ID
-- =============================================
CREATE PROCEDURE [dbo].[spGetWebDisplayFieldInfo]
	-- Add the parameters for the stored procedure here
	@fkAppID bigint

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM webDisplayFields WHERE fkAppID = @fkAppID ORDER BY intDisplayOrder
END

GO
/****** Object:  StoredProcedure [dbo].[spGetWebKeyValuePairInfo]    Script Date: 10/20/2016 1:19:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alberto Campos
-- Create date: 10/1/16
-- Description:	Retrieves key-value pair information from webKVP where fkGroupID matches the supplied ID
-- =============================================
CREATE PROCEDURE [dbo].[spGetWebKeyValuePairInfo]
	-- Add the parameters for the stored procedure here
	@fkGroupID bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM webKVP WHERE fkGroupID = @fkGroupID ORDER BY intDisplayOrder
END

GO
/****** Object:  Table [dbo].[webAppIDs]    Script Date: 10/20/2016 1:19:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[webAppIDs](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_webAppIDs] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[webControlTypes]    Script Date: 10/20/2016 1:19:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[webControlTypes](
	[ID] [tinyint] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_webControlTypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[webDisplayFields]    Script Date: 10/20/2016 1:19:19 AM ******/
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
	[fkKVPGroupIDDecoder] [bigint] NOT NULL,
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
/****** Object:  Table [dbo].[webKVP]    Script Date: 10/20/2016 1:19:19 AM ******/
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
/****** Object:  Table [dbo].[webKVPGroupIDDecoder]    Script Date: 10/20/2016 1:19:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[webKVPGroupIDDecoder](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](1000) NOT NULL,
 CONSTRAINT [PK_webKVPGroupIDDecoder] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[webDisplayFields] ADD  CONSTRAINT [DF_Table_1_intControlType]  DEFAULT ((1)) FOR [fkControlType]
GO
ALTER TABLE [dbo].[webDisplayFields] ADD  CONSTRAINT [DF_Table_1_intKVPIGroupID]  DEFAULT ((1)) FOR [fkKVPGroupIDDecoder]
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
ALTER TABLE [dbo].[webDisplayFields]  WITH CHECK ADD  CONSTRAINT [FK_webDisplayFields_fkKVPGroupIDDecoder_webKVPGroupIDDecoder_ID] FOREIGN KEY([fkKVPGroupIDDecoder])
REFERENCES [dbo].[webKVPGroupIDDecoder] ([ID])
GO
ALTER TABLE [dbo].[webDisplayFields] CHECK CONSTRAINT [FK_webDisplayFields_fkKVPGroupIDDecoder_webKVPGroupIDDecoder_ID]
GO
ALTER TABLE [dbo].[webKVP]  WITH CHECK ADD  CONSTRAINT [FK_webKVP_fkGroupID_webKVPGroupIDDecoder_ID] FOREIGN KEY([fkGroupID])
REFERENCES [dbo].[webKVPGroupIDDecoder] ([ID])
GO
ALTER TABLE [dbo].[webKVP] CHECK CONSTRAINT [FK_webKVP_fkGroupID_webKVPGroupIDDecoder_ID]
GO
USE [master]
GO
ALTER DATABASE [AskAndAnswer] SET  READ_WRITE 
GO
