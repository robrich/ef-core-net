CREATE TABLE [dbo].[Phones]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[CustomerId] [int] NOT NULL,
[PhoneNumber] [nvarchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[PhoneType] [char] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Phones] ADD CONSTRAINT [PK_Phones] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Phones] ADD CONSTRAINT [FK_Phones_Customers] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customers] ([Id])
GO
