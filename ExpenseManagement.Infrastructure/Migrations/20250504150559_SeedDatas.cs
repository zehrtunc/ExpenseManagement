using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpenseManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedDatas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            INSERT INTO ExpenseCategories(Name, Description) 
            VALUES('Ulaşım', 'Ulaşım'), 
            ('Konaklama', 'Konaklama'),
            ('Yeme-içme', 'Yeme-içme');

            INSERT INTO [Roles](Name) 
            VALUES('Admin'), 
            ('Personel');

            INSERT INTO [Users](Name, [Surname], Email, PasswordHash) 
            VALUES('Admin', 'Admin', 'admin@admin.com', 'Asd123.'), 
            ('Test Name', 'Test Surname', 'test@test.com', 'Asd123.');

            INSERT INTO [RoleUser]([UsersId], [RolesId]) 
            VALUES(1, 1), (2, 2);
        ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
