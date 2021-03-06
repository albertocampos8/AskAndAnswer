USE [AskAndAnswer]
GO
/****** Object:  StoredProcedure [dbo].[spLOCLookUpAddressByStreet]    Script Date: 4/3/2017 10:20:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alberto Campos
-- Create date: 3/29/2017
-- Description:	Returns all entries from locLocation that match a given street address;
-- use this to autocomplete all address fields.
--NOTE: This SP may return mutiple lines, since it only searches by the street address (e.g,
--1 street address could have multiple floors or details).
-- =============================================
CREATE PROCEDURE [dbo].[spLOCLookUpAddressByStreet] 
	@strAddress varchar(200)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT locAddress.strAddress,
	       locCity.strCity,
		   locStateProvince.strStateProvince,
		   locPostalCode.strPostalCode,
		   locCountry.strCountry,
		   locDetail.strDetail,
		   locLocation.strFloor,
		   locLocation.dtDefined,
		   otsUsers.strName
	FROM
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
		JOIN otsUsers
			ON otsUsers.ID = locLocation.keyDefinedBy
	WHERE locAddress.strAddress = @strAddress
END

GO
