CREATE DATABASE dbsinais;

CREATE TABLE [dbo].[sinal] (
    [id]          INT           IDENTITY (1, 1) NOT NULL,
    [codigo]      VARCHAR (20)  NOT NULL,
    [descricao]   VARCHAR (250) NULL,
    [tipo_sinal]  INT           DEFAULT ((0)) NOT NULL,
    [forma_sinal] INT           DEFAULT ((0)) NOT NULL,
    [em_uso]      BIT           NOT NULL,
    [morada]      VARCHAR (250) NULL,
    [imagem_url]  VARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

CREATE TABLE [dbo].[intervencao] (
    [id]               INT           IDENTITY (1, 1) NOT NULL,
    [sinal_id]         INT           NOT NULL,
    [descricao]        VARCHAR (MAX) NOT NULL,
    [morada]           VARCHAR (MAX) NOT NULL,
    [data_intervencao] DATETIME      NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [fk_intervencao_sinal] FOREIGN KEY ([sinal_id]) REFERENCES [dbo].[sinal] ([id])
);

CREATE TABLE [dbo].[acidente] (
    [id]             INT           IDENTITY (1, 1) NOT NULL,
    [intervencao_id] INT           NOT NULL,
    [descricao]      VARCHAR (MAX) NOT NULL,
    [morada]         VARCHAR (MAX) NOT NULL,
    [data_acidente]  DATETIME      NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [fk_acidente_intervencao] FOREIGN KEY ([intervencao_id]) REFERENCES [dbo].[intervencao] ([id])
);