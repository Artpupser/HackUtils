using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RyazanTrip.DataAccess.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "bonuses",
                columns: table => new
                {
                    BonusId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DiscountPercent = table.Column<short>(type: "smallint", nullable: true),
                    QrCode = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bonuses", x => x.BonusId);
                });

            migrationBuilder.CreateTable(
                name: "images",
                columns: table => new
                {
                    ImageId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Url = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_images", x => x.ImageId);
                });

            migrationBuilder.CreateTable(
                name: "levels",
                columns: table => new
                {
                    LevelId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    RequiredExp = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_levels", x => x.LevelId);
                });

            migrationBuilder.CreateTable(
                name: "qr_codes",
                columns: table => new
                {
                    QrId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    ReferenceId = table.Column<int>(type: "integer", nullable: true),
                    Content = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_qr_codes", x => x.QrId);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "mushrooms",
                columns: table => new
                {
                    MushroomId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Location = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    ImageId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mushrooms", x => x.MushroomId);
                    table.ForeignKey(
                        name: "FK_mushrooms_images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "images",
                        principalColumn: "ImageId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "tours",
                columns: table => new
                {
                    TourId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true),
                    TourTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ImageId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tours", x => x.TourId);
                    table.ForeignKey(
                        name: "FK_tours_images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "images",
                        principalColumn: "ImageId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    LevelId = table.Column<int>(type: "integer", nullable: false),
                    Email = table.Column<string>(type: "character varying(254)", maxLength: 254, nullable: false),
                    PasswordHash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Username = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Town = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Experience = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_users_levels_LevelId",
                        column: x => x.LevelId,
                        principalTable: "levels",
                        principalColumn: "LevelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_users_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "mushroom_submissions",
                columns: table => new
                {
                    SubmissionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    MushroomId = table.Column<int>(type: "integer", nullable: true),
                    ImageId = table.Column<int>(type: "integer", nullable: true),
                    Status = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mushroom_submissions", x => x.SubmissionId);
                    table.ForeignKey(
                        name: "FK_mushroom_submissions_images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "images",
                        principalColumn: "ImageId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_mushroom_submissions_mushrooms_MushroomId",
                        column: x => x.MushroomId,
                        principalTable: "mushrooms",
                        principalColumn: "MushroomId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_mushroom_submissions_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    Token = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sessions_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_bonuses",
                columns: table => new
                {
                    UserBonusId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    BonusId = table.Column<int>(type: "integer", nullable: true),
                    Status = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_bonuses", x => x.UserBonusId);
                    table.ForeignKey(
                        name: "FK_user_bonuses_bonuses_BonusId",
                        column: x => x.BonusId,
                        principalTable: "bonuses",
                        principalColumn: "BonusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_bonuses_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_tours",
                columns: table => new
                {
                    UserTourId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    TourId = table.Column<int>(type: "integer", nullable: true),
                    GuideOption = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: true),
                    Status = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: true),
                    TourEntityTourId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_tours", x => x.UserTourId);
                    table.ForeignKey(
                        name: "FK_user_tours_tours_TourEntityTourId",
                        column: x => x.TourEntityTourId,
                        principalTable: "tours",
                        principalColumn: "TourId");
                    table.ForeignKey(
                        name: "FK_user_tours_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "payments",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserTourId = table.Column<int>(type: "integer", nullable: true),
                    Amount = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true),
                    Status = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: true),
                    YukassaPaymentId = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_payments_user_tours_UserTourId",
                        column: x => x.UserTourId,
                        principalTable: "user_tours",
                        principalColumn: "UserTourId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "levels",
                columns: new[] { "LevelId", "Name", "RequiredExp" },
                values: new object[,]
                {
                    { 1, "Медь", 0 },
                    { 2, "Железо", 100 },
                    { 3, "Золото", 500 },
                    { 4, "Платина", 1000 },
                    { 5, "Брилиант", 5000 }
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "RoleId", "Name" },
                values: new object[,]
                {
                    { 1, "User" },
                    { 2, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "Email", "Experience", "LevelId", "PasswordHash", "RoleId", "Town", "Username" },
                values: new object[] { 1, "artpupser@ya.ru", 0, 1, "5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5", 2, "Рязань", "artpupser" });

            migrationBuilder.CreateIndex(
                name: "IX_mushroom_submissions_ImageId",
                table: "mushroom_submissions",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_mushroom_submissions_MushroomId",
                table: "mushroom_submissions",
                column: "MushroomId");

            migrationBuilder.CreateIndex(
                name: "IX_mushroom_submissions_UserId",
                table: "mushroom_submissions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_mushrooms_ImageId",
                table: "mushrooms",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_payments_UserTourId",
                table: "payments",
                column: "UserTourId");

            migrationBuilder.CreateIndex(
                name: "IX_sessions_UserId",
                table: "sessions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tours_ImageId",
                table: "tours",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_user_bonuses_BonusId",
                table: "user_bonuses",
                column: "BonusId");

            migrationBuilder.CreateIndex(
                name: "IX_user_bonuses_UserId",
                table: "user_bonuses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_user_tours_TourEntityTourId",
                table: "user_tours",
                column: "TourEntityTourId");

            migrationBuilder.CreateIndex(
                name: "IX_user_tours_UserId",
                table: "user_tours",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_users_LevelId",
                table: "users",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_users_RoleId",
                table: "users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mushroom_submissions");

            migrationBuilder.DropTable(
                name: "payments");

            migrationBuilder.DropTable(
                name: "qr_codes");

            migrationBuilder.DropTable(
                name: "sessions");

            migrationBuilder.DropTable(
                name: "user_bonuses");

            migrationBuilder.DropTable(
                name: "mushrooms");

            migrationBuilder.DropTable(
                name: "user_tours");

            migrationBuilder.DropTable(
                name: "bonuses");

            migrationBuilder.DropTable(
                name: "tours");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "images");

            migrationBuilder.DropTable(
                name: "levels");

            migrationBuilder.DropTable(
                name: "roles");
        }
    }
}
