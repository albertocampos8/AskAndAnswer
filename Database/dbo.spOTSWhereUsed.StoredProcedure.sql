USE [AskAndAnswer]
GO
/****** Object:  StoredProcedure [dbo].[spOTSWhereUsed]    Script Date: 12/31/2016 8:11:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alberto Campos
-- Create date: 12/29/2016
-- Description:	Performs a 'where-used' for a part number
-- using its ID in asyBOMParts.keyPN
-- Returns two datasets:
-- 1) A one record data set that gets otsParts.strPartNumber for the given keyPN
-- 2) A dataset with all the where used data for the given keyPN
-- =============================================
CREATE PROCEDURE [dbo].[spOTSWhereUsed]
	-- Add the parameters for the stored procedure here
	@keyPN bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT strPartNumber FROM otsParts WHERE ID = @keyPN

    -- Insert statements for procedure here
	SELECT asyNames.strName, asyPNs.strAssyPartNumber, asyRevs.strRevision,
	asyStatus.strAssyStatus, asyBOM.intBOMRev, otsUsers.strName AS USERNAME, kvpBU.valDisplayedValue
	FROM asyBOM
		JOIN asyBOMParts
			ON asyBOMParts.keyAssy = asyBOM.ID
		JOIN asyNames
			ON asyBOM.keyTopLevelName = asyNames.ID
		JOIN asyPNs
			ON asyBOM.keyAssyPN = asyPNs.ID
		JOIN asyRevs
			ON asyRevs.ID = asyBOM.keyAssyRev
		JOIN asyStatus
			ON asyStatus.ID = asyBOM.keyAssyStatus
		JOIN otsUsers
			ON otsUsers.ID = asyBOM.keyUploadedBy
		JOIN kvpBU
			ON kvpBU.ID = asyBOM.keyAssyBU
	WHERE asyBOMParts.keyPN = @keyPN
	ORDER BY asyNames.strName ASC, asyPNs.strAssyPartNumber DESC, intBOMRev DESC
END

GO
