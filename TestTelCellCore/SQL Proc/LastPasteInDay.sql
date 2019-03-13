Use [TestDB]

go

CREATE PROCEDURE [dbo].[LastPasteInDay]
	 
AS
	SELECT  DISTINCT   CONVERT(date,  a1.AccessDate) AS MyAccessDate,
 ( SELECT TOP 1 a2.PastStrId 
  FROM AccessDatesLog as a2
WHERE  CONVERT(date, a2.AccessDate) =CONVERT(date,  a1.AccessDate)    
ORDER BY a2.AccessDate DESC) AS PastStrId
 FROM dbo.AccessDatesLog a1
 GROUP BY CONVERT(date, AccessDate)
  

Go