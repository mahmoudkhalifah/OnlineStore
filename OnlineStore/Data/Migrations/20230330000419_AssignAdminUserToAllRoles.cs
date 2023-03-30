using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class AssignAdminUserToAllRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into AspNetUserRoles(UserId,RoleId) select '016995ea-8d25-465a-acac-97411c48820e',Id from AspNetRoles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from AspNetUserRoles where UserId ='016995ea-8d25-465a-acac-97411c48820e'");
        }
    }
}
