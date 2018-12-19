DELETE FROM dbo.catalogs;
DELETE FROM dbo.categories;
DELETE FROM dbo.dimensions;
DELETE FROM dbo.finishes;
DELETE FROM dbo.materials;
DELETE FROM dbo.orders;
DELETE FROM dbo.measures;
DELETE FROM dbo.materialFinishes;
DELETE FROM dbo.productMaterials;
DELETE FROM dbo.ordersAndProducts;
DELETE FROM dbo.products;
DELETE FROM dbo.childMaterialRestrictions;
DELETE FROM dbo.restrictions;
DELETE FROM dbo.users;

SET IDENTITY_INSERT dbo.users ON
insert into dbo.users(Id,FirstName, LastName, Email, Password) values(1,'John', 'Doe', 'johndoe@email.com', 'password1');
insert into dbo.users(Id,FirstName, LastName, Email, Password) values(2,'Jorge', 'Men', 'jorgemen@email.com', 'password1');
SET IDENTITY_INSERT dbo.users OFF

SET IDENTITY_INSERT dbo.categories ON
insert into dbo.categories(Id,Name, ParentID) values (1,'Living Room', NULL);
insert into dbo.categories(Id,Name, ParentID) values (2,'Complementary', NULL);
insert into dbo.categories(Id,Name, ParentID) values (3,'Wardrobe', 1);
insert into dbo.categories(Id,Name, ParentID) values (4,'Drawer', 2);
insert into dbo.categories(Id,Name, ParentID) values (5,'Closet', 1);
insert into dbo.categories(Id,Name, ParentID) values (6,'Custom Drawer', 4);
SET IDENTITY_INSERT dbo.categories OFF

SET IDENTITY_INSERT dbo.finishes ON
insert into dbo.finishes(Id,Name,Description) values(1,'Wax','Wax finish good for woods');
insert into dbo.finishes(Id,Name,Description) values(2,'Polish','Polish finish good for metals');
insert into dbo.finishes(Id,Name,Description) values(3,'Thicc Wax','Hmm.');
SET IDENTITY_INSERT dbo.finishes OFF

SET IDENTITY_INSERT dbo.materials ON
insert into dbo.materials(Id,Name,Description) values(1,'Light wood','Light brown wood');
insert into dbo.materials(Id,Name,Description) values(2,'Dark wood','Dark brown wood');
insert into dbo.materials(Id,Name,Description) values(3,'Iron','Grey iron');
SET IDENTITY_INSERT dbo.materials OFF

SET IDENTITY_INSERT dbo.materialFinishes ON
insert into dbo.materialFinishes(Id, MaterialId, FinishId) values(1, 1, 1);
insert into dbo.materialFinishes(Id, MaterialId, FinishId) values(2, 1, 2);
insert into dbo.materialFinishes(Id, MaterialId, FinishId) values(3, 2, 2);
insert into dbo.materialFinishes(Id, MaterialId, FinishId) values(4, 3, 1);
SET IDENTITY_INSERT dbo.materialFinishes OFF

SET IDENTITY_INSERT dbo.restrictions ON
insert into dbo.restrictions(Id) values(1);
insert into dbo.restrictions(Id) values(2);
SET IDENTITY_INSERT dbo.restrictions OFF

SET IDENTITY_INSERT dbo.measures ON
insert into dbo.measures(Id, Value, ValueMax, isDiscrete) values (1, 1000, 0, 1);
insert into dbo.measures(Id, Value, ValueMax, isDiscrete) values (2, 50, 0, 1);
insert into dbo.measures(Id, Value, ValueMax, isDiscrete) values (3, 100, 120, 0);
insert into dbo.measures(Id, Value, ValueMax, isDiscrete) values (4, 50, 70, 0);
insert into dbo.measures(Id, Value, ValueMax, isDiscrete) values (5, 100, 0, 1);
insert into dbo.measures(Id, Value, ValueMax, isDiscrete) values (6, 470,0,1);
insert into dbo.measures(Id, Value, ValueMax, isDiscrete) values (7, 470,500,0);
SET IDENTITY_INSERT dbo.measures OFF

