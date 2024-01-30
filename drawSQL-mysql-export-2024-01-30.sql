CREATE TABLE `Exercise`(
    `id` INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    `Type` VARCHAR(255) NOT NULL,
    `Weight` VARCHAR(255) NOT NULL,
    `Reps` INT NOT NULL,
    `Sets` INT NOT NULL,
    `RoutineID` INT NOT NULL
);
CREATE TABLE `User`(
    `id` INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    `FirstName` VARCHAR(255) NOT NULL,
    `LastName` VARCHAR(255) NOT NULL
);
CREATE TABLE `Routine`(
    `id` INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    `Title` VARCHAR(255) NOT NULL,
    `Discription` VARCHAR(255) NOT NULL,
    `AthleteID` INT NOT NULL
);
CREATE TABLE `TrainingEvent`(
    `id` INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    `Date` DATETIME NOT NULL,
    `AthleteID` INT NOT NULL,
    `RoutineID` INT NOT NULL
);
ALTER TABLE
    `Routine` ADD CONSTRAINT `routine_athleteid_foreign` FOREIGN KEY(`AthleteID`) REFERENCES `User`(`id`);
ALTER TABLE
    `Exercise` ADD CONSTRAINT `exercise_routineid_foreign` FOREIGN KEY(`RoutineID`) REFERENCES `Routine`(`id`);
ALTER TABLE
    `TrainingEvent` ADD CONSTRAINT `trainingevent_athleteid_foreign` FOREIGN KEY(`AthleteID`) REFERENCES `User`(`id`);
ALTER TABLE
    `TrainingEvent` ADD CONSTRAINT `trainingevent_routineid_foreign` FOREIGN KEY(`RoutineID`) REFERENCES `Routine`(`id`);