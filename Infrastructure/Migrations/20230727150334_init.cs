using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApiLog",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestURL = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    IPAddress = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    RequestByURL = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    RequestBody = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Response = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponseStatusCode = table.Column<int>(type: "int", nullable: false),
                    RequestAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResponseAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiLog", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "EmailTemplates",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    Subject = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    Body = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplates", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Entity",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entity", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ExceptionLog",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntityID = table.Column<int>(type: "int", nullable: false),
                    Method = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    JSON = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestUrl = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    RequestJSON = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Exception = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExceptionLog", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Gender",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gender", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "State",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Code = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_State", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SystemSettings",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SettingName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    SettingKey = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    SettingValue = table.Column<string>(type: "varchar(1000)", unicode: false, maxLength: 1000, nullable: false),
                    SettingCategory = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Label = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemSettings", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    Password = table.Column<string>(type: "varchar(2000)", unicode: false, maxLength: 2000, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    PasswordRequestHash = table.Column<string>(type: "varchar(1500)", unicode: false, maxLength: 1500, nullable: true),
                    PasswordRequestDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "File",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fk_EntityID = table.Column<int>(type: "int", nullable: false),
                    RecordID = table.Column<int>(type: "int", nullable: false),
                    fileURL = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    fileType = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    fileSize = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_File", x => x.ID);
                    table.ForeignKey(
                        name: "FK_File_Entity",
                        column: x => x.fk_EntityID,
                        principalTable: "Entity",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "UserToRole",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fk_UserID = table.Column<int>(type: "int", nullable: false),
                    fk_RoleID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToRole", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserToRole_Role",
                        column: x => x.fk_RoleID,
                        principalTable: "Role",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_UserToRole_User",
                        column: x => x.fk_UserID,
                        principalTable: "User",
                        principalColumn: "ID");
                });

            migrationBuilder.InsertData(
                table: "EmailTemplates",
                columns: new[] { "ID", "Active", "Body", "Description", "Name", "Subject" },
                values: new object[] { 1, true, "<!DOCTYPE html>  <html xmlns='http://www.w3.org/1999/xhtml'>  <head>      <link href='https://fonts.googleapis.com/css2?family=Roboto&display=swap'  rel='stylesheet' />      <style type='text/css'>            @font-face {              font-family: Roboto;              src: url('Roboto');               src: url('Roboto-webfont.eot?#iefix') format('embedded-opentype'), url('Roboto-webfont.woff')  format('woff'), url('Roboto-webfont.ttf') format('truetype'), url('Roboto-webfont.svg#Sri-TSCRegular') format('svg');     font-weight: normal;              font-style: normal;          }            body {              font-family: Roboto, serif;    font-size: 12px;              font-style: normal;              font-weight: 400;              padding: 0;      text-align: center;              background: #f6f6f6;          }            .bigscreen1 {              padding-bottom: 20px;    border-left: 40px solid #006aff;              border-right: 40px solid #006aff;          }            .bigscreen {     padding-bottom: 20px;              border-left: 40px solid #006aff;              border-right: 40px solid #006aff;      border-top: 30px solid #006aff;          }          .innertablewidth {              width: 60%;          }    @media only screen and (max-width: 600px) {              .bigscreen {                  padding-bottom: 20px;     border-left: 0;                  border-right: 0;                  border-top: 0;              }                .bigscreen1 {    padding-bottom: 20px;                  border-left: 0;                  border-right: 0;              }    .innertablewidth {                  width: 90%;              }          }      </style>      <meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />      <meta name='viewport' content='width=device-width, initial-scale=1.0' />  </head>  <body>    <table border='0' cellpadding='0' cellspacing='0' width='100%'>            <tr>              <td style='padding-bottom: 30px; padding-top: 10px; background: #f6f6f6'>                   <table class='innertablewidth' align='center' border='0' cellpadding='0' cellspacing='0'>                 <tr>                            <td style='padding-top:10px; background-color: #006aff'></td>              </tr>                      <tr>                              <td align='center' bgcolor='#ffffff' class='bigscreen' style='padding-top:30px;'>      <P style='padding-top:5px; padding-left: 10px;padding-right: 10px;width: 100%;max-width: 255px;font-size: 25px;'>   <b>Medteq</b> </p>                           </td>                        </tr>                      <tr>       <td align='center' bgcolor='#ffffff' class='bigscreen1' style='color: #006aff; font-weight: bold; font-size: 24px;  line-height: 29px; padding-top: 15px;'>                                Hi [First Name]                           </td>                      </tr>                      <tr>                            <td bgcolor='#ffffff' style='padding-bottom: 30px; padding-right: 40px; padding-left: 40px;'>   <table border='0' cellpadding='0' cellspacing='0' width='100%'>                                  <tr>      <td style='padding-top:20px; font-size: 16px; line-height: 23px; color: #000000; text-align:left;'>          We’ve received a request to reset your password, if you did not submit this request please ignore this email.   Click on the link below to reset your password..<br/> <b><a href='[URL]'</a>Click Here</b>      </td>                                  </tr>                                    <tr>       <td style='padding-top:20px; color: #000; font-weight: 500; font-size: 16px;line-height:19px; text-align:left;'>   </td>                                  </tr>                                   <tr>                 <td align='center' style='padding-top:30px; font-weight: 400; letter-spacing: 0px; font-size: 32px; line-height: 38px;  color: #006aff;'>                                            Have a great day,                                      </td>     </tr>                                  <tr>                                         <td align='center' style='padding-bottom:15px; font-weight: 900; letter-spacing: 0px; font-size: 32px; line-height: 38px; color: #006aff;'>                                            The Medteq                                      </td>                                  </tr>                              </table>                          </td>                      </tr>                  </table>              </td>            </tr>      </table>  </body></html>", "Reset Password Email", "Reset_Password_Email", "Reset Password Email" });

            migrationBuilder.InsertData(
                table: "Gender",
                columns: new[] { "ID", "Active", "Value" },
                values: new object[,]
                {
                    { 1, true, "Male" },
                    { 2, true, "Female" },
                    { 3, true, "Other" }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "ID", "Active", "RoleName" },
                values: new object[] { 1, true, "Admin" });

            migrationBuilder.InsertData(
                table: "State",
                columns: new[] { "ID", "Active", "Code", "Name" },
                values: new object[,]
                {
                    { 1, true, "AL", "Alabama" },
                    { 2, true, "AK", "Alaska" },
                    { 3, true, "AZ", "Arizona" },
                    { 4, true, "AR", "Arkansas" },
                    { 5, true, "CA", "California" },
                    { 6, true, "CO", "Colorado" },
                    { 7, true, "CT", "Connecticut" },
                    { 8, true, "DE", "Delaware" },
                    { 9, true, "FL", "Florida" },
                    { 10, true, "GA", "Georgia" },
                    { 11, true, "HI", "Hawaii" },
                    { 12, true, "ID", "Idaho" },
                    { 13, true, "IL", "Illinois" },
                    { 14, true, "IN", "Indiana" },
                    { 15, true, "IA", "Iowa" },
                    { 16, true, "KS", "Kansas" },
                    { 17, true, "KY", "Kentucky" },
                    { 18, true, "LA", "Louisiana" },
                    { 19, true, "ME", "Maine" },
                    { 20, true, "MD", "Maryland" },
                    { 21, true, "MA", "Massachusetts" },
                    { 22, true, "MI", "Michigan" },
                    { 23, true, "MN", "Minnesota" },
                    { 24, true, "MS", "Mississippi" },
                    { 25, true, "MO", "Missouri" },
                    { 26, true, "MT", "Montana" },
                    { 27, true, "NE", "Nebraska" },
                    { 28, true, "NV", "Nevada" },
                    { 29, true, "NH", "New Hampshire" },
                    { 30, true, "NJ", "New Jersey" },
                    { 31, true, "NM", "New Mexico" },
                    { 32, true, "NY", "New York" },
                    { 33, true, "NC", "North Carolina" },
                    { 34, true, "ND", "North Dakota" },
                    { 35, true, "OH", "Ohio" },
                    { 36, true, "OK", "Oklahoma" },
                    { 37, true, "OR", "Oregon" }
                });

            migrationBuilder.InsertData(
                table: "State",
                columns: new[] { "ID", "Active", "Code", "Name" },
                values: new object[,]
                {
                    { 38, true, "PA", "Pennsylvania[" },
                    { 39, true, "RI", "Rhode Island" },
                    { 40, true, "SC", "South Carolina" },
                    { 41, true, "SD", "South Dakota" },
                    { 42, true, "TN", "Tennessee" },
                    { 43, true, "TX", "Texas" },
                    { 44, true, "UT", "Utah" },
                    { 45, true, "VT", "Vermont" },
                    { 46, true, "VA", "Virginia" },
                    { 47, true, "WA", "Washington" },
                    { 48, true, "WV", "West Virginia" },
                    { 49, true, "WI", "Wisconsin" },
                    { 50, true, "WY", "Wyoming" }
                });

            migrationBuilder.InsertData(
                table: "SystemSettings",
                columns: new[] { "ID", "Active", "Label", "SettingCategory", "SettingKey", "SettingName", "SettingValue" },
                values: new object[,]
                {
                    { 1, true, "From Mail", "EmailSetting", "NotificationFromMailAddress", "FromMail", "medteqreporteq@gmail.com" },
                    { 2, true, "Smtp Client", "EmailSetting", "SMTPClient", "SmtpClient", "smtp.gmail.com" },
                    { 3, true, "Smtp Port", "EmailSetting", "Smtp Port", "SmtpPort", "587" },
                    { 4, true, "Smtp User", "EmailSetting", "Smtp User Name", "SmtpUser", "medteqreporteq@gmail.com" },
                    { 5, true, "Smtp Password", "EmailSetting", "Smtp Password", "SmtpPassword", "medteqreporteq@1" },
                    { 6, true, "URL Expiry Time", "GeneralSetting", "Expiry Time", "URLExpiryTime", "32" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "ID", "Active", "CreatedBy", "CreatedDate", "Email", "FirstName", "IsDeleted", "LastName", "ModifiedBy", "ModifiedDate", "Password", "PasswordRequestDate", "PasswordRequestHash" },
                values: new object[] { 1, true, 0, new DateTime(2023, 6, 1, 11, 23, 53, 353, DateTimeKind.Utc).AddTicks(7581), "admin@mail.com", "Adam", false, "Admin", 0, new DateTime(2023, 6, 1, 11, 23, 53, 353, DateTimeKind.Utc).AddTicks(7581), "$2a$11$..uxlbXXP.MHHwioGquxQeOIEtsCcG.yXFuJ.oRoj3Q2m7rwoTMlG", null, null });

            migrationBuilder.InsertData(
                table: "UserToRole",
                columns: new[] { "ID", "fk_RoleID", "fk_UserID" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_File_fk_EntityID",
                table: "File",
                column: "fk_EntityID");

            migrationBuilder.CreateIndex(
                name: "IX_UserToRole_fk_RoleID",
                table: "UserToRole",
                column: "fk_RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_UserToRole_fk_UserID",
                table: "UserToRole",
                column: "fk_UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiLog");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "EmailTemplates");

            migrationBuilder.DropTable(
                name: "ExceptionLog");

            migrationBuilder.DropTable(
                name: "File");

            migrationBuilder.DropTable(
                name: "Gender");

            migrationBuilder.DropTable(
                name: "State");

            migrationBuilder.DropTable(
                name: "SystemSettings");

            migrationBuilder.DropTable(
                name: "UserToRole");

            migrationBuilder.DropTable(
                name: "Entity");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
