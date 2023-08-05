using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleBlazorGrid.EntityFramework.Sandbox.Migrations
{
    /// <inheritdoc />
    public partial class Data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                insert into customers (CustomerId, CompanyName, ContactName, ContactTitle, Address, City, Region, PostalCode, Country, Phone, Fax)
                values 
                ('A0001', 'Grey Gravel Co.', 'Gray Mann', 'Mr', '317 Industry Way', 'Dallas', 'Southern US', '87876', 'United States of America', '+1 555 8978', '+1 555 9900'),
                ('A0002', 'Builders League United', 'Blutarch Mann', 'Mr', '10090 Alliance Warehouse', 'Fort Worth', 'Southern US', '84454', 'United States of America', '+1 555 5566', '+1 555 5285'),
                ('A0003', 'Reliable Excavation Demolition', 'Redmund Mann', 'Mr', 'Unit 5, Customs House', 'New York', 'Eastern US', '15484', 'United States of America', '+1 555 7741', '+1 555 1478'),
                ('B0011', 'Mann Co.', 'Saxton Hale', 'Mr', 'Mann Hedquarters', 'New Mexico', 'Southern US', '45884', 'United States of America', '+1 555 2002', '+1 555 2003'),
                ('B0012', 'Gold Medal Dynamites', 'Athena Pauling', 'Miss', 'Unit 7, Coventry Way', 'London', 'Greater London', 'SE11AA', 'United Kingdom', '+44 020 5543', '+44 020 0989'),
                ('B0013', 'Goldbloom Tropical Flora Export', 'Chell Gutiérrez', 'Miss', '67651 Export House', 'Oslo', 'Oslo', '09090911', 'Norway', '+47 111 1312', '+47 111 1313');");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
