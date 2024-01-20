BEGIN
	INSERT INTO tblCustomer (Id, FirstName, LastName, Address, City, State, Zip, Phone, UserId)
	VALUES
	(1, 'Novak', 'Djokovic', '24 Slam Ct.', 'Arthur Ashe', 'New York', '12345', '2424242424', 25),
	(2, 'Roger', 'Federer', 'Green Grass Ct.', 'Centre Court', 'England', '98765', '2020202020', 20),
	(3, 'Rafael', 'Nadal', 'Dirt Clay Ct.', 'Philippe Chatrier Court', 'France', '45678', '2222222222', 22)
END