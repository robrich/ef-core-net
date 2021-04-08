SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE PROCEDURE [dbo].[CustomerById] (
  @CustomerId int
)
AS
BEGIN
	SET NOCOUNT ON;

  SELECT Id, Name, BirthDate
  FROM dbo.Customers
  WHERE Id = @CustomerId
END
GO
