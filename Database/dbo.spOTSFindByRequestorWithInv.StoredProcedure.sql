USE [AskAndAnswer]
GO
/****** Object:  StoredProcedure [dbo].[spOTSFindByRequestorWithInv]    Script Date: 4/4/2017 9:21:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Alberto Campos
-- Create date: 3/29/2017
-- Description:	Selects Part Number, Description, Part Status, 
-- Requestor, Requestor BU, and Date Requested based on Supplied
-- Requestor ID
-- Also returns the sum of all quantities in invBulk for each match.
-- =============================================
CREATE PROCEDURE [dbo].[spOTSFindByRequestorWithInv]
	@searchID bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT otsParts.ID, otsParts.strPartNumber, otsParts.dtRequested, otsParts.strDescription,
		otsPartStatus.strStatus, otsUsers.strName, kvpBU.valDisplayedValue, 
		otsProduct.strProduct, SUM(COALESCE(invBulk.intQty,0)) AS OnHand
	FROM otsParts 
		JOIN otsPartStatus ON
			otsParts.keyPartStatus = otsPartStatus.ID
		JOIN otsProduct ON
			otsProduct.ID = otsParts.keyRequestedForProduct
		JOIN otsUsers ON
			otsUsers.ID = otsParts.keyRequestedBy
		JOIN kvpBU ON
			otsUsers.keyBU = kvpBU.ID
		JOIN otsRelation ON
			otsParts.ID = otsRelation.keyOTSPart
		JOIN otsVendorPN ON
			otsRelation.keyVendorPN = otsVendorPN.ID
		LEFT JOIN invBulk ON
			invBulk.keyBulkItem = otsVendorPN.ID
	WHERE otsUsers.ID = @searchID
	GROUP BY otsParts.ID, otsParts.strPartNumber, otsParts.dtRequested, otsParts.strDescription, otsPartStatus.strStatus,
		otsUsers.strName, kvpBU.valDisplayedValue, otsProduct.strProduct	
	ORDER BY otsParts.dtRequested DESC;
END


GO
