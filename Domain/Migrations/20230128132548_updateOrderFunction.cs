using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service.Migrations
{
    public partial class updateOrderFunction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meals_MealCategorys_MealCategoryID",
                table: "Meals");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Meals_MealID",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Orders_OrderID1",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_MealID",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderID1",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_OrderProcesses_OrderID",
                table: "OrderProcesses");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "MealID",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "MealQuantity",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderID1",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "PrcessStatus",
                table: "Orders",
                newName: "Note");

            migrationBuilder.AddColumn<decimal>(
                name: "SumPrice",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "OrderProcessID",
                table: "OrderProcesses",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "MealCategoryID",
                table: "Meals",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderProcesses",
                table: "OrderProcesses",
                column: "OrderProcessID");

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderDetailID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrderID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MealID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MealQuantity = table.Column<int>(type: "int", nullable: false),
                    MealPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.OrderDetailID);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Meals_MealID",
                        column: x => x.MealID,
                        principalTable: "Meals",
                        principalColumn: "MealID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderProcesses_OrderID",
                table: "OrderProcesses",
                column: "OrderID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_MealID",
                table: "OrderDetails",
                column: "MealID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderID",
                table: "OrderDetails",
                column: "OrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_MealCategorys_MealCategoryID",
                table: "Meals",
                column: "MealCategoryID",
                principalTable: "MealCategorys",
                principalColumn: "MealCategoryID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meals_MealCategorys_MealCategoryID",
                table: "Meals");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderProcesses",
                table: "OrderProcesses");

            migrationBuilder.DropIndex(
                name: "IX_OrderProcesses_OrderID",
                table: "OrderProcesses");

            migrationBuilder.DropColumn(
                name: "SumPrice",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderProcessID",
                table: "OrderProcesses");

            migrationBuilder.RenameColumn(
                name: "Note",
                table: "Orders",
                newName: "PrcessStatus");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MealID",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MealQuantity",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OrderID1",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MealCategoryID",
                table: "Meals",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_MealID",
                table: "Orders",
                column: "MealID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderID1",
                table: "Orders",
                column: "OrderID1");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProcesses_OrderID",
                table: "OrderProcesses",
                column: "OrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_MealCategorys_MealCategoryID",
                table: "Meals",
                column: "MealCategoryID",
                principalTable: "MealCategorys",
                principalColumn: "MealCategoryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Meals_MealID",
                table: "Orders",
                column: "MealID",
                principalTable: "Meals",
                principalColumn: "MealID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Orders_OrderID1",
                table: "Orders",
                column: "OrderID1",
                principalTable: "Orders",
                principalColumn: "OrderID");
        }
    }
}
