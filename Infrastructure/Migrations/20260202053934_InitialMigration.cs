using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AllClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClaimValue = table.Column<bool>(type: "bit", nullable: false),
                    groupClaims = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "rankTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rankTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "sells",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sells", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(34)", maxLength: 34, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ranks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RankTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ranks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ranks_rankTypes_RankTypeId",
                        column: x => x.RankTypeId,
                        principalTable: "rankTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Department_Id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rankid = table.Column<int>(type: "int", nullable: true),
                    UserNameInLocalLang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Departments_Department_Id",
                        column: x => x.Department_Id,
                        principalTable: "Departments",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_ranks_Rankid",
                        column: x => x.Rankid,
                        principalTable: "ranks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(34)", maxLength: 34, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AllClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "groupClaims" },
                values: new object[,]
                {
                    { 1, "Authentication Management", true, "Authentication" },
                    { 2, "Add User", true, "Authentication" },
                    { 3, "Edit User", true, "Authentication" },
                    { 4, "User Activation", true, "Authentication" },
                    { 5, "Add Role", true, "Authentication" },
                    { 6, "Edit Role", true, "Authentication" },
                    { 7, "Role Management", true, "Authentication" }
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "ac976318-0f8a-4a98-81e1-c855bc496cbd", null, "Roles", "Super Admin", "SUPER ADMIN" });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "DepartmentId", "DepartmentName" },
                values: new object[] { "1", "ریاست مخابره" });

            migrationBuilder.InsertData(
                table: "rankTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "نظامی" },
                    { 2, "ملکی" },
                    { 3, "قراردادی" },
                    { 4, "بالمقطع" }
                });

            migrationBuilder.InsertData(
                table: "ranks",
                columns: new[] { "Id", "Name", "RankTypeId" },
                values: new object[,]
                {
                    { 1, "سترپاسوال", 1 },
                    { 2, "لوی پاسوال", 1 },
                    { 3, "پاسوال", 1 },
                    { 4, "مل پاسوال", 1 },
                    { 5, "سمونوال", 1 },
                    { 6, "سمونمل", 1 },
                    { 7, "سمونیار", 1 },
                    { 8, "څارمن", 1 },
                    { 9, "لمړی څارن", 1 },
                    { 10, "دوهم څارن", 1 },
                    { 11, "لمړی ساتنمن", 1 },
                    { 12, "دوهم ساتنمن", 1 },
                    { 13, "دریم ساتنمن", 1 },
                    { 14, "لمړی ساتونکی", 1 },
                    { 15, "دوهم ساتونکی", 1 },
                    { 16, "مجاهد", 1 },
                    { 17, "خارج رتبه", 2 },
                    { 18, "مافوق رتبه", 2 },
                    { 19, "فوق رتبه", 2 },
                    { 20, "اول", 2 },
                    { 21, "دوم", 2 },
                    { 22, "سوم", 2 },
                    { 23, "چهارم", 2 },
                    { 24, "پنجم", 2 },
                    { 25, "ششم", 2 },
                    { 26, "هفتم", 2 },
                    { 27, "هشتم", 2 },
                    { 28, "A", 3 },
                    { 29, "A1", 3 },
                    { 30, "A2", 3 },
                    { 31, "A3", 3 },
                    { 32, "A4", 3 },
                    { 33, "A5", 3 },
                    { 34, "A6", 3 },
                    { 35, "A7", 3 },
                    { 36, "A8", 3 },
                    { 37, "A9", 3 },
                    { 38, "A10", 3 },
                    { 39, "B", 3 },
                    { 40, "B1", 3 },
                    { 41, "B2", 3 },
                    { 42, "B3", 3 },
                    { 43, "B4", 3 },
                    { 44, "B5", 3 },
                    { 45, "B6", 3 },
                    { 46, "B7", 3 },
                    { 47, "B8", 3 },
                    { 48, "B9", 3 },
                    { 49, "B10", 3 },
                    { 50, "C", 3 },
                    { 51, "C1", 3 },
                    { 52, "C2", 3 },
                    { 53, "C3", 3 },
                    { 54, "C4", 3 },
                    { 55, "C5", 3 },
                    { 56, "C6", 3 },
                    { 57, "C7", 3 },
                    { 58, "C8", 3 },
                    { 59, "C9", 3 },
                    { 60, "C10", 3 },
                    { 61, "D", 3 },
                    { 62, "D1", 3 },
                    { 63, "D2", 3 },
                    { 64, "D3", 3 },
                    { 65, "D4", 3 },
                    { 66, "D5", 3 },
                    { 67, "D6", 3 },
                    { 68, "D7", 3 },
                    { 69, "D8", 3 },
                    { 70, "D9", 3 },
                    { 71, "D10", 3 },
                    { 72, "E", 3 },
                    { 73, "E1", 3 },
                    { 74, "E2", 3 },
                    { 75, "E3", 3 },
                    { 76, "E4", 3 },
                    { 77, "E5", 3 },
                    { 78, "E6", 3 },
                    { 79, "E7", 3 },
                    { 80, "E8", 3 },
                    { 81, "E9", 3 },
                    { 82, "E10", 3 },
                    { 83, "F", 3 },
                    { 84, "F1", 3 },
                    { 85, "F2", 3 },
                    { 86, "F3", 3 },
                    { 87, "F4", 3 },
                    { 88, "F5", 3 },
                    { 89, "F6", 3 },
                    { 90, "F7", 3 },
                    { 91, "F8", 3 },
                    { 92, "F9", 3 },
                    { 93, "F10", 3 },
                    { 94, "G", 3 },
                    { 95, "G1", 3 },
                    { 96, "G2", 3 },
                    { 97, "G3", 3 },
                    { 98, "G4", 3 },
                    { 99, "G5", 3 },
                    { 100, "G6", 3 },
                    { 101, "G7", 3 },
                    { 102, "G8", 3 },
                    { 103, "G9", 3 },
                    { 104, "G10", 3 },
                    { 105, "H", 3 },
                    { 106, "H1", 3 },
                    { 107, "H2", 3 },
                    { 108, "H3", 3 },
                    { 109, "H4", 3 },
                    { 110, "H5", 3 },
                    { 111, "H6", 3 },
                    { 112, "H7", 3 },
                    { 113, "H8", 3 },
                    { 114, "H9", 3 },
                    { 115, "H10", 3 },
                    { 116, "ندارد", 4 },
                    { 117, "مجاهد سترپاسوال", 1 },
                    { 118, "مجاهد لوی پاسوال", 1 },
                    { 119, "مجاهد پاسوال", 1 },
                    { 120, "مجاهد مل پاسوال", 1 },
                    { 121, "مجاهد سمونوال", 1 },
                    { 122, "مجاهد سمونمل", 1 },
                    { 123, "مجاهد سمونیار", 1 },
                    { 124, "مجاهد څارمن", 1 },
                    { 125, "مجاهد لمړی څارن", 1 },
                    { 126, "مجاهد دوهم څارن", 1 },
                    { 127, "مجاهد لمړی ساتنمن", 1 },
                    { 128, "مجاهد دوهم ساتنمن", 1 },
                    { 129, "مجاهد دریم ساتنمن", 1 },
                    { 130, "مجاهد لمړی ساتونکی", 1 },
                    { 131, "مجاهد دوهم ساتونکی", 1 },
                    { 132, "ملکی در بست سترپاسوال", 1 },
                    { 133, "ملکی در بست لوی پاسوال", 1 },
                    { 134, "ملکی در بست پاسوال", 1 },
                    { 135, "ملکی در بست مل پاسوال", 1 },
                    { 136, "ملکی در بست سمونوال", 1 },
                    { 137, "ملکی در بست سمونمل", 1 },
                    { 138, "ملکی در بست سمونیار", 1 },
                    { 139, "ملکی در بست څارمن", 1 },
                    { 140, "ملکی در بست لمړی څارن", 1 },
                    { 141, "ملکی در بست دوهم څارن", 1 },
                    { 142, "ملکی در بست لمړی ساتنمن", 1 },
                    { 143, "ملکی در بست دوهم ساتنمن", 1 },
                    { 144, "ملکی در بست دریم ساتنمن", 1 },
                    { 145, "ملکی در بست لمړی ساتونکی", 1 },
                    { 146, "ملکی در بست دوهم ساتونکی", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Department_Id",
                table: "AspNetUsers",
                column: "Department_Id");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Rankid",
                table: "AspNetUsers",
                column: "Rankid");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ranks_RankTypeId",
                table: "ranks",
                column: "RankTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllClaims");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "sells");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "ranks");

            migrationBuilder.DropTable(
                name: "rankTypes");
        }
    }
}
