USE [AskAndAnswer]
GO
/****** Object:  StoredProcedure [dbo].[spEditReleaseNoteByID]    Script Date: 1/16/2017 9:44:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alberto Campos
-- Create date: 1/16/2017
-- Description:	Edits an entry in asyChangeReasons by its asyChangeReasons.ID
-- =============================================
CREATE PROCEDURE [dbo].[spEditReleaseNoteByID]
	@ID bigint,
	@strReason varchar(MAX)
AS
BEGIN
    UPDATE asyChangeReasons
		SET strReason = @strReason
		WHERE ID = @ID
END

GO
