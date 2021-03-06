USE [AskAndAnswer]
GO
/****** Object:  StoredProcedure [dbo].[spOTSInsertNewPartNumber2]    Script Date: 2/7/2017 11:15:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Alberto Campos
-- Create date: 2/1/2017
-- Description:	Inserts a new OTS PN, and the associated foreign keys, in the database
-- very similar to spOTSInsertNewPartNumber, except this version does not require
-- us to know the ID of the requestor and part type ahead of time; the SP determines it for us.
--Return codes:
-- -100: Type did not yet exist in otsPartTypes
-- =============================================
CREATE PROCEDURE [dbo].[spOTSInsertNewPartNumber2]
	@strPartNumber varchar(20), 
	@strName varchar(50),	--Name of the person who requested the part; this SP finds the ID of the user 
	@strVendor varchar(300), 
	@strVendorPartNumber varchar(300), 
	@strDescription nvarchar(300),
	@strDescription2 nvarchar(300),
	@strType varchar(150), --Name of the type of the part; this SP find the ID of the part type
	@strProduct varchar(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET @strPartNumber = UPPER(@strPartNumber);
	SET @strVendor = UPPER(@strVendor);
	SET @strVendorPartNumber = UPPER(@strVendorPartNumber);
	SET @strDescription = UPPER(@strDescription);
	SET @strDescription2 = UPPER(@strDescription2);
	SET @strProduct = UPPER(@strProduct);
	--a Variable to hold the ID of the product, requestor, and parttype
	DECLARE @keyProduct bigint
	DECLARE @keyRequestedBy bigint
	DECLARE @keyType int

	--First, we must insert the Part Type.
	--Reset our temp variables
	--Init @keyType to 1 (unknown)
	SET @keyType=1
	DECLARE @tVar TABLE (ID bigint)
	DECLARE @source TABLE (strText varchar(150))
	
	INSERT @source VALUES (@strType)
	MERGE otsPartType AS t
	USING @source AS s
	ON s.strText = t.strType
	WHEN MATCHED THEN
		UPDATE SET @keyType = t.ID;
	--WHEN NOT MATCHED THEN
	--	INSERT (strType)
	--	VALUES (@strType)
	--	OUTPUT INSERTED.ID INTO @tVar;
	--	SELECT TOP 1 @keyType = ID FROM @tVar;
	--UNCOMMENT OUT FOLLOWING SECTION IF YOU WANT TO ABORT INSERTION IF PART TYPE NOT FOUND
	--IF @keyType=-1
	--	BEGIN
	--		@keyType=1;
	--	END
	--END-- we have value for keyType

	--Next, we must insert the product.
	DELETE FROM @tVar
	DELETE FROM @source
	INSERT @source VALUES (@strProduct)
	MERGE otsProduct AS t
	USING @source AS s
	ON s.strText = t.strProduct
	WHEN MATCHED THEN
		UPDATE SET @keyProduct = t.ID
	WHEN NOT MATCHED THEN
		INSERT (strProduct)
		VALUES (@strProduct)
		OUTPUT INSERTED.ID INTO @tVar;
		SELECT TOP 1 @keyProduct = ID FROM @tVar;
	--END-- we have value for keyProduct

	--Now, we must insert the Requestor.
	--Reset our temp variables
	DELETE FROM @tVar
	DELETE FROM @source
	INSERT @source VALUES (@strName)
	MERGE otsUsers AS t
	USING @source AS s
	ON s.strText = t.strName
	WHEN MATCHED THEN
		UPDATE SET @keyRequestedBy = t.ID
	WHEN NOT MATCHED THEN
		INSERT (strName)
		VALUES (@strName)
		OUTPUT INSERTED.ID INTO @tVar;
		SELECT TOP 1 @keyRequestedBy = ID FROM @tVar;
	--END-- we have value for keyRequestedBy



	--Next, we insert the information for the OTS Part Number, so we can get the Inserted ID
	DECLARE @ots TABLE (ID bigint)
	DECLARE @keyOTSPart bigint
	INSERT INTO otsParts (strPartNumber, keyRequestedBy, strDescription, strDescription2, keyType, keyRequestedForProduct)
		OUTPUT INSERTED.ID INTO @ots 
		VALUES (@strPartNumber, @keyRequestedBy, @strDescription, @strDescription2, @keyType, @keyProduct)
	SELECT TOP 1 @keyOTSPart = ID FROM @ots
	--END-- we have the ID of the inserted OTS part

	--We will use @keyOTSPart later, when making our insertion into otsRelation.  Before we do
	--that, we need to
	--Get the ID for OTS Vendor
	DECLARE @tVendor TABLE (ID int)
	DECLARE @keyVendor int
	DECLARE @sourceVendor TABLE (strVendor varchar(300))
	INSERT @sourceVendor VALUES (@strVendor)
	MERGE otsVendor AS t
	USING @sourceVendor AS s
	ON s.strVendor = t.strVendor
	WHEN MATCHED THEN
		UPDATE SET @keyVendor = t.ID
	WHEN NOT MATCHED THEN
		INSERT (strVendor)
		VALUES (@strVendor)
		OUTPUT INSERTED.ID INTO @tVendor;
		SELECT TOP 1 @keyVendor = ID FROM @tVendor;
	--END-- we now have value for keyVendor
	--So, make the last insertion in otsVendorPN
	DECLARE @tVendorPN TABLE (ID bigint)
	DECLARE @VendorPNID bigint
	DECLARE @sourceVendorPN TABLE (strVendorPartNumber varchar(300))
	INSERT @sourceVendorPN VALUES (@strVendorPartNumber)
	MERGE otsVendorPN AS t
	USING @sourceVendorPN AS s
	ON s.strVendorPartNumber = t.strVendorPartNumber
	WHEN MATCHED THEN
		UPDATE SET @VendorPNID = t.ID
	WHEN NOT MATCHED THEN
		INSERT (strVendorPartNumber, keyVendor)
		VALUES (@strVendorPartNumber, @keyVendor)
		OUTPUT INSERTED.ID INTO @tVendorPN;
		SELECT TOP 1 @VendorPNID = ID FROM @tVendorPN;

	--Finally, record the relationship between the OTS PN and the Vendor PN in otsRelation
	INSERT INTO otsRelation (keyOTSPart, keyVendorPN) 
		VALUES (@keyOTSPart, @VendorPNID);
	
END


GO
