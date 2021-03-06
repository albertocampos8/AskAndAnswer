USE [AskAndAnswer]
GO
/****** Object:  StoredProcedure [dbo].[spInvGetPartHistory]    Script Date: 4/4/2017 9:21:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alberto Campos
-- Create date: 3/31/2017
-- Description:	Gets Inventory History of a part with otsParts.ID
-- =============================================
CREATE PROCEDURE [dbo].[spInvGetPartHistory] 
	@ID bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT otsVendorPN.strVendorPartNumber,
		   invHistory.dtTransaction,
		   invHistory.strComment,
		   invHistory.intDelta,
		   CHANGEDUSERS.strName AS ChangedBy, 
		   traType.strType,
		   COALESCE(dbo.fnGenerateFullAddress(
				locDetail.strDetail,
				locLocation.strFloor,
				locAddress.strAddress,
				locCity.strCity,
				locStateProvince.strStateProvince,
				locPostalCode.strPostalCode,
				locCountry.strCountry
			),'') AS FullAddress, 
		   CONTACTUSERS.strName AS Contact,
		   invSubInv.strSubInv
	FROM otsParts
		JOIN otsRelation ON
			otsParts.ID = otsRelation.keyOTSPart
		JOIN otsVendorPN ON
			otsRelation.keyVendorPN = otsVendorPN.ID
		JOIN invHistory ON
			otsVendorPN.ID = invHistory.keyBulkItem
		JOIN traType ON
			traType.ID = invHistory.keyTransactionType
		JOIN locLocation ON
			locLocation.ID = invHistory.keyLocationBulk
		JOIN locDetail ON
			locDetail.ID = locLocation.keyDetail
		JOIN locAddress ON
			locAddress.ID = locLocation.keyAddress
		JOIN locCity ON
			locCity.ID = locLocation.keyCity
		JOIN locStateProvince ON
			locStateProvince.ID = locLocation.keyStateProvince
		JOIN locPostalCode ON
			locPostalCode.ID = locLocation.keyPostalCode
		JOIN locCountry ON
			locCountry.ID = locLocation.keyCountry
		JOIN otsUsers CHANGEDUSERS ON
			invHistory.keyChangedBy = CHANGEDUSERS.ID
		JOIN otsUsers CONTACTUSERS ON
			invHistory.keyOwner = CONTACTUSERS.ID
		JOIN invSubInv ON
			invSubInv.ID = invHistory.keySubInv
	WHERE otsParts.ID = @ID
	ORDER BY invHistory.dtTransaction DESC, otsVendorPN.strVendorPartNumber ASC, invSubInv.strSubInv ASC

END

GO
