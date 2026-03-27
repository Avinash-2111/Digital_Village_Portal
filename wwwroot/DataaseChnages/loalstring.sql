use villagedb

CREATE TABLE Users
(
    Id INT PRIMARY KEY IDENTITY,
    UserName NVARCHAR(100),
    Email NVARCHAR(100),
    Password NVARCHAR(200),
    PhoneNumber NVARCHAR(20),
    Gender NVARCHAR(10),
    Age INT,
    Role NVARCHAR(20)
)
select * from users 
ALTER TABLE Users
ADD CONSTRAINT UQ_Users_PhoneNumber UNIQUE (PhoneNumber);
ALTER TABLE Users
ADD CONSTRAINT UQ_Users_Email UNIQUE (Email);
CREATE TABLE Schemes
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    SchemeName NVARCHAR(200) NOT NULL
)
CREATE TABLE Leaders
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(200) NOT NULL,
    VillageRole NVARCHAR(100),
    WardNumber INT NULL,
    WardName NVARCHAR(200) NULL,
    Photo NVARCHAR(300)
)

CREATE TABLE Leaders
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(200) NOT NULL,
    VillageRole NVARCHAR(100),
    WardNumber INT NULL,
    WardName NVARCHAR(200) NULL,
    Photo NVARCHAR(300)
)
CREATE TABLE VillageGallery
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(200) NOT NULL,
    MediaType NVARCHAR(20) NOT NULL,
    FileName NVARCHAR(300) NOT NULL,
    Description NVARCHAR(500) NULL
)
CREATE TABLE Pensions
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(200) NOT NULL,
  
    Amount DECIMAL(10,2),
   
)
CREATE TABLE Families
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    OwnerName NVARCHAR(200) NOT NULL,
    PhoneNumber NVARCHAR(20),
    RationCardNumber NVARCHAR(50),
    TotalAcres DECIMAL(10,2),
    VehiclesCount INT,
    OwnHouse BIT,
    GasConnection BIT
)
CREATE TABLE VehicleTypes
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    VehicleTypeName NVARCHAR(200) NOT NULL
)
CREATE TABLE FamilyMembers
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FamilyId INT NOT NULL,
    Name NVARCHAR(200) NOT NULL,
    AadhaarNumber NVARCHAR(20) NULL,
    PhoneNumber NVARCHAR(20) NULL,
    Gender NVARCHAR(20) NULL,
    IsPhysicallyDisabled BIT NOT NULL DEFAULT 0,
    IsMarried BIT NOT NULL DEFAULT 0
)

CREATE TABLE FamilyMemberProfessionMap
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FamilyMemberId INT NOT NULL,
    ProfessionId INT NOT NULL
)

CREATE TABLE FamilyMemberSchemeMap
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FamilyMemberId INT NOT NULL,
    SchemeId INT NOT NULL
)


CREATE TABLE FamilyMemberPensionMap
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FamilyMemberId INT NOT NULL,
    PensionId INT NOT NULL
)

CREATE TABLE FamilyMemberVehicleTypeMap
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FamilyMemberId INT NOT NULL,
    VehicleTypeId INT NOT NULL
)

ALTER TABLE FamilyMemberProfessionMap
ADD CONSTRAINT FK_FamilyMemberProfessionMap_FamilyMembers
FOREIGN KEY (FamilyMemberId) REFERENCES FamilyMembers(Id);

ALTER TABLE FamilyMemberProfessionMap
ADD CONSTRAINT FK_FamilyMemberProfessionMap_Professions
FOREIGN KEY (ProfessionId) REFERENCES Professions(Id);


ALTER TABLE FamilyMemberSchemeMap
ADD CONSTRAINT FK_FamilyMemberSchemeMap_FamilyMembers
FOREIGN KEY (FamilyMemberId) REFERENCES FamilyMembers(Id);

ALTER TABLE FamilyMemberSchemeMap
ADD CONSTRAINT FK_FamilyMemberSchemeMap_Schemes
FOREIGN KEY (SchemeId) REFERENCES Schemes(Id);


ALTER TABLE FamilyMemberPensionMap
ADD CONSTRAINT FK_FamilyMemberPensionMap_FamilyMembers
FOREIGN KEY (FamilyMemberId) REFERENCES FamilyMembers(Id);

ALTER TABLE FamilyMemberPensionMap
ADD CONSTRAINT FK_FamilyMemberPensionMap_Pensions
FOREIGN KEY (PensionId) REFERENCES Pensions(Id);


ALTER TABLE FamilyMemberVehicleTypeMap
ADD CONSTRAINT FK_FamilyMemberVehicleTypeMap_FamilyMembers
FOREIGN KEY (FamilyMemberId) REFERENCES FamilyMembers(Id);

ALTER TABLE FamilyMemberVehicleTypeMap
ADD CONSTRAINT FK_FamilyMemberVehicleTypeMap_VehicleTypes
FOREIGN KEY (VehicleTypeId) REFERENCES VehicleTypes(Id);
select * from FamilyMemberSchemeMap

ALTER TABLE Families
ADD WaterConnection BIT NOT NULL DEFAULT 0,
    PetId INT NULL,
    PetCount INT NULL;

	CREATE TABLE Pets
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    PetName NVARCHAR(100) NOT NULL
);

ALTER TABLE Families
ADD CONSTRAINT FK_Families_Pets
FOREIGN KEY (PetId) REFERENCES Pets(Id);

INSERT INTO Pets (PetName) VALUES ('Dog');
INSERT INTO Pets (PetName) VALUES ('Cat');
INSERT INTO Pets (PetName) VALUES ('Cow');
INSERT INTO Pets (PetName) VALUES ('Buffalo');
INSERT INTO Pets (PetName) VALUES ('Goat');
INSERT INTO Pets (PetName) VALUES ('Sheep');
INSERT INTO Pets (PetName) VALUES ('Hen');

CREATE TABLE FamilyPetMaps
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FamilyId INT NOT NULL,
    PetId INT NOT NULL,
    PetCount INT NOT NULL
);

ALTER TABLE FamilyPetMaps
ADD CONSTRAINT FK_FamilyPetMaps_Families
FOREIGN KEY (FamilyId) REFERENCES Families(Id);

ALTER TABLE FamilyPetMaps
ADD CONSTRAINT FK_FamilyPetMaps_Pets
FOREIGN KEY (PetId) REFERENCES Pets(Id);   
select * from FamilyPetMaps
select * from Pets
