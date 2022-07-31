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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220514144150_CreateInitial')
BEGIN
    CREATE TABLE [Cars] (
        [Id] int NOT NULL IDENTITY,
        [BrandName] nvarchar(max) NULL,
        [ManufactureYear] nvarchar(max) NULL,
        [Model] nvarchar(max) NULL,
        CONSTRAINT [PK_Cars] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220514144150_CreateInitial')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220514144150_CreateInitial', N'7.0.0-preview.5.22302.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220606153745_CarClassAnnotations')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Cars]') AND [c].[name] = N'Model');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Cars] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Cars] ALTER COLUMN [Model] nvarchar(25) NOT NULL;
    ALTER TABLE [Cars] ADD DEFAULT N'' FOR [Model];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220606153745_CarClassAnnotations')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Cars]') AND [c].[name] = N'ManufactureYear');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Cars] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Cars] ALTER COLUMN [ManufactureYear] nvarchar(4) NOT NULL;
    ALTER TABLE [Cars] ADD DEFAULT N'' FOR [ManufactureYear];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220606153745_CarClassAnnotations')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Cars]') AND [c].[name] = N'BrandName');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Cars] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [Cars] ALTER COLUMN [BrandName] nvarchar(25) NOT NULL;
    ALTER TABLE [Cars] ADD DEFAULT N'' FOR [BrandName];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220606153745_CarClassAnnotations')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220606153745_CarClassAnnotations', N'7.0.0-preview.5.22302.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220606160459_CarDataSeed')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'BrandName', N'ManufactureYear', N'Model') AND [object_id] = OBJECT_ID(N'[Cars]'))
        SET IDENTITY_INSERT [Cars] ON;
    EXEC(N'INSERT INTO [Cars] ([Id], [BrandName], [ManufactureYear], [Model])
    VALUES (1, N''LAMBORGINI'', N''1998'', N''COUNTACH''),
    (2, N''PORSCHE'', N''1976'', N''911 TURBO''),
    (3, N''FORD'', N''1968'', N''MUSTANG''),
    (4, N''HONDA'', N''2001'', N''CIVIC''),
    (5, N''JEEP'', N''2019'', N''RUBICON''),
    (6, N''SUBARU'', N''1999'', N''IMPREZA''),
    (7, N''CHEVROLET'', N''2004'', N''CORVETTE''),
    (8, N''FERRARI'', N''1997'', N''F40''),
    (9, N''DODGE'', N''2013'', N''CHARGER''),
    (10, N''MAZDA'', N''1998'', N''RX-3''),
    (11, N''MERCEDES'', N''2010'', N''G-CLASS''),
    (12, N''DODGE'', N''2002'', N''VIPER SRT''),
    (13, N''TOYOTA'', N''1999'', N''Supra''),
    (14, N''HONDA'', N''2002'', N''S2000''),
    (15, N''BMW'', N''2022'', N''M5'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'BrandName', N'ManufactureYear', N'Model') AND [object_id] = OBJECT_ID(N'[Cars]'))
        SET IDENTITY_INSERT [Cars] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220606160459_CarDataSeed')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220606160459_CarDataSeed', N'7.0.0-preview.5.22302.2');
END;
GO

COMMIT;
GO

