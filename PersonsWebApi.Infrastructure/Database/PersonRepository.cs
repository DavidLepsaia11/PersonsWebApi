using PersonsWebApi.Core.Domain;
using PersonsWebApi.Infrastructure.Database.Interfaces;

namespace PersonsWebApi.Infrastructure.Database
{
    public class PersonRepository : RepositoryBase<Person>, IPersonRepository
    {
        public PersonRepository(PersonDbContext context) : base(context)
        {

        }
    }
}
