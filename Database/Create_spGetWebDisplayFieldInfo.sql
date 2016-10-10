USE [AskAndAnswer]
GO

/****** Object:  StoredProcedure [dbo].[spGetWebDisplayFieldInfo]    Script Date: 10/9/2016 11:51:44 PM ******/
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

