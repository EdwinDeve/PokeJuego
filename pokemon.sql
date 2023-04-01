CREATE DATABASE dbPokemon


CREATE TABLE _tbTipoPokemon(
	intTipoPokemon int PRIMARY KEY IDENTITY(1,1) NOT NULL,
	strTipoPokemon VARCHAR (50) NOT NULL,
	
)

CREATE TABLE _tbAtaques(
	intAtaque int PRIMARY KEY IDENTITY(1,1) NOT NULL,
	strNombreAtaque varchar (50) NOT NULL,
	intDaño int NOT NULL,
	
)

CREATE TABLE _tbAtaqueEspecial(
	intAtaqueEsp int PRIMARY KEY IDENTITY(1,1) NOT NULL,
	strNombreAtaqueEsp varchar (50) NOT NULL,
	intDañoEsp int NOT NULL,
	
)

CREATE TABLE _tbPokemon(
	intPokemon int PRIMARY KEY IDENTITY(1,1) NOT NULL,
	strNombrePokemon VARCHAR(50) NOT NULL,
	strImagen, VARCHAR(MAX),
	tnyVida tinyint NOT NULL,
	intTipoPokemon int NOT NULL,
	intAtaque int NOT NULL,
	intAtaqueEsp int NOT NULL,
	FOREIGN KEY(intTipoPokemon) REFERENCES _tbTipoPokemon (intTipoPokemon),
	FOREIGN KEY(intAtaque) REFERENCES _tbAtaques (intAtaque),
	FOREIGN KEY(intAtaqueEsp) REFERENCES _tbAtaqueEspecial (intAtaqueEsp),
	
)