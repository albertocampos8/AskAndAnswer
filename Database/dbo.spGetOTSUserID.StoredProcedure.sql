USE [AskAndAnswer]
GO
/****** Object:  StoredProcedure [dbo].[spGetOTSUserID]    Script Date: 4/11/2017 4:39:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alberto Campos
-- Create date: 4/7/2017
-- Description:	Get otsUsers.ID for a given user name
-- =============================================
CREATE PROCEDURE [dbo].[spGetOTSUserID] 
	@strName varchar(300)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT ID FROM otsUsers WHERE strName = @strName
END

GO
