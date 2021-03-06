USE [AskAndAnswer]
GO
/****** Object:  UserDefinedFunction [dbo].[csvStringToTable]    Script Date: 12/30/2016 10:15:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alberto Campos
-- Create date: 11/29/2016
-- Description:	Splits a csv string into a table
-- where each row is an item in the csv string.
-- taken from the xml approach here:
-- http://sqlperformance.com/2012/07/t-sql-queries/split-strings
-- =============================================
CREATE FUNCTION [dbo].[csvStringToTable]
(	
	-- Add the parameters for the function here
	@List		NVARCHAR(MAX),
	@Delimiter	NVARCHAR(255)
)
RETURNS TABLE 
WITH SCHEMABINDING
AS
RETURN 
(
	SELECT Item = y.i.value('(./text())[1]', 'nvarchar(4000)')
	FROM
	( SELECT x = CONVERT(XML, '<i>'
		+ REPLACE(@List, @Delimiter, '</i><i>')
		+ '</i>').query('.')
	) AS a CROSS APPLY x.nodes('i') AS y(i)

)

GO
