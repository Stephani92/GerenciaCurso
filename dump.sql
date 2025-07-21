CREATE TABLE Roles (
    Nome NVARCHAR(100) NOT NULL PRIMARY KEY
);


CREATE TABLE Usuarios (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    Nome NVARCHAR(100) NOT NULL,
    Sobrenome NVARCHAR(100) NOT NULL,
    Cpf NVARCHAR(11) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(MAX) NOT NULL,
    Email NVARCHAR(150) NOT NULL UNIQUE,
    DataNascimento DATETIME NOT NULL,
    Ativo BIT NOT NULL,
    Discriminator NVARCHAR(100) NOT NULL
);

CREATE TABLE UserRoles (
    UsuarioId UNIQUEIDENTIFIER NOT NULL,
    RoleNome NVARCHAR(100) NOT NULL,
    PRIMARY KEY (UsuarioId, RoleNome),
    FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id),
    FOREIGN KEY (RoleNome) REFERENCES Roles(Nome)
);

CREATE TABLE Turmas (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    Nome NVARCHAR(100) NOT NULL,
    Descricao NVARCHAR(255),
    DataCriacao DATETIME NOT NULL,
    Ativo BIT NOT NULL
);

CREATE TABLE Matriculas (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    AlunoId UNIQUEIDENTIFIER NOT NULL ,
    TurmaId UNIQUEIDENTIFIER NOT NULL,
    DataMatricula DATETIME NOT NULL,
    Ativo BIT NOT NULL,
    FOREIGN KEY (AlunoId) REFERENCES Usuarios(Id),
    FOREIGN KEY (TurmaId) REFERENCES Turmas(Id),
    CONSTRAINT UQ_Matriculas_Aluno_Turma UNIQUE (AlunoId, TurmaId)
);

insert Roles(Nome) values('Adm'), ('Aluno');

INSERT INTO Usuarios (
    Nome,
    Sobrenome,
    Cpf,
    PasswordHash,
    Email,
    DataNascimento,
    Ativo,
    Discriminator
)
VALUES (
    'João',
    'Silva',
    '54142330047', 
    'WRJ//aVgpMRx0SAiDGVuD+pfRAz0cxvm0vpFyTpVl+A=', -- senha =  SouSenh@213
    'joao.silva@example.com', 
    '1990-05-15',
    1,
    'Usuario'
);