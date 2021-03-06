USE [AskAndAnswer]
GO
/****** Object:  StoredProcedure [dbo].[spGetInvBulkIDWithVPNandSubInvIDOnly]    Script Date: 4/11/2017 4:39:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alberto Campos
-- Create date: 4/7/2017
-- Description:	Get invBulk.ID and the rest of the row, given two of the table's four foreign key values
-- (bulk item (vendor pn), sub inv)
-- =============================================
CREATE PROCEDURE [dbo].[spGetInvBulkIDWithVPNandSubInvIDOnly]
	@keyBulkItem bigint,
	@keySubInv int

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM invBulk
	WHERE keyBulkItem = @keyBulkItem AND
	keySubInv = @keySubInv
END

GO
