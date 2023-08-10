using System;
using demo1.Data;
using demo1.Interface;
using demo1.model;
using demo1.service.Interface;

namespace demo1.service
{
    public class PersonService : IPersonService
    {
        private readonly ApplicationDbContext _dbContext;

        public PersonService(ApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }
        //Get Person Details By Person Id  
        public IEnumerable<Person> GetPersonByUserId(int UserId)
        {
            return _dbContext.Person.Where(x => x.Id == UserId).ToList();
        }
        //GET All Perso Details   
        public IEnumerable<Person> GetAllPersons()
        {
            try
            {
                return _dbContext.Person.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Get Person by Person Name  
        public Person GetPersonByUserName(string UserName)
        {
            return _dbContext.Person.Where(x => x.UserEmail == UserName).FirstOrDefault();
        }
        //Add Person  
        public async Task<Person> AddPerson(Person person)
        {

            var data = await _dbContext.Person.AddAsync(person);
            _dbContext.SaveChanges();
            return data.Entity;
        }
        //Delete Person   
        public bool DeletePerson(string UserEmail)
        {

            try
            {
                var DataList = _dbContext.Person.Where(x => x.UserEmail == UserEmail).ToList();
                foreach (var item in DataList)
                {
                    _dbContext.Person.Remove(item);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        //Update Person Details  
        public bool UpdatePerson(Person person)
        {
            try
            {
                var DataList = _dbContext.Person.Where(x => x.IsDeleted != true).ToList();
                foreach (var item in DataList)
                {
                    _dbContext.Person.Update(item);
                }
                return true;
            }
            catch (Exception)
            {
                return true;
            }
        }
    }
}
