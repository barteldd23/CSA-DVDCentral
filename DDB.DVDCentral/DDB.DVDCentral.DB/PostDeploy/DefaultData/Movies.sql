BEGIN
	INSERT INTO tblMovie(Id, Title, Description, Cost, RatingId, FormatId, DirectorId, InStkQty, ImagePath)
	VALUES
	(1, 'Shrek', 'Ogre Fun Film', $8.99, 2, 1, 1, 3, 'Shrek.png'),
	(2, 'Gran Torino', 'A Korean War Verteran deals with his neighborhood being transformed into a Hmong neighborhood.', $9.99, 4, 3, 2, 2, 'gran_torino.png'),
	(3, 'Gaurdians of the Galaxy', 'Space Traveling mis fits save the universe.', $6.99, 3, 2, 3, 0, 'Gaurdians.png')
END