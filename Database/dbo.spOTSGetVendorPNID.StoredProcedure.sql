USE [AskAndAnswer]
GO
/****** Object:  StoredProcedure [dbo].[spOTSGetVendorPNID]    Script Date: 4/11/2017 4:39:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alberto Campos
-- Create date: 4/7/2017
-- Description:	Returns otsVendorPartNumber.ID for a given
-- 1) Part Number 2) Vendor Part Number 3) Vendor Name
-- =============================================
CREATE PROCEDURE [dbo].[spOTSGetVendorPNID]
	@strPartNumber varchar(100),
	@strVendorPartNumber varchar(300),
	@strVendor varchar(300)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT otsVendorPN.ID FROM
		otsVendorPN
	JOIN otsRelation ON
		otsRelation.keyVendorPN = otsVendorPN.ID
	JOIN otsParts ON
		otsParts.ID = otsRelation.keyOTSPart
	JOIN otsVendor ON
		otsVendor.ID = otsVendorPN.keyVendor
	WHERE
		otsParts.strPartNumber = @strPartNumber
		AND
		otsVendorPN.strVendorPartNumber = @strVendorPartNumber
		AND
		otsVendor.strVendor = @strVendor
END

GO
