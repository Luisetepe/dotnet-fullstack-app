using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Template.Application.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "app_routes",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(50)", nullable: false),
                    name = table.Column<string>(type: "varchar(50)", nullable: false),
                    path = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_app_routes", x => x.id);
                }
            );

            migrationBuilder.CreateTable(
                name: "locations",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(50)", nullable: false),
                    name = table.Column<string>(type: "varchar(100)", nullable: false),
                    longitude = table.Column<decimal>(type: "numeric", nullable: false),
                    latitude = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_locations", x => x.id);
                }
            );

            migrationBuilder.CreateTable(
                name: "plant_statuses",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(50)", nullable: false),
                    name = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_plant_statuses", x => x.id);
                }
            );

            migrationBuilder.CreateTable(
                name: "plant_types",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(50)", nullable: false),
                    name = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_plant_types", x => x.id);
                }
            );

            migrationBuilder.CreateTable(
                name: "portfolios",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(50)", nullable: false),
                    name = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_portfolios", x => x.id);
                }
            );

            migrationBuilder.CreateTable(
                name: "resource_types",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(50)", nullable: false),
                    name = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_resource_types", x => x.id);
                }
            );

            migrationBuilder.CreateTable(
                name: "user_roles",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(50)", nullable: false),
                    name = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_roles", x => x.id);
                }
            );

            migrationBuilder.CreateTable(
                name: "plants",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(50)", nullable: false),
                    name = table.Column<string>(type: "varchar(100)", nullable: false),
                    plant_id = table.Column<string>(type: "varchar(20)", nullable: false),
                    capacity_dc = table.Column<decimal>(type: "numeric", nullable: false),
                    capacity_ac = table.Column<decimal>(type: "numeric", nullable: false),
                    storage_capacity = table.Column<decimal>(type: "numeric", nullable: false),
                    project_company = table.Column<string>(type: "varchar(100)", nullable: false),
                    utility_company = table.Column<string>(type: "varchar(100)", nullable: false),
                    voltage = table.Column<int>(type: "integer", nullable: false),
                    asset_manager = table.Column<string>(type: "varchar(100)", nullable: false),
                    tags = table.Column<string>(type: "varchar(100)", nullable: false),
                    notes = table.Column<string>(type: "varchar(500)", nullable: false),
                    plant_type_id = table.Column<string>(type: "varchar(50)", nullable: false),
                    resource_type_id = table.Column<string>(type: "varchar(50)", nullable: false),
                    status_id = table.Column<string>(type: "varchar(50)", nullable: false),
                    location_id = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_plants", x => x.id);
                    table.ForeignKey(
                        name: "fk_plants_locations_location_id",
                        column: x => x.location_id,
                        principalTable: "locations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict
                    );
                    table.ForeignKey(
                        name: "fk_plants_plant_statuses_status_id",
                        column: x => x.status_id,
                        principalTable: "plant_statuses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict
                    );
                    table.ForeignKey(
                        name: "fk_plants_plant_types_plant_type_id",
                        column: x => x.plant_type_id,
                        principalTable: "plant_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict
                    );
                    table.ForeignKey(
                        name: "fk_plants_resource_types_resource_type_id",
                        column: x => x.resource_type_id,
                        principalTable: "resource_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "app_route_user_role",
                columns: table => new
                {
                    role_routes_id = table.Column<string>(type: "varchar(50)", nullable: false),
                    user_roles_id = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_app_route_user_role", x => new { x.role_routes_id, x.user_roles_id });
                    table.ForeignKey(
                        name: "fk_app_route_user_role_app_routes_role_routes_id",
                        column: x => x.role_routes_id,
                        principalTable: "app_routes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "fk_app_route_user_role_user_roles_user_roles_id",
                        column: x => x.user_roles_id,
                        principalTable: "user_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(50)", nullable: false),
                    user_name = table.Column<string>(type: "varchar(100)", nullable: false),
                    email = table.Column<string>(type: "varchar(100)", nullable: false),
                    password_hash = table.Column<string>(type: "varchar(50)", nullable: false),
                    password_salt = table.Column<string>(type: "varchar(50)", nullable: false),
                    role_id = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                    table.ForeignKey(
                        name: "fk_users_user_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "user_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "plant_portfolio",
                columns: table => new
                {
                    plants_id = table.Column<string>(type: "varchar(50)", nullable: false),
                    portfolios_id = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_plant_portfolio", x => new { x.plants_id, x.portfolios_id });
                    table.ForeignKey(
                        name: "fk_plant_portfolio_plants_plants_id",
                        column: x => x.plants_id,
                        principalTable: "plants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "fk_plant_portfolio_portfolios_portfolios_id",
                        column: x => x.portfolios_id,
                        principalTable: "portfolios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "user_sessions",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(50)", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    expires_at = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    user_id = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_sessions", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_sessions_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "ix_app_route_user_role_user_roles_id",
                table: "app_route_user_role",
                column: "user_roles_id"
            );

            migrationBuilder.CreateIndex(name: "ix_app_routes_path", table: "app_routes", column: "path", unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_plant_portfolio_portfolios_id",
                table: "plant_portfolio",
                column: "portfolios_id"
            );

            migrationBuilder.CreateIndex(name: "ix_plants_location_id", table: "plants", column: "location_id");

            migrationBuilder.CreateIndex(name: "ix_plants_name", table: "plants", column: "name", unique: true);

            migrationBuilder.CreateIndex(name: "ix_plants_plant_type_id", table: "plants", column: "plant_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_plants_resource_type_id",
                table: "plants",
                column: "resource_type_id"
            );

            migrationBuilder.CreateIndex(name: "ix_plants_status_id", table: "plants", column: "status_id");

            migrationBuilder.CreateIndex(name: "ix_user_roles_name", table: "user_roles", column: "name", unique: true);

            migrationBuilder.CreateIndex(name: "ix_user_sessions_user_id", table: "user_sessions", column: "user_id");

            migrationBuilder.CreateIndex(name: "ix_users_email", table: "users", column: "email", unique: true);

            migrationBuilder.CreateIndex(name: "ix_users_role_id", table: "users", column: "role_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "app_route_user_role");

            migrationBuilder.DropTable(name: "plant_portfolio");

            migrationBuilder.DropTable(name: "user_sessions");

            migrationBuilder.DropTable(name: "app_routes");

            migrationBuilder.DropTable(name: "plants");

            migrationBuilder.DropTable(name: "portfolios");

            migrationBuilder.DropTable(name: "users");

            migrationBuilder.DropTable(name: "locations");

            migrationBuilder.DropTable(name: "plant_statuses");

            migrationBuilder.DropTable(name: "plant_types");

            migrationBuilder.DropTable(name: "resource_types");

            migrationBuilder.DropTable(name: "user_roles");
        }
    }
}
