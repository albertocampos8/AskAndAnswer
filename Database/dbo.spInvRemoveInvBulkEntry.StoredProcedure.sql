USE [AskAndAnswer]
GO
/****** Object:  StoredProcedure [dbo].[spInvRemoveInvBulkEntry]    Script Date: 4/4/2017 9:21:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alberto Campos
-- Create date: 3/31/2017
-- Description:	Removes an entry from invBulk.
-- If the resulting qty delta due to remova is >0,  an entry is also made
-- in invHistory
-- =============================================
CREATE PROCEDURE [dbo].[spInvRemoveInvBulkEntry] 
	-- Add the parameters for the stored procedure here
	@ID bigint, 
	@keyBulkItem bigint, 
	@keyChangedBy bigint,
	@intDelta int,
	@strComment varchar(500),
	@keyTransactionType tinyint,
	@keyLocationBulk int,
	@keySubInv int,
	@keyOwner bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DELETE FROM invBulk WHERE ID = @ID
	IF @intDelta > 0
		INSERT INTO invHistory (keyBulkItem,keyChangedBy,keyTransactionType,intDelta,strComment,keyLocationBulk,keyOwner,keySubInv)
		VALUES (@keyBulkItem,@keyChangedBy,@keyTransactionType,@intDelta,@strComment,@keyLocationBulk,@keyOwner,@keySubInv)
END

GO
