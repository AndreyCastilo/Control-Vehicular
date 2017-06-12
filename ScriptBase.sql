CREATE TABLE Empresa(
	Codigo	   INT PRIMARY KEY IDENTITY,
	Nombre     NVARCHAR(100),
	Fisica	   BIT NOT NULL DEFAULT 1,
	Cedula	   NVARCHAR(10),
	Telefono   NVARCHAR(10),
);

CREATE TABLE Unidad(
	Codigo	   			  INT PRIMARY KEY IDENTITY,
	Empresa    			  INT FOREIGN KEY REFERENCES Empresa(Codigo),
	Placa      			  NVARCHAR(100),
	Anno        			  NVARCHAR(100),
	UltimoAnnoRevision	  int,
	Marca      			  NVARCHAR(100),
	Modelo     			  NVARCHAR(100),
	Latitud				  FLOAT NOT NULL DEFAULT 0,
	Longitud			  FLOAT NOT NULL DEFAULT 0,
	Capacidad  			  INT NOT NULL,
	URLFotografiaUnidad   TEXT,
	URLTarjetaCirculacion TEXT,
	URLRevisionTecnica    TEXT
);

CREATE TABLE Conductor(
	Codigo	   			  INT PRIMARY KEY IDENTITY,
	Empresa    			  INT FOREIGN KEY REFERENCES Empresa(Codigo),
	Nombre     			  NVARCHAR(100),
	URLFotografiaCedula   TEXT,
	URLFotografiaLicencia TEXT,
	TipoLicencia		  TEXT,
	FechaVencimiento	  DATE
);

CREATE TABLE Seguro (
	Codigo	   			  INT PRIMARY KEY IDENTITY,
	Empresa    			  INT FOREIGN KEY REFERENCES Empresa(Codigo),
	Nombre     			  NVARCHAR(100),
	Tipo     			            NVARCHAR(100),
	Detalle				  TEXT
);



CREATE TABLE PadreCliente (
	Codigo	   			  INT PRIMARY KEY IDENTITY,
	Empresa    			  INT FOREIGN KEY REFERENCES Empresa(Codigo),
	Nombre     			  NVARCHAR(100),
	MostrarComo			  NVARCHAR(100),
	Direccion			            TEXT
);


CREATE TABLE ClienteHijo (
	Codigo	   			  INT PRIMARY KEY IDENTITY,
    PadreCliente                 		  INT FOREIGN KEY REFERENCES PadreCliente (Codigo),
	Nombre     			  NVARCHAR(100),
	MostrarComo			  NVARCHAR(100),
);

CREATE TABLE Ruta (
	Codigo	   			  INT PRIMARY KEY IDENTITY,
	Empresa    			  INT FOREIGN KEY REFERENCES Empresa(Codigo),
	Conductor  			  INT FOREIGN KEY REFERENCES Conductor(Codigo),
	Vehiculo   			  INT FOREIGN KEY REFERENCES Unidad(Codigo),
	Nombre     			  NVARCHAR(100),
);

CREATE TABLE ClienteRuta (
	Ruta    			  INT FOREIGN KEY REFERENCES Ruta(Codigo),
	ClienteHijo			  INT FOREIGN KEY REFERENCES ClienteHijo(Codigo),
	CONSTRAINT PK_CLIENTE_RUTA PRIMARY KEY (Ruta, ClienteHijo)
);