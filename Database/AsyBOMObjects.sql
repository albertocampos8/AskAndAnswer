USE [AskAndAnswer]
GO
/****** Object:  Table [dbo].[asyBOM]    Script Date: 12/13/2016 12:20:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[asyBOM](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[keyTopLevelName] [int] NOT NULL,
	[keyAssyPN] [bigint] NOT NULL,
	[keyAssyRev] [int] NOT NULL,
	[keyUploadedBy] [bigint] NOT NULL,
	[dtUploaded] [datetime] NOT NULL,
	[keyAssyStatus] [tinyint] NOT NULL,
	[keyAssyBU] [tinyint] NOT NULL,
	[intBOMRev] [smallint] NOT NULL,
	[keyReasonForRev] [bigint] NOT NULL,
 CONSTRAINT [PK_asyBOM_ID] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[asyBOMParts]    Script Date: 12/13/2016 12:20:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[asyBOMParts](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[keyAssy] [bigint] NOT NULL,
	[keyPN] [bigint] NOT NULL,
	[strRefDes] [varchar](max) NOT NULL,
	[strBOMNotes] [varchar](max) NOT NULL,
	[intQty] [smallint] NOT NULL,
 CONSTRAINT [PK_asyBOMParts_ID] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[asyChangeReasons]    Script Date: 12/13/2016 12:20:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[asyChangeReasons](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[strReason] [varchar](max) NOT NULL,
 CONSTRAINT [PK_asyChangeReasons_ID] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[asyNames]    Script Date: 12/13/2016 12:20:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[asyNames](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[strName] [varchar](30) NOT NULL,
	[strDescription] [varchar](100) NOT NULL,
 CONSTRAINT [PK_asyNames_ID] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[asyPNs]    Script Date: 12/13/2016 12:20:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[asyPNs](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[strAssyPartNumber] [varchar](20) NOT NULL,
 CONSTRAINT [PK_asyPNs_ID] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[asyRevs]    Script Date: 12/13/2016 12:20:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[asyRevs](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[strRevision] [varchar](4) NOT NULL,
	[intMajor] [tinyint] NOT NULL,
	[intMinor] [tinyint] NOT NULL,
 CONSTRAINT [PK_asyRevs_ID] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[asyStatus]    Script Date: 12/13/2016 12:20:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[asyStatus](
	[ID] [tinyint] IDENTITY(1,1) NOT NULL,
	[strAssyStatus] [varchar](30) NOT NULL,
 CONSTRAINT [PK_asyStatus_ID] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[asyBOM] ADD  CONSTRAINT [DF_asyBOM_keyTopLevelName]  DEFAULT ((1)) FOR [keyTopLevelName]
GO
ALTER TABLE [dbo].[asyBOM] ADD  CONSTRAINT [DF_asyBOM_keyAssyPN]  DEFAULT ((1)) FOR [keyAssyPN]
GO
ALTER TABLE [dbo].[asyBOM] ADD  CONSTRAINT [DF_asyBOM_keyAssyRev]  DEFAULT ((1)) FOR [keyAssyRev]
GO
ALTER TABLE [dbo].[asyBOM] ADD  CONSTRAINT [DF_asyBOM_keyUploadedBy]  DEFAULT ((1)) FOR [keyUploadedBy]
GO
ALTER TABLE [dbo].[asyBOM] ADD  CONSTRAINT [DF_asyBOM_dtUploaded]  DEFAULT (getdate()) FOR [dtUploaded]
GO
ALTER TABLE [dbo].[asyBOM] ADD  CONSTRAINT [DF_asyBOM_keyAssyStatus]  DEFAULT ((1)) FOR [keyAssyStatus]
GO
ALTER TABLE [dbo].[asyBOM] ADD  CONSTRAINT [DF_asyBOM_keyAssyBU]  DEFAULT ((1)) FOR [keyAssyBU]
GO
ALTER TABLE [dbo].[asyBOM] ADD  CONSTRAINT [DF_asyBOM_intBOMRev]  DEFAULT ((0)) FOR [intBOMRev]
GO
ALTER TABLE [dbo].[asyBOM] ADD  CONSTRAINT [DF_asyBOM_keyReasonForRev]  DEFAULT ((1)) FOR [keyReasonForRev]
GO
ALTER TABLE [dbo].[asyBOMParts] ADD  CONSTRAINT [DF_asyBOMParts_keyAssy]  DEFAULT ((1)) FOR [keyAssy]
GO
ALTER TABLE [dbo].[asyBOMParts] ADD  CONSTRAINT [DF_asyBOMParts_keyPN]  DEFAULT ((1)) FOR [keyPN]
GO
ALTER TABLE [dbo].[asyBOMParts] ADD  CONSTRAINT [DF_asyBOMParts_strRefDes]  DEFAULT ('') FOR [strRefDes]
GO
ALTER TABLE [dbo].[asyBOMParts] ADD  CONSTRAINT [DF_asyBOMParts_strBOMNotes]  DEFAULT ('') FOR [strBOMNotes]
GO
ALTER TABLE [dbo].[asyBOMParts] ADD  CONSTRAINT [DF_asyBOMParts_intQty]  DEFAULT ((0)) FOR [intQty]
GO
ALTER TABLE [dbo].[asyChangeReasons] ADD  CONSTRAINT [DF_asyChangeReasons_strReason]  DEFAULT ('') FOR [strReason]
GO
ALTER TABLE [dbo].[asyNames] ADD  CONSTRAINT [DF_asyNames_strName]  DEFAULT ('') FOR [strName]
GO
ALTER TABLE [dbo].[asyNames] ADD  CONSTRAINT [DF_asyNames_strDescription]  DEFAULT ('') FOR [strDescription]
GO
ALTER TABLE [dbo].[asyPNs] ADD  CONSTRAINT [DF_asyPNs_strAssyPartNumber]  DEFAULT ('') FOR [strAssyPartNumber]
GO
ALTER TABLE [dbo].[asyRevs] ADD  CONSTRAINT [DF_asyRevs_strRevision]  DEFAULT ('') FOR [strRevision]
GO
ALTER TABLE [dbo].[asyRevs] ADD  CONSTRAINT [DF_asyRevs_intMajor]  DEFAULT ((0)) FOR [intMajor]
GO
ALTER TABLE [dbo].[asyRevs] ADD  CONSTRAINT [DF_asyRevs_intMinor]  DEFAULT ((0)) FOR [intMinor]
GO
ALTER TABLE [dbo].[asyStatus] ADD  CONSTRAINT [DF_asyStatus_strAssyStatus]  DEFAULT ('') FOR [strAssyStatus]
GO
ALTER TABLE [dbo].[asyBOM]  WITH CHECK ADD  CONSTRAINT [FK_asyBOM_keyAssyBU_kvpBU_ID] FOREIGN KEY([keyAssyBU])
REFERENCES [dbo].[kvpBU] ([ID])
GO
ALTER TABLE [dbo].[asyBOM] CHECK CONSTRAINT [FK_asyBOM_keyAssyBU_kvpBU_ID]
GO
ALTER TABLE [dbo].[asyBOM]  WITH CHECK ADD  CONSTRAINT [FK_asyBOM_keyAssyPN_asyPNs_ID] FOREIGN KEY([keyAssyPN])
REFERENCES [dbo].[asyPNs] ([ID])
GO
ALTER TABLE [dbo].[asyBOM] CHECK CONSTRAINT [FK_asyBOM_keyAssyPN_asyPNs_ID]
GO
ALTER TABLE [dbo].[asyBOM]  WITH CHECK ADD  CONSTRAINT [FK_asyBOM_keyAssyRev_asyRevs_ID] FOREIGN KEY([keyAssyRev])
REFERENCES [dbo].[asyRevs] ([ID])
GO
ALTER TABLE [dbo].[asyBOM] CHECK CONSTRAINT [FK_asyBOM_keyAssyRev_asyRevs_ID]
GO
ALTER TABLE [dbo].[asyBOM]  WITH CHECK ADD  CONSTRAINT [FK_asyBOM_keyAssyStatus_asyStatus_ID] FOREIGN KEY([keyAssyStatus])
REFERENCES [dbo].[asyStatus] ([ID])
GO
ALTER TABLE [dbo].[asyBOM] CHECK CONSTRAINT [FK_asyBOM_keyAssyStatus_asyStatus_ID]
GO
ALTER TABLE [dbo].[asyBOM]  WITH CHECK ADD  CONSTRAINT [FK_asyBOM_keyReasonForRev_asyChangeReasons_ID] FOREIGN KEY([keyReasonForRev])
REFERENCES [dbo].[asyChangeReasons] ([ID])
GO
ALTER TABLE [dbo].[asyBOM] CHECK CONSTRAINT [FK_asyBOM_keyReasonForRev_asyChangeReasons_ID]
GO
ALTER TABLE [dbo].[asyBOM]  WITH CHECK ADD  CONSTRAINT [FK_asyBOM_keyTopLevelName_asyNames_ID] FOREIGN KEY([keyTopLevelName])
REFERENCES [dbo].[asyNames] ([ID])
GO
ALTER TABLE [dbo].[asyBOM] CHECK CONSTRAINT [FK_asyBOM_keyTopLevelName_asyNames_ID]
GO
ALTER TABLE [dbo].[asyBOM]  WITH CHECK ADD  CONSTRAINT [FK_asyBOM_keyUploadedBy_otsUsers_ID] FOREIGN KEY([keyUploadedBy])
REFERENCES [dbo].[otsUsers] ([ID])
GO
ALTER TABLE [dbo].[asyBOM] CHECK CONSTRAINT [FK_asyBOM_keyUploadedBy_otsUsers_ID]
GO
ALTER TABLE [dbo].[asyBOMParts]  WITH CHECK ADD  CONSTRAINT [FK_asyBOMParts_keyAssy_asyBOM_ID] FOREIGN KEY([keyAssy])
REFERENCES [dbo].[asyBOM] ([ID])
GO
ALTER TABLE [dbo].[asyBOMParts] CHECK CONSTRAINT [FK_asyBOMParts_keyAssy_asyBOM_ID]
GO
ALTER TABLE [dbo].[asyBOMParts]  WITH CHECK ADD  CONSTRAINT [FK_asyBOMParts_keyPN_otsParts_ID] FOREIGN KEY([keyPN])
REFERENCES [dbo].[otsParts] ([ID])
GO
ALTER TABLE [dbo].[asyBOMParts] CHECK CONSTRAINT [FK_asyBOMParts_keyPN_otsParts_ID]
GO
/****** Object:  StoredProcedure [dbo].[spUpsertAssyBOM]    Script Date: 12/13/2016 12:20:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alberto Campos
-- Create date: 12/12/16
-- Description:	Makes an entry in asyBOM if keyTopLevelName,
-- keyAssyPN, keyAssyRev, and intBOMRev do not yet exist in the table;
-- otherwise updates the table.
-- Returns the ID of the inserted/updated Assembly BOM
-- =============================================
CREATE PROCEDURE [dbo].[spUpsertAssyBOM]
	-- The following four parameters determine whether a new entry is made in asyBOM 
	@strName varchar(30),			-- Assy Name; used to get asyNames.ID = asyBOM.keyTopLevelName
	@strAssyPartNumber varchar(20), -- Assy PN; used to get asyPNs.ID = asyBOM.keyAssyPN
	@strRevision varchar(4),		-- Assy Rev; use to get asyRevs.ID = asyBOMkeyAssyRev
	@intBOMRev smallint,			-- Revision of this Assembly BOM
	
	@strDescription varchar(100),	-- Description of the Assembly BOM; this is only used if
									-- we need to insert @strName into asyNames.
	
	@intMajor tinyint,				-- Major and
	@intMinor tinyint,				-- Minor
									-- revision.  Only needed if we need to insert @strRevision into asyRevs.
	
	@keyUploadedBy bigint,			-- ID of the Uploader
	@keyAssyStatus tinyint,			-- ID of the Assy Status
	@keyAssyBU tinyint,				-- ID of the Assy BU
	
	@keyReasonForRev bigint = 1 OUTPUT,	-- the default value for asyBOM.keyReasonForRev
	@strReason varchar(MAX) = '',	-- if this is <> '', then an entry is made in asyChangeReasons,
									-- and the inserted ID is given to @keyReasonForRev.
	@ID BIGINT OUTPUT				--The inserted/exsiting ID of this Assembly BOM name/pn/revision/bom revision combo

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	--tmpTable to hold inserted IDs that are bigints
	DECLARE @tBigintVar TABLE (ID bigint)
	DECLARE @tIntVar TABLE (ID bigint)

	--Get asyNames.ID == @keyTopLevelName
	DECLARE @keyTopLevelName bigint

	DECLARE @srcTblAsyNames TABLE (strName varchar(30))

	INSERT @srcTblAsyNames VALUES (@strName)
	
	MERGE asyNames AS t
	USING @srcTblAsyNames AS s
	ON t.strName = s.strName
	WHEN MATCHED THEN
		UPDATE SET @keyTopLevelName = t.ID
	WHEN NOT MATCHED THEN
		INSERT (strName, strDescription)
		VALUES (@strName, @strDescription)
		OUTPUT INSERTED.ID INTO @tBigintVar;
		SELECT TOP 1 @keyTopLevelName = ID FROM @tBigintVar;
		
	--************ END ID FOR asyNames

	--Get asyPNs.ID == @keyAssyPN
	DELETE FROM @tBigintVar;
	DECLARE @keyAssyPN bigint

	DECLARE @srcTblAsyPNs TABLE (strAssyPartNumber varchar(20))

	INSERT @srcTblAsyPns VALUES (@strAssyPartNumber)
	
	MERGE asyPNs AS t
	USING @srcTblAsyPNs AS s
	ON t.strAssyPartNumber = s.strAssyPartNumber
	WHEN MATCHED THEN
		UPDATE SET @keyAssyPN = t.ID
	WHEN NOT MATCHED THEN
		INSERT (strAssyPartNumber)
		VALUES (@strAssyPartNumber)
		OUTPUT INSERTED.ID INTO @tBigintVar;
		SELECT TOP 1 @keyAssyPN = ID FROM @tBigintVar;
	
	--************ END ID FOR asyPNs

	--Get asyRevs.ID == @keyAssyRev
	DECLARE @keyAssyRev int

	DECLARE @srcTblAsyRevs TABLE (strRevision varchar(4))

	INSERT @srcTblAsyRevs VALUES (@strRevision)
	
	MERGE asyRevs AS t
	USING @srcTblAsyRevs AS s
	ON t.strRevision = s.strRevision
	WHEN MATCHED THEN
		UPDATE SET @keyAssyRev = t.ID
	WHEN NOT MATCHED THEN
		INSERT (strRevision, intMajor, intMinor)
		VALUES (@strRevision, @intMajor, @intMinor)
		OUTPUT INSERTED.ID INTO @tIntVar;
		SELECT TOP 1 @keyAssyRev = ID FROM @tIntVar;
	
	--************ END ID FOR asyRevs

	--Get asyChangeReasons.ID
	IF @strReason != '' 
		BEGIN
			INSERT INTO
				asyChangeReasons (strReason)
				VALUES (@strReason);
			SET @keyReasonForRev = @@IDENTITY;
		END
	-- END id for asyChangeReasons

	--Get the ID for the inserted/updated record in asyBOM
	DELETE FROM @tBigintVar

	DECLARE @tmp TABLE (keyTopLevelName bigint,
					keyAssyPN bigint,
					keyAssyRev int,
					intbomRev int)
	INSERT @tmp VALUES (@keyTopLevelName, @keyAssyPN, @keyAssyRev, @intbomRev)

	MERGE asyBOM AS t
	USING @tmp AS s
	ON
		t.keyTopLevelName = s.keyTopLevelName
		AND
		t.keyAssyPN = s.keyAssyPN
		AND
		t.keyAssyRev = s.keyAssyRev
		AND
		t.intbomRev = s.intbomrev
	WHEN MATCHED THEN
		UPDATE SET @ID = t.ID, 
					t.keyUploadedBy = @keyUploadedBy, 
					t.dtUploaded = GETDATE(), 
					t.keyAssyStatus = @keyAssyStatus, 
					t.keyAssyBU = @keyAssyBU, 
					t.keyReasonForRev = @keyReasonForRev
	WHEN NOT MATCHED THEN
		INSERT (keyTopLevelName, 
				keyAssyPN,
				keyAssyRev,
				keyUploadedBy,
				keyAssyStatus,
				keyAssyBU,
				intbomRev,
				keyReasonForRev)
		VALUES (@keyTopLevelName,
				@keyAssyPN,
				@keyAssyRev,
				@keyUploadedBy,
				@keyAssyStatus,
				@keyAssyBU,
				@intbomRev,
				@keyReasonForRev)
		OUTPUT INSERTED.ID INTO @tBigintVar;
		SELECT TOP 1 @ID = ID FROM @tBigintVar;
END

GO
EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'Name of the Top Level Assembl' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asyBOM', @level2type=N'COLUMN',@level2name=N'keyTopLevelName'
GO
EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'Assembly Part Number' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asyBOM', @level2type=N'COLUMN',@level2name=N'keyAssyPN'
GO
EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'FK to otsUsers' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asyBOM', @level2type=N'COLUMN',@level2name=N'keyUploadedBy'
GO
EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'Date Assembly BOM was uploaded' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asyBOM', @level2type=N'COLUMN',@level2name=N'dtUploaded'
GO
EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'FK to asyStatus' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asyBOM', @level2type=N'COLUMN',@level2name=N'keyAssyStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'FK to kvpBU' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asyBOM', @level2type=N'COLUMN',@level2name=N'keyAssyBU'
GO
EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'Revision of this Assembly BOM' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asyBOM', @level2type=N'COLUMN',@level2name=N'intBOMRev'
GO
EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'FK to asyChangeReasons' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asyBOM', @level2type=N'COLUMN',@level2name=N'keyReasonForRev'
GO
EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'FK to asyAssemblies' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asyBOMParts', @level2type=N'COLUMN',@level2name=N'keyAssy'
GO
EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'FK to otsParts' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asyBOMParts', @level2type=N'COLUMN',@level2name=N'keyPN'
GO
EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'csv list of Reference Designators for the part represented by keyPN' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asyBOMParts', @level2type=N'COLUMN',@level2name=N'strRefDes'
GO
EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'Special BOM notes for the PN represented by keyPN' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asyBOMParts', @level2type=N'COLUMN',@level2name=N'strBOMNotes'
GO
EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'Quantity of the PN represented by keyPN' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asyBOMParts', @level2type=N'COLUMN',@level2name=N'intQty'
GO
EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'Description of reason for change' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asyChangeReasons', @level2type=N'COLUMN',@level2name=N'strReason'
GO
EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'Name of Assembly' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asyNames', @level2type=N'COLUMN',@level2name=N'strName'
GO
EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'Description of Assembly' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asyNames', @level2type=N'COLUMN',@level2name=N'strDescription'
GO
EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'Part Number for Assembly' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asyPNs', @level2type=N'COLUMN',@level2name=N'strAssyPartNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'Revision Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asyRevs', @level2type=N'COLUMN',@level2name=N'strRevision'
GO
EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'Major Revision' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asyRevs', @level2type=N'COLUMN',@level2name=N'intMajor'
GO
EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'Minor Revision' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asyRevs', @level2type=N'COLUMN',@level2name=N'intMinor'
GO
EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'Desription of the Status Code' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asyStatus', @level2type=N'COLUMN',@level2name=N'strAssyStatus'
GO
