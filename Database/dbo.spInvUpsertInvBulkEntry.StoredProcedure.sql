USE [AskAndAnswer]
GO
/****** Object:  StoredProcedure [dbo].[spInvUpsertInvBulkEntry]    Script Date: 4/4/2017 9:21:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alberto Campos
-- Create date: 3/31/2017
-- Description:	Upserts an entry in invBulk.
-- If intDelta > 0, also makes an entry in invHistory
-- =============================================
CREATE PROCEDURE [dbo].[spInvUpsertInvBulkEntry]
	@ID bigint,
	@keyBulkItem bigint,
	@keyOwner int,
	@keyChangedBy int,
	@keyLocationBulk int,
	@intQty int,
	@intDelta int,
	@strComment varchar(500),
	@keyTransactionType int,
	@strSubInv varchar(50)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	--The SubInv field is a free-form field provided by the user, so we must look up its ID
	DECLARE @keySubInv int
	DECLARE @tmpSubInv TABLE (strSubInv varchar(50));
	DECLARE @tblSubInvID TABLE (ID int)

	INSERT @tmpSubInv VALUES (@strSubInv)
	MERGE invSubInv AS t
	USING @tmpSubInv AS s
	ON s.strSubInv = t.strSubInv
	WHEN MATCHED THEN
		UPDATE SET @keySubInv = t.ID
	WHEN NOT MATCHED THEN
		INSERT (strSubInv)
		VALUES (@strSubInv)
		OUTPUT INSERTED.ID INTO @tblSubInvID;
		SELECT TOP 1 @keySubInv = ID FROM @tblSubInvID;

	DECLARE @tmp TABLE (ID bigint);
	INSERT @tmp VALUES (@ID)
	
	MERGE invBulk AS t
	USING @tmp AS s
	ON s.ID = t.ID
	WHEN MATCHED THEN
		UPDATE SET t.keyLocationBulk = @keyLocationBulk,
				   t.intQty = @intQty,
				   t.keyOwner = @keyOwner,
				   t.keySubInv = @keySubInv
	WHEN NOT MATCHED THEN
		INSERT (keyLocationBulk,
				keyBulkItem,
				intQty,
				keyOwner,
				keySubInv)
		VALUES (@keyLocationBulk,
				@keyBulkItem,
				@intQty,
				@keyOwner,
				@keySubInv);

	--Update the history table for this item.
	INSERT INTO invHistory (keyBulkItem,
							keyChangedBy,
							keyTransactionType,
							intDelta,
							strComment,
							keyLocationBulk,
							keyOwner,
							keySubInv)
	VALUES (@keyBulkItem,
			@keyChangedBy,
			@keyTransactionType,
			@intDelta,
			@strComment,
			@keyLocationBulk,
			@keyOwner,
			@keySubInv)

END

GO
