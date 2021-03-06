USE [AskAndAnswer]
GO
/****** Object:  StoredProcedure [dbo].[spAC_LocStateProvince]    Script Date: 4/3/2017 10:20:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Alberto Campos
-- Create date: 3/29/2017
-- Description:	Gets auto-complete entries for locStateProvince
-- =============================================
CREATE PROCEDURE [dbo].[spAC_LocStateProvince]
	@filter varchar(500)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT strStateProvince FROM locStateProvince WHERE strStateProvince LIKE @filter ORDER BY strStateProvince
END


GO
