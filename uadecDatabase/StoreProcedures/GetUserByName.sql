
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
	SELECT p.Id AS Id,
	p.Name AS FirstName,
	p.LastName AS LastName,
	p.LastNameMother AS  LastNameMother
	FROM People p
	WHERE p.Name LIKE '%'+ @ClientId + '%'
END
GO



