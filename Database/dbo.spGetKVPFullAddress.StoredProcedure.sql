USE [AskAndAnswer]
GO
/****** Object:  StoredProcedure [dbo].[spGetKVPFullAddress]    Script Date: 4/5/2017 12:21:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alberto Campos
-- Create date: 3/30/2017
-- Description:	Returns a mapping of locLocation.ID to a formatted location string:
-- (Detail)(Floor) |Address |City| State |Postal Code| Country
-- =============================================
CREATE PROCEDURE [dbo].[spGetKVPFullAddress]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT locLocation.ID AS keyActualValue, 
	dbo.fnGenerateFullAddress(
		locDetail.strDetail,
		locLocation.strFloor,
		locAddress.strAddress,
		locCity.strCity,
		locStateProvince.strStateProvince,
		locPostalCode.strPostalCode,
		locCountry.strCountry
	)  AS valDisplayedValue FROM
	locLocation
	JOIN locCountry
		ON locCountry.ID = locLocation.keyCountry
	JOIN locPostalCode
		ON locPostalCode.ID = locLocation.keyPostalCode
	JOIN locStateProvince
		ON locStateProvince.ID = locLocation.keyStateProvince
	JOIN locCity
		ON locCity.ID = locLocation.keyCity
	JOIN locAddress
		ON locAddress.ID = locLocation.keyAddress
	JOIN locDetail
		ON locDetail.ID = locLocation.keyDetail
	ORDER BY locAddress.strAddress, locDetail.strDetail, locLocation.strFloor
END

GO
