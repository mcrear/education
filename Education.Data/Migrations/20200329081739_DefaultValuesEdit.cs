using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Education.Data.Migrations
{
    public partial class DefaultValuesEdit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Users",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 3, 29, 11, 8, 54, 488, DateTimeKind.Local).AddTicks(2142));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Users",
                nullable: false,
                defaultValueSql: "newId()",
                oldClrType: typeof(Guid),
                oldDefaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Topics",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 3, 29, 11, 8, 54, 486, DateTimeKind.Local).AddTicks(3346));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Topics",
                nullable: false,
                defaultValueSql: "newId()",
                oldClrType: typeof(Guid),
                oldDefaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Schools",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 3, 29, 11, 8, 54, 484, DateTimeKind.Local).AddTicks(1891));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Schools",
                nullable: false,
                defaultValueSql: "newId()",
                oldClrType: typeof(Guid),
                oldDefaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Roles",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 3, 29, 11, 8, 54, 481, DateTimeKind.Local).AddTicks(9811));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Roles",
                nullable: false,
                defaultValueSql: "newId()",
                oldClrType: typeof(Guid),
                oldDefaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Questions",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 3, 29, 11, 8, 54, 479, DateTimeKind.Local).AddTicks(6117));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Questions",
                nullable: false,
                defaultValueSql: "newId()",
                oldClrType: typeof(Guid),
                oldDefaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Permissions",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 3, 29, 11, 8, 54, 477, DateTimeKind.Local).AddTicks(1926));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Permissions",
                nullable: false,
                defaultValueSql: "newId()",
                oldClrType: typeof(Guid),
                oldDefaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Lessons",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 3, 29, 11, 8, 54, 473, DateTimeKind.Local).AddTicks(2551));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Lessons",
                nullable: false,
                defaultValueSql: "newId()",
                oldClrType: typeof(Guid),
                oldDefaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Exams",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 3, 29, 11, 8, 54, 475, DateTimeKind.Local).AddTicks(2675));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Exams",
                nullable: false,
                defaultValueSql: "newId()",
                oldClrType: typeof(Guid),
                oldDefaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Classrooms",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 3, 29, 11, 8, 54, 471, DateTimeKind.Local).AddTicks(2885));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Classrooms",
                nullable: false,
                defaultValueSql: "newId()",
                oldClrType: typeof(Guid),
                oldDefaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Answers",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 3, 29, 11, 8, 54, 467, DateTimeKind.Local).AddTicks(2858));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Answers",
                nullable: false,
                defaultValueSql: "newId()",
                oldClrType: typeof(Guid),
                oldDefaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(2020, 3, 29, 11, 8, 54, 488, DateTimeKind.Local).AddTicks(2142),
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Users",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldDefaultValueSql: "newId()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Topics",
                nullable: false,
                defaultValue: new DateTime(2020, 3, 29, 11, 8, 54, 486, DateTimeKind.Local).AddTicks(3346),
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Topics",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldDefaultValueSql: "newId()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Schools",
                nullable: false,
                defaultValue: new DateTime(2020, 3, 29, 11, 8, 54, 484, DateTimeKind.Local).AddTicks(1891),
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Schools",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldDefaultValueSql: "newId()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Roles",
                nullable: false,
                defaultValue: new DateTime(2020, 3, 29, 11, 8, 54, 481, DateTimeKind.Local).AddTicks(9811),
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Roles",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldDefaultValueSql: "newId()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Questions",
                nullable: false,
                defaultValue: new DateTime(2020, 3, 29, 11, 8, 54, 479, DateTimeKind.Local).AddTicks(6117),
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Questions",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldDefaultValueSql: "newId()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Permissions",
                nullable: false,
                defaultValue: new DateTime(2020, 3, 29, 11, 8, 54, 477, DateTimeKind.Local).AddTicks(1926),
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Permissions",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldDefaultValueSql: "newId()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Lessons",
                nullable: false,
                defaultValue: new DateTime(2020, 3, 29, 11, 8, 54, 473, DateTimeKind.Local).AddTicks(2551),
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Lessons",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldDefaultValueSql: "newId()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Exams",
                nullable: false,
                defaultValue: new DateTime(2020, 3, 29, 11, 8, 54, 475, DateTimeKind.Local).AddTicks(2675),
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Exams",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldDefaultValueSql: "newId()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Classrooms",
                nullable: false,
                defaultValue: new DateTime(2020, 3, 29, 11, 8, 54, 471, DateTimeKind.Local).AddTicks(2885),
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Classrooms",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldDefaultValueSql: "newId()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Answers",
                nullable: false,
                defaultValue: new DateTime(2020, 3, 29, 11, 8, 54, 467, DateTimeKind.Local).AddTicks(2858),
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Answers",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldDefaultValueSql: "newId()");
        }
    }
}
