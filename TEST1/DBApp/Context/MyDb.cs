using DBApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DBApp.Context;

public class MyDb : DbContext
{
    public MyDb()
    {
    }

    public MyDb(DbContext context)
    {
    }
    
    public MyDb(DbContextOptions options)
        : base(options)
    {
    }

    //    public DbSet<ClassName> name;
    public DbSet<BoatStandard> BoatStandards;
    public DbSet<Sailboat> Sailboats;
    public DbSet<Reservation> Reservations;
    public DbSet<Sailboat_Reservation> SailboatReservations;
    public DbSet<Client> Clients;
    public DbSet<ClientCategory> ClientCategories;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BoatStandard>(e =>
            {
                e.HasKey(k => k.IdBoatStandard);
                e.Property(p => p.Name).HasMaxLength(100).IsRequired();
                e.Property(p => p.Level).IsRequired();
                e.HasMany(p => p.Sailboats).WithOne(p => p.BoatStandard)
                    .HasForeignKey(p => p.IdBoatStandard).OnDelete(DeleteBehavior.Restrict);;
                e.HasMany(p => p.Reservations).WithOne(p => p.BoatStandard)
                    .HasForeignKey(p => p.IdBoatStandard).OnDelete(DeleteBehavior.Restrict);;
            }
        );
        modelBuilder.Entity<Sailboat>(e =>
            {
                e.HasKey(k => k.IdSailboat);
                e.Property(p => p.Name).HasMaxLength(100).IsRequired();
                e.Property(p => p.Capacity).IsRequired();
                e.Property(p => p.Description).HasMaxLength(100).IsRequired();
                e.HasMany(p => p.Reservations).WithMany(e => e.Sailboats)
                    .UsingEntity<Sailboat_Reservation>(
                    l => l.HasOne<Reservation>().WithMany().HasForeignKey(e => e.IdReservation),
                    l => l.HasOne<Sailboat>().WithMany().HasForeignKey(e => e.IdSailboat));
                e.Property(p => p.Price);
            }
        );
        modelBuilder.Entity<Reservation>(e =>
            {
                e.HasKey(k => k.IdReservation);
                
                e.Property(p => p.DateFrom).IsRequired();
                e.Property(p => p.DateTo).IsRequired();

                e.Property(p => p.Capacity).IsRequired();
                e.Property(p => p.Fulfilled).IsRequired();
                e.Property(p => p.Price);
                e.Property(p => p.CancelReason).HasMaxLength(200);
            }
        );
        modelBuilder.Entity<ClientCategory>(e =>
            {
                e.HasKey(k => k.IdClientCategory);
                e.Property(p => p.Name).HasMaxLength(100).IsRequired();
                e.Property(p => p.DiscountPerc).IsRequired();
                e.HasMany(p => p.Clients).WithOne(p => p.ClientCategory)
                    .HasForeignKey(p => p.IdClientCategory);
            }
        );
        modelBuilder.Entity<Client>(e =>
            {
                e.HasKey(k => k.IdClient);
                e.Property(p => p.Name).HasMaxLength(100).IsRequired();
                e.Property(p => p.LastName).HasMaxLength(100).IsRequired();
                e.Property(p => p.Birthday).IsRequired();
                e.Property(p => p.Pesel).HasMaxLength(100).IsRequired();
                e.Property(p => p.Email).HasMaxLength(100).IsRequired();
            }
        );
        
        /*
        modelBuilder.Entity<Medicament>(m =>
        {
            m.HasKey(et => et.IdMedicament);
            m.Property(p => p.Name).HasMaxLength(100).IsRequired();
            m.Property(p => p.Description).HasMaxLength(100).IsRequired();
            m.Property(p => p.Type).HasMaxLength(100).IsRequired();
        }
        );
        modelBuilder.Entity<Patient>(m =>
            {
                m.HasKey(et => et.IdPatient);
                m.Property(p => p.FirstName).HasMaxLength(100).IsRequired();
                m.Property(p => p.LastName).HasMaxLength(100).IsRequired();
                m.Property(p => p.Birthdate).IsRequired();
                m.HasMany(p => p.Prescriptions).WithOne(p => p.Patient).HasForeignKey(p => p.IdPatient);
            }
        );
        modelBuilder.Entity<Doctor>(m =>
            {
                m.HasKey(et => et.IdDoctor);
                m.Property(p => p.FirstName).HasMaxLength(100).IsRequired();
                m.Property(p => p.LastName).HasMaxLength(100).IsRequired();
                m.Property(p => p.Email).HasMaxLength(100).IsRequired();
                m.HasMany(p => p.Prescriptions).WithOne(p => p.Doctor).HasForeignKey(p => p.IdDoctor);
            }
        );
        modelBuilder.Entity<Prescription>(m =>
            {
                m.HasKey(et => et.IdPrecription);
                m.Property(p => p.Date).IsRequired();
                m.Property(p => p.DueDate).IsRequired();
            }
        );
        modelBuilder.Entity<Prescription_Medicament>(m =>
            {
                m.HasKey(et => et.IdMedicament);
                m.HasKey(et => et.IdPrescription);
                m.Property(p => p.Dose);
                m.Property(p => p.Details).HasMaxLength(100).IsRequired();
            }
        );
        
        

Connection string

Data Source=db-mssql;Initial Catalog= s24651;Integrated Security=True;TrustServerCertificate=True



Komendy do EF

 cd ProjectName

 dotnet new tool-manifest
 dotnet tool install dotnet-ef --version 8.0.0
 dotnet ef migrations add InitialMigration
 dotnet ef database update
 dotnet ef migrations remove



EF w APBD



New project webappi







lib packages:

entity framework core

entity framework core.SQL Server

entity framework tools



dir Models



dir Context



(robić kawałkami, sprawdzać po każdej klasie czy działa)



Model:

class X (np. Animal) {

pola

ICollection<Owner> owners - tak się dodaje wiele do wielu

}



Context:

class X_Db : DbContext

{

ctor X_Db()



ctor X_Db(DbContext context)



DbSet<Animal> animals {get; set;}



override

OnModelCreating (ModelBuilder builder) {

builder,Entity<Animal> (e =>

{

e.hasKey(et => et.IdAnimal)

e.property(p => p.name)

//okreslanie relaci

e.HasOne(p => p.Category).Withmany(p => p.Animal).HasForeignKey(p => p.IdCategory)

}

}



override

OnConfigurate() {

connection string

}


        */
    }
}