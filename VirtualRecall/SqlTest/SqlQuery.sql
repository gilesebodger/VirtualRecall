select 
	a.Id as [AnimalId]
	,a.Name
	,c.ID as [ClientId]
	,c.Forename
	,c.Surname 
from dbo.Animal a
join dbo.ClientAnimal ca
on ca.AnimalId = a.ID
join dbo.Client c
on c.ID = ca.ClientId
join dbo.Treatment t
on t.AnimalId = a.ID
join dbo.Product p
on p.ID = t.ID

where p.Sku = 'Heart'
and IsNull(a.Deceased, 0) = 0 -- presuming that a null entry would equate to NOT being deceased
and IsNull(c.Active, 0) = 1 -- presuming that a null entry would mean the client is NOT active


-- DDL Statements
/*
Presuming if you want to keep the primary keys on the tables then we can make better with non clustered indexs
*/
CREATE NONCLUSTERED INDEX [IX_dbo.ClientAnimal_AnimalId] ON dbo.ClientAnimal (AnimalId)
CREATE NONCLUSTERED INDEX [IX_dbo.ClientAnimal_ClientId] ON dbo.ClientAnimal (ClientId)
CREATE NONCLUSTERED INDEX [IX_dbo.Product_Sku] ON dbo.Product (SkU)