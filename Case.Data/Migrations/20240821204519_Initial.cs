using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Case.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TipoProduto = table.Column<int>(type: "int", nullable: false),
                    DataVencimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Disponivel = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CpfCnpj = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Papel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CpfCnpj = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clientes_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Investimentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProdutoId = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DataCompra = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClienteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Investimentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Investimentos_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Investimentos_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvestimentoId = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transacoes_Investimentos_InvestimentoId",
                        column: x => x.InvestimentoId,
                        principalTable: "Investimentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Produtos",
                columns: new[] { "Id", "DataVencimento", "Disponivel", "Nome", "TipoProduto", "Valor" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 21, 20, 45, 19, 216, DateTimeKind.Utc).AddTicks(3696), true, "HGLG11", 1, 161.88m },
                    { 2, new DateTime(2024, 10, 21, 20, 45, 19, 216, DateTimeKind.Utc).AddTicks(3704), true, "MXRF11", 1, 10.42m },
                    { 3, new DateTime(2024, 12, 21, 20, 45, 19, 216, DateTimeKind.Utc).AddTicks(3705), true, "CVCB3", 0, 2.30m },
                    { 4, new DateTime(2024, 10, 21, 20, 45, 19, 216, DateTimeKind.Utc).AddTicks(3706), true, "PETZ3", 0, 5.16m },
                    { 5, new DateTime(2024, 10, 21, 20, 45, 19, 216, DateTimeKind.Utc).AddTicks(3708), true, "AMER3", 0, 0.9m }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "CpfCnpj", "Email", "Nome", "Papel", "Senha" },
                values: new object[,]
                {
                    { 1, "987654321", "admin@seed.com", "Admin", 0, "" },
                    { 2, "987654321", "Operacao@seed.com", "Operacao", 1, "" },
                    { 3, "458787878", "cliente@seed.com", "Maria Doe", 2, "" },
                    { 4, "125879875", "cliente2@seed.com", "John Doe", 2, "" }
                });

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "CpfCnpj", "UsuarioId" },
                values: new object[] { 1, "458787878", 3 });

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "CpfCnpj", "UsuarioId" },
                values: new object[] { 2, "125879875", 4 });

            migrationBuilder.InsertData(
                table: "Investimentos",
                columns: new[] { "Id", "ClienteId", "DataCompra", "Preco", "ProdutoId", "Quantidade" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 5, 21, 20, 45, 19, 216, DateTimeKind.Utc).AddTicks(3725), 1618.80m, 1, 10 },
                    { 2, 1, new DateTime(2024, 6, 21, 20, 45, 19, 216, DateTimeKind.Utc).AddTicks(3726), 521.00m, 2, 50 },
                    { 3, 1, new DateTime(2024, 7, 21, 20, 45, 19, 216, DateTimeKind.Utc).AddTicks(3728), 460.00m, 3, 200 },
                    { 4, 1, new DateTime(2024, 7, 21, 20, 45, 19, 216, DateTimeKind.Utc).AddTicks(3729), 516.00m, 4, 100 },
                    { 5, 2, new DateTime(2024, 5, 21, 20, 45, 19, 216, DateTimeKind.Utc).AddTicks(3730), 809.40m, 1, 5 },
                    { 6, 2, new DateTime(2024, 6, 21, 20, 45, 19, 216, DateTimeKind.Utc).AddTicks(3731), 270.00m, 5, 300 }
                });

            migrationBuilder.InsertData(
                table: "Transacoes",
                columns: new[] { "Id", "Data", "InvestimentoId", "Preco", "Quantidade", "Tipo" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 8, 19, 20, 45, 19, 216, DateTimeKind.Utc).AddTicks(3744), 1, 809.55m, 5m, 0 },
                    { 2, new DateTime(2024, 8, 20, 20, 45, 19, 216, DateTimeKind.Utc).AddTicks(3748), 1, 809.55m, 5m, 1 },
                    { 3, new DateTime(2024, 7, 21, 20, 45, 19, 216, DateTimeKind.Utc).AddTicks(3749), 2, 208.4m, 20m, 0 },
                    { 4, new DateTime(2024, 8, 21, 20, 45, 19, 216, DateTimeKind.Utc).AddTicks(3750), 2, 104.2m, 10m, 1 },
                    { 5, new DateTime(2024, 7, 21, 20, 45, 19, 216, DateTimeKind.Utc).AddTicks(3751), 3, 230m, 100m, 0 },
                    { 6, new DateTime(2024, 8, 21, 20, 45, 19, 216, DateTimeKind.Utc).AddTicks(3754), 3, 115m, 50m, 1 },
                    { 7, new DateTime(2024, 8, 21, 20, 45, 19, 216, DateTimeKind.Utc).AddTicks(3755), 4, 258m, 50m, 0 },
                    { 8, new DateTime(2024, 6, 21, 20, 45, 19, 216, DateTimeKind.Utc).AddTicks(3760), 5, 323.76m, 2m, 0 },
                    { 9, new DateTime(2024, 7, 21, 20, 45, 19, 216, DateTimeKind.Utc).AddTicks(3761), 6, 90m, 100m, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_UsuarioId",
                table: "Clientes",
                column: "UsuarioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Investimentos_ClienteId",
                table: "Investimentos",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Investimentos_ProdutoId",
                table: "Investimentos",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_Transacoes_InvestimentoId",
                table: "Transacoes",
                column: "InvestimentoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transacoes");

            migrationBuilder.DropTable(
                name: "Investimentos");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
