CREATE TABLE FileProcessTest
(
	FileName varchar(255) NULL,
	ExpectedValue [bit] NOT NULL,
	CausesException [bit] NOT NULL
)
GO

INSERT INTO FileProcessTest
VALUES ('C:\Windows\Regedit.ext', 1, 0);

INSERT INTO FileProcessTest
VALUES ('C:\NotExists.txt', 0, 0);

INSERT INTO FileProcessTest
VALUES (null, 0, 1);