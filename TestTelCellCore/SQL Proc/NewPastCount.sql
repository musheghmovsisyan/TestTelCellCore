Use [TestDB]

go

CREATE PROCEDURE [dbo].[NewPastCount]
	 
AS
	SELECT CONVERT(date, InputDate), COUNT(id) AS PastCount
 FROM dbo.Paste
 GROUP BY CONVERT(date, InputDate)

 go