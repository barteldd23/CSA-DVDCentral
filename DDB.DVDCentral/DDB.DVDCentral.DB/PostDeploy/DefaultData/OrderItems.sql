BEGIN
	INSERT INTO tblOrderItem(Id, OrderId, MovieId, Quantity, Cost)
	VALUES
	(1, 2, 2, 1, $10.00),
	(2, 1, 1, 2, $19.50),
	(3, 3, 3, 1, $9.99)
END