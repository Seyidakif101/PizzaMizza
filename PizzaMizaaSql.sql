CREATE DATABASE PizzaMizzaDB
USE PizzaMizzaDB
drop table Pizzas 
CREATE TABLE Pizzas (
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(50) not null ,
    Price money not null ,
    IngredientCount INT not null 
)
