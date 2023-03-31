Update Products
set Image1 =(SELECT * FROM OPENROWSET(BULK N'D:\Kolyah\ITI\MVC\Project\Images\mug3_2.jpg', SINGLE_BLOB) AS img_data)
where ProductID=10