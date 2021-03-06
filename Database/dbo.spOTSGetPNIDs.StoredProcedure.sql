USE [AskAndAnswer]
GO
/****** Object:  StoredProcedure [dbo].[spOTSGetPNIDs]    Script Date: 1/4/2017 9:51:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alberto Campos	
-- Create date: 12/27/2016
-- Description:	Given a csv list of Part Numbers, returns a dataset that consists of
-- 1) the Part Number's ID
-- 2) the Part Number
-- 3) the Part Number status code
-- Use this to help verify whether all part numbers in the string are defined in the database (and not obsolete)
-- =============================================
CREATE PROCEDURE [dbo].[spOTSGetPNIDs]
	-- Add the parameters for the stored procedure here
	@csvPN VARCHAR(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	DECLARE @bom TABLE
		(
			pn VARCHAR(MAX)
		)

	INSERT INTO @bom (pn)
		SELECT * FROM csvStringToTable(@csvPN,',')

	SELECT otsParts.ID, b.pn, otsParts.keyPartStatus
		FROM otsParts
		RIGHT JOIN @bom b
			ON otsParts.strPartNumber = b.pn
END

GO
