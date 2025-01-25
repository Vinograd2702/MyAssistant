using auth_servise.Core.Domain;
using auth_servise.Infrastructure.Persistence;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace auth_servise.tests.Common
{
    public class AuthContextFactory
    {
        public static Guid RegistrationAttemptIdToDeleteById = Guid.NewGuid();
        public static Guid RegistrationAttemptIdToDeleteLikeNeedDate = Guid.NewGuid();
        public static Guid RegistrationAttemptIdToDeleteLikeElderNeedDate = Guid.NewGuid();
        public static DateTime RegistrationAttemptOldTime = DateTime.UtcNow - new TimeSpan(0, 5, 0);
        public static Guid RegistrationAttemptIdToDeleteAfterRegistrationNewUser = Guid.NewGuid();
        public static Guid RegistrationAttemptIdToDeleteAfterBlockEmail = Guid.NewGuid();
        public static Guid UserToUpdate = Guid.NewGuid();
        public static Guid UserToUpdateWithNullProp = Guid.NewGuid();
        public static Guid UserToDelete = Guid.NewGuid();

        public static int CountOfRegistrationAttemptsInTestDB;
        public static int CountOfUsersInTestDB;
        public static int CountOfBlockedEmailsInTestDB;

        public static AuthServiseDbContext Create()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<AuthServiseDbContext>()
                .UseSqlite(connection)
                .Options;

            var context = new AuthServiseDbContext(options);
            context.Database.EnsureCreated();

            var testArrayOfRegistrationAttempts = new RegistrationAttempt[]
            {
                // Для проверки добавления при занятом логине
                new RegistrationAttempt
                {
                    Id = new Guid(),
                    Login = "UsedLoginRA",
                    EmailAddress = "email1",
                    PasswordHash = "paswHash1",
                    HashedEmail = "emailHash1",
                    DateOfRegistration = DateTime.UtcNow
                },
                // Для проверки добавления при занятом емейл
                new RegistrationAttempt
                {
                    Id = new Guid(),
                    Login = "login2",
                    EmailAddress = "UsedEmailRA",
                    PasswordHash = "paswHash2",
                    HashedEmail = "emailHash2",
                    DateOfRegistration = DateTime.UtcNow
                },
                // Для проверки удаления по id
                new RegistrationAttempt
                {
                    Id = RegistrationAttemptIdToDeleteById,
                    Login = "toDeliteByID",
                    EmailAddress = "toDeliteByID",
                    PasswordHash = "toDeliteByID",
                    HashedEmail = "toDeliteByID",
                    DateOfRegistration = DateTime.UtcNow
                },
                // Для проверки удаления старых заметок
                new RegistrationAttempt
                {
                    Id = RegistrationAttemptIdToDeleteLikeNeedDate,
                    Login = "toDeliteLikeNeedDate",
                    EmailAddress = "toDeliteLikeNeedDate",
                    PasswordHash = "toDeliteLikeNeedDate",
                    HashedEmail = "toDeliteLikeNeedDate",
                    DateOfRegistration = RegistrationAttemptOldTime
                },
                new RegistrationAttempt
                {
                    Id = RegistrationAttemptIdToDeleteLikeElderNeedDate,
                    Login = "toDeliteLikeElderNeedDate",
                    EmailAddress = "toDeliteLikeElderNeedDate",
                    PasswordHash = "toDeliteLikeElderNeedDate",
                    HashedEmail = "toDeliteLikeElderNeedDate",
                    DateOfRegistration = RegistrationAttemptOldTime - new TimeSpan(0, 5, 0)
                },
                // Для для регистрации нового пользователя по RA
                new RegistrationAttempt
                {
                    Id = RegistrationAttemptIdToDeleteAfterRegistrationNewUser,
                    Login = "toRegNewUserLogin",
                    EmailAddress = "toRegNewUserEmail",
                    PasswordHash = "toRegNewUserPasswordHash",
                    HashedEmail = "toRegNewUserHashedEmail",
                    DateOfRegistration = DateTime.UtcNow
                },
                // Для для отклонения регистрации и блокировки емейла для новых регистраций
                new RegistrationAttempt
                {
                    Id = RegistrationAttemptIdToDeleteAfterBlockEmail,
                    Login = "toBlockLogin",
                    EmailAddress = "toBlockEmail",
                    PasswordHash = "toBlockPasswordHash",
                    HashedEmail = "toBlockHashedEmail",
                    DateOfRegistration = DateTime.UtcNow
                },
                // Для проверки получения по Id
                new RegistrationAttempt
                {
                    Id = Guid.Parse("12613B2E-2E2A-4863-A237-8FBF35899F07"),
                    Login = "toGetById",
                    EmailAddress = "toGetById",
                    PasswordHash = "toGetById",
                    HashedEmail = "toGetById",
                    DateOfRegistration = DateTime.UtcNow
                }
            };

            CountOfRegistrationAttemptsInTestDB = testArrayOfRegistrationAttempts.Length;

            context.RegistrationAttempts.AddRange(testArrayOfRegistrationAttempts);

            var testArrayOfUsers = new User[]
            {
                // Для проверки добавления заявки при занятом логине
                new User
                {
                    Id = new Guid(),
                    Login = "UsedLogin",
                    EmailAddress = "email3",
                    PasswordHash = "paswHash3",
                    UserRole = "client",
                    PhoneNumber = "phone3",
                    FirstName = "fist3",
                    LastName = "last3",
                    Patronymic = "patronomic3",
                    DateOfRegistration = DateTime.UtcNow
                },
                // Для проверки добавления заявки при занятом емейле
                new User
                {
                    Id = new Guid(),
                    Login = "login4",
                    EmailAddress = "UsedEmail",
                    PasswordHash = "paswHash4",
                    UserRole = "client",
                    PhoneNumber = "phone4",
                    FirstName = "fist4",
                    LastName = "last4",
                    Patronymic = "patronomic4",
                    DateOfRegistration = DateTime.UtcNow
                },
                // TestUpdateUser
                new User
                {
                    Id = UserToUpdate,
                    Login = "login5",
                    EmailAddress = "Email5",
                    PasswordHash = "paswHash5",
                    UserRole = "client",
                    PhoneNumber = "oldPhone",
                    FirstName = "oldFirstName",
                    LastName = "oldLastName",
                    Patronymic = "oldPatronomic",
                    DateOfRegistration = DateTime.UtcNow
                },
                new User
                {
                    Id = UserToUpdateWithNullProp,
                    Login = "login6",
                    EmailAddress = "Email6",
                    PasswordHash = "paswHash6",
                    UserRole = "client",
                    PhoneNumber = null,
                    FirstName = null,
                    LastName = null,
                    Patronymic = null,
                    DateOfRegistration = DateTime.UtcNow
                },
                // TestDeleteUser
                new User
                {
                    Id = UserToDelete,
                    Login = "login7",
                    EmailAddress = "Email7",
                    PasswordHash = "paswHash7",
                    UserRole = "client",
                    PhoneNumber = "phone7",
                    FirstName = "fist7",
                    LastName = "last7",
                    Patronymic = "patronomic7",
                    DateOfRegistration = DateTime.UtcNow
                },
                // Для проверки получения детальной информации о пользователе с полной информацией
                new User
                {
                    Id = Guid.Parse("DA5373F2-E0F9-43FD-B488-B61F2F08A9B1"),
                    Login = "DetailLoginOfFullDataUser",
                    EmailAddress = "DetailEmailAddressOfFullDataUser",
                    PasswordHash = "DetailPasswordHashOfFullDataUser",
                    UserRole = "client",
                    PhoneNumber = "DetailPhoneNumberOfFullDataUser",
                    FirstName = "DetailFirstNameOfFullDataUser",
                    LastName = "DetailLastNameOfFullDataUser",
                    Patronymic = "DetailPatronymicOfFullDataUser",
                    DateOfRegistration = DateTime.UtcNow
                },
                // Для проверки получения детальной информации о пользователе с не полной информацией
                new User
                {
                    Id = Guid.Parse("40ACA14B-96ED-46D3-ADE9-F281197167BB"),
                    Login = "DetailLoginOfUser",
                    EmailAddress = "DetailEmailAddressOfUser",
                    PasswordHash = "DetailPasswordHashOfUser",
                    UserRole = "client",
                    PhoneNumber = null,
                    FirstName = null,
                    LastName = null,
                    Patronymic = null,
                    DateOfRegistration = DateTime.UtcNow
                },
                // Для получения токена авторизации
                new User
                {
                    Id = Guid.Parse("60924E30-2B40-4E67-BC3E-D95808EA9A49"),
                    Login = "Example",
                    EmailAddress = "Example@example.com",
                    PasswordHash = "HashedQWERTYUIOPASDFGHJK",
                    UserRole = "client",
                    PhoneNumber = null,
                    FirstName = null,
                    LastName = null,
                    Patronymic = null,
                    DateOfRegistration = DateTime.UtcNow
                }
            };

            CountOfUsersInTestDB = testArrayOfUsers.Length;

            context.Users.AddRange(testArrayOfUsers);

            var testArrayOfBlockedEmails = new BlockedEmail[]
            {
                new BlockedEmail
                {
                    Id = new Guid(),
                    EmailAddress = "blockedEmail",
                    DateOfBlock = DateTime.UtcNow
                },
                new BlockedEmail
                {
                    Id = new Guid(),
                    EmailAddress = "blockedEmail2",
                    DateOfBlock = DateTime.UtcNow
                }
            };

            CountOfBlockedEmailsInTestDB = testArrayOfBlockedEmails.Length;

            context.BlockedEmails.AddRange(testArrayOfBlockedEmails);

            context.SaveChanges();
            return context;
        }

        public static void Destroy(AuthServiseDbContext context)
        {
            context.Database.EnsureDeleted();  
            context.Dispose();
        }
    }
}
