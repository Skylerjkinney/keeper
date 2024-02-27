-- Active: 1708990134882@@52.38.240.109@3306@wookdb
CREATE TABLE IF NOT EXISTS accounts (
    id VARCHAR(255) NOT NULL primary key COMMENT 'primary key', createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created', updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update', name varchar(255) COMMENT 'User Name', email varchar(255) COMMENT 'User Email', picture varchar(255) COMMENT 'User Picture', coverImg VARCHAR(255) COMMENT 'Cover Image' DEFAULT 'https://images.unsplash.com/photo-1547619292-8816ee7cdd50?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MjB8fG1lbWV8ZW58MHx8MHx8fDA%3D'
) default charset utf8 COMMENT '';

CREATE TABLE vaults (
    id INT AUTO_INCREMENT PRIMARY KEY, name VARCHAR(100) NOT NULL, creatorId VARCHAR(255), description VARCHAR(1000) NOT NULL, img VARCHAR(255) NOT NULL, isPrivate BOOLEAN DEFAULT FALSE, createdAt DATETIME DEFAULT CURRENT_TIMESTAMP, updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP, FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

CREATE TABLE keeps (
    id INT AUTO_INCREMENT PRIMARY KEY, name VARCHAR(100) NOT NULL, description VARCHAR(1000) NOT NULL, img VARCHAR(255) NOT NULL, creatorId VARCHAR(255), views INT, kept INT, FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

CREATE TABLE vaultKeeps (
    id INT AUTO_INCREMENT PRIMARY KEY, creatorId VARCHAR(255), vaultId INT NOT NULL, keepId INT NOT NULL, Foreign Key (creatorId) REFERENCES accounts (id) ON DELETE CASCADE, Foreign Key (vaultId) REFERENCES vaults (id) ON DELETE CASCADE, Foreign Key (keepId) REFERENCES keeps (id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

INSERT INTO
    vaultKeeps (creatorId, vaultId, keepId)
VALUES (@creatorId, @vaultId, @keepId);

SELECT vaults.* FROM vaults