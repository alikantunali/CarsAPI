IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Cars] (
    [Id] int NOT NULL IDENTITY,
    [BrandName] nvarchar(max) NULL,
    [ManufactureYear] nvarchar(max) NULL,
    [Model] nvarchar(max) NULL,
    CONSTRAINT [PK_Cars] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220514144150_CreateInitial', N'7.0.0-preview.6.22329.4');
GO

COMMIT;
GO

