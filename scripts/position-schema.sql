CREATE TYPE SkillType AS ENUM ('0', '1', '2');

CREATE TABLE IF NOT EXISTS "Position"
(
    "Id"                UUID         NOT NULL DEFAULT gen_random_uuid(),
    "UserCreatorId"     UUID         NOT NULL UNIQUE,
    "CreationTime"      DATE         NOT NULL,
    "State"             BOOL         NOT NULL,
    "UserModifierId"    UUID         NOT NULL UNIQUE,
    "DateLastModify"    DATE         NOT NULL,
    "Description"       VARCHAR(500) NOT NULL,
    "ClientId"          UUID         NOT NULL,
    "ClientDescription" VARCHAR(200) NOT NULL,
    "PositionLevel"     VARCHAR(50)  NOT NULL
);

CREATE TABLE IF NOT EXISTS "PositionSkill"
(
    "Id"                UUID        NOT NULL DEFAULT gen_random_uuid(),
    "UserCreatorId"     UUID        NOT NULL UNIQUE,
    "CreationTime"      DATE        NOT NULL,
    "State"             BOOL        NOT NULL,
    "UserModifierId"    UUID        NOT NULL UNIQUE,
    "DateLastModify"    DATE        NOT NULL,
    "SkillId"           UUID        NOT NULL,
    "PositionId"        UUID        NOT NULL,
    "SkillName"         VARCHAR(80) NOT NULL,
    "MinToAccept"       SMALLINT    NOT NULL,
    "PositionSkillType" SkillType   NOT NULL

);

CREATE TABLE IF NOT EXISTS "ResourcePosition"
(
    "Id"                  UUID         NOT NULL DEFAULT gen_random_uuid(),
    "UserCreatorId"       UUID         NOT NULL UNIQUE,
    "CreationTime"        DATE         NOT NULL,
    "State"               BOOL         NOT NULL,
    "UserModifierId"      UUID         NOT NULL UNIQUE,
    "DateLastModify"      DATE         NOT NULL,
    "ResourceId"          UUID         NOT NULL,
    "PositionId"          UUID         NOT NULL,
    "PercentMathPosition" SMALLINT     NOT NULL,
    "IsDefault"           BOOL         NOT NULL,
    "ResourceName"        VARCHAR(100) NOT NULL
);

ALTER TABLE "Position"
    ADD CONSTRAINT "PK_Position_Id" PRIMARY KEY ("Id");
ALTER TABLE "PositionSkill"
    ADD CONSTRAINT "PK_PositionSkill_Id" PRIMARY KEY ("Id");
ALTER TABLE "ResourcePosition"
    ADD CONSTRAINT "PK_ResourcePosition_Id" PRIMARY KEY ("Id");

ALTER TABLE "PositionSkill"
    ADD CONSTRAINT "FK_PositionSkill_Position_Id" FOREIGN KEY ("PositionId") REFERENCES "Position" ("Id");
ALTER Table "ResourcePosition"
    ADD CONSTRAINT "FK_ResourcePosition_Position_Id" FOREIGN KEY ("PositionId") REFERENCES "Position" ("Id");
