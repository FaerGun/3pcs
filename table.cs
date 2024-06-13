using System.Numerics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
public class AppDbContext : DbContext
{
    public DbSet<Department> Departments { get; set; } = null!;
    public DbSet<Subject> Subjects { get; set; } = null!;
    public DbSet<Enrollee> Enrollees { get; set; } = null!;
    public DbSet<Achievement> Achievements { get; set; } = null!;
    public DbSet<Programm> Programms { get; set; } = null!;
    public DbSet<Enrollee_Achievement> Enrollee_Achievements { get; set; } = null!;
    public DbSet<Enrollee_Subject> Enrollee_Subjects { get; set; } = null!;
    public DbSet<Pogramm_Enrollee> Pogramm_Enrollees { get; set; } = null!;
    public DbSet<Programm_Subject> Programm_Subjects { get; set; } = null!;
    public AppDbContext()
    {
        Database.EnsureCreated();

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>()
            .HasKey(t => t.ID);
        modelBuilder.Entity<Subject>()
            .HasKey(t => t.ID);
        modelBuilder.Entity<Enrollee>()
            .HasKey(t => t.ID);
        modelBuilder.Entity<Achievement>()
            .HasKey(t => t.ID);
        modelBuilder.Entity<Programm>()
            .HasKey(t => t.ID);
        modelBuilder.Entity<Enrollee_Achievement>()
            .HasKey(t => t.ID);
        modelBuilder.Entity<Enrollee_Subject>()
            .HasKey(t => t.ID);
        modelBuilder.Entity<Programm_Enrollee>()
            .HasKey(t => t.ID);
        modelBuilder.Entity<Programm_Subject>()
            .HasKey(t => t.ID);

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=university;Username=postgres;Passwor=postrges");
    }
}
public class Department
{
    public long ID { get; set; }
    public string? name_department { get; set; }
}
public class Subject
{
    public long ID { get; set; }
    public required string name_enrollee { get; set; }
}
public class Achievement
{
    public long ID { get; set; }
    public required string name_achievement { get; set }
    public int bonus { get; set; }
}
public class Programm
{
    public long ID { get; set; }
    public Department? Department { get; set; }
    public required string name_programm { get; set; }
    public int plan { get; set; }
}
public class Enrollee_Achievement
{
    public long ID { get; set; }
    public Enrollee? Enrollee { get; set; }
    public Achievement? Achievement { get; set; }
}
public class Programm_Enrollee
{
    public long ID { get; set; }
    public Programm? Programm { get; set; }
    public Enrollee? Enrollee { get; set; }
}
public class Programm_Subject
{
    public long ID { get; set; }
    public Programm? Programm { get; set; }
    public int min_result { get; set; }
}
public class Ennrollee_Subject
{
    public long ID { get; set; }
    public Enrollee? Enrollee { get; set; }
    public Subject? Subject { get; set; }
    public int result { get; set; }
}


