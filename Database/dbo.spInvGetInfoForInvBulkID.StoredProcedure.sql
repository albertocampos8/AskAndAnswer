USE [AskAndAnswer]
GO
/****** Object:  StoredProcedure [dbo].[spInvGetInfoForInvBulkID]    Script Date: 4/3/2017 10:20:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alberto Campos
-- Create date: 3/31/2017
-- Description:	Returns the qty of a line item in invBulk for a given invBulk.ID
-- =============================================
CREATE PROCEDURE [dbo].[spInvGetInfoForInvBulkID] 
	@ID bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM invBulk WHERE ID = @ID
END

GO
