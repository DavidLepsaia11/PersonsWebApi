using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PersonsWebApi.Core.Domain;

namespace PersonsWebApi.Infrastructure.Database
{
    public class PersonDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<PersonAudit> PersonsAudit { get; set; }

        public PersonDbContext(DbContextOptions<PersonDbContext> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }


        public override int SaveChanges()
        {
             OnBeforeSaveChanges();
            var result = base.SaveChanges();
            //OnAfterSaveChanges(auditEntries);
            return result;
        }

        private void OnBeforeSaveChanges()
        {

            ChangeTracker.DetectChanges();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Person && entry.State == EntityState.Added)
                {
                    var addedPerson = (Person)entry.Entity;
                    var auditEntry = new PersonAudit()
                    {
                        PersonId = addedPerson.Id,
                        Firstname = addedPerson.Firstname,
                        LastName = addedPerson.LastName,
                        Age = addedPerson.Age,
                        Salary = addedPerson.Salary,
                        LogInstance = 0
                    };
                    PersonsAudit.Add(auditEntry);
                }


                if (entry.Entity is Person && entry.State == EntityState.Modified)
                {
                    var updatedPerson = (Person)entry.Entity;

                    var personsAuditById = PersonsAudit.Where(x => x.PersonId == updatedPerson.Id).ToList();

                    foreach (var person in personsAuditById)
                    {
                        person.LogInstance += 1;
                        PersonsAudit.Attach(person);
                        this.Entry(person).State = EntityState.Modified;
                    }

                    var auditEntry = new PersonAudit()
                    {
                        PersonId = updatedPerson.Id,
                        Firstname = updatedPerson.Firstname,
                        LastName = updatedPerson.LastName,
                        Age = updatedPerson.Age,
                        Salary = updatedPerson.Salary,
                        LogInstance = 0
                    };
                    PersonsAudit.Add(auditEntry);
                }
                else
                    continue;
            }
        }
    }
}
