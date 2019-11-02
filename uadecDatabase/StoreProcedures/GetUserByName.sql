
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
	SELECT u.Id AS Id,
	u.Name AS FirstName,
	u.LastName AS LastName,
	u.LastNameMother AS  LastNameMother
	FROM Users u
	WHERE u.Name LIKE '%'+ @ClientId + '%'
END
GO



