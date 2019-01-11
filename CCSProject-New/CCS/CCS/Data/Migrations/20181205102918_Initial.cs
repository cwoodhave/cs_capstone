using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CCS.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "IsAdmin",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserIDName",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserPassword",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CityName = table.Column<string>(nullable: true),
                    StateShortName = table.Column<string>(nullable: true),
                    Zipcode = table.Column<string>(nullable: true),
                    StreetAddress1 = table.Column<string>(nullable: true),
                    StreetAddress2 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressID);
                });

            migrationBuilder.CreateTable(
                name: "Adjustment",
                columns: table => new
                {
                    AdjustmentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CCSUserID = table.Column<int>(nullable: false),
                    Weight = table.Column<decimal>(nullable: false),
                    FoodCategory = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    IsUSDA = table.Column<bool>(nullable: false),
                    USDANumber = table.Column<string>(nullable: true),
                    Cases = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adjustment", x => x.AdjustmentID);
                    table.ForeignKey(
                        name: "FK_Adjustment_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DonationTypes",
                columns: table => new
                {
                    DonationTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonationTypes", x => x.DonationTypeID);
                });

            migrationBuilder.CreateTable(
                name: "FoodCategories",
                columns: table => new
                {
                    FoodCategoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FoodCategoryType = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodCategories", x => x.FoodCategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    LocationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoomName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.LocationID);
                });

            migrationBuilder.CreateTable(
                name: "Log",
                columns: table => new
                {
                    LogID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CCSUserID = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log", x => x.LogID);
                    table.ForeignKey(
                        name: "FK_Log_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Agencies",
                columns: table => new
                {
                    AgencyID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddressID = table.Column<int>(nullable: false),
                    AgencyName = table.Column<string>(nullable: true),
                    IsDonor = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agencies", x => x.AgencyID);
                    table.ForeignKey(
                        name: "FK_Agencies_Addresses_AddressID",
                        column: x => x.AddressID,
                        principalTable: "Addresses",
                        principalColumn: "AddressID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Container",
                columns: table => new
                {
                    ContainerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FoodCategoryID = table.Column<int>(nullable: false),
                    LocationID = table.Column<int>(nullable: false),
                    BinNumber = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Weight = table.Column<decimal>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Container", x => x.ContainerID);
                    table.ForeignKey(
                        name: "FK_Container_FoodCategories_FoodCategoryID",
                        column: x => x.FoodCategoryID,
                        principalTable: "FoodCategories",
                        principalColumn: "FoodCategoryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Container_Location_LocationID",
                        column: x => x.LocationID,
                        principalTable: "Location",
                        principalColumn: "LocationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DonationTransactions",
                columns: table => new
                {
                    DonationTransactionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AgencyID = table.Column<int>(nullable: false),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    TransactionType = table.Column<int>(nullable: false),
                    FoodCategory = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonationTransactions", x => x.DonationTransactionID);
                    table.ForeignKey(
                        name: "FK_DonationTransactions_Agencies_AgencyID",
                        column: x => x.AgencyID,
                        principalTable: "Agencies",
                        principalColumn: "AgencyID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransactionLineItems",
                columns: table => new
                {
                    TransactionLineItemID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FoodCategoryID = table.Column<int>(nullable: false),
                    DonationTypeID = table.Column<int>(nullable: false),
                    DonationTransactionID = table.Column<int>(nullable: false),
                    Weight = table.Column<decimal>(nullable: false),
                    Quantity = table.Column<int>(nullable: true),
                    Cases = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionLineItems", x => x.TransactionLineItemID);
                    table.ForeignKey(
                        name: "FK_TransactionLineItems_DonationTransactions_DonationTransactionID",
                        column: x => x.DonationTransactionID,
                        principalTable: "DonationTransactions",
                        principalColumn: "DonationTransactionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransactionLineItems_DonationTypes_DonationTypeID",
                        column: x => x.DonationTypeID,
                        principalTable: "DonationTypes",
                        principalColumn: "DonationTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransactionLineItems_FoodCategories_FoodCategoryID",
                        column: x => x.FoodCategoryID,
                        principalTable: "FoodCategories",
                        principalColumn: "FoodCategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adjustment_UserId",
                table: "Adjustment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Agencies_AddressID",
                table: "Agencies",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_Container_FoodCategoryID",
                table: "Container",
                column: "FoodCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Container_LocationID",
                table: "Container",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_DonationTransactions_AgencyID",
                table: "DonationTransactions",
                column: "AgencyID");

            migrationBuilder.CreateIndex(
                name: "IX_Log_UserId",
                table: "Log",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionLineItems_DonationTransactionID",
                table: "TransactionLineItems",
                column: "DonationTransactionID");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionLineItems_DonationTypeID",
                table: "TransactionLineItems",
                column: "DonationTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionLineItems_FoodCategoryID",
                table: "TransactionLineItems",
                column: "FoodCategoryID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Adjustment");

            migrationBuilder.DropTable(
                name: "Container");

            migrationBuilder.DropTable(
                name: "Log");

            migrationBuilder.DropTable(
                name: "TransactionLineItems");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "DonationTransactions");

            migrationBuilder.DropTable(
                name: "DonationTypes");

            migrationBuilder.DropTable(
                name: "FoodCategories");

            migrationBuilder.DropTable(
                name: "Agencies");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserIDName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserPassword",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
