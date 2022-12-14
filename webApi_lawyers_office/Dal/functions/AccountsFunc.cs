using Dal.converters;
using Dal.interfaces;
using Dal.models;
using EntitiesDTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dal.functions
{
    public class AccountsFunc : Iaccounts
    {
        Layers_OfficeContext db;
        public AccountsFunc(Layers_OfficeContext _db)
        {
            db = _db;
        }

        public async Task<AccountDTO> LogInAsync(string email, string password)
        {
            User user = await db.Users.FirstOrDefaultAsync(item => item.UserPassword == password);
            if (user == null) throw new Exception("User doesn't exist.");

            Person person = await db.People.FirstOrDefaultAsync(item => item.Id == user.PersonId && item.Email == email);
            if (person == null) throw new Exception("User doesn't exist.");

            IEnumerable<int> bagsIDs = new List<int>();
            bagsIDs = await db.BagsToPeople.Where(item => item.PersonId == person.Id).Select(bag => bag.BagId).ToListAsync();

            return new AccountDTO() { PersonId = person.Id, Name = person.FirstName + " " + person.LastName, UserType = user.UserType, BagsIDs = bagsIDs, IsFirstVisit = person.LivingAddress == " " };
        }

        public async Task RegisterAsync(RegisterDto registerDto)
        {
            Person personToUpdate = await db.People.FirstOrDefaultAsync(p => p.Id == registerDto.PersonID) ?? throw new Exception("Person Not Found!");
            personToUpdate.LivingAddress = registerDto.LivingAddress;
            personToUpdate.SecondPhone = registerDto.SecondPhone;

            User userToUpdate = await db.Users.FirstOrDefaultAsync(item => item.PersonId == registerDto.PersonID) ?? throw new Exception("User doesn't exist.");
            userToUpdate.UserPassword = registerDto.Password;

            db.People.Update(personToUpdate);
            db.Users.Update(userToUpdate);

            await db.SaveChangesAsync();
        }
    }
}
