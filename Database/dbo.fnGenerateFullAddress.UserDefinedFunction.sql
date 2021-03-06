USE [AskAndAnswer]
GO
/****** Object:  UserDefinedFunction [dbo].[fnGenerateFullAddress]    Script Date: 4/3/2017 10:20:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Alberto Campos
-- Create date: 3/31/2017
-- Description:	Retuns a string for a FULL address, formatted as
--<Address Detail, Floor> FORMAL ADDRESS
-- =============================================
CREATE FUNCTION [dbo].[fnGenerateFullAddress]
(
	@strDetail varchar(1000),
	@strFloor varchar(1000),
	@strAddress varchar(1000),
	@strCity varchar(1000),
	@strStateProvince varchar(1000),
	@strPostalCode varchar(1000),
	@strCountry varchar(1000)
)
RETURNS varchar(1100)
AS
BEGIN
	RETURN 	'[' + COALESCE(NULLIF(@strDetail,''),'') +
	COALESCE(', Floor ' + NULLIF(@strFloor,''),'') + '] ' 
	 + @strAddress + ' ' + @strCity + ', ' + @strStateProvince + ' ' + @strPostalCode + 
	' ' + @strCountry
END

GO
