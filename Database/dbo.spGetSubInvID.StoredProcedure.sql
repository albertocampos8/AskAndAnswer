USE [AskAndAnswer]
GO
/****** Object:  StoredProcedure [dbo].[spGetSubInvID]    Script Date: 4/11/2017 4:39:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alberto Campos
-- Create date: 4/7/2017
-- Description:	Get invSubInv.ID for a given lot code
-- =============================================
CREATE PROCEDURE [dbo].[spGetSubInvID] 
	@strSubInv varchar(300)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT ID FROM invSubInv WHERE strSubInv = @strSubInv
END

GO
