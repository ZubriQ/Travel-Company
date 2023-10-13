using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Travel_Company.WPF.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Hotel",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    @class = table.Column<string>(name: "class", type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotel", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Street",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Street", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "PopulatedPlace",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    country_id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PopulatedPlace", x => x.id);
                    table.ForeignKey(
                        name: "FK_PopulatedPlace_Country",
                        column: x => x.country_id,
                        principalTable: "Country",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Route",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    start_datetime = table.Column<DateTime>(type: "datetime", nullable: false),
                    end_datetime = table.Column<DateTime>(type: "datetime", nullable: false),
                    cost = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    country_id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Route", x => x.id);
                    table.ForeignKey(
                        name: "FK_Route_Country",
                        column: x => x.country_id,
                        principalTable: "Country",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "TourGuide",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    first_name = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    last_name = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    patronymic = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    street_id = table.Column<long>(type: "INTEGER", nullable: false),
                    birthdate = table.Column<DateTime>(type: "date", nullable: false),
                    salary = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    is_fired = table.Column<bool>(type: "INTEGER", nullable: false),
                    fired_date = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourGuide", x => x.id);
                    table.ForeignKey(
                        name: "FK_TourGuide_Street",
                        column: x => x.street_id,
                        principalTable: "Street",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "RoutesPopulatedPlaces",
                columns: table => new
                {
                    route_id = table.Column<long>(type: "INTEGER", nullable: false),
                    populated_place_id = table.Column<long>(type: "INTEGER", nullable: false),
                    hotel_id = table.Column<int>(type: "INTEGER", nullable: false),
                    stay_start_datetime = table.Column<DateTime>(type: "datetime", nullable: false),
                    stay_end_datetime = table.Column<DateTime>(type: "datetime", nullable: false),
                    excursion_program = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoutesPopulatedPlaces", x => new { x.route_id, x.populated_place_id });
                    table.ForeignKey(
                        name: "FK_RoutesPopulatedPlaces_Hotel",
                        column: x => x.hotel_id,
                        principalTable: "Hotel",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_RoutesPopulatedPlaces_PopulatedPlace",
                        column: x => x.populated_place_id,
                        principalTable: "PopulatedPlace",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_RoutesPopulatedPlaces_Route",
                        column: x => x.route_id,
                        principalTable: "Route",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "TouristGroup",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    tour_guide_id = table.Column<int>(type: "INTEGER", nullable: false),
                    route_id = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TouristGroup", x => x.id);
                    table.ForeignKey(
                        name: "FK_TouristGroup_Route",
                        column: x => x.route_id,
                        principalTable: "Route",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_TouristGroup_TourGuide",
                        column: x => x.tour_guide_id,
                        principalTable: "TourGuide",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    first_name = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    last_name = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    patronymic = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    birthdate = table.Column<DateTime>(type: "date", nullable: false),
                    street_id = table.Column<long>(type: "INTEGER", nullable: false),
                    tourist_group_id = table.Column<long>(type: "INTEGER", nullable: false),
                    passport_series = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    passport_number = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    passport_issue_date = table.Column<DateTime>(type: "date", nullable: false),
                    passport_issuing_authority = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    photograph = table.Column<byte[]>(type: "image", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.id);
                    table.ForeignKey(
                        name: "FK_Client_Street",
                        column: x => x.street_id,
                        principalTable: "Street",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Client_TouristGroup",
                        column: x => x.tourist_group_id,
                        principalTable: "TouristGroup",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Penalty",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    client_id = table.Column<long>(type: "INTEGER", nullable: false),
                    tour_guide_id = table.Column<int>(type: "INTEGER", nullable: false),
                    compensation_amount = table.Column<decimal>(type: "decimal(18, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Penalty", x => x.id);
                    table.ForeignKey(
                        name: "FK_Penalty_Client",
                        column: x => x.client_id,
                        principalTable: "Client",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Penalty_TourGuide",
                        column: x => x.tour_guide_id,
                        principalTable: "TourGuide",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Client_street_id",
                table: "Client",
                column: "street_id");

            migrationBuilder.CreateIndex(
                name: "IX_Client_tourist_group_id",
                table: "Client",
                column: "tourist_group_id");

            migrationBuilder.CreateIndex(
                name: "IX_Penalty_client_id",
                table: "Penalty",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "IX_Penalty_tour_guide_id",
                table: "Penalty",
                column: "tour_guide_id");

            migrationBuilder.CreateIndex(
                name: "IX_PopulatedPlace_country_id",
                table: "PopulatedPlace",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "IX_Route_country_id",
                table: "Route",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "IX_RoutesPopulatedPlaces_hotel_id",
                table: "RoutesPopulatedPlaces",
                column: "hotel_id");

            migrationBuilder.CreateIndex(
                name: "IX_RoutesPopulatedPlaces_populated_place_id",
                table: "RoutesPopulatedPlaces",
                column: "populated_place_id");

            migrationBuilder.CreateIndex(
                name: "IX_TourGuide_street_id",
                table: "TourGuide",
                column: "street_id");

            migrationBuilder.CreateIndex(
                name: "IX_TouristGroup_route_id",
                table: "TouristGroup",
                column: "route_id");

            migrationBuilder.CreateIndex(
                name: "IX_TouristGroup_tour_guide_id",
                table: "TouristGroup",
                column: "tour_guide_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Penalty");

            migrationBuilder.DropTable(
                name: "RoutesPopulatedPlaces");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Hotel");

            migrationBuilder.DropTable(
                name: "PopulatedPlace");

            migrationBuilder.DropTable(
                name: "TouristGroup");

            migrationBuilder.DropTable(
                name: "Route");

            migrationBuilder.DropTable(
                name: "TourGuide");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "Street");
        }
    }
}
