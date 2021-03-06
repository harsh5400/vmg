/*
   Monday, December 7, 20151:53:04 PM
   User: sa
   Server: LHUBPLAHARSHLAP
   Database: dbPushVOD
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.tblPlaylist ADD
	Approved bit NULL
GO
ALTER TABLE dbo.tblPlaylist SET (LOCK_ESCALATION = TABLE)
GO
COMMIT



/****** Object:  Table [dbo].[tblAutomation]    Script Date: 07-Dec-15 2:33:51 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE TABLE [dbo].[tblAutomation](
	[SID] [bigint] IDENTITY(1,1) NOT NULL,
	[AutomationPlateform] [nvarchar](100) NULL,
 CONSTRAINT [PK_tblAutomation] PRIMARY KEY CLUSTERED 
(
	[SID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


/****** Object:  View [dbo].[vwApprovedPlaylist]    Script Date: 07-Dec-15 2:34:40 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vwApprovedPlaylist]
AS
SELECT SID, PlayoutTime, Date, PlayoutPortSID, MediaSID, addon, modon, Status, PlayOrderSID, FixedEvent, SchDate, Approved
FROM     dbo.tblPlaylist
WHERE  (Approved = 'True')

GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "tblPlaylist"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 242
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 13
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwApprovedPlaylist'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwApprovedPlaylist'
GO


/*
   Monday, December 7, 20152:36:04 PM
   User: sa
   Server: LHUBPLAHARSHLAP
   Database: dbPushVOD
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.tblReportVersion ADD
	ChannelSID bigint NULL
GO
ALTER TABLE dbo.tblReportVersion SET (LOCK_ESCALATION = TABLE)
GO
COMMIT


/*
   Monday, December 7, 20152:42:19 PM
   User: sa
   Server: LHUBPLAHARSHLAP
   Database: dbPushVOD
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.tblAutomation SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_tblPlayoutPort
	(
	SID bigint NOT NULL IDENTITY (1, 1),
	Descriptions nvarchar(100) NULL,
	ChannelName nvarchar(100) NULL,
	AutomationSID bigint NULL,
	StartTime time(7) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_tblPlayoutPort SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_tblPlayoutPort ON
GO
IF EXISTS(SELECT * FROM dbo.tblPlayoutPort)
	 EXEC('INSERT INTO dbo.Tmp_tblPlayoutPort (SID, Descriptions, ChannelName)
		SELECT SID, Descriptions, ChannelName FROM dbo.tblPlayoutPort WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_tblPlayoutPort OFF
GO
ALTER TABLE dbo.tblPlaylist
	DROP CONSTRAINT FK_tblPlaylist_tblPlayoutPort
GO
DROP TABLE dbo.tblPlayoutPort
GO
EXECUTE sp_rename N'dbo.Tmp_tblPlayoutPort', N'tblPlayoutPort', 'OBJECT' 
GO
ALTER TABLE dbo.tblPlayoutPort ADD CONSTRAINT
	PK_tblPlayoutPort PRIMARY KEY CLUSTERED 
	(
	SID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.tblPlayoutPort ADD CONSTRAINT
	FK_tblPlayoutPort_tblAutomation FOREIGN KEY
	(
	AutomationSID
	) REFERENCES dbo.tblAutomation
	(
	SID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.tblPlaylist ADD CONSTRAINT
	FK_tblPlaylist_tblPlayoutPort FOREIGN KEY
	(
	PlayoutPortSID
	) REFERENCES dbo.tblPlayoutPort
	(
	SID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.tblPlaylist SET (LOCK_ESCALATION = TABLE)
GO
COMMIT





USE [dbPushVOD]
GO
SET IDENTITY_INSERT [dbo].[tblAutomation] ON 

GO
INSERT [dbo].[tblAutomation] ([SID], [AutomationPlateform]) VALUES (1, N'EVS')
GO
INSERT [dbo].[tblAutomation] ([SID], [AutomationPlateform]) VALUES (2, N'Itx')
GO
INSERT [dbo].[tblAutomation] ([SID], [AutomationPlateform]) VALUES (3, N'Sundance')
GO
SET IDENTITY_INSERT [dbo].[tblAutomation] OFF
GO

DELETE from tblPlayoutPort
GO

SET IDENTITY_INSERT [dbo].[tblPlayoutPort] ON 
GO
INSERT [dbo].[tblPlayoutPort] ([SID], [Descriptions], [ChannelName], [AutomationSID], [StartTime]) VALUES (1, N'Push Vod', N'Push Vod', 1, CAST(N'05:00:00' AS Time))
GO
INSERT [dbo].[tblPlayoutPort] ([SID], [Descriptions], [ChannelName], [AutomationSID], [StartTime]) VALUES (2, N'Dance Studio Q1', N'Dance Studio Q1', 3, CAST(N'10:00:00' AS Time))
GO
INSERT [dbo].[tblPlayoutPort] ([SID], [Descriptions], [ChannelName], [AutomationSID], [StartTime]) VALUES (3, N'Dance Studio Q2', N'Dance Studio Q2', 3, CAST(N'10:00:00' AS Time))
GO
INSERT [dbo].[tblPlayoutPort] ([SID], [Descriptions], [ChannelName], [AutomationSID], [StartTime]) VALUES (4, N'Dance Studio Q3', N'Dance Studio Q3', 3, CAST(N'10:00:00' AS Time))
GO
INSERT [dbo].[tblPlayoutPort] ([SID], [Descriptions], [ChannelName], [AutomationSID], [StartTime]) VALUES (5, N'Dance Studio Q4', N'Dance Studio Q4', 3, CAST(N'10:00:00' AS Time))
GO
SET IDENTITY_INSERT [dbo].[tblPlayoutPort] OFF
GO


/*
   Monday, December 7, 20152:51:54 PM
   User: sa
   Server: LHUBPLAHARSHLAP
   Database: dbPushVOD
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.tblPlaylist ADD
	Exported bit NULL
GO
ALTER TABLE dbo.tblPlaylist SET (LOCK_ESCALATION = TABLE)
GO
COMMIT





DROP VIEW [dbo].[vwApprovedPlaylist]
GO

/****** Object:  View [dbo].[vwApprovedPlaylist]    Script Date: 07-Dec-15 2:54:39 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vwApprovedPlaylist]
AS
SELECT SID, PlayoutTime, Date, PlayoutPortSID, MediaSID, addon, modon, Status, PlayOrderSID, FixedEvent, SchDate, Approved, Exported
FROM     dbo.tblPlaylist
WHERE  (Approved = 'True') AND (Exported IS NULL OR
                  Exported = 'False')

GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[20] 2[16] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "tblPlaylist"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 242
            End
            DisplayFlags = 280
            TopColumn = 9
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 13
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1176
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1356
         SortOrder = 1416
         GroupBy = 1350
         Filter = 1356
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwApprovedPlaylist'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwApprovedPlaylist'
GO

Delete from [dbo].[tblAutomation]
Go
DELETE from [dbo].[tblPlayoutPort]
Go
SET IDENTITY_INSERT [dbo].[tblAutomation] ON 

INSERT [dbo].[tblAutomation] ([SID], [AutomationPlateform]) VALUES (1, N'EVS')
INSERT [dbo].[tblAutomation] ([SID], [AutomationPlateform]) VALUES (2, N'Itx')
INSERT [dbo].[tblAutomation] ([SID], [AutomationPlateform]) VALUES (3, N'Sundance')
SET IDENTITY_INSERT [dbo].[tblAutomation] OFF
SET IDENTITY_INSERT [dbo].[tblPlayoutPort] ON 

INSERT [dbo].[tblPlayoutPort] ([SID], [Descriptions], [ChannelName], [AutomationSID], [StartTime]) VALUES (1, N'Push Vod', N'Push Vod', 1, CAST(N'05:00:00' AS Time))
INSERT [dbo].[tblPlayoutPort] ([SID], [Descriptions], [ChannelName], [AutomationSID], [StartTime]) VALUES (2, N'Dance Studio Q1', N'Dance Studio Q1', 3, CAST(N'10:00:00' AS Time))
INSERT [dbo].[tblPlayoutPort] ([SID], [Descriptions], [ChannelName], [AutomationSID], [StartTime]) VALUES (3, N'Dance Studio Q2', N'Dance Studio Q2', 3, CAST(N'10:00:00' AS Time))
INSERT [dbo].[tblPlayoutPort] ([SID], [Descriptions], [ChannelName], [AutomationSID], [StartTime]) VALUES (4, N'Dance Studio Q3', N'Dance Studio Q3', 3, CAST(N'10:00:00' AS Time))
INSERT [dbo].[tblPlayoutPort] ([SID], [Descriptions], [ChannelName], [AutomationSID], [StartTime]) VALUES (5, N'Dance Studio Q4', N'Dance Studio Q4', 3, CAST(N'10:00:00' AS Time))
SET IDENTITY_INSERT [dbo].[tblPlayoutPort] OFF
