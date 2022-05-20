create or alter view rentList
as
SELECT R.ID, CONCAT(C.FirstName,' ',C.SecondName,' ',C.Patronymic)AS 'FIOClient',CONCAT(E.FirstName,' ', E.SecondName,' ', E.Patronymic)AS 'FIOEmployee',p.Product,
R.TimeRent, R.TimeRentEnd, R.isDelete, R.Cost,(SELECT dbo.getDeadline(R.TimeRentEnd)) as 'tagDeadline'
FROM RENT R LEFT JOIN 
Client C ON C.ID=R.IDClient LEFT JOIN 
Employee E ON E.ID = R.IDEmployee left join 
Product p on p.ID =r.IDProduct







