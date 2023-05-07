CREATE TABLE [dbo].[Pizza] (
    [id]    INT        NOT NULL,
    [name]  NCHAR (13) NOT NULL,
    [price] FLOAT (53) NOT NULL,
    CONSTRAINT [PK_Pizza] PRIMARY KEY CLUSTERED ([id] ASC)
);

