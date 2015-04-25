namespace Proyecto2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial2013175 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CuentaAhorroes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NombreCuenta = c.String(maxLength: 7),
                        Saldo = c.Int(nullable: false),
                        PersonaDpi = c.String(maxLength: 13),
                        Users_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Personas", t => t.PersonaDpi)
                .ForeignKey("dbo.AspNetUsers", t => t.Users_Id)
                .Index(t => t.PersonaDpi)
                .Index(t => t.Users_Id);
            
            CreateTable(
                "dbo.AbonoAhorroes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CuentaAhorroSaldo = c.Int(nullable: false),
                        CuentaAhorroId = c.Int(nullable: false),
                        Descripcion = c.String(maxLength: 10),
                        Monto = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CuentaAhorroes", t => t.CuentaAhorroId, cascadeDelete: true)
                .Index(t => t.CuentaAhorroId);
            
            CreateTable(
                "dbo.Personas",
                c => new
                    {
                        Dpi = c.String(nullable: false, maxLength: 13),
                        Nombre = c.String(maxLength: 60),
                        UserName = c.String(),
                        Email = c.String(nullable: false),
                        Apellido = c.String(maxLength: 60),
                        Direccion = c.String(maxLength: 200),
                        Telefono = c.String(maxLength: 10),
                        Referencia1 = c.String(maxLength: 100),
                        TelefonoReferencia1 = c.String(maxLength: 10),
                        Referencia2 = c.String(maxLength: 100),
                        TelefonoReferencia2 = c.String(maxLength: 12),
                    })
                .PrimaryKey(t => t.Dpi);
            
            CreateTable(
                "dbo.Cuentas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        nombreCuenta = c.String(maxLength: 10),
                        Tipo = c.Int(),
                        saldo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PersonaDpi = c.String(maxLength: 13),
                        Users_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Personas", t => t.PersonaDpi)
                .ForeignKey("dbo.AspNetUsers", t => t.Users_Id)
                .Index(t => t.PersonaDpi)
                .Index(t => t.Users_Id);
            
            CreateTable(
                "dbo.Prestamoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CuentaMonetariaSaldo = c.Int(nullable: false),
                        CuentaMonetariaId = c.Int(nullable: false),
                        Descripcion = c.String(maxLength: 10),
                        Monto = c.Int(nullable: false),
                        Cuenta_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CuentaMonetarias", t => t.CuentaMonetariaId, cascadeDelete: true)
                .ForeignKey("dbo.Cuentas", t => t.Cuenta_Id)
                .Index(t => t.CuentaMonetariaId)
                .Index(t => t.Cuenta_Id);
            
            CreateTable(
                "dbo.CuentaMonetarias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NombreCuenta = c.String(maxLength: 10),
                        Saldo = c.Int(nullable: false),
                        PersonaDpi = c.String(maxLength: 13),
                        Users_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Personas", t => t.PersonaDpi)
                .ForeignKey("dbo.AspNetUsers", t => t.Users_Id)
                .Index(t => t.PersonaDpi)
                .Index(t => t.Users_Id);
            
            CreateTable(
                "dbo.TarjetaDebitoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NumeroTarjeta = c.String(),
                        Estado = c.Int(),
                        Credito = c.Int(nullable: false),
                        CuentaMonetariaSaldo = c.Int(nullable: false),
                        CuentaMonetariaId = c.Int(nullable: false),
                        Pin = c.String(maxLength: 10),
                        Cuenta_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CuentaMonetarias", t => t.CuentaMonetariaId, cascadeDelete: true)
                .ForeignKey("dbo.Cuentas", t => t.Cuenta_Id)
                .Index(t => t.CuentaMonetariaId)
                .Index(t => t.Cuenta_Id);
            
            CreateTable(
                "dbo.AbonoDebitoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TarjetaDebitoId = c.Int(nullable: false),
                        TarjetaDebitoCredito = c.Int(nullable: false),
                        Descripcion = c.String(maxLength: 10),
                        Monto = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TarjetaDebitoes", t => t.TarjetaDebitoId, cascadeDelete: true)
                .Index(t => t.TarjetaDebitoId);
            
            CreateTable(
                "dbo.RetiroDebitoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TarjetaDebitoId = c.Int(nullable: false),
                        TarjetaDebitoCredito = c.Int(nullable: false),
                        Descripcion = c.String(maxLength: 10),
                        Monto = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TarjetaDebitoes", t => t.TarjetaDebitoId, cascadeDelete: true)
                .Index(t => t.TarjetaDebitoId);
            
            CreateTable(
                "dbo.TarjetaCreditoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NumeroTarjeta = c.String(),
                        Estado = c.Int(),
                        Credito = c.Int(nullable: false),
                        CuentaAhorroSaldo = c.Int(nullable: false),
                        CuentaMonetariaSaldo = c.Int(nullable: false),
                        CuentaMonetariaId = c.Int(nullable: false),
                        Pin = c.String(maxLength: 10),
                        Cuenta_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CuentaMonetarias", t => t.CuentaMonetariaId, cascadeDelete: true)
                .ForeignKey("dbo.Cuentas", t => t.Cuenta_Id)
                .Index(t => t.CuentaMonetariaId)
                .Index(t => t.Cuenta_Id);
            
            CreateTable(
                "dbo.CargoCreditoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CuentaMonetariaSaldo = c.Int(nullable: false),
                        CuentaMonetariaId = c.Int(nullable: false),
                        Descripcion = c.String(maxLength: 10),
                        Monto = c.Int(nullable: false),
                        TarjetaCredito_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CuentaMonetarias", t => t.CuentaMonetariaId, cascadeDelete: true)
                .ForeignKey("dbo.TarjetaCreditoes", t => t.TarjetaCredito_Id)
                .Index(t => t.CuentaMonetariaId)
                .Index(t => t.TarjetaCredito_Id);
            
            CreateTable(
                "dbo.PagoCreditoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TarjetaCreditoCredito = c.Int(nullable: false),
                        TarjetaCreditoId = c.Int(nullable: false),
                        Descripcion = c.String(maxLength: 10),
                        Monto = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TarjetaCreditoes", t => t.TarjetaCreditoId, cascadeDelete: true)
                .Index(t => t.TarjetaCreditoId);
            
            CreateTable(
                "dbo.RetiroAhorroes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CuentaAhorroId = c.Int(nullable: false),
                        CuentaAhorroSaldo = c.Int(nullable: false),
                        Descripcion = c.String(maxLength: 10),
                        Monto = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CuentaAhorroes", t => t.CuentaAhorroId, cascadeDelete: true)
                .Index(t => t.CuentaAhorroId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PersonaDpi = c.String(maxLength: 13),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Personas", t => t.PersonaDpi)
                .Index(t => t.PersonaDpi);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.LoginProvider, t.ProviderKey })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "PersonaDpi", "dbo.Personas");
            DropForeignKey("dbo.CuentaMonetarias", "Users_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.CuentaAhorroes", "Users_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Cuentas", "Users_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.RetiroAhorroes", "CuentaAhorroId", "dbo.CuentaAhorroes");
            DropForeignKey("dbo.CuentaAhorroes", "PersonaDpi", "dbo.Personas");
            DropForeignKey("dbo.TarjetaCreditoes", "Cuenta_Id", "dbo.Cuentas");
            DropForeignKey("dbo.TarjetaDebitoes", "Cuenta_Id", "dbo.Cuentas");
            DropForeignKey("dbo.Prestamoes", "Cuenta_Id", "dbo.Cuentas");
            DropForeignKey("dbo.PagoCreditoes", "TarjetaCreditoId", "dbo.TarjetaCreditoes");
            DropForeignKey("dbo.TarjetaCreditoes", "CuentaMonetariaId", "dbo.CuentaMonetarias");
            DropForeignKey("dbo.CargoCreditoes", "TarjetaCredito_Id", "dbo.TarjetaCreditoes");
            DropForeignKey("dbo.CargoCreditoes", "CuentaMonetariaId", "dbo.CuentaMonetarias");
            DropForeignKey("dbo.RetiroDebitoes", "TarjetaDebitoId", "dbo.TarjetaDebitoes");
            DropForeignKey("dbo.TarjetaDebitoes", "CuentaMonetariaId", "dbo.CuentaMonetarias");
            DropForeignKey("dbo.AbonoDebitoes", "TarjetaDebitoId", "dbo.TarjetaDebitoes");
            DropForeignKey("dbo.Prestamoes", "CuentaMonetariaId", "dbo.CuentaMonetarias");
            DropForeignKey("dbo.CuentaMonetarias", "PersonaDpi", "dbo.Personas");
            DropForeignKey("dbo.Cuentas", "PersonaDpi", "dbo.Personas");
            DropForeignKey("dbo.AbonoAhorroes", "CuentaAhorroId", "dbo.CuentaAhorroes");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "User_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "PersonaDpi" });
            DropIndex("dbo.RetiroAhorroes", new[] { "CuentaAhorroId" });
            DropIndex("dbo.PagoCreditoes", new[] { "TarjetaCreditoId" });
            DropIndex("dbo.CargoCreditoes", new[] { "TarjetaCredito_Id" });
            DropIndex("dbo.CargoCreditoes", new[] { "CuentaMonetariaId" });
            DropIndex("dbo.TarjetaCreditoes", new[] { "Cuenta_Id" });
            DropIndex("dbo.TarjetaCreditoes", new[] { "CuentaMonetariaId" });
            DropIndex("dbo.RetiroDebitoes", new[] { "TarjetaDebitoId" });
            DropIndex("dbo.AbonoDebitoes", new[] { "TarjetaDebitoId" });
            DropIndex("dbo.TarjetaDebitoes", new[] { "Cuenta_Id" });
            DropIndex("dbo.TarjetaDebitoes", new[] { "CuentaMonetariaId" });
            DropIndex("dbo.CuentaMonetarias", new[] { "Users_Id" });
            DropIndex("dbo.CuentaMonetarias", new[] { "PersonaDpi" });
            DropIndex("dbo.Prestamoes", new[] { "Cuenta_Id" });
            DropIndex("dbo.Prestamoes", new[] { "CuentaMonetariaId" });
            DropIndex("dbo.Cuentas", new[] { "Users_Id" });
            DropIndex("dbo.Cuentas", new[] { "PersonaDpi" });
            DropIndex("dbo.AbonoAhorroes", new[] { "CuentaAhorroId" });
            DropIndex("dbo.CuentaAhorroes", new[] { "Users_Id" });
            DropIndex("dbo.CuentaAhorroes", new[] { "PersonaDpi" });
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.RetiroAhorroes");
            DropTable("dbo.PagoCreditoes");
            DropTable("dbo.CargoCreditoes");
            DropTable("dbo.TarjetaCreditoes");
            DropTable("dbo.RetiroDebitoes");
            DropTable("dbo.AbonoDebitoes");
            DropTable("dbo.TarjetaDebitoes");
            DropTable("dbo.CuentaMonetarias");
            DropTable("dbo.Prestamoes");
            DropTable("dbo.Cuentas");
            DropTable("dbo.Personas");
            DropTable("dbo.AbonoAhorroes");
            DropTable("dbo.CuentaAhorroes");
        }
    }
}
