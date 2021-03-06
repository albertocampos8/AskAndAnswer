USE [AskAndAnswer]
GO
/****** Object:  StoredProcedure [dbo].[spAC_LocAddress]    Script Date: 4/3/2017 10:20:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alberto Campos
-- Create date: 3/29/2017
-- Description:	Gets auto-complete entries for locAddress
-- =============================================
CREATE PROCEDURE [dbo].[spAC_LocAddress]
	@filter varchar(500)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT strAddress FROM locAddress WHERE strAddress LIKE @filter ORDER BY strAddress
END

GO
