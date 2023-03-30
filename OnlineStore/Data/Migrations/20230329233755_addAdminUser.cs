using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class addAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [ProfilePicture]) VALUES (N'016995ea-8d25-465a-acac-97411c48820e', N'mostafa@gmail.com', N'MOSTAFA@GMAIL.COM', N'mostafa@gmail.com', N'MOSTAFA@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEEyf12sYvMap9ndl7zO7bbZfGWaYj0jZ6z5CA/Qx5+Jch8D1gc7HmY+TqkUlhq5BOg==', N'7G3U7BCVZWKK5OWHEAHNMW3NTX3JZOT2', N'8034a11b-670b-4d51-b07b-1993bf9329c9', NULL, 0, 0, NULL, 1, 0, N'mostafa', N'mohamed', null)\r\n");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete FROM [dbo].[AspNetUsers] WHERE Id='016995ea-8d25-465a-acac-97411c48820e' ");
        }
    }
}
