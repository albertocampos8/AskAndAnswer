USE [AskAndAnswer]
GO
/****** Object:  StoredProcedure [dbo].[spOTSDeletePartNumber]    Script Date: 2/7/2017 11:15:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alberto Campos
-- Create date: 2/6/2017
-- Description:	Delets OTS PNs from otsRelation and otsParts
-- =============================================
CREATE PROCEDURE [dbo].[spOTSDeletePartNumber]
	@csvPNs varchar(MAX)
AS
BEGIN
	DECLARE @tblBOM TABLE
		(
			pn VARCHAR(MAX)
		)

	INSERT INTO @tblBOM (pn)
		SELECT * FROM csvStringToTable(@csvPNs,',')

	DECLARE @tblID TABLE
		(
			ID bigint
		)
	INSERT INTO @tblID (ID)
		SELECT ID FROM otsParts
			JOIN @tblBOM tb
				ON otsParts.strPartNumber = tb.pn

	--First, delete from otsRelation
	DELETE otsRelation
	FROM otsRelation 
		INNER JOIN @tblID tI
			ON otsRelation.keyOTSPart = tI.ID

	---...this allows us to delete from otsParts without violating any indices
	DELETE otsParts
	FROM otsParts
		INNER JOIN @tblID tI
			ON otsParts.ID = tI.ID

END

GO