SET IDENTITY_INSERT dbo.dimensions ON
insert into dbo.dimensions(Id,WidthId,HeightId,DepthId) values(1, 1, 1, 1);
insert into dbo.dimensions(Id,WidthId,HeightId,DepthId) values(2, 5, 4, 3);
insert into dbo.dimensions(Id,WidthId,HeightId,DepthId) values(3, 6, 6, 6);
insert into dbo.dimensions(Id,WidthId,HeightId,DepthId) values(4, 7, 7, 7);
SET IDENTITY_INSERT dbo.dimensions OFF

SET IDENTITY_INSERT dbo.catalogs ON
insert into dbo.catalogs(Id,date) values (1,'03/10/2018');
insert into dbo.catalogs(Id,date) values (2,'03/10/2020');
SET IDENTITY_INSERT dbo.catalogs OFF

SET IDENTITY_INSERT dbo.childMaterialRestrictions ON
insert into dbo.childMaterialRestrictions(Id, MaterialId, RestrictionId) values (1,3,2);
insert into dbo.childMaterialRestrictions(Id, MaterialId, RestrictionId) values (2,1,1);
insert into dbo.childMaterialRestrictions(Id, MaterialId, RestrictionId) values (3,2,1);
SET IDENTITY_INSERT dbo.childMaterialRestrictions OFF

SET IDENTITY_INSERT dbo.products ON
insert into dbo.products(Id,Name,Description,Price,CategoryId,DimensionId, RestrictionId,ParentId, MinOccupation, MaxOccupation) values (1,'Closet','Default description',50,5,1,1,null, 0, 75);
insert into dbo.products(Id,Name,Description,Price,CategoryId,DimensionId, RestrictionId,ParentId, MinOccupation, MaxOccupation) values (2,'Drawer','Closet Drawer',150,4,2,1,1, 0, 100);
insert into dbo.products(Id,Name,Description,Price,CategoryId,DimensionId, RestrictionId,ParentId, MinOccupation, MaxOccupation) values (3,'Wardrobe','Default description',20,3,2,1,1, 0, 100);
insert into dbo.products(Id,Name,Description,Price,CategoryId,DimensionId, RestrictionId,ParentId, MinOccupation, MaxOccupation) values (4,'Closet','Default description',1000,3,1,2,null, 0, 100);
insert into dbo.products(Id,Name,Description,Price,CategoryId,DimensionId, RestrictionId,ParentId, MinOccupation, MaxOccupation) values (5,'Closet','Default description',1000,3,4,1,null, 0, 100);
insert into dbo.products(Id,Name,Description,Price,CategoryId,DimensionId, RestrictionId,ParentId, MinOccupation, MaxOccupation) values (6,'Drawer','Closet Drawer',150,4,2,1,null, 0, 100);
insert into dbo.products(Id,Name,Description,Price,CategoryId,DimensionId, RestrictionId,ParentId, MinOccupation, MaxOccupation) values (7,'Wardrobe','Default description',20,3,2,1,null, 0, 100);
SET IDENTITY_INSERT dbo.products OFF

SET IDENTITY_INSERT dbo.productMaterials ON
insert into dbo.productMaterials(Id, ProductId, MaterialId) values (1, 1, 1);
insert into dbo.productMaterials(Id, ProductId, MaterialId) values (2, 1, 2);
insert into dbo.productMaterials(Id, ProductId, MaterialId) values (3, 2, 1);
insert into dbo.productMaterials(Id, ProductId, MaterialId) values (4, 3, 2);
insert into dbo.productMaterials(Id, ProductId, MaterialId) values (5, 4, 3);
insert into dbo.productMaterials(Id, ProductId, MaterialId) values (6, 5, 3);
insert into dbo.productMaterials(Id, ProductId, MaterialId) values (7, 6, 1);
insert into dbo.productMaterials(Id, ProductId, MaterialId) values (8, 7, 2);
SET IDENTITY_INSERT dbo.productMaterials OFF

SET IDENTITY_INSERT dbo.orders ON
insert into dbo.orders(Id,Date,UserId) values (1,'04/10/2018',1);
SET IDENTITY_INSERT dbo.orders OFF

SET IDENTITY_INSERT dbo.ordersandproducts ON
insert into dbo.ordersandproducts(Id,OrderId,ProductId) values (1,1,1);
insert into dbo.ordersandproducts(Id,OrderId,ProductId) values (2,1,4);
SET IDENTITY_INSERT dbo.ordersandproducts OFF