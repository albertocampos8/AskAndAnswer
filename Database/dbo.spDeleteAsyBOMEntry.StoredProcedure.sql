USE [AskAndAnswer]
GO
/****** Object:  StoredProcedure [dbo].[spDeleteAsyBOMEntry]    Script Date: 1/5/2017 12:12:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alberto Campos
-- Create date: 1/4/2017
-- Description:	Deletes an Assembly BOM with a given Name/Rev/BOM Rev from asyBOM
-- =============================================
CREATE PROCEDURE [dbo].[spDeleteAsyBOMEntry]
	-- Add the parameters for the stored procedure here
	@strName VARCHAR(20),
	@strRevision VARCHAR(4),
	@intbomRev int
AS
BEGIN
	DECLARE @IDtoDelete bigint;

	SELECT @IDtoDelete=asyBOM.ID
	 FROM asyBOM
		JOIN asyStatus
			ON asyBOM.keyAssyStatus = asyStatus.ID
		JOIN asyNames
			ON asyNames.ID = asyBOM.keyTopLevelName
		JOIN asyRevs
			ON asyRevs.ID = asyBOM.keyAssyRev
	WHERE
		asyNames.strName = @strName
		AND
		asyRevs.strRevision = @strRevision
		AND
		asyBOM.intBOMRev = @intbomRev

	--Delete the Asy BOM
	EXEC	[dbo].[spDeleteAssyBOMParts]
		@keyAssy = @IDtoDelete

    -- Insert statements for procedure here
	DELETE FROM asyBOM
	 FROM asyBOM
		JOIN asyStatus
			ON asyBOM.keyAssyStatus = asyStatus.ID
		JOIN asyNames
			ON asyNames.ID = asyBOM.keyTopLevelName
		JOIN asyRevs
			ON asyRevs.ID = asyBOM.keyAssyRev
	WHERE
		asyNames.strName = @strName
		AND
		asyRevs.strRevision = @strRevision
		AND
		asyBOM.intBOMRev = @intbomRev
END

GO
