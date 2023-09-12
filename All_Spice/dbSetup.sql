CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL PRIMARY KEY COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) DEFAULT CHARSET utf8 COMMENT '';

CREATE TABLE recipes(
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT COMMENT 'primary key',
  title VARCHAR(255) NOT NULL COMMENT 'Recipe Title',
  instructions VARCHAR(255) NOT NULL COMMENT 'Recipe Instructions',
  img VARCHAR(255) NOT NULL COMMENT 'Recipe Image',
  category ENUM(
    'Soup',
    'Pasta',
    'Fish',
    'Salads',
    'Beef',
    'Burgers',
    'Cheese',
    'Italian',
    'Mexican',
    'Specialty Coffee'
  ) NOT NULL DEFAULT 'Soup' COMMENT 'Everything is just soup',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  creatorId VARCHAR(255) NOT NULL COMMENT 'Recipe account reference',
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) DEFAULT CHARSET utf8 COMMENT '';

DROP TABLE recipes;

SELECT
  r.*,
  a.*
FROM
  recipes r
  JOIN accounts a ON r.creatorId = a.id
WHERE
  title LIKE '%senior%';

INSERT INTO
  recipes (title, instructions, img, category, creatorId)
VALUES
  (
    "Title",
    "Instructions",
    "Img",
    "Cheese",
    "64adc5560e2c4b808960071a"
  );

UPDATE
  recipes r
SET
  title = @Title,
  instructions = @Instructions,
  img = @Img,
  category = @Category
WHERE
  r.id = @Id;

SELECT
  r.*,
  a.*
FROM
  recipes r
  JOIN accounts a ON r.creatorId = a.id
WHERE
  r.id = @Id;

DELETE FROM
  recipes
WHERE
  id = @recipeId
LIMIT
  1;

CREATE TABLE ingredients(
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT COMMENT 'primary key',
  name VARCHAR(255) NOT NULL COMMENT 'Ingredient name',
  quantity VARCHAR(255) NOT NULL COMMENT 'Ingredient quantity',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  recipeId INT NOT NULL COMMENT 'Ingredient recipe reference',
  FOREIGN KEY (recipeId) REFERENCES recipes(id) ON DELETE CASCADE
) DEFAULT CHARSET utf8 COMMENT '';

DROP TABLE ingredients;

INSERT INTO
  ingredients (name, quantity, recipeId)
VALUES
  ('Name', 'Quantity', 1);

SELECT
  i.*,
  r.*
FROM
  ingredients i
  JOIN recipes r ON i.recipeId = r.id
WHERE
  i.id = 2;

CREATE TABLE favorites(
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  recipeId INT NOT NULL COMMENT 'Ingredient recipe reference',
  accountId VARCHAR(255) NOT NULL COMMENT 'Ingredient recipe reference',
  FOREIGN KEY (recipeId) REFERENCES recipes(id) ON DELETE CASCADE,
  FOREIGN KEY (accountId) REFERENCES accounts(id) ON DELETE CASCADE
) DEFAULT CHARSET utf8 COMMENT '';

DROP TABLE favorites;

SELECT
  *
FROM
  favorites
WHERE
  accountId = '64adc5560e2c4b808960071a';