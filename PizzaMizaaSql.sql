CREATE DATABASE PizzaMizzaDB
USE PizzaMizzaDB
drop table Pizzas 
CREATE TABLE Pizzas (
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100) not null ,
    Price DECIMAL(6,2) not null ,
    IngredientCount INT not null 
)
