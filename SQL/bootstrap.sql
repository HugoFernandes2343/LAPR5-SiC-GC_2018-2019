DELETE FROM catalogs;
DELETE FROM categories;
DELETE FROM dimensions;
DELETE FROM finishes;
DELETE FROM materials;
DELETE FROM orders;
DELETE FROM measures;
DELETE FROM materialFinishes;
DELETE FROM productMaterials;
DELETE FROM ordersAndProducts;
DELETE FROM products;
DELETE FROM childMaterialRestrictions;
DELETE FROM restrictions;
DELETE FROM sqlite_sequence;
DELETE FROM users;

insert into users(Id,FirstName, LastName, Email, Password) values(1,"John", "Doe", "johndoe@email.com", "password1");
insert into users(Id,FirstName, LastName, Email, Password) values(2,"Jorge", "Men", "jorgemen@email.com", "password1");

insert into categories(Id,Name, ParentID) values (1,"Living Room", NULL);
insert into categories(Id,Name, ParentID) values (2,"Complementary", NULL);
insert into categories(Id,Name, ParentID) values (3,"Wardrobe", 1);
insert into categories(Id,Name, ParentID) values (4,"Drawer", 2);
insert into categories(Id,Name, ParentID) values (5,"Closet", 1);
insert into categories(Id,Name, ParentID) values (6,"Custom Drawer", 4);

insert into finishes(Id,Name,Description) values(1,"Wax","Wax finish good for woods");
insert into finishes(Id,Name,Description) values(2,"Polish","Polish finish good for metals");

insert into materials(Id,Name,Description) values(1,"Light wood","Light brown wood");
insert into materials(Id,Name,Description) values(2,"Dark wood","Dark brown wood");
insert into materials(Id,Name,Description) values(3,"Iron","Grey iron");

insert into materialFinishes(Id, MaterialId, FinishId) values(1, 1, 1);
insert into materialFinishes(Id, MaterialId, FinishId) values(2, 1, 2);
insert into materialFinishes(Id, MaterialId, FinishId) values(3, 2, 2);
insert into materialFinishes(Id, MaterialId, FinishId) values(4, 3, 1);

insert into restrictions(Id) values(1);
insert into restrictions(Id) values(2);

insert into measures(Id, Value, ValueMax, isDiscrete) values (1, 1000, 0, 1);
insert into measures(Id, Value, ValueMax, isDiscrete) values (2, 50, 0, 1);
insert into measures(Id, Value, ValueMax, isDiscrete) values (3, 100, 120, 0);
insert into measures(Id, Value, ValueMax, isDiscrete) values (4, 50, 70, 0);
insert into measures(Id, Value, ValueMax, isDiscrete) values (5, 100, 0, 1);
insert into measures(Id, Value, ValueMax, isDiscrete) values (6, 470,0,1);
insert into measures(Id, Value, ValueMax, isDiscrete) values (7, 470,500,0);

insert into dimensions(Id,WidthId,HeightId,DepthId) values(1, 1, 1, 1);
insert into dimensions(Id,WidthId,HeightId,DepthId) values(2, 5, 4, 3);
insert into dimensions(Id,WidthId,HeightId,DepthId) values(3, 6, 6, 6);
insert into dimensions(Id,WidthId,HeightId,DepthId) values(4, 7, 7, 7);

insert into catalogs(Id,date) values (1,"03/10/2018");

insert into childMaterialRestrictions(Id, MaterialId, RestrictionId) values (1,3,2);
insert into childMaterialRestrictions(Id, MaterialId, RestrictionId) values (2,1,1);
insert into childMaterialRestrictions(Id, MaterialId, RestrictionId) values (3,2,1);

insert into products(Id,Name,Description,Price,CategoryId,DimensionId, RestrictionId,ParentId, MinOccupation, MaxOccupation) values (1,"Closet","Default description",50,5,1,1,null, 0, 75);
insert into products(Id,Name,Description,Price,CategoryId,DimensionId, RestrictionId,ParentId, MinOccupation, MaxOccupation) values (2,"Drawer","Closet Drawer",150,4,2,1,1, 0, 100);
insert into products(Id,Name,Description,Price,CategoryId,DimensionId, RestrictionId,ParentId, MinOccupation, MaxOccupation) values (3,"Wardrobe","Default description",20,3,2,1,1, 0, 100);
insert into products(Id,Name,Description,Price,CategoryId,DimensionId, RestrictionId,ParentId, MinOccupation, MaxOccupation) values (4,"Closet","Default description",1000,3,1,2,null, 0, 100);
insert into products(Id,Name,Description,Price,CategoryId,DimensionId, RestrictionId,ParentId, MinOccupation, MaxOccupation) values (5,"Closet","Default description",1000,3,4,1,null, 0, 100);
insert into products(Id,Name,Description,Price,CategoryId,DimensionId, RestrictionId,ParentId, MinOccupation, MaxOccupation) values (6,"Drawer","Closet Drawer",150,4,2,1,null, 0, 100);
insert into products(Id,Name,Description,Price,CategoryId,DimensionId, RestrictionId,ParentId, MinOccupation, MaxOccupation) values (7,"Wardrobe","Default description",20,3,2,1,null, 0, 100);

insert into productMaterials(Id, ProductId, MaterialId) values (1, 1, 1);
insert into productMaterials(Id, ProductId, MaterialId) values (2, 1, 2);
insert into productMaterials(Id, ProductId, MaterialId) values (3, 2, 1);
insert into productMaterials(Id, ProductId, MaterialId) values (4, 3, 2);
insert into productMaterials(Id, ProductId, MaterialId) values (5, 4, 3);
insert into productMaterials(Id, ProductId, MaterialId) values (6, 5, 3);
insert into productMaterials(Id, ProductId, MaterialId) values (7, 6, 1);
insert into productMaterials(Id, ProductId, MaterialId) values (8, 7, 2);

insert into orders(Id,Date,UserId) values (1,"04/10/2018",1);

insert into ordersandproducts(Id,OrderId,ProductId) values (1,1,1);
insert into ordersandproducts(Id,OrderId,ProductId) values (2,1,4);
