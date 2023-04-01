

Update Products
set Image1 =(SELECT * FROM OPENROWSET(BULK N'D:/Kolyah/ITI/MVC/Project/OnlineStore/OnlineStore/Images/KittyNote.jpeg', SINGLE_BLOB) AS img_data)
where ProductID=1
Update Products
set Image2 =(SELECT * FROM OPENROWSET(BULK N'D:/Kolyah/ITI/MVC/Project/OnlineStore/OnlineStore/Images/kittyNoteBook2.jpg', SINGLE_BLOB) AS img_data)
where ProductID=1

Update Products
set Image1 =(SELECT * FROM OPENROWSET(BULK N'D:/Kolyah/ITI/MVC/Project/OnlineStore/OnlineStore/Images/BrownNote.jpeg', SINGLE_BLOB) AS img_data)
where ProductID=2

Update Products
set Image1 =(SELECT * FROM OPENROWSET(BULK N'D:/Kolyah/ITI/MVC/Project/OnlineStore/OnlineStore/Images/mug1.png', SINGLE_BLOB) AS img_data)
where ProductID=3


Update Products
set Image1 =(SELECT * FROM OPENROWSET(BULK N'D:/Kolyah/ITI/MVC/Project/OnlineStore/OnlineStore/Images/mug2.jpg', SINGLE_BLOB) AS img_data)
where ProductID=4


Update Products
set Image1 =(SELECT * FROM OPENROWSET(BULK N'D:/Kolyah/ITI/MVC/Project/OnlineStore/OnlineStore/Images/SilverEarrings.jpg', SINGLE_BLOB) AS img_data)
where ProductID=6

Update Products
set Image1 =(SELECT * FROM OPENROWSET(BULK N'D:/Kolyah/ITI/MVC/Project/OnlineStore/OnlineStore/Images/frame1.jpeg', SINGLE_BLOB) AS img_data)
where ProductID=8
Update Products
set Image2 =(SELECT * FROM OPENROWSET(BULK N'D:/Kolyah/ITI/MVC/Project/OnlineStore/OnlineStore/Images/frame1_2.jpeg', SINGLE_BLOB) AS img_data)
where ProductID=8

Update Products
set Image1 =(SELECT * FROM OPENROWSET(BULK N'D:/Kolyah/ITI/MVC/Project/OnlineStore/OnlineStore/Images/frame2.jpeg', SINGLE_BLOB) AS img_data)
where ProductID=9
Update Products
set Image2 =(SELECT * FROM OPENROWSET(BULK N'D:/Kolyah/ITI/MVC/Project/OnlineStore/OnlineStore/Images/frame2_1.jpg', SINGLE_BLOB) AS img_data)
where ProductID=9

Update Products
set Image1 =(SELECT * FROM OPENROWSET(BULK N'D:/Kolyah/ITI/MVC/Project/OnlineStore/OnlineStore/Images/mug3.jpeg', SINGLE_BLOB) AS img_data)
where ProductID=10
Update Products
set Image2 =(SELECT * FROM OPENROWSET(BULK N'D:/Kolyah/ITI/MVC/Project/OnlineStore/OnlineStore/Images/mug3_2.jpg', SINGLE_BLOB) AS img_data)
where ProductID=10



