use PRODUTO;

--criação de tabelas
CREATE TABLE marcas
(
	id INT PRIMARY KEY IDENTITY(1,1),
	nome VARCHAR (50),
);

CREATE TABLE produto
(
	id INT PRIMARY KEY IDENTITY(1,1),
	marcas_id INT,
	nome VARCHAR(100),
	tamanho VARCHAR(5),
	fotos VARCHAR(50)
);

--criação de relacionamentos
ALTER TABLE produto
ADD CONSTRAINT fk_marcas_id
FOREIGN KEY (marcas_id)
REFERENCES marcas(id);

--inserção de dados
INSERT marcas
(nome)
VALUES
('BANDAI'),
('JAKKS'),
('IRON STUDIOS'),
('HOT TOYS');

INSERT produto
(marcas_id,nome,tamanho,fotos)
VALUES
(1,'Gundam Exia - Mobile Suit Gundam 00','30cm','fbl.png'),
(2,'Metroid Samus Aran','15cm','fbl.png'),
(3,'Hattori Hanzo - Scorpion','40cm','fbl.png'),
(4,'Zombie Deadpool','41cm','fbl.png');



select * from marcas;
select * from produto;