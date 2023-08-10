using demo1.model;

namespace demo1.service.Interface
{
    public interface IPersonService
    {
        public IEnumerable<Person> GetPersonByUserId(int UserId);
        public IEnumerable<Person> GetAllPersons();
        public Person GetPersonByUserName(string UserName);
        public Task<Person> AddPerson(Person Person);
        public bool DeletePerson(string UserEmail);
        public bool UpdatePerson(Person person);



    }
}
