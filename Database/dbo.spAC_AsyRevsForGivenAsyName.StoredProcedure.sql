USE [AskAndAnswer]
GO
/****** Object:  StoredProcedure [dbo].[spAC_AsyRevsForGivenAsyName]    Script Date: 12/30/2016 10:15:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Alberto Campos
-- Create date: 12/30/16
-- Description:	Returns items from asyRevs that = asyNames.@strName
-- and match @filter;
-- intended for use with Auto-complete
-- =============================================
CREATE PROCEDURE [dbo].[spAC_AsyRevsForGivenAsyName] 
	-- Add the parameters for the stored procedure here
	@strName varchar(100),
	@filter varchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT DISTINCT UPPER(asyRevs.strRevision) AS strRevision FROM asyRevs  
	JOIN asyBOM
		ON asyBOM.keyAssyRev = asyRevs.ID
	JOIN asyNames
		ON asyBOM.keyTopLevelName = asyNames.ID
	 WHERE strRevision LIKE @filter 
		AND strName = @strName
	ORDER BY 1 ASC
END


GO
