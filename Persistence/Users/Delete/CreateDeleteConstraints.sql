ALTER TABLE [my-auth].[dbo].[User]
ADD IsDeleted BIT NOT NULL DEFAULT 0;

ALTER TABLE [my-auth].[dbo].[Notification]
ADD IsDeleted BIT NOT NULL DEFAULT 0;

ALTER TABLE [my-auth].[dbo].[AuditLog]
ADD IsDeleted BIT NOT NULL DEFAULT 0;

ALTER TABLE [my-auth].[dbo].[Notification]
DROP CONSTRAINT FK__Notificat__UserI__5EBF139D; -- drop old fk constraint

ALTER TABLE [my-auth].[dbo].[Notification]
ADD CONSTRAINT FK_Notification_User
FOREIGN KEY (UserId)
REFERENCES [my-auth].[dbo].[User](Id)
ON DELETE CASCADE;

ALTER TABLE [my-auth].[dbo].[AuditLog]
DROP CONSTRAINT FK_AuditLog_User;

ALTER TABLE [my-auth].[dbo].[AuditLog]
ADD CONSTRAINT FK_AuditLog_User
FOREIGN KEY (ChangedById)
REFERENCES [my-auth].[dbo].[User](Id)
ON DELETE CASCADE;

ALTER TABLE [my-auth].[dbo].[RoleUser]
DROP CONSTRAINT FK_RoleUser_User_UsersId;

ALTER TABLE [my-auth].[dbo].[RoleUser]
ADD CONSTRAINT FK_RoleUser_User
FOREIGN KEY (UsersId)
REFERENCES [my-auth].[dbo].[User](Id)
ON DELETE CASCADE;