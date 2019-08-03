CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(95) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
);

CREATE TABLE `Metas` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` longtext NULL,
    `Slug` longtext NULL,
    `Type` int NOT NULL,
    `Description` longtext NULL,
    `Sort` int NOT NULL,
    `Parent` int NOT NULL,
    `Count` int NOT NULL,
    `Created` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    `Deleted` bit NOT NULL,
    CONSTRAINT `PK_Metas` PRIMARY KEY (`Id`)
);

CREATE TABLE `Options` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` longtext NULL,
    `Value` longtext NULL,
    `Description` longtext NULL,
    `Editable` bit NOT NULL,
    `Created` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    `Deleted` bit NOT NULL,
    CONSTRAINT `PK_Options` PRIMARY KEY (`Id`)
);

CREATE TABLE `Users` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Username` longtext NULL,
    `Password` longtext NULL,
    `Email` longtext NULL,
    `HomeUrl` longtext NULL,
    `ScreenName` longtext NULL,
    `Created` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    `Deleted` bit NOT NULL,
    `Activated` datetime(6) NOT NULL,
    `Logged` datetime(6) NOT NULL,
    `GroupName` longtext NULL,
    CONSTRAINT `PK_Users` PRIMARY KEY (`Id`)
);

CREATE TABLE `Attachments` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `AuthorId` int NOT NULL,
    `FName` longtext NULL,
    `FType` longtext NULL,
    `FKey` longtext NULL,
    `Created` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    `Deleted` bit NOT NULL,
    CONSTRAINT `PK_Attachments` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Attachments_Users_AuthorId` FOREIGN KEY (`AuthorId`) REFERENCES `Users` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `Contents` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Title` longtext NULL,
    `Slug` longtext NULL,
    `Modified` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
    `Content` longtext NULL,
    `Hits` int NOT NULL,
    `Type` int NOT NULL,
    `FmtType` longtext NULL,
    `ThumbImg` longtext NULL,
    `Tags` longtext NULL,
    `Categories` longtext NULL,
    `Status` int NOT NULL,
    `CommentsNum` int NOT NULL,
    `AllowComment` bit NOT NULL,
    `AllowPing` bit NOT NULL,
    `AllowFeed` bit NOT NULL,
    `Url` longtext NULL,
    `AuthorId` int NOT NULL,
    `Created` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    `Deleted` bit NOT NULL,
    CONSTRAINT `PK_Contents` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Contents_Users_AuthorId` FOREIGN KEY (`AuthorId`) REFERENCES `Users` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `BlogSyncRelationships` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Created` datetime(6) NOT NULL,
    `Deleted` bit NOT NULL,
    `ContentId` int NOT NULL,
    `Target` int NOT NULL,
    `TargetPostId` longtext NULL,
    `SyncData` datetime(6) NOT NULL,
    `Message` longtext NULL,
    `ExtensionProperty` longtext NULL,
    CONSTRAINT `PK_BlogSyncRelationships` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_BlogSyncRelationships_Contents_ContentId` FOREIGN KEY (`ContentId`) REFERENCES `Contents` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `Comments` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `IsAuthor` bit NOT NULL,
    `Created` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    `Deleted` bit NOT NULL,
    `Cid` int NOT NULL,
    `Author` longtext NULL,
    `OwnerId` int NOT NULL,
    `Mail` longtext NULL,
    `Url` longtext NULL,
    `Ip` longtext NULL,
    `Agent` longtext NULL,
    `Content` longtext NULL,
    `Type` longtext NULL,
    `Status` int NOT NULL,
    `Parent` int NOT NULL,
    CONSTRAINT `PK_Comments` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Comments_Contents_Cid` FOREIGN KEY (`Cid`) REFERENCES `Contents` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `Relationships` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Cid` int NOT NULL,
    `Mid` int NOT NULL,
    `Created` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    `Deleted` bit NOT NULL,
    CONSTRAINT `PK_Relationships` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Relationships_Contents_Cid` FOREIGN KEY (`Cid`) REFERENCES `Contents` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Relationships_Metas_Mid` FOREIGN KEY (`Mid`) REFERENCES `Metas` (`Id`) ON DELETE CASCADE
);

CREATE INDEX `IX_Attachments_AuthorId` ON `Attachments` (`AuthorId`);

CREATE INDEX `IX_BlogSyncRelationships_ContentId` ON `BlogSyncRelationships` (`ContentId`);

CREATE INDEX `IX_Comments_Cid` ON `Comments` (`Cid`);

CREATE INDEX `IX_Contents_AuthorId` ON `Contents` (`AuthorId`);

CREATE INDEX `IX_Relationships_Cid` ON `Relationships` (`Cid`);

CREATE INDEX `IX_Relationships_Mid` ON `Relationships` (`Mid`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20190803040633_init', '2.2.4-servicing-10062');

