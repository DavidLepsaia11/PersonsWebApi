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
            // 1. Snapshot the current tracked entries so we don’t modify them as we iterate.
            var entries = ChangeTracker.Entries().ToList();

            // 2. Create lists to hold all the new or updated PersonAudit objects.
            var auditsToAdd = new List<PersonAudit>();
            var auditsToUpdate = new List<PersonAudit>();

            foreach (var entry in entries)
            {
                // We only care if it’s a Person
                if (entry.Entity is not Person person)
                    continue;

                if (entry.State == EntityState.Added)
                {
                    // We will create a new audit record
                    auditsToAdd.Add(new PersonAudit
                    {
                        PersonId = person.Id,
                        Firstname = person.Firstname,
                        LastName = person.LastName,
                        Age = person.Age,
                        Salary = person.Salary,
                        LogInstance = 0
                    });
                }
                else if (entry.State == EntityState.Modified)
                {
                    auditsToAdd.Add(new PersonAudit
                    {
                        PersonId = person.Id,
                        Firstname = person.Firstname,
                        LastName = person.LastName,
                        Age = person.Age,
                        Salary = person.Salary,
                        LogInstance = 0
                    });

                    var personsAuditById = PersonsAudit
                        .Where(x => x.PersonId == person.Id)
                        .ToList();

                    foreach (var auditRow in personsAuditById)
                    {
                        auditRow.LogInstance += 1;
                        auditsToUpdate.Add(auditRow);
                    }
                }
            }

            foreach (var existingAudit in auditsToUpdate)
            {
                PersonsAudit.Attach(existingAudit);
                Entry(existingAudit).State = EntityState.Modified;
            }

            PersonsAudit.AddRange(auditsToAdd);
        }

    }
}
