CREATE TABLE IF NOT EXISTS "Position"
(
    "Id"                      UUID         NOT NULL DEFAULT gen_random_uuid(),
    "UserCreatorId"           UUID         NOT NULL,
    "CreationTime"            DATE         NOT NULL,
    "State"                   BOOL         NOT NULL,
    "UserModifierId"          UUID         NOT NULL,
    "DateLastModify"          DATE,
    "Description"             VARCHAR(500) NOT NULL,
    "CatalogLevelDescription" VARCHAR(50),
    "ClientDescription"       VARCHAR(120),
    "CatalogLevelId"          UUID,
    "ClientId"                UUID

);

CREATE TABLE IF NOT EXISTS "PositionSkill"
(
    "Id"                UUID     NOT NULL DEFAULT gen_random_uuid(),
    "UserCreatorId"     UUID     NOT NULL,
    "CreationTime"      DATE     NOT NULL,
    "State"             BOOL     NOT NULL,
    "UserModifierId"    UUID     NOT NULL,
    "DateLastModify"    DATE,
    "SkillId"           UUID,
    "PositionId"        UUID     NOT NULL,
    "SkillName"         VARCHAR(80),
    "MinToAccept"       SMALLINT NOT NULL
        CONSTRAINT "MinToAccept_CHECK" CHECK ("MinToAccept" >= 0 AND "MinToAccept" <= 100),
    "PositionSkillType" SMALLINT NOT NULL,
    CONSTRAINT "CK_PositionSkillType"
        CHECK ( "PositionSkillType" IN (0, 1, 2) ),
    CONSTRAINT "UK_SkillId_PositionId"
        UNIQUE ("SkillId", "PositionId")

);

CREATE TABLE IF NOT EXISTS "ResourcePosition"
(
    "Id"                   UUID         NOT NULL DEFAULT gen_random_uuid(),
    "UserCreatorId"        UUID         NOT NULL,
    "CreationTime"         DATE         NOT NULL,
    "State"                BOOL         NOT NULL,
    "UserModifierId"       UUID         NOT NULL,
    "DateLastModify"       DATE,
    "PercentMatchPosition" SMALLINT     NOT NULL DEFAULT 0,
    "IsDefault"            BOOL         NOT NULL,
    "ResourceName"         VARCHAR(100),
    "ResourceId"           UUID,
    "PositionId"           UUID         NOT NULL,
    "RomaId"               VARCHAR(100) NOT NULL
);

ALTER TABLE "Position"
    ADD CONSTRAINT "PK_Position_Id" PRIMARY KEY ("Id");
ALTER TABLE "PositionSkill"
    ADD CONSTRAINT "PK_PositionSkill_Id" PRIMARY KEY ("Id");
ALTER TABLE "ResourcePosition"
    ADD CONSTRAINT "PK_ResourcePosition_Id" PRIMARY KEY ("Id");

ALTER TABLE "PositionSkill"
    ADD CONSTRAINT "FK_PositionSkill_Position_Id" FOREIGN KEY ("PositionId") REFERENCES "Position" ("Id")
        ON UPDATE CASCADE;
ALTER Table "ResourcePosition"
    ADD CONSTRAINT "FK_ResourcePosition_Position_Id" FOREIGN KEY ("PositionId") REFERENCES "Position" ("Id")
        ON UPDATE CASCADE;
