USE [AskAndAnswer]
GO
/****** Object:  StoredProcedure [dbo].[spOTSGetPNInfo]    Script Date: 4/4/2017 9:21:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alberto Campos
-- Create date: 10/13/2016
-- Description:	Gets all information associated with a part number
-- given its ID
--NOTE: This returns TWO datasets.
-- The first dataset has the information about the Part Number.
-- The second dataset has the information about the Vendor Part Numbers associated
-- with the Part Number
-- =============================================
CREATE PROCEDURE [dbo].[spOTSGetPNInfo] 
	@pnID bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Dataset 1: Info about the part number
	SELECT otsParts.ID, otsParts.strPartNumber, otsParts.dtRequested, otsParts.strDescription, otsParts.strDescription2,
		otsParts.strParameters, otsParts.decMaxHeight, otsParts.decLowVolCost, otsParts.decHighVolCost,
		otsParts.decEngCost, otsParts.intVersion, otsParts.keyRequestedBy,
		otsUsers.strName,
		kvpBU.valDisplayedValue AS BU,
		otsPartType.strType,
		otsPartType.ID AS PTYPEID,
		otsPartSubType.strSubType,
		otsPartSubType.ID AS PSUBTYPEID,
		otsProduct.strProduct,
		otsPartStatus.strStatus,
		otsEnvironCode.strECodeName
	FROM otsParts
		JOIN otsUsers
			ON otsParts.keyRequestedBy = otsUsers.ID
		JOIN kvpBU
			ON otsUsers.keyBU = kvpBU.ID
		JOIN otsPartType
			ON otsParts.keyType = otsPartType.ID
		JOIN otsPartSubType
			ON otsParts.keySubType = otsPartSubType.ID
		JOIN otsProduct
			ON otsParts.keyRequestedForProduct = otsProduct.ID
		JOIN otsPartStatus
			ON otsParts.keyPartStatus = otsPartStatus.ID
		JOIN otsEnvironCode
			ON otsParts.keyOTSEnvironCode = otsEnvironCode.ID
	WHERE otsParts.ID = @pnID;

	--Second result set as all the data in the Vendor PN for this ID
	SELECT otsVendorPN.ID AS VendorPNID, otsVendorPN.strVendorPartNumber, 
		otsVendorPN.keyVendorPartStatus,otsVendorPN.decLowVolCost,
		otsVendorPN.decHighVolCost, otsVendorPN.decEngCost, 
		otsVendorPN.strDatasheetURL, otsVendorPN.keyVendorEnvironCode,
		otsVendorPN.decMaxHeight, otsVendorPN.intVersion,
		otsVendor.strVendor,
		otsVendor.keyVendorStatus,
		otsVendorStatus.strVendorStatus,
		otsParts.ID, otsParts.strPartNumber, otsParts.keyRequestedBy,
		otsPartStatus.strStatus,
		otsEnvironCode.strECodeName
			FROM otsVendorPN 
		JOIN otsVendor
			ON otsVendorPN.keyVendor = otsVendor.ID
		JOIN otsVendorStatus
			ON otsVendor.keyVendorStatus = otsVendorStatus.ID
		JOIN otsRelation
			ON otsVendorPN.ID = otsRelation.keyVendorPN
		JOIN otsParts
			ON otsRelation.keyOTSPart = otsParts.ID
		JOIN otsPartStatus
			ON otsVendorPN.keyVendorPartStatus = otsPartStatus.ID
		JOIN otsEnvironCode
			ON otsVendorPN.keyVendorEnvironCode = otsEnvironCode.ID
	WHERE
		otsRelation.keyOTSPart = @pnID;

END

GO
