USE [AskAndAnswer]
GO
/****** Object:  StoredProcedure [dbo].[spAC_OTSVendor]    Script Date: 12/30/2016 10:15:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Alberto Campos
-- Create date: 12/30/16
-- Description:	Returns items from otsVendor that match @filter;
-- intended for use with Auto-complete
-- =============================================
CREATE PROCEDURE [dbo].[spAC_OTSVendor] 
	-- Add the parameters for the stored procedure here
	@filter varchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT strVendor FROM otsVendor WHERE strVendor LIKE @filter ORDER BY strVendor
END


GO
