USE [AskAndAnswer]
GO
/****** Object:  StoredProcedure [dbo].[spGetAssyHistory]    Script Date: 1/12/2017 11:39:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Alberto Campos
-- Create date: 1/12/17
-- Description:	For a given Product Name, returns:
-- Assy Part Number
-- Assy Part Number Revision
-- Assy Status
-- BOM Revision
-- Date Uploaded
-- Uploaded By
-- Release Notes
-- =============================================
CREATE PROCEDURE [dbo].[spGetAssyHistory] 
	-- Add the parameters for the stored procedure here
	@strName varchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT UPPER(asyRevs.strRevision) AS strRevision, 
					asyPNs.strAssyPartNumber,
					otsUsers.strName,
					asyStatus.strAssyStatus,
					asyChangeReasons.strReason,
					asyChangeReasons.ID,
					asyBOM.dtUploaded,
					asyBOM.intbomRev  
	FROM asyBOM
	JOIN asyNames
		ON asyBOM.keyTopLevelName = asyNames.ID
	JOIN asyPNs
		ON asyBOM.keyAssyPN = asyPNs.ID
	JOIN asyRevs
		ON asyBOM.keyAssyRev = asyRevs.ID
	JOIN otsUsers
		ON asyBOM.keyUploadedBy = otsUsers.ID
	JOIN asyStatus
		ON asyBOM.keyAssyStatus = asyStatus.ID
	JOIN asyChangeReasons
		ON asyBOM.keyReasonForRev = asyChangeReasons.ID
	WHERE asyNames.strName = @strName
	ORDER BY asyRevs.strRevision DESC, intBOMRev DESC
END



GO
