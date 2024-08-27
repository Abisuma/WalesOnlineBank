-- Replace 'user-id-to-delete' with the actual User ID of the user you want to delete.
DECLARE @UserId NVARCHAR(450) = 'b5e23fff-2957-4102-ad7f-2b5835ac3cc9';

-- Delete from AspNetUserRoles
DELETE FROM AspNetUserRoles
WHERE UserId = @UserId;

-- Delete from AspNetUserClaims
DELETE FROM AspNetUserClaims
WHERE UserId = @UserId;

-- Delete from AspNetUserLogins
DELETE FROM AspNetUserLogins
WHERE UserId = @UserId;

-- Delete from AspNetUserTokens
DELETE FROM AspNetUserTokens
WHERE UserId = @UserId;

-- Finally, delete the user from AspNetUsers
DELETE FROM AspNetUsers
WHERE Id = @UserId;

-- Optionally, you might want to delete related records in other custom tables 
-- where UserId is a foreign key.

-- For example:
-- DELETE FROM SomeCustomTable WHERE UserId = @UserId;
