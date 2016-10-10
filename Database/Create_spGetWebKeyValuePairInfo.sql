USE [AskAndAnswer]
GO

/****** Object:  StoredProcedure [dbo].[spGetWebKeyValuePairInfo]    Script Date: 10/9/2016 11:52:07 PM ******/
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

