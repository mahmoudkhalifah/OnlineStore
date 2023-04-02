
SET IDENTITY_INSERT [dbo].[Categories] ON 
SET IDENTITY_INSERT [dbo].[Products] ON 
SET IDENTITY_INSERT [dbo].[Carts] ON 
SET IDENTITY_INSERT [dbo].[Customers] ON 
SET IDENTITY_INSERT [dbo].[Addresses] ON 
SET IDENTITY_INSERT [dbo].[Orders] ON 
INSERT [dbo].[Categories] ([CategoryId], [CategoryName], [IsDeleted]) VALUES (5, N'New', 0)
INSERT [dbo].[Products] ([ProductID], [ProductName], [Price], [Color], [Width], [Height], [Discount], [Description], [AvailableQuantity], [Sold], [IsDeleted], [ReleaseDate], [Image1], [Image2], [Image3]) VALUES (10, N'Mug3', CAST(100.00 AS Decimal(18, 2)), 5, CAST(40.00 AS Decimal(18, 2)), CAST(40.00 AS Decimal(18, 2)), 0, N'MUg', 20, 0, 0, CAST(N'2022-03-03T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL)
INSERT [dbo].[Carts] ([CartId], [CustomerId]) VALUES (8, 8)
INSERT [dbo].[Customers] ([CustomerId], [Fname], [Lname], [Gender], [PhoneNumber], [CartId]) VALUES (8, N'Noura', N'Ali', 1, N'01006564294', 8)
INSERT [dbo].[Addresses] ([AddressId], [ApartmentNo], [FloorNumber], [Street], [Zone], [City], [Governorate], [Country], [NearestLandmark], [CustomerId]) VALUES (6, N'123', N'123', N'123', N'123', N'salam', N'cairo', N'egypt', N'qwe', 8)
INSERT [dbo].[CategoryProduct] ([CategoriesCategoryId], [ProductsProductID]) VALUES (2, 10)
INSERT [dbo].[Orders] ([OrderId], [OrderDate], [ShippingDate], [ArrivalDate], [Bill], [OrderState], [PaymentMethod], [CustomerId]) VALUES (4, CAST(N'2023-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, CAST(250.00 AS Decimal(18, 2)), 1, 0, 8)
INSERT [dbo].[OrderProduct] ([OrdersOrderId], [ProductsProductID]) VALUES (4, 1)
INSERT [dbo].[OrderProduct] ([OrdersOrderId], [ProductsProductID]) VALUES (4, 2)
INSERT [dbo].[ProductsCarts] ([CartId], [ProductId], [ProductQuantity]) VALUES (8, 1, 3)
INSERT [dbo].[ProductsCarts] ([CartId], [ProductId], [ProductQuantity]) VALUES (8, 2, 2)


SET IDENTITY_INSERT [dbo].[Categories] off 
SET IDENTITY_INSERT [dbo].[Products] OFF 
SET IDENTITY_INSERT [dbo].[Carts] off 
SET IDENTITY_INSERT [dbo].[Customers] OFF 
SET IDENTITY_INSERT [dbo].[Addresses] OFF 
SET IDENTITY_INSERT [dbo].[Orders] OFF 