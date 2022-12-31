using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ITProjectmanager.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>( type: "nvarchar(450)", nullable: false ),
                    Name = table.Column<string>( type: "nvarchar(256)", maxLength: 256, nullable: true ),
                    NormalizedName = table.Column<string>( type: "nvarchar(256)", maxLength: 256, nullable: true ),
                    ConcurrencyStamp = table.Column<string>( type: "nvarchar(max)", nullable: true )
                },
                constraints: table =>
                {
                    table.PrimaryKey( "PK_AspNetRoles", x => x.Id );
                } );

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>( type: "nvarchar(450)", nullable: false ),
                    Name = table.Column<string>( type: "nvarchar(100)", nullable: true ),
                    Surname = table.Column<string>( type: "nvarchar(100)", nullable: true ),
                    UserName = table.Column<string>( type: "nvarchar(256)", maxLength: 256, nullable: true ),
                    NormalizedUserName = table.Column<string>( type: "nvarchar(256)", maxLength: 256, nullable: true ),
                    Email = table.Column<string>( type: "nvarchar(256)", maxLength: 256, nullable: true ),
                    NormalizedEmail = table.Column<string>( type: "nvarchar(256)", maxLength: 256, nullable: true ),
                    EmailConfirmed = table.Column<bool>( type: "bit", nullable: false ),
                    PasswordHash = table.Column<string>( type: "nvarchar(max)", nullable: true ),
                    SecurityStamp = table.Column<string>( type: "nvarchar(max)", nullable: true ),
                    ConcurrencyStamp = table.Column<string>( type: "nvarchar(max)", nullable: true ),
                    PhoneNumber = table.Column<string>( type: "nvarchar(max)", nullable: true ),
                    PhoneNumberConfirmed = table.Column<bool>( type: "bit", nullable: false ),
                    TwoFactorEnabled = table.Column<bool>( type: "bit", nullable: false ),
                    LockoutEnd = table.Column<DateTimeOffset>( type: "datetimeoffset", nullable: true ),
                    LockoutEnabled = table.Column<bool>( type: "bit", nullable: false ),
                    AccessFailedCount = table.Column<int>( type: "int", nullable: false )
                },
                constraints: table =>
                {
                    table.PrimaryKey( "PK_AspNetUsers", x => x.Id );
                } );

            migrationBuilder.CreateTable(
                name: "Managers",
                columns: table => new
                {
                    Id = table.Column<int>( type: "int", nullable: false )
                        .Annotation( "SqlServer:Identity", "1, 1" ),
                    Name = table.Column<string>( type: "nvarchar(50)", maxLength: 50, nullable: false ),
                    Surname = table.Column<string>( type: "nvarchar(30)", maxLength: 30, nullable: false ),
                    Telephone = table.Column<string>( type: "nvarchar(12)", maxLength: 12, nullable: false )
                },
                constraints: table =>
                {
                    table.PrimaryKey( "PK_Managers", x => x.Id );
                } );

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<int>( type: "int", nullable: false )
                        .Annotation( "SqlServer:Identity", "1, 1" ),
                    Name = table.Column<string>( type: "nvarchar(max)", nullable: false ),
                    Surname = table.Column<string>( type: "nvarchar(max)", nullable: false ),
                    Email = table.Column<string>( type: "nvarchar(max)", nullable: true ),
                    DateOfBirth = table.Column<DateTime>( type: "datetime2", nullable: false )
                },
                constraints: table =>
                {
                    table.PrimaryKey( "PK_Members", x => x.Id );
                } );

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>( type: "int", nullable: false )
                        .Annotation( "SqlServer:Identity", "1, 1" ),
                    Name = table.Column<string>( type: "nvarchar(50)", maxLength: 50, nullable: true ),
                    Description = table.Column<string>( type: "nvarchar(250)", maxLength: 250, nullable: true ),
                    AssigmentDate = table.Column<DateTime>( type: "datetime2", nullable: false ),
                    DeadlineDate = table.Column<DateTime>( type: "datetime2", nullable: false )
                },
                constraints: table =>
                {
                    table.PrimaryKey( "PK_Teams", x => x.Id );
                } );

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>( type: "int", nullable: false )
                        .Annotation( "SqlServer:Identity", "1, 1" ),
                    RoleId = table.Column<string>( type: "nvarchar(450)", nullable: false ),
                    ClaimType = table.Column<string>( type: "nvarchar(max)", nullable: true ),
                    ClaimValue = table.Column<string>( type: "nvarchar(max)", nullable: true )
                },
                constraints: table =>
                {
                    table.PrimaryKey( "PK_AspNetRoleClaims", x => x.Id );
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade );
                } );

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>( type: "int", nullable: false )
                        .Annotation( "SqlServer:Identity", "1, 1" ),
                    UserId = table.Column<string>( type: "nvarchar(450)", nullable: false ),
                    ClaimType = table.Column<string>( type: "nvarchar(max)", nullable: true ),
                    ClaimValue = table.Column<string>( type: "nvarchar(max)", nullable: true )
                },
                constraints: table =>
                {
                    table.PrimaryKey( "PK_AspNetUserClaims", x => x.Id );
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade );
                } );

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>( type: "nvarchar(450)", nullable: false ),
                    ProviderKey = table.Column<string>( type: "nvarchar(450)", nullable: false ),
                    ProviderDisplayName = table.Column<string>( type: "nvarchar(max)", nullable: true ),
                    UserId = table.Column<string>( type: "nvarchar(450)", nullable: false )
                },
                constraints: table =>
                {
                    table.PrimaryKey( "PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey } );
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade );
                } );

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>( type: "nvarchar(450)", nullable: false ),
                    RoleId = table.Column<string>( type: "nvarchar(450)", nullable: false )
                },
                constraints: table =>
                {
                    table.PrimaryKey( "PK_AspNetUserRoles", x => new { x.UserId, x.RoleId } );
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade );
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade );
                } );

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>( type: "nvarchar(450)", nullable: false ),
                    LoginProvider = table.Column<string>( type: "nvarchar(450)", nullable: false ),
                    Name = table.Column<string>( type: "nvarchar(450)", nullable: false ),
                    Value = table.Column<string>( type: "nvarchar(max)", nullable: true )
                },
                constraints: table =>
                {
                    table.PrimaryKey( "PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name } );
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade );
                } );

            migrationBuilder.CreateTable(
                name: "ManagerMember",
                columns: table => new
                {
                    ManagersId = table.Column<int>( type: "int", nullable: false ),
                    MembersId = table.Column<int>( type: "int", nullable: false )
                },
                constraints: table =>
                {
                    table.PrimaryKey( "PK_ManagerMember", x => new { x.ManagersId, x.MembersId } );
                    table.ForeignKey(
                        name: "FK_ManagerMember_Managers_ManagersId",
                        column: x => x.ManagersId,
                        principalTable: "Managers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade );
                    table.ForeignKey(
                        name: "FK_ManagerMember_Members_MembersId",
                        column: x => x.MembersId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade );
                } );

            migrationBuilder.CreateTable(
                name: "ManagerTeam",
                columns: table => new
                {
                    ManagersId = table.Column<int>( type: "int", nullable: false ),
                    TeamsId = table.Column<int>( type: "int", nullable: false )
                },
                constraints: table =>
                {
                    table.PrimaryKey( "PK_ManagerTeam", x => new { x.ManagersId, x.TeamsId } );
                    table.ForeignKey(
                        name: "FK_ManagerTeam_Managers_ManagersId",
                        column: x => x.ManagersId,
                        principalTable: "Managers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade );
                    table.ForeignKey(
                        name: "FK_ManagerTeam_Teams_TeamsId",
                        column: x => x.TeamsId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade );
                } );

            migrationBuilder.CreateTable(
                name: "MemberTeam",
                columns: table => new
                {
                    MembersId = table.Column<int>( type: "int", nullable: false ),
                    TeamsId = table.Column<int>( type: "int", nullable: false )
                },
                constraints: table =>
                {
                    table.PrimaryKey( "PK_MemberTeam", x => new { x.MembersId, x.TeamsId } );
                    table.ForeignKey(
                        name: "FK_MemberTeam_Members_MembersId",
                        column: x => x.MembersId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade );
                    table.ForeignKey(
                        name: "FK_MemberTeam_Teams_TeamsId",
                        column: x => x.TeamsId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade );
                } );

            migrationBuilder.InsertData(
                table: "Managers",
                columns: new[] { "Id", "Name", "Surname", "Telephone" },
                values: new object[,]
                {
                    { 1, "Maciej", "Krasko", "123-456-789" },
                    { 2, "Zuzanna", "Krasko", "987-654-321" }
                } );

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "DateOfBirth", "Email", "Name", "Surname" },
                values: new object[,]
                {
                    { 1, new DateTime(2000, 10, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "krzysiek.palonek@gmail.com", "Krzysztof", "Palonek" },
                    { 2, new DateTime(2001, 5, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "marz.koł@gmail.com", "Marzena", "Kołodziej" },
                    { 3, new DateTime(1989, 7, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "jan.kow@gmail.com", "Jan", "Kowalski" },
                    { 4, new DateTime(1999, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nat.uro@gmail.com", "Natalia", "Urodek" }
                } );

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "AssigmentDate", "DeadlineDate", "Description", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 12, 28, 22, 42, 32, 217, DateTimeKind.Local).AddTicks(5340), new DateTime(2023, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Creating connection between database and API", "Back-end" },
                    { 2, new DateTime(2022, 12, 28, 22, 42, 32, 217, DateTimeKind.Local).AddTicks(5424), new DateTime(2023, 5, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Creating website", "Front-end" }
                } );

            migrationBuilder.InsertData(
                table: "ManagerMember",
                columns: new[] { "ManagersId", "MembersId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 3 },
                    { 2, 4 }
                } );

            migrationBuilder.InsertData(
                table: "ManagerTeam",
                columns: new[] { "ManagersId", "TeamsId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 }
                } );

            migrationBuilder.InsertData(
                table: "MemberTeam",
                columns: new[] { "MembersId", "TeamsId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 2 },
                    { 4, 2 }
                } );

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId" );

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL" );

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId" );

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId" );

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId" );

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail" );

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL" );

            migrationBuilder.CreateIndex(
                name: "IX_ManagerMember_MembersId",
                table: "ManagerMember",
                column: "MembersId" );

            migrationBuilder.CreateIndex(
                name: "IX_ManagerTeam_TeamsId",
                table: "ManagerTeam",
                column: "TeamsId" );

            migrationBuilder.CreateIndex(
                name: "IX_MemberTeam_TeamsId",
                table: "MemberTeam",
                column: "TeamsId" );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims" );

            migrationBuilder.DropTable(
                name: "AspNetUserClaims" );

            migrationBuilder.DropTable(
                name: "AspNetUserLogins" );

            migrationBuilder.DropTable(
                name: "AspNetUserRoles" );

            migrationBuilder.DropTable(
                name: "AspNetUserTokens" );

            migrationBuilder.DropTable(
                name: "ManagerMember" );

            migrationBuilder.DropTable(
                name: "ManagerTeam" );

            migrationBuilder.DropTable(
                name: "MemberTeam" );

            migrationBuilder.DropTable(
                name: "AspNetRoles" );

            migrationBuilder.DropTable(
                name: "AspNetUsers" );

            migrationBuilder.DropTable(
                name: "Managers" );

            migrationBuilder.DropTable(
                name: "Members" );

            migrationBuilder.DropTable(
                name: "Teams" );
        }
    }
}