BEGIN
	INSERT INTO tblOrder(Id, CustomerId, OrderDate, UserId, ShipDate)
	VALUES
	(1, 2, '2023-4-16', 20, '2023-4-20'),
	(2, 1, '2023-7-1', 25, '2023-8-2'),
	(3, 3, '2023-8-16', 22, '2023-9-10')
END