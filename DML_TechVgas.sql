USE Db_TechVagas
GO

INSERT INTO TipoUsuario (NomeTipoUsuario)
VALUES		('Administrador'), ('Candidato'), ('Empresa');
GO

INSERT INTO Curso (NomeCurso, TipoCurso)
VALUES		('Desenvolvimento de Sistemas - 1T - M', 'T�cnico'),
			('Desenvolvimento de Sistemas - 2T - M', 'T�cnico'),
			('Desenvolvimento de Sistemas - 3T - M', 'T�cnico'),
			('Desenvolvimento de Sistemas - 1T - T', 'T�cnico'),
			('Desenvolvimento de Sistemas - 2T - T', 'T�cnico'),
			('Desenvolvimento de Sistemas - 3T - T', 'T�cnico'),
			('Redes de Computadores - 1T - M', 'T�cnico'),
			('Redes de Computadores - 2T - M', 'T�cnico'),
			('Redes de Computadores - 3T - M', 'T�cnico'),
			('Redes de Computadores - 1T - T', 'T�cnico'),
			('Redes de Computadores - 2T - T', 'T�cnico'),
			('Redes de Computadores - 3T - T', 'T�cnico'),
			('Multim�dia - 1T - M', 'T�cnico'),
			('Multim�dia - 2T - M', 'T�cnico'),
			('Multim�dia - 3T - M', 'T�cnico'),
			('Multim�dia - 1T - T', 'T�cnico'),
			('Multim�dia - 2T - T', 'T�cnico'),
			('Multim�dia - 3T - T', 'T�cnico');
GO

INSERT INTO StatusInscricao (NomeStatusInscricao)
VALUES		('Aprovado'),
			('Em Andamento'),
			('Recusado');
GO

INSERT INTO Tecnologia (NomeTecnologia)
VALUES		('C#'),
			('C++'),
			('Flutter'),
			('React'),
			('Xanarin'),
			('JavaScript'),
			('Dart'),
			('Ruby'),
			('.NET');
GO

INSERT INTO Usuario (Email, Senha, IdTipoUsuario)
VALUES		('possarle@email.com', 'possarle1243', 1),
			('douglas@email.com', 'douglas1243', 2),
			('alexia@email.com', 'alexia1243', 2),
			('marcos@email.com', 'marcos1243', 3);
GO


/*CONSERTAR COLUNA NOMERESPONSAVEL*/
INSERT INTO Empresa (NomeResponsavel, CNPJ, EmailContato. NomeFantasia, RazaoSocial, Telefone, NumFuncionario,
						NumCNAE, CEP, Logradouro, Complemento, Localidade, UF, IdUsuario)

VALUES				('Marcos', '3242432433', 'Marcos@gmail.com', 'TechVagas', 'TechVagas', '40028922', 150, '23232342', '01001000', 
						'Pra�a da S�, 345', 'S�o Paulo', 'SP', 4);
