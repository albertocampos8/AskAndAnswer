USE [AskAndAnswer]
GO
/****** Object:  StoredProcedure [dbo].[spEditReleaseNote_RESET]    Script Date: 1/16/2017 9:44:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Alberto Campos
-- Create date: 1/16/2017
-- Description:	Resets Release Note associated with a given Product/Rev/BOM Rev
-- to 'Initial Release' (asyChangeReasons.ID = 1)
-- =============================================
CREATE PROCEDURE [dbo].[spEditReleaseNote_RESET]
	@strName VARCHAR(20),
	@strRevision VARCHAR(4),
	@intbomRev int
AS
BEGIN

	DECLARE @tVar table(ID bigint)

	-- Insert the Release Note in asyChangeReasons
	DECLARE @changeID bigint

	-- Get keyReasonForRev for the current Revision combination, 
	-- store it in @changeID
	SELECT @changeID = aB.keyReasonForRev
	FROM asyBOM aB
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

	UPDATE asyBOM
		SET keyReasonForRev = 1
		WHERE keyReasonForRev  = @changeID

	--Clean up the table
	DELETE FROM asyChangeReasons
		WHERE ID = @changeID
END




GO
