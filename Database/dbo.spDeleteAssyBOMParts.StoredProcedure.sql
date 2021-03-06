USE [AskAndAnswer]
GO
/****** Object:  StoredProcedure [dbo].[spDeleteAssyBOMParts]    Script Date: 12/30/2016 10:15:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alberto Campos
-- Create date: 12/27/2016
-- Description:	Deletes all entries in asyBOMParts where keyAssy = @keyAssy
-- =============================================
CREATE PROCEDURE [dbo].[spDeleteAssyBOMParts]
	@keyAssy bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DELETE FROM asyBOMParts
		WHERE keyAssy = @keyAssy
END

GO
