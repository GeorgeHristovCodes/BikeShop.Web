using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BikeShop.Web.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accessories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category = table.Column<int>(type: "int", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accessories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                });

            migrationBuilder.CreateTable(
                name: "Bicycles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FrameSize = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bicycles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
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
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
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

            migrationBuilder.CreateTable(
                name: "BicycleImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BicycleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BicycleImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BicycleImages_Bicycles_BicycleId",
                        column: x => x.BicycleId,
                        principalTable: "Bicycles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BicycleId = table.Column<int>(type: "int", nullable: true),
                    AccessoriesId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    RentalStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RentalEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Accessories_AccessoriesId",
                        column: x => x.AccessoriesId,
                        principalTable: "Accessories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CartItems_Bicycles_BicycleId",
                        column: x => x.BicycleId,
                        principalTable: "Bicycles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BicycleId = table.Column<int>(type: "int", nullable: true),
                    AccessoriesId = table.Column<int>(type: "int", nullable: true),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryStreet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryStreetNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDelivery = table.Column<bool>(type: "bit", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Accessories_AccessoriesId",
                        column: x => x.AccessoriesId,
                        principalTable: "Accessories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Bicycles_BicycleId",
                        column: x => x.BicycleId,
                        principalTable: "Bicycles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Rentals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BicycleId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rentals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rentals_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rentals_Bicycles_BicycleId",
                        column: x => x.BicycleId,
                        principalTable: "Bicycles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Accessories",
                columns: new[] { "Id", "Brand", "Category", "Description", "ImageUrl", "Name", "Price", "Status", "Stock" },
                values: new object[,]
                {
                    { 1, "DRAG", 0, "Лека и удобна каска за градски и планински маршрути.", "/images/demo-accessories/helmet1.jpg", "Каска DRAG One", 89m, 0, 12 },
                    { 2, "SPECIALIZED", 0, "Професионална каска с вентилация и регулируема лента.", "/images/demo-accessories/helmet2.jpg", "Каска Specialized Pro", 119m, 0, 8 },
                    { 3, "NS BIKES", 0, "Градска каска със стилен дизайн и защита.", "/images/demo-accessories/helmet3.jpg", "Каска CitySafe", 65m, 0, 15 },
                    { 4, "DRAG", 1, "Къси ръкавици за лятно каране с дишаща материя.", "/images/demo-accessories/gloves1.jpg", "Ръкавици DRAG Short", 35m, 0, 20 },
                    { 5, "NS BIKES", 1, "Гумени елементи за по-добро сцепление и комфорт.", "/images/demo-accessories/gloves2.jpg", "Ръкавици NS Grip", 42m, 0, 14 },
                    { 6, "SPECIALIZED", 1, "Ежедневни ръкавици за кратки разходки.", "/images/demo-accessories/gloves3.jpg", "Ръкавици UrbanRide", 29m, 0, 18 },
                    { 7, "DRAG", 2, "Лека преносима помпа с универсален вентил.", "/images/demo-accessories/pump1.jpg", "Помпа DRAG Mini", 22m, 0, 25 },
                    { 8, "SPECIALIZED", 2, "Помпа с манометър и алуминиево тяло.", "/images/demo-accessories/pump2.jpg", "Помпа Specialized Air", 45m, 0, 10 },
                    { 9, "NS BIKES", 2, "Здрава помпа с удобна дръжка за планински велосипеди.", "/images/demo-accessories/pump3.jpg", "Помпа TrailPump", 35m, 0, 18 },
                    { 10, "SPECIALIZED", 3, "Комплект с най-нужните инструменти за пътя.", "/images/demo-accessories/tool1.jpg", "Инструментален комплект 9 в 1", 55m, 0, 11 },
                    { 11, "NS BIKES", 3, "Компактен мултиинструмент за чанта или джоб.", "/images/demo-accessories/tool2.jpg", "Мултиинструмент NS", 49m, 0, 7 },
                    { 12, "DRAG", 3, "Инструментариум за домашни ремонти на велосипеда.", "/images/demo-accessories/tool3.jpg", "Инструменти HomeFix", 60m, 0, 6 },
                    { 13, "DRAG", 4, "Подсилени наколенки за планинско каране.", "/images/demo-accessories/knee1.jpg", "Наколенки DRAG Protect", 75m, 0, 10 },
                    { 14, "NS BIKES", 4, "Меки и удобни наколенки с проветрив материал.", "/images/demo-accessories/knee2.jpg", "Наколенки Trail Soft", 59m, 0, 12 },
                    { 15, "SPECIALIZED", 4, "Градски стил с основна защита.", "/images/demo-accessories/knee3.jpg", "Наколенки CityFlex", 49m, 0, 9 },
                    { 16, "DRAG", 5, "Здрава защита за лакти при агресивно каране.", "/images/demo-accessories/elbow1.jpg", "Лакътници DRAG Shield", 55m, 0, 10 },
                    { 17, "SPECIALIZED", 5, "Защита с гъвкав дизайн и комфортна подложка.", "/images/demo-accessories/elbow2.jpg", "Лакътници UrbanPad", 38m, 0, 12 },
                    { 18, "NS BIKES", 5, "Леки лакътници за ежедневно каране.", "/images/demo-accessories/elbow3.jpg", "Лакътници LightRide", 29m, 0, 15 },
                    { 19, "DRAG", 6, "Слънцезащитни очила с UV филтър.", "/images/demo-accessories/glasses1.jpg", "Очила DRAG ProVision", 89m, 0, 13 },
                    { 20, "NS BIKES", 6, "Спортен дизайн и сменяеми стъкла.", "/images/demo-accessories/glasses2.jpg", "Очила TrailVision", 95m, 0, 11 },
                    { 21, "SPECIALIZED", 6, "Очилата за модерни велосипедисти в града.", "/images/demo-accessories/glasses3.jpg", "Очила CityView", 70m, 0, 16 }
                });

            migrationBuilder.InsertData(
                table: "Bicycles",
                columns: new[] { "Id", "Brand", "Category", "Description", "FrameSize", "ImageUrl", "IsAvailable", "Name", "Price", "Quantity", "Type" },
                values: new object[,]
                {
                    { 1, "DRAG", 0, "Лек и бърз шосеен велосипед, идеален за състезания и дълги карания по асфалт.", "M", "/images/road1-1.jpg", true, "Drag C1 Road", 3200m, 5, 1 },
                    { 2, "SPECIALIZED", 0, "Надежден и ефективен шосеен велосипед с отлична аеродинамика.", "L", "/images/road2-1.jpg", true, "Specialized Allez", 4600m, 3, 1 },
                    { 3, "NS BIKES", 1, "Маневрен хардтейл за трикове и каране в градска среда.", "S", "/images/hardtail1-1.jpg", true, "NS Bikes Zircus", 2800m, 4, 1 },
                    { 4, "DRAG", 1, "Планински велосипед с твърда рамка, идеален за off-road каране.", "M", "/images/hardtail2-1.jpg", true, "Drag Hardy Pro", 3000m, 7, 1 },
                    { 5, "YT INDUSTRIES", 2, "Сериозен планински велосипед с пълно окачване за максимален контрол по пресечени терени.", "L", "/images/full1-1.jpg", true, "YT Jeffsy Core", 5200m, 6, 1 },
                    { 6, "SPECIALIZED", 2, "Премиум MTB за тежки терени. Със здрава рамка и модерна геометрия.", "XL", "/images/full2-1.jpg", true, "Specialized Stumpjumper Evo", 5800m, 2, 1 },
                    { 7, "DRAG", 3, "Удобен и стилен градски велосипед – идеален за ежедневно придвижване.", "M", "/images/city1-1.jpg", true, "Drag City Rider", 2200m, 8, 1 },
                    { 8, "NS BIKES", 3, "Хибриден велосипед за град и офроуд. Съчетава комфорт и здравина.", "L", "/images/city2-1.jpg", true, "NS Bikes Metropolis", 2500m, 4, 1 },
                    { 9, "DRAG", 4, "Цветен велосипед за деца от 5 до 8 години. Сигурен и лек.", "XS", "/images/kid1-1.jpg", true, "Drag Kid 20\"", 950m, 10, 1 },
                    { 10, "YT INDUSTRIES", 4, "Качествен велосипед за деца от 8 до 12 г. Стабилен и удобен за каране.", "S", "/images/kid2-1.jpg", true, "YT Primus 24\"", 1050m, 9, 1 },
                    { 11, "DRAG", 0, "Шосеен велосипед за наем с алуминиева рамка, лек и комфортен.", "M", "/images/demo-bicycles/road-rent1.jpg", true, "Drag X-Rent Road", 25m, 3, 0 },
                    { 12, "SPECIALIZED", 0, "Шосейно колело за бързо придвижване в града и околностите.", "L", "/images/demo-bicycles/road-rent2.jpg", true, "Specialized Allez Rent", 30m, 4, 0 },
                    { 13, "NS BIKES", 1, "Хардтейл колело за планинско каране и скокове – перфектно за наем.", "S", "/images/demo-bicycles/hardtail-rent1.jpg", true, "NS Rent Zircus", 35m, 5, 0 },
                    { 14, "DRAG", 1, "Здрав велосипед под наем, идеален за офроуд разходки в планината.", "M", "/images/demo-bicycles/hardtail-rent2.jpg", true, "Drag Hardy Rent", 38m, 6, 0 },
                    { 15, "YT INDUSTRIES", 2, "Пълноокачен велосипед за под наем с максимално удобство по тежък терен.", "L", "/images/demo-bicycles/full-rent1.jpg", true, "YT Jeffsy Rent", 45m, 3, 0 },
                    { 16, "SPECIALIZED", 2, "Професионален MTB за сериозни трасета, достъпен под наем.", "XL", "/images/demo-bicycles/full-rent2.jpg", true, "Stumpjumper Evo Rent", 48m, 2, 0 },
                    { 17, "DRAG", 3, "Градски велосипед под наем за комфортно придвижване.", "M", "/images/demo-bicycles/city-rent1.jpg", true, "Drag City Rent", 20m, 5, 0 },
                    { 18, "NS BIKES", 3, "Стилен градски велосипед, удобен за кратки наеми.", "L", "/images/demo-bicycles/city-rent2.jpg", true, "NS City Urban Rent", 22m, 4, 0 },
                    { 19, "DRAG", 4, "Безопасен детски велосипед за наем, подходящ за деца 5-8г.", "XS", "/images/demo-bicycles/kid-rent1.jpg", true, "Drag Kiddo Rent 20\"", 12m, 4, 0 },
                    { 20, "YT INDUSTRIES", 4, "Детски велосипед за деца 8-12г. Подходящ за разходки в парка.", "S", "/images/demo-bicycles/kid-rent2.jpg", true, "YT Primus Rent 24\"", 14m, 5, 0 }
                });

            migrationBuilder.InsertData(
                table: "BicycleImages",
                columns: new[] { "Id", "BicycleId", "ImageUrl" },
                values: new object[,]
                {
                    { 1, 1, "/images/demo-bicycles/road1-1.jpg" },
                    { 2, 1, "/images/demo-bicycles/road1-2.jpg" },
                    { 3, 1, "/images/demo-bicycles/road1-3.jpg" },
                    { 4, 2, "/images/demo-bicycles/road2-1.jpg" },
                    { 5, 2, "/images/demo-bicycles/road2-2.jpg" },
                    { 6, 2, "/images/demo-bicycles/road2-3.jpg" },
                    { 7, 3, "/images/demo-bicycles/hardtail1-1.jpg" },
                    { 8, 3, "/images/demo-bicycles/hardtail1-2.jpg" },
                    { 9, 3, "/images/demo-bicycles/hardtail1-3.jpg" },
                    { 10, 4, "/images/demo-bicycles/hardtail2-1.jpg" },
                    { 11, 4, "/images/demo-bicycles/hardtail2-2.jpg" },
                    { 12, 4, "/images/demo-bicycles/hardtail2-3.jpg" },
                    { 13, 5, "/images/demo-bicycles/full1-1.jpg" },
                    { 14, 5, "/images/demo-bicycles/full1-2.jpg" },
                    { 15, 5, "/images/demo-bicycles/full1-3.jpg" },
                    { 16, 6, "/images/demo-bicycles/full2-1.jpg" },
                    { 17, 6, "/images/demo-bicycles/full2-2.jpg" },
                    { 18, 6, "/images/demo-bicycles/full2-3.jpg" },
                    { 19, 7, "/images/demo-bicycles/city1-1.jpg" },
                    { 20, 7, "/images/demo-bicycles/city1-2.jpg" },
                    { 21, 7, "/images/demo-bicycles/city1-3.jpg" },
                    { 22, 8, "/images/demo-bicycles/city2-1.jpg" },
                    { 23, 8, "/images/demo-bicycles/city2-2.jpg" },
                    { 24, 8, "/images/demo-bicycles/city2-3.jpg" },
                    { 25, 9, "/images/demo-bicycles/kid1-1.jpg" },
                    { 26, 9, "/images/demo-bicycles/kid1-2.jpg" },
                    { 27, 9, "/images/demo-bicycles/kid1-3.jpg" },
                    { 28, 10, "/images/demo-bicycles/kid2-1.jpg" },
                    { 29, 10, "/images/demo-bicycles/kid2-2.jpg" },
                    { 30, 10, "/images/demo-bicycles/kid2-3.jpg" },
                    { 31, 11, "/images/demo-bicycles/road-rent1.jpg" },
                    { 32, 11, "/images/demo-bicycles/road-rent1-2.jpg" },
                    { 33, 11, "/images/demo-bicycles/road-rent1-3.jpg" },
                    { 34, 12, "/images/demo-bicycles/road-rent2.jpg" },
                    { 35, 12, "/images/demo-bicycles/road-rent2-2.jpg" },
                    { 36, 12, "/images/demo-bicycles/road-rent2-3.jpg" },
                    { 37, 13, "/images/demo-bicycles/hardtail-rent1.jpg" },
                    { 38, 13, "/images/demo-bicycles/hardtail-rent1-2.jpg" },
                    { 39, 13, "/images/demo-bicycles/hardtail-rent1-3.jpg" },
                    { 40, 14, "/images/demo-bicycles/hardtail-rent2.jpg" },
                    { 41, 14, "/images/demo-bicycles/hardtail-rent2-2.jpg" },
                    { 42, 14, "/images/demo-bicycles/hardtail-rent2-3.jpg" },
                    { 43, 15, "/images/demo-bicycles/full-rent1.jpg" },
                    { 44, 15, "/images/demo-bicycles/full-rent1-2.jpg" },
                    { 45, 15, "/images/demo-bicycles/full-rent1-3.jpg" },
                    { 46, 16, "/images/demo-bicycles/full-rent2.jpg" },
                    { 47, 16, "/images/demo-bicycles/full-rent2-2.jpg" },
                    { 48, 16, "/images/demo-bicycles/full-rent2-3.jpg" },
                    { 49, 17, "/images/demo-bicycles/city-rent1.jpg" },
                    { 50, 17, "/images/demo-bicycles/city-rent1-2.jpg" },
                    { 51, 17, "/images/demo-bicycles/city-rent1-3.jpg" },
                    { 52, 18, "/images/demo-bicycles/city-rent2.jpg" },
                    { 53, 18, "/images/demo-bicycles/city-rent2-2.jpg" },
                    { 54, 18, "/images/demo-bicycles/city-rent2-3.jpg" },
                    { 55, 19, "/images/demo-bicycles/kid-rent1.jpg" },
                    { 56, 19, "/images/demo-bicycles/kid-rent1-2.jpg" },
                    { 57, 19, "/images/demo-bicycles/kid-rent1-3.jpg" },
                    { 58, 20, "/images/demo-bicycles/kid-rent2.jpg" },
                    { 59, 20, "/images/demo-bicycles/kid-rent2-2.jpg" },
                    { 60, 20, "/images/demo-bicycles/kid-rent2-3.jpg" }
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
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BicycleImages_BicycleId",
                table: "BicycleImages",
                column: "BicycleId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_AccessoriesId",
                table: "CartItems",
                column: "AccessoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_BicycleId",
                table: "CartItems",
                column: "BicycleId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AccessoriesId",
                table: "Orders",
                column: "AccessoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BicycleId",
                table: "Orders",
                column: "BicycleId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_BicycleId",
                table: "Rentals",
                column: "BicycleId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_UserId",
                table: "Rentals",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "BicycleImages");

            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Rentals");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Accessories");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Bicycles");
        }
    }
}
