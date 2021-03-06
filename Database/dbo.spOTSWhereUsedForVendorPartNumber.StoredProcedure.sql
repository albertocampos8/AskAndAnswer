USE [AskAndAnswer]
GO
/****** Object:  StoredProcedure [dbo].[spOTSWhereUsedForVendorPartNumber]    Script Date: 12/31/2016 8:11:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alberto Campos
-- Create date: 11/5/2016
-- Description:	Performs a Where Used to find which OTSParts use a 
-- given Vendor Part Number ID
-- =============================================
CREATE PROCEDURE [dbo].[spOTSWhereUsedForVendorPartNumber] 
	@keyVendorPN bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT otsParts.ID,
		   otsParts.strPartNumber, 
		   otsParts.dtRequested, 
		   otsUsers.strName,
		   kvpBU.strBUCode,
		   otsProduct.strProduct
	FROM
		otsParts
	JOIN otsRelation
		ON otsRelation.keyOTSPart = otsParts.ID
	JOIN otsUsers
		ON otsUsers.ID = otsParts.keyRequestedBy
	JOIN kvpBU
		ON kvpBU.ID = otsUsers.keyBU
	JOIN otsProduct
		ON otsProduct.ID = otsParts.keyRequestedForProduct
	WHERE
		otsRelation.keyVendorPN = @keyVendorPN

END

GO
