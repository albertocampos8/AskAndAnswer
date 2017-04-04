USE [AskAndAnswer]
GO
/****** Object:  StoredProcedure [dbo].[spInvUpsertInvBulkEntry]    Script Date: 4/3/2017 10:20:39 PM ******/
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
	@keyTransactionType int

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @tmp TABLE (ID bigint);
	INSERT @tmp VALUES (@ID)
	
	MERGE invBulk AS t
	USING @tmp AS s
	ON s.ID = t.ID
	WHEN MATCHED THEN
		UPDATE SET t.keyLocationBulk = @keyLocationBulk,
				   t.intQty = @intQty,
				   t.keyOwner = @keyOwner
	WHEN NOT MATCHED THEN
		INSERT (keyLocationBulk,
				keyBulkItem,
				intQty,
				keyOwner)
		VALUES (@keyLocationBulk,
				@keyBulkItem,
				@intQty,
				@keyOwner);

	--Update the history table for this item.
	INSERT INTO invHistory (keyBulkItem,
							keyChangedBy,
							keyTransactionType,
							intDelta,
							strComment,
							keyLocationBulk,
							keyOwner)
	VALUES (@keyBulkItem,
			@keyChangedBy,
			@keyTransactionType,
			@intDelta,
			@strComment,
			@keyLocationBulk,
			@keyOwner)

END

GO
