
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/11/2017 09:48:05
-- Generated from EDMX file: C:\Users\Administrator\Desktop\MagneticNote-new\Model\MagneticNote\MagneticNote.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [MagneticNote];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_UserBookGroup]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BookGroup] DROP CONSTRAINT [FK_UserBookGroup];
GO
IF OBJECT_ID(N'[dbo].[FK_NoteBookNote]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Note] DROP CONSTRAINT [FK_NoteBookNote];
GO
IF OBJECT_ID(N'[dbo].[FK_UserNoteBook]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NoteBook] DROP CONSTRAINT [FK_UserNoteBook];
GO
IF OBJECT_ID(N'[dbo].[FK_BookGroupNoteBook]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NoteBook] DROP CONSTRAINT [FK_BookGroupNoteBook];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User];
GO
IF OBJECT_ID(N'[dbo].[BookGroup]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BookGroup];
GO
IF OBJECT_ID(N'[dbo].[NoteBook]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NoteBook];
GO
IF OBJECT_ID(N'[dbo].[Note]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Note];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'User'
CREATE TABLE [dbo].[User] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Email] nvarchar(48)  NOT NULL,
    [Password] nvarchar(255)  NOT NULL,
    [Account] nvarchar(48)  NOT NULL
);
GO

-- Creating table 'BookGroup'
CREATE TABLE [dbo].[BookGroup] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [UserId] int  NOT NULL
);
GO

-- Creating table 'NoteBook'
CREATE TABLE [dbo].[NoteBook] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [UserId] int  NOT NULL,
    [BookGroupId] int  NOT NULL
);
GO

-- Creating table 'Note'
CREATE TABLE [dbo].[Note] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(255)  NOT NULL,
    [Content] nvarchar(max)  NOT NULL,
    [CreateDate] datetime  NOT NULL,
    [UpdateDate] datetime  NOT NULL,
    [NoteBookId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'User'
ALTER TABLE [dbo].[User]
ADD CONSTRAINT [PK_User]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BookGroup'
ALTER TABLE [dbo].[BookGroup]
ADD CONSTRAINT [PK_BookGroup]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'NoteBook'
ALTER TABLE [dbo].[NoteBook]
ADD CONSTRAINT [PK_NoteBook]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Note'
ALTER TABLE [dbo].[Note]
ADD CONSTRAINT [PK_Note]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserId] in table 'BookGroup'
ALTER TABLE [dbo].[BookGroup]
ADD CONSTRAINT [FK_UserBookGroup]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[User]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserBookGroup'
CREATE INDEX [IX_FK_UserBookGroup]
ON [dbo].[BookGroup]
    ([UserId]);
GO

-- Creating foreign key on [NoteBookId] in table 'Note'
ALTER TABLE [dbo].[Note]
ADD CONSTRAINT [FK_NoteBookNote]
    FOREIGN KEY ([NoteBookId])
    REFERENCES [dbo].[NoteBook]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_NoteBookNote'
CREATE INDEX [IX_FK_NoteBookNote]
ON [dbo].[Note]
    ([NoteBookId]);
GO

-- Creating foreign key on [UserId] in table 'NoteBook'
ALTER TABLE [dbo].[NoteBook]
ADD CONSTRAINT [FK_UserNoteBook]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[User]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserNoteBook'
CREATE INDEX [IX_FK_UserNoteBook]
ON [dbo].[NoteBook]
    ([UserId]);
GO

-- Creating foreign key on [BookGroupId] in table 'NoteBook'
ALTER TABLE [dbo].[NoteBook]
ADD CONSTRAINT [FK_BookGroupNoteBook]
    FOREIGN KEY ([BookGroupId])
    REFERENCES [dbo].[BookGroup]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BookGroupNoteBook'
CREATE INDEX [IX_FK_BookGroupNoteBook]
ON [dbo].[NoteBook]
    ([BookGroupId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------