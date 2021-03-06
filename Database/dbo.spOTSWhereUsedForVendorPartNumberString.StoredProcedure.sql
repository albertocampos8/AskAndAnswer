USE [AskAndAnswer]
GO
/****** Object:  StoredProcedure [dbo].[spOTSWhereUsedForVendorPartNumberString]    Script Date: 12/31/2016 8:11:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alberto Campos
-- Create date: 11/7/2016
-- Description:	Performs WHERE USED for a Vendor Part Number
-- (i.e., which OTS PNs call out a Vendor PN).
-- This is similar to WhereUSedForVendorPartNumber, except
-- it seaches by the actual Vendor Part Number string instead
-- of its Database ID
-- =============================================
CREATE PROCEDURE [dbo].[spOTSWhereUsedForVendorPartNumberString]
	@strVendorPartNumber varchar(300)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT
		   otsParts.ID,
		   otsParts.strPartNumber, 
		   otsParts.dtRequested, 
		   otsUsers.strName,
		   kvpBU.strBUCode,
		   otsProduct.strProduct
	FROM
		otsParts
	JOIN otsRelation
		ON otsRelation.keyOTSPart = otsParts.ID
	JOIN otsVendorPN
		ON otsRelation.keyVendorPN = otsVendorPN.ID
	JOIN otsUsers
		ON otsUsers.ID = otsParts.keyRequestedBy
	JOIN kvpBU
		ON kvpBU.ID = otsUsers.keyBU
	JOIN otsProduct
		ON otsProduct.ID = otsParts.keyRequestedForProduct
	WHERE
		otsVendorPN.strVendorPartNumber = @strVendorPartNumber
END

GO
