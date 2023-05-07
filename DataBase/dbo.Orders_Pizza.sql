CREATE TABLE [dbo].[Orders_Pizza] (
    [id]       INT NOT NULL,
    [id_order] INT NOT NULL,
    [id_pizza] INT NOT NULL,
    [count]    INT NOT NULL,
    CONSTRAINT [PK_Orders_Pizza] PRIMARY KEY ([id]), 
    CONSTRAINT [FK_Orders_Pizza_ToOrders] FOREIGN KEY ([id_order]) REFERENCES [Orders]([id]), 
    CONSTRAINT [FK_Orders_Pizza_ToPizza] FOREIGN KEY ([id_pizza]) REFERENCES [Pizza]([id])
);

