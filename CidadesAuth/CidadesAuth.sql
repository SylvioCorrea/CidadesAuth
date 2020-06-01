IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'CidadesAPIdb')
BEGIN
CREATE DATABASE CidadesAPIdb
END

GO
USE CidadesAPIdb;

CREATE TABLE cidades (
  codigo INT IDENTITY(1,1) PRIMARY KEY,
  nome VARCHAR(100) not null,
  uf CHAR(2) not null,
  CONSTRAINT ak_nome_uf UNIQUE(nome,uf)
);

INSERT INTO cidades (nome, uf) VALUES ('Porto Alegre', 'RS');
INSERT INTO cidades (nome, uf) VALUES ('São Paulo', 'SP');
INSERT INTO cidades (nome, uf) VALUES ('Florianópolis', 'SC');
INSERT INTO cidades (nome, uf) VALUES ('Rio de Janeiro', 'RJ');
INSERT INTO cidades (nome, uf) VALUES ('Belo Horizonte', 'MG');
INSERT INTO cidades (nome, uf) VALUES ('Curitiba', 'PR');

CREATE TABLE usuarios (
  id_usuario INT IDENTITY(1,1) PRIMARY KEY,
  nome VARCHAR(20) not null,
  senha VARCHAR(10) not null,
  CONSTRAINT ak_nome UNIQUE(nome)
);

INSERT INTO usuarios (nome, senha) VALUES ('admin', 'admin');

GO
CREATE PROCEDURE cidades_por_uf
AS
SELECT uf, COUNT(*) as cidades_inseridas FROM cidades GROUP BY uf;