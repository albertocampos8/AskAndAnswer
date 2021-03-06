USE [AskAndAnswer]
GO
/****** Object:  StoredProcedure [dbo].[spReleaseBOM]    Script Date: 12/30/2016 10:15:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Alberto Campos
-- Create date: 12/29/2016
-- Description:	Changes status of given Product/Rev/BOM Rev to Released (value 2)
-- =============================================
CREATE PROCEDURE [dbo].[spReleaseBOM]
	-- Add the parameters for the stored procedure here
	@strName VARCHAR(20),
	@strRevision VARCHAR(4),
	@intbomRev int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	UPDATE aB
	SET aB.keyAssyStatus = 2
	FROM asyBOM aB
		JOIN asyStatus
			ON aB.keyAssyStatus = asyStatus.ID
		JOIN asyNames
			ON asyNames.ID = aB.keyTopLevelName
		JOIN asyRevs
			ON asyRevs.ID = aB.keyAssyRev
	WHERE
		asyNames.strName = @strName
		AND
		asyRevs.strRevision = @strRevision
		AND
		aB.intBOMRev = @intbomRev
END


GO
