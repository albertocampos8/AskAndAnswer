USE [AskAndAnswer]
GO
/****** Object:  StoredProcedure [dbo].[spGetAssyStatus]    Script Date: 12/30/2016 10:15:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alberto Campos
-- Create date: 12/28/2016
-- Description:	Returns the Status Value (aka asyStatus.strAssyStatus) of an assembly BOM in asyBOM
-- =============================================
CREATE PROCEDURE [dbo].[spGetAssyStatus]
	-- Add the parameters for the stored procedure here
	@strName VARCHAR(20),
	@strRevision VARCHAR(4),
	@intbomRev int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT asyStatus.strAssyStatus FROM asyStatus
		JOIN asyBOM
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
