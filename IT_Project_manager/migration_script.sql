CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);

BEGIN TRANSACTION;

CREATE TABLE "Managers" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Managers" PRIMARY KEY AUTOINCREMENT,
    "first_name" TEXT NOT NULL,
    "surname" TEXT NOT NULL,
    "telephone" TEXT NOT NULL
);

CREATE TABLE "Members" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Members" PRIMARY KEY AUTOINCREMENT,
    "Name" TEXT NOT NULL,
    "Surname" TEXT NOT NULL,
    "Email" TEXT NULL,
    "DateOfBirth" TEXT NOT NULL
);

CREATE TABLE "ManagerMember" (
    "ManagersId" INTEGER NOT NULL,
    "MembersId" INTEGER NOT NULL,
    CONSTRAINT "PK_ManagerMember" PRIMARY KEY ("ManagersId", "MembersId"),
    CONSTRAINT "FK_ManagerMember_Managers_ManagersId" FOREIGN KEY ("ManagersId") REFERENCES "Managers" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_ManagerMember_Members_MembersId" FOREIGN KEY ("MembersId") REFERENCES "Members" ("Id") ON DELETE CASCADE
);

INSERT INTO "Managers" ("Id", "first_name", "surname", "telephone")
VALUES (1, 'Maciej', 'Krasko', '123-456-789');
INSERT INTO "Managers" ("Id", "first_name", "surname", "telephone")
VALUES (2, 'Zuzanna', 'Krasko', '987-654-321');

INSERT INTO "Members" ("Id", "DateOfBirth", "Email", "Name", "Surname")
VALUES (1, '0001-01-01 00:00:00', 'krzysiek.palonek@gmail.com', 'Krzysztof', 'Palonek');
INSERT INTO "Members" ("Id", "DateOfBirth", "Email", "Name", "Surname")
VALUES (2, '0001-01-01 00:00:00', 'marz.koł@gmail.com', 'Marzena', 'Kołodziej');
INSERT INTO "Members" ("Id", "DateOfBirth", "Email", "Name", "Surname")
VALUES (3, '0001-01-01 00:00:00', 'jan.kow@gmail.com', 'Jan', 'Kowalski');
INSERT INTO "Members" ("Id", "DateOfBirth", "Email", "Name", "Surname")
VALUES (4, '0001-01-01 00:00:00', 'Nat.uro@gmail.com', 'Natalia', 'Urodek');

INSERT INTO "ManagerMember" ("ManagersId", "MembersId")
VALUES (1, 1);
INSERT INTO "ManagerMember" ("ManagersId", "MembersId")
VALUES (1, 2);
INSERT INTO "ManagerMember" ("ManagersId", "MembersId")
VALUES (2, 3);
INSERT INTO "ManagerMember" ("ManagersId", "MembersId")
VALUES (2, 4);

CREATE INDEX "IX_ManagerMember_MembersId" ON "ManagerMember" ("MembersId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20221121204702_Creating', '7.0.0');

COMMIT;

