USE Enterprise
GO
--1,5,10,25,50,100,500,1000,2000,20,200,250,2500,5000,20000,25000,50000,2,3,15,40
--1,2,3,5,10,15,20,25,40,50,100,200,250,500,1000,2000,2500,5000,20000,25000,50000
--1 2 3 5 10 15 20 25 50 100 200 500 1000 2500 5000 10000
EXEC usp_InsertUpdateBaseDenom @Name = '0.01', @Description = 'One Cent'
GO
EXEC usp_InsertUpdateBaseDenom @Name = '0.02', @Description = 'Two Cents'
GO
EXEC usp_InsertUpdateBaseDenom @Name = '0.03', @Description = 'Three Cents'
GO
EXEC usp_InsertUpdateBaseDenom @Name = '0.05', @Description = 'Five Cents'
GO
EXEC usp_InsertUpdateBaseDenom @Name = '0.10', @Description = 'Ten Cents'
GO
EXEC usp_InsertUpdateBaseDenom @Name = '0.15', @Description = 'Fifteen Cents'
GO
EXEC usp_InsertUpdateBaseDenom @Name = '0.20', @Description = 'Twenty Cents'
GO
EXEC usp_InsertUpdateBaseDenom @Name = '0.25', @Description = 'Twenty Five Cents'
GO
EXEC usp_InsertUpdateBaseDenom @Name = '0.40', @Description = 'Fourty Cents'
GO
EXEC usp_InsertUpdateBaseDenom @Name = '0.50', @Description = 'Fifty Cents'
GO
EXEC usp_InsertUpdateBaseDenom @Name = '1.0', @Description = 'One Dollar'
GO
EXEC usp_InsertUpdateBaseDenom @Name = '2.0', @Description = 'Two Dollars'
GO
EXEC usp_InsertUpdateBaseDenom @Name = '2.5', @Description = 'Two and Half Dollars'
GO
EXEC usp_InsertUpdateBaseDenom @Name = '5.00', @Description = 'Five Dollars'
GO
EXEC usp_InsertUpdateBaseDenom @Name = '10.00', @Description = 'Ten Dollars'
GO
EXEC usp_InsertUpdateBaseDenom @Name = '20.00', @Description = 'Twenty Dollars'
GO
EXEC usp_InsertUpdateBaseDenom @Name = '25.00', @Description = 'Twenty Five Dollars'
GO
EXEC usp_InsertUpdateBaseDenom @Name = '50.00', @Description = 'Fifty Dollars'
GO
EXEC usp_InsertUpdateBaseDenom @Name = '100.00', @Description = 'One Hundred Dollars'
GO
EXEC usp_InsertUpdateBaseDenom @Name = '200.00', @Description = 'Two Hundred Dollars'
GO
EXEC usp_InsertUpdateBaseDenom @Name = '250.00', @Description = 'Two Hundred and Fifty Dollars'
GO
EXEC usp_InsertUpdateBaseDenom @Name = '500.00', @Description = 'Five Hundred Dollars'
GO

