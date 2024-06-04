SET IDENTITY_INSERT [dbo].[Zina] ON
INSERT INTO [dbo].[Zina] ([ZinaID], [DialogsID], [AutorsID], [DatumsUnLaiks], [Saturs]) VALUES (2, 1, 1, N'2024-05-26 00:00:00', N'Hello')
INSERT INTO [dbo].[Zina] ([ZinaID], [DialogsID], [AutorsID], [DatumsUnLaiks], [Saturs]) VALUES (3, 1, 1, N'2024-05-27 00:00:00', N'Yo')
SET IDENTITY_INSERT [dbo].[Zina] OFF

SET IDENTITY_INSERT [dbo].[Lietotajs] ON
INSERT INTO [dbo].[Lietotajs] ([LietotajsID], [Vards], [Uzvards], [DzimsanasGads], [Dzimums], [Lietotajvards], [Epasts], [Parole], [Anonimitate], [GooglePilnvara]) VALUES (1, N'Testeris', N'Testeris', N'2024-05-26', 1, N'TESTERIS', N'@gmail.com', 0x32333233320000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000, 1, N'23')
SET IDENTITY_INSERT [dbo].[Lietotajs] OFF

SET IDENTITY_INSERT [dbo].[Dialogs] ON
INSERT INTO [dbo].[Dialogs] ([DialogsID], [LietotajsID], [SpecialistsID]) VALUES (1, 1, 1)
SET IDENTITY_INSERT [dbo].[Dialogs] OFF

