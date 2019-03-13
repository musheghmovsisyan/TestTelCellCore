Use [TestDB]


GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




ALTER PROCEDURE [dbo].[NoAccessDay]	 

@date DATE=null
AS
	

DECLARE @FoundDateMax DATE;
DECLARE @FoundDateMin DATE;
 
DECLARE @notfound bit;
DECLARE @i INT; 


SET @i=1;

SET @notfound=1;


IF NOT EXISTS(SELECT * FROM dbo.AccessDatesLog WHERE  CONVERT(date, AccessDate)=@date)
 BEGIN
  set @notfound=0;
  SELECT @date AS FoundDate
 END 


WHILE (@notfound <> 0  )

BEGIN

SET @FoundDateMax= CONVERT(date,  DATEADD( day, @i, @date ));  
SET @FoundDateMin= CONVERT(date,  DATEADD( day, @i* -1, @date ));  

 IF NOT EXISTS(SELECT * FROM dbo.AccessDatesLog WHERE  CONVERT(date, AccessDate)=@FoundDateMax)
 BEGIN
  set @notfound=0;
 END
 ELSE
 SET @FoundDateMax =NULL; 
  IF NOT EXISTS(SELECT * FROM dbo.AccessDatesLog WHERE  CONVERT(date, AccessDate)=@FoundDateMin)
 BEGIN 
 SET @notfound=0;
 END
 ELSE
  SET @FoundDateMin =NULL;


SET @i=@i+1;

IF @FoundDateMin IS NOT NULL AND  @FoundDateMax IS NOT NULL
 SELECT @FoundDateMin AS FoundDateMin , @FoundDateMax AS FoundDateMax

 ELSE
 IF @FoundDateMin IS NOT NULL  
 SELECT @FoundDateMin AS FoundDateMin  
 ELSE
 IF    @FoundDateMax IS NOT NULL
 SELECT   @FoundDateMax AS FoundDateMax

END





