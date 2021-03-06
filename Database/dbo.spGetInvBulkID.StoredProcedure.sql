USE [AskAndAnswer]
GO
/****** Object:  StoredProcedure [dbo].[spGetInvBulkID]    Script Date: 4/11/2017 4:39:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alberto Campos
-- Create date: 4/7/2017
-- Description:	Get invBulk.ID given the table's four foreign key values
-- (location, bulk item (vendor pn), owner, and sub inv)
-- =============================================
CREATE PROCEDURE [dbo].[spGetInvBulkID] 
	@keyBulkItem bigint,
	@keyLocationBulk int,
	@keyOwner int,
	@keySubInv int

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT ID FROM invBulk
	WHERE keyBulkItem = @keyBulkItem AND
	keyLocationBulk = @keyLocationBulk AND
	keyOwner = @keyOwner AND
	keySubInv = @keySubInv
END

GO
