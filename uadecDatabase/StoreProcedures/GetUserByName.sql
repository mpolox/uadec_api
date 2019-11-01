
/****** Object:  StoredProcedure [dbo].[GetUserByName]    Script Date: 10/30/2019 2:22:53 PM ******/
DROP PROCEDURE [dbo].[GetUserByName]
GO

/****** Object:  StoredProcedure [dbo].[GetUserByName]    Script Date: 10/30/2019 2:22:53 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Marcopolo,>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetUserByName]
(
	@ClientId NVARCHAR(MAX)
)
AS
BEGIN
	SELECT s.Id AS Id,
	s.Name AS FirstName,
	s.LastName AS LastName,
	s.LastNameMother AS  LastNameMother
	FROM Students s
	WHERE s.Name LIKE '%'+ @ClientId + '%'
END
GO



