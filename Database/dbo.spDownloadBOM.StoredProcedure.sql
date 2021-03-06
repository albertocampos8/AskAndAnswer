USE [AskAndAnswer]
GO
/****** Object:  StoredProcedure [dbo].[spDownloadBOM]    Script Date: 12/30/2016 10:15:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alberto Campos
-- Create date: 12/28/16
-- Description:	Returns two datasets for a given Product Name/Product Revision/BOM Revision:
-- 1) Information about the product as queried from table asyBOM
-- 2) The BOM Information from asyBOMParts
-- =============================================
CREATE PROCEDURE [dbo].[spDownloadBOM] 
	@strName varchar(20),
	@strRevision varchar(4),
	@intbomRev int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @assyID bigint

    -- Dataset 1: Information about the assembly
	SELECT asyNames.strName, asyNames.strDescription, asyPNs.strAssyPartNumber, asyRevs.strRevision,
	asyRevs.intMajor, asyRevs.intMinor, otsUsers.ID AS USERID, otsUsers.strName AS USERNAME, asyStatus.strAssyStatus,
	kvpBU.strBUCode, kvpBU.ID, asyChangeReasons.strReason, asyBOM.intBOMRev, asyBOM.dtUploaded, 
	asyBOM.ID, asyBOM.keyAssyStatus
		FROM asyBOM
		JOIN asyNames
			ON asyNames.ID = asyBOM.keyTopLevelName
		JOIN asyPNs
			ON asyPNs.ID = asyBOM.keyAssyPN
		JOIN asyRevs
			ON asyRevs.ID = keyAssyRev
		JOIN otsUsers
			ON asyBOM.keyUploadedBy = otsUsers.ID
		JOIN asyStatus
			ON asyStatus.ID = asyBOM.keyAssyStatus
		JOIN kvpBU
			ON kvpBU.ID = keyAssyBU
		JOIN asyChangeReasons
			ON asyChangeReasons.ID = asyBOM.keyReasonForRev
	WHERE asyNames.strName = @strNAME
	AND asyRevs.strRevision = @strRevision
	AND asyBOM.intBOMRev = @intbomRev

	SELECT @assyID = asyBOM.ID
		FROM asyBOM
		JOIN asyNames
			ON asyNames.ID = asyBOM.keyTopLevelName
		JOIN asyRevs
			ON asyRevs.ID = keyAssyRev
	WHERE asyNames.strName = @strNAME
	AND asyRevs.strRevision = @strRevision
	AND asyBOM.intBOMRev = @intbomRev

	SELECT asyBOMParts.*, otsParts.strDescription, otsParts.strPartNumber
	FROM asyBOMParts
		JOIN otsParts
			ON otsParts.ID = asyBOMParts.keyPN
	WHERE keyAssy = @assyID
END

GO
