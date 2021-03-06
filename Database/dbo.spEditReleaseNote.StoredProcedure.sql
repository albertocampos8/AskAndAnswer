USE [AskAndAnswer]
GO
/****** Object:  StoredProcedure [dbo].[spEditReleaseNote]    Script Date: 1/16/2017 9:44:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Alberto Campos
-- Create date: 1/16/2017
-- Description:	Changes Release Note associated with a given Product/Rev/BOM Rev
-- =============================================
CREATE PROCEDURE [dbo].[spEditReleaseNote]
	@strName VARCHAR(20),
	@strRevision VARCHAR(4),
	@intbomRev int,
	@strReason varchar(MAX)
AS
BEGIN

	DECLARE @tVar table(ID bigint)
	IF (@strReason = 'INITIAL RELEASE')
		RETURN

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


	IF (@changeID = 1)
		BEGIN
			INSERT INTO asyChangeReasons (strReason)
				OUTPUT INSERTED.ID INTO @tVar
				VALUES (@strReason)

			SELECT @changeID = ID
				FROM @tVar

			UPDATE aB
				SET aB.keyReasonForRev = @changeID
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
		END
	ELSE
		BEGIN
			EXEC	[dbo].[spEditReleaseNoteByID]
					@ID = @changeID,
					@strReason = @strReason

		END
	


END



GO
