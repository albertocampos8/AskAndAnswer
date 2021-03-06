USE [AskAndAnswer]
GO
/****** Object:  StoredProcedure [dbo].[spOTSGetPNInventoryInfo]    Script Date: 4/4/2017 9:21:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Alberto Campos
-- Create date: 3/30/20176
-- Description:	Returns two results:
-- 1) The total inventory of the given otsParts.ID (Result 1)
-- 2) The qty and location information for each Vendor PN reporting to a given otsParts.ID (Result 2)
-- =============================================
CREATE PROCEDURE [dbo].[spOTSGetPNInventoryInfo] 
	@pnID bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	--SET 1: The total number of parts
	SELECT COALESCE(SUM(COALESCE(invBulk.intQty,0)),0) AS OnHand FROM
		otsParts
		JOIN otsRelation
			ON otsParts.ID = otsRelation.keyOTSPart
		JOIN otsVendorPN
			ON otsVendorPN.ID = otsRelation.keyVendorPN
		JOIN invBulk
			ON otsVendorPN.ID = invBulk.keyBulkItem
	WHERE otsParts.ID = @pnID


	--Second result set as all the data in the Vendor PN for this ID
	SELECT otsVendorPN.ID AS VendorPNID, otsVendorPN.strVendorPartNumber, 
		otsVendor.strVendor,
		otsParts.ID, otsParts.strPartNumber,
		COALESCE(invBulk.keyLocationBulk,-1) AS keyLocationBulk,
		COALESCE(invBulk.keyOwner,-1) AS keyOwner,
		COALESCE(invBulk.intQty,0) AS OnHand,
		COALESCE(invSubInv.strSubInv,'') AS strSubInv,
		COALESCE(locLocation.strFloor,'') AS strFloor,
		COALESCE(locDetail.strDetail,'') AS strDetail,
		COALESCE(locAddress.strAddress,'') AS strAddress,
		COALESCE(locCity.strCity,'') AS strCity,
		COALESCE(locStateProvince.strStateProvince,'') AS strStateProvince,
		COALESCE(locCountry.strCountry,'') AS strCountry,
		COALESCE(invBulk.ID,-1) AS invBulkID
			FROM otsVendorPN 
		JOIN otsVendor
			ON otsVendorPN.keyVendor = otsVendor.ID
		JOIN otsRelation
			ON otsVendorPN.ID = otsRelation.keyVendorPN
		JOIN otsParts
			ON otsRelation.keyOTSPart = otsParts.ID
		LEFT JOIN invBulk
			ON otsVendorPN.ID = invBulk.keyBulkItem
		LEFT JOIN locLocation
			ON invBulk.keyLocationBulk = locLocation.ID
		LEFT JOIN locDetail
			ON locDetail.ID = locLocation.keyDetail
		LEFT JOIN locAddress
			ON locAddress.ID = locLocation.keyAddress
		LEFT JOIN locCity
			ON locCity.ID = locLocation.keyCity
		LEFT JOIN locStateProvince
			ON locStateProvince.ID = locLocation.keyStateProvince
		LEFT JOIN locCountry
			ON locCountry.ID = locLocation.keyCountry
		LEFT JOIN invSubInv
			ON invBulk.keySubInv = invSubInv.ID
	WHERE
		otsRelation.keyOTSPart = @pnID;

END


GO
