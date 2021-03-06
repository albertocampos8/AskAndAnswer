USE [AskAndAnswer]
GO
/****** Object:  StoredProcedure [dbo].[spOTSFindUndefinedPartTypes]    Script Date: 2/7/2017 11:37:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alberto Campos
-- Create date: 2/6/2017
-- Description:	Finds all Part Types not yet defined in otsPartType
-- =============================================
CREATE PROCEDURE [dbo].[spOTSFindUndefinedPartTypes] 
	@csvPartTypes varchar(max)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @newPartTypes TABLE
			(
				pn VARCHAR(MAX)
			)

	INSERT INTO @newPartTypes (pn)
		SELECT * FROM csvStringToTable(@csvPartTypes,',')


    -- Insert statements for procedure here
	SELECT DISTINCT pn
		FROM @newPartTypes npt
		LEFT JOIN otsPartType
			ON npt.pn = otsPartType.strType
		WHERE otsPartType.strType IS NULL
END

GO
