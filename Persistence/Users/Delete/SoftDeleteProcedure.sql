CREATE PROCEDURE usp_SoftDeleteUser
    @UserId uniqueidentifier
AS
BEGIN
    BEGIN TRANSACTION;

    UPDATE [my-auth].[dbo].[User]
    SET IsDeleted = 1
    WHERE Id = @UserId;

    UPDATE [my-auth].[dbo].[Notification]
    SET IsDeleted = 1
    WHERE UserId = @UserId;

    UPDATE [my-auth].[dbo].[AuditLog]
    SET IsDeleted = 1
    WHERE ChangedById = @UserId;

    IF @@ERROR = 0
    BEGIN
        COMMIT TRANSACTION;
    END
    ELSE
    BEGIN
        ROLLBACK TRANSACTION;
    END
END;