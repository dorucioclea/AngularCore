CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(95) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
);

CREATE TABLE `Comments` (
    `Id` varchar(255) NOT NULL,
    `CreatedAt` datetime(6) NOT NULL,
    `ModifiedAt` datetime(6) NULL,
    `UserId` varchar(255) NOT NULL,
    `PostId` varchar(255) NOT NULL,
    `Content` varchar(256) NOT NULL,
    CONSTRAINT `PK_Comments` PRIMARY KEY (`Id`)
);

CREATE TABLE `Users` (
    `Id` varchar(255) NOT NULL,
    `CreatedAt` datetime(6) NOT NULL,
    `ModifiedAt` datetime(6) NULL,
    `Email` longtext NOT NULL,
    `Password` longtext NOT NULL,
    `Name` longtext NULL,
    `Surname` longtext NULL,
    `ProfilePictureId` varchar(255) NULL,
    CONSTRAINT `PK_Users` PRIMARY KEY (`Id`)
);

CREATE TABLE `Posts` (
    `Id` varchar(255) NOT NULL,
    `CreatedAt` datetime(6) NOT NULL,
    `ModifiedAt` datetime(6) NULL,
    `AuthorId` varchar(255) NOT NULL,
    `WallOwnerId` varchar(255) NOT NULL,
    `Content` longtext NOT NULL,
    `Discriminator` longtext NOT NULL,
    `MediaUrl` longtext NULL,
    CONSTRAINT `PK_Posts` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Posts_Users_AuthorId` FOREIGN KEY (`AuthorId`) REFERENCES `Users` (`Id`) ON DELETE RESTRICT,
    CONSTRAINT `FK_Posts_Users_WallOwnerId` FOREIGN KEY (`WallOwnerId`) REFERENCES `Users` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `UserFriends` (
    `UserId` varchar(255) NOT NULL,
    `FriendId` varchar(255) NOT NULL,
    `CreatedAt` datetime(6) NOT NULL,
    `ModifiedAt` datetime(6) NULL,
    CONSTRAINT `PK_UserFriends` PRIMARY KEY (`UserId`, `FriendId`),
    CONSTRAINT `AK_UserFriends_FriendId_UserId` UNIQUE (`FriendId`, `UserId`),
    CONSTRAINT `FK_UserFriends_Users_FriendId` FOREIGN KEY (`FriendId`) REFERENCES `Users` (`Id`) ON DELETE RESTRICT,
    CONSTRAINT `FK_UserFriends_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`Id`) ON DELETE CASCADE
);

CREATE INDEX `IX_Comments_PostId` ON `Comments` (`PostId`);

CREATE INDEX `IX_Comments_UserId` ON `Comments` (`UserId`);

CREATE INDEX `IX_Posts_AuthorId` ON `Posts` (`AuthorId`);

CREATE INDEX `IX_Posts_WallOwnerId` ON `Posts` (`WallOwnerId`);

CREATE INDEX `IX_Users_ProfilePictureId` ON `Users` (`ProfilePictureId`);

ALTER TABLE `Comments` ADD CONSTRAINT `FK_Comments_Posts_PostId` FOREIGN KEY (`PostId`) REFERENCES `Posts` (`Id`) ON DELETE CASCADE;

ALTER TABLE `Comments` ADD CONSTRAINT `FK_Comments_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`Id`) ON DELETE RESTRICT;

ALTER TABLE `Users` ADD CONSTRAINT `FK_Users_Posts_ProfilePictureId` FOREIGN KEY (`ProfilePictureId`) REFERENCES `Posts` (`Id`) ON DELETE RESTRICT;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20190107074610_InitialCreate', '2.1.4-rtm-31024');

ALTER TABLE `Posts` DROP FOREIGN KEY `FK_Posts_Users_WallOwnerId`;

ALTER TABLE `Users` DROP FOREIGN KEY `FK_Users_Posts_ProfilePictureId`;

ALTER TABLE `Posts` DROP COLUMN `MediaUrl`;

ALTER TABLE `Posts` DROP COLUMN `Discriminator`;

CREATE TABLE `Images` (
    `Id` varchar(255) NOT NULL,
    `CreatedAt` datetime(6) NOT NULL,
    `ModifiedAt` datetime(6) NULL,
    `AuthorId` varchar(255) NOT NULL,
    `MediaUrl` longtext NOT NULL,
    `Title` longtext NULL,
    CONSTRAINT `PK_Images` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Images_Users_AuthorId` FOREIGN KEY (`AuthorId`) REFERENCES `Users` (`Id`) ON DELETE RESTRICT
);

CREATE INDEX `IX_Images_AuthorId` ON `Images` (`AuthorId`);

ALTER TABLE `Posts` ADD CONSTRAINT `FK_Posts_Users_WallOwnerId` FOREIGN KEY (`WallOwnerId`) REFERENCES `Users` (`Id`) ON DELETE RESTRICT;

ALTER TABLE `Users` ADD CONSTRAINT `FK_Users_Images_ProfilePictureId` FOREIGN KEY (`ProfilePictureId`) REFERENCES `Images` (`Id`) ON DELETE RESTRICT;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20190209193312_Add_images_reference_to_user', '2.1.4-rtm-31024');

ALTER TABLE `Users` ADD `IsAdmin` bit NOT NULL DEFAULT FALSE;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20190220204111_Add_isAdmin_property_for_user', '2.1.4-rtm-31024');

