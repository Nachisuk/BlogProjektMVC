
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/21/2019 21:48:21
-- Generated from EDMX file: D:\BlogProjekt-master\BlogProjekt\Models\BlogBaza.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [BlogBazaDanych];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_BlogsKinds_Blogs]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BlogsKinds] DROP CONSTRAINT [FK_BlogsKinds_Blogs];
GO
IF OBJECT_ID(N'[dbo].[FK_BlogsKinds_Kinds]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BlogsKinds] DROP CONSTRAINT [FK_BlogsKinds_Kinds];
GO
IF OBJECT_ID(N'[dbo].[FK_BlogsPost]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PostSet] DROP CONSTRAINT [FK_BlogsPost];
GO
IF OBJECT_ID(N'[dbo].[FK_UsersBlogs]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BlogsSet] DROP CONSTRAINT [FK_UsersBlogs];
GO
IF OBJECT_ID(N'[dbo].[FK_BlogsFollows]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FollowsSet] DROP CONSTRAINT [FK_BlogsFollows];
GO
IF OBJECT_ID(N'[dbo].[FK_PostComments]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CommentsSet] DROP CONSTRAINT [FK_PostComments];
GO
IF OBJECT_ID(N'[dbo].[FK_CommentsVotes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VotesSet] DROP CONSTRAINT [FK_CommentsVotes];
GO
IF OBJECT_ID(N'[dbo].[FK_PostTags_Post]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PostTags] DROP CONSTRAINT [FK_PostTags_Post];
GO
IF OBJECT_ID(N'[dbo].[FK_PostTags_Tags]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PostTags] DROP CONSTRAINT [FK_PostTags_Tags];
GO
IF OBJECT_ID(N'[dbo].[FK_UsersFollows]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FollowsSet] DROP CONSTRAINT [FK_UsersFollows];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[BlogsSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BlogsSet];
GO
IF OBJECT_ID(N'[dbo].[KindsSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[KindsSet];
GO
IF OBJECT_ID(N'[dbo].[CommentsSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CommentsSet];
GO
IF OBJECT_ID(N'[dbo].[PostSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PostSet];
GO
IF OBJECT_ID(N'[dbo].[TagsSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TagsSet];
GO
IF OBJECT_ID(N'[dbo].[UsersSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UsersSet];
GO
IF OBJECT_ID(N'[dbo].[VotesSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VotesSet];
GO
IF OBJECT_ID(N'[dbo].[FollowsSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FollowsSet];
GO
IF OBJECT_ID(N'[dbo].[BlogsKinds]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BlogsKinds];
GO
IF OBJECT_ID(N'[dbo].[PostTags]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PostTags];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'BlogsSet'
CREATE TABLE [dbo].[BlogsSet] (
    [Blog_ID] int IDENTITY(1,1) NOT NULL,
    [dataZalozenia] datetime  NOT NULL,
    [status] nvarchar(max)  NOT NULL,
    [dataAktualizacji] datetime  NOT NULL,
    [nazwa] nvarchar(max)  NOT NULL,
    [UsersUser_ID] int  NOT NULL,
    [followCount] int  NOT NULL
);
GO

-- Creating table 'KindsSet'
CREATE TABLE [dbo].[KindsSet] (
    [Kind_ID] int IDENTITY(1,1) NOT NULL,
    [kindName] nvarchar(max)  NOT NULL,
    [dataStworzenia] datetime  NOT NULL,
    [dataAktualizacji] datetime  NOT NULL
);
GO

-- Creating table 'CommentsSet'
CREATE TABLE [dbo].[CommentsSet] (
    [Comment_ID] int IDENTITY(1,1) NOT NULL,
    [dataIGodzina] datetime  NOT NULL,
    [upVotes] int  NOT NULL,
    [Post_ID] int  NOT NULL,
    [User_ID] int  NOT NULL,
    [content] nvarchar(max)  NOT NULL,
    [dataStworzenia] datetime  NOT NULL,
    [dataAktualizacji] datetime  NOT NULL,
    [PostPost_ID] int  NOT NULL
);
GO

-- Creating table 'PostSet'
CREATE TABLE [dbo].[PostSet] (
    [Post_ID] int IDENTITY(1,1) NOT NULL,
    [dataStworzenia] datetime  NOT NULL,
    [status] nvarchar(max)  NOT NULL,
    [ifTop] bit  NOT NULL,
    [dataAktualizacji] datetime  NOT NULL,
    [text_content] nvarchar(max)  NOT NULL,
    [img_route] nvarchar(max)  NOT NULL,
    [title] nvarchar(max)  NOT NULL,
    [image_file_name] nvarchar(max)  NOT NULL,
    [image_content_type] nvarchar(max)  NOT NULL,
    [image_file_size] int  NOT NULL,
    [image_upload_Date] datetime  NOT NULL,
    [BlogsBlog_ID] int  NOT NULL
);
GO

-- Creating table 'TagsSet'
CREATE TABLE [dbo].[TagsSet] (
    [Tag_ID] int IDENTITY(1,1) NOT NULL,
    [tagName] nvarchar(max)  NOT NULL,
    [dataStworzenia] datetime  NOT NULL,
    [dataAktualizacji] datetime  NOT NULL
);
GO

-- Creating table 'UsersSet'
CREATE TABLE [dbo].[UsersSet] (
    [User_ID] int IDENTITY(1,1) NOT NULL,
    [username] nvarchar(max)  NOT NULL,
    [password] nvarchar(max)  NOT NULL,
    [name] nvarchar(max)  NOT NULL,
    [surname] nvarchar(max)  NOT NULL,
    [email] nvarchar(max)  NOT NULL,
    [phoneNumber] nvarchar(max)  NOT NULL,
    [dataStworzenia] datetime  NOT NULL,
    [dataAktualizacji] datetime  NOT NULL,
    [isAdmin] bit  NOT NULL
);
GO

-- Creating table 'VotesSet'
CREATE TABLE [dbo].[VotesSet] (
    [Vote_ID] int IDENTITY(1,1) NOT NULL,
    [CommentsComment_ID] int  NOT NULL,
    [UsersUser_ID] int  NOT NULL
);
GO

-- Creating table 'FollowsSet'
CREATE TABLE [dbo].[FollowsSet] (
    [Follow_ID] int IDENTITY(1,1) NOT NULL,
    [UsersUser_ID] int  NOT NULL,
    [BlogsBlog_ID] int  NOT NULL
);
GO

-- Creating table 'BlogsKinds'
CREATE TABLE [dbo].[BlogsKinds] (
    [Blogs_Blog_ID] int  NOT NULL,
    [Kinds_Kind_ID] int  NOT NULL
);
GO

-- Creating table 'PostTags'
CREATE TABLE [dbo].[PostTags] (
    [Post_Post_ID] int  NOT NULL,
    [Tags_Tag_ID] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Blog_ID] in table 'BlogsSet'
ALTER TABLE [dbo].[BlogsSet]
ADD CONSTRAINT [PK_BlogsSet]
    PRIMARY KEY CLUSTERED ([Blog_ID] ASC);
GO

-- Creating primary key on [Kind_ID] in table 'KindsSet'
ALTER TABLE [dbo].[KindsSet]
ADD CONSTRAINT [PK_KindsSet]
    PRIMARY KEY CLUSTERED ([Kind_ID] ASC);
GO

-- Creating primary key on [Comment_ID] in table 'CommentsSet'
ALTER TABLE [dbo].[CommentsSet]
ADD CONSTRAINT [PK_CommentsSet]
    PRIMARY KEY CLUSTERED ([Comment_ID] ASC);
GO

-- Creating primary key on [Post_ID] in table 'PostSet'
ALTER TABLE [dbo].[PostSet]
ADD CONSTRAINT [PK_PostSet]
    PRIMARY KEY CLUSTERED ([Post_ID] ASC);
GO

-- Creating primary key on [Tag_ID] in table 'TagsSet'
ALTER TABLE [dbo].[TagsSet]
ADD CONSTRAINT [PK_TagsSet]
    PRIMARY KEY CLUSTERED ([Tag_ID] ASC);
GO

-- Creating primary key on [User_ID] in table 'UsersSet'
ALTER TABLE [dbo].[UsersSet]
ADD CONSTRAINT [PK_UsersSet]
    PRIMARY KEY CLUSTERED ([User_ID] ASC);
GO

-- Creating primary key on [Vote_ID] in table 'VotesSet'
ALTER TABLE [dbo].[VotesSet]
ADD CONSTRAINT [PK_VotesSet]
    PRIMARY KEY CLUSTERED ([Vote_ID] ASC);
GO

-- Creating primary key on [Follow_ID] in table 'FollowsSet'
ALTER TABLE [dbo].[FollowsSet]
ADD CONSTRAINT [PK_FollowsSet]
    PRIMARY KEY CLUSTERED ([Follow_ID] ASC);
GO

-- Creating primary key on [Blogs_Blog_ID], [Kinds_Kind_ID] in table 'BlogsKinds'
ALTER TABLE [dbo].[BlogsKinds]
ADD CONSTRAINT [PK_BlogsKinds]
    PRIMARY KEY CLUSTERED ([Blogs_Blog_ID], [Kinds_Kind_ID] ASC);
GO

-- Creating primary key on [Post_Post_ID], [Tags_Tag_ID] in table 'PostTags'
ALTER TABLE [dbo].[PostTags]
ADD CONSTRAINT [PK_PostTags]
    PRIMARY KEY CLUSTERED ([Post_Post_ID], [Tags_Tag_ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Blogs_Blog_ID] in table 'BlogsKinds'
ALTER TABLE [dbo].[BlogsKinds]
ADD CONSTRAINT [FK_BlogsKinds_Blogs]
    FOREIGN KEY ([Blogs_Blog_ID])
    REFERENCES [dbo].[BlogsSet]
        ([Blog_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Kinds_Kind_ID] in table 'BlogsKinds'
ALTER TABLE [dbo].[BlogsKinds]
ADD CONSTRAINT [FK_BlogsKinds_Kinds]
    FOREIGN KEY ([Kinds_Kind_ID])
    REFERENCES [dbo].[KindsSet]
        ([Kind_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BlogsKinds_Kinds'
CREATE INDEX [IX_FK_BlogsKinds_Kinds]
ON [dbo].[BlogsKinds]
    ([Kinds_Kind_ID]);
GO

-- Creating foreign key on [BlogsBlog_ID] in table 'PostSet'
ALTER TABLE [dbo].[PostSet]
ADD CONSTRAINT [FK_BlogsPost]
    FOREIGN KEY ([BlogsBlog_ID])
    REFERENCES [dbo].[BlogsSet]
        ([Blog_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BlogsPost'
CREATE INDEX [IX_FK_BlogsPost]
ON [dbo].[PostSet]
    ([BlogsBlog_ID]);
GO

-- Creating foreign key on [UsersUser_ID] in table 'BlogsSet'
ALTER TABLE [dbo].[BlogsSet]
ADD CONSTRAINT [FK_UsersBlogs]
    FOREIGN KEY ([UsersUser_ID])
    REFERENCES [dbo].[UsersSet]
        ([User_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UsersBlogs'
CREATE INDEX [IX_FK_UsersBlogs]
ON [dbo].[BlogsSet]
    ([UsersUser_ID]);
GO

-- Creating foreign key on [BlogsBlog_ID] in table 'FollowsSet'
ALTER TABLE [dbo].[FollowsSet]
ADD CONSTRAINT [FK_BlogsFollows]
    FOREIGN KEY ([BlogsBlog_ID])
    REFERENCES [dbo].[BlogsSet]
        ([Blog_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BlogsFollows'
CREATE INDEX [IX_FK_BlogsFollows]
ON [dbo].[FollowsSet]
    ([BlogsBlog_ID]);
GO

-- Creating foreign key on [PostPost_ID] in table 'CommentsSet'
ALTER TABLE [dbo].[CommentsSet]
ADD CONSTRAINT [FK_PostComments]
    FOREIGN KEY ([PostPost_ID])
    REFERENCES [dbo].[PostSet]
        ([Post_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PostComments'
CREATE INDEX [IX_FK_PostComments]
ON [dbo].[CommentsSet]
    ([PostPost_ID]);
GO

-- Creating foreign key on [CommentsComment_ID] in table 'VotesSet'
ALTER TABLE [dbo].[VotesSet]
ADD CONSTRAINT [FK_CommentsVotes]
    FOREIGN KEY ([CommentsComment_ID])
    REFERENCES [dbo].[CommentsSet]
        ([Comment_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CommentsVotes'
CREATE INDEX [IX_FK_CommentsVotes]
ON [dbo].[VotesSet]
    ([CommentsComment_ID]);
GO

-- Creating foreign key on [Post_Post_ID] in table 'PostTags'
ALTER TABLE [dbo].[PostTags]
ADD CONSTRAINT [FK_PostTags_Post]
    FOREIGN KEY ([Post_Post_ID])
    REFERENCES [dbo].[PostSet]
        ([Post_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Tags_Tag_ID] in table 'PostTags'
ALTER TABLE [dbo].[PostTags]
ADD CONSTRAINT [FK_PostTags_Tags]
    FOREIGN KEY ([Tags_Tag_ID])
    REFERENCES [dbo].[TagsSet]
        ([Tag_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PostTags_Tags'
CREATE INDEX [IX_FK_PostTags_Tags]
ON [dbo].[PostTags]
    ([Tags_Tag_ID]);
GO

-- Creating foreign key on [UsersUser_ID] in table 'FollowsSet'
ALTER TABLE [dbo].[FollowsSet]
ADD CONSTRAINT [FK_UsersFollows]
    FOREIGN KEY ([UsersUser_ID])
    REFERENCES [dbo].[UsersSet]
        ([User_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UsersFollows'
CREATE INDEX [IX_FK_UsersFollows]
ON [dbo].[FollowsSet]
    ([UsersUser_ID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------