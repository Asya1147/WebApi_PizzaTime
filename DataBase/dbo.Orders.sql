CREATE TABLE [dbo].[Orders] (
    [id]       INT       NOT NULL,
    [customer] CHAR (13) NOT NULL,
    [summa]    REAL      NOT NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED ([id] ASC)
);

