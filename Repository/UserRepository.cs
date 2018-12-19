using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SiC.DTO;
using SiC.Model;
using SiC.Persistence;

namespace SiC.Persistence
{
    public class UserRepository : Repository<User, UserDTO>
    {

        private PersistenceContext context;

        public UserRepository(PersistenceContext context)
        {
            this.context = context;
        }


        public List<GetUserDTO> FindAllDTO()
        {
            List<User> users = context.users.ToList();
            List<GetUserDTO> usersDTO = new List<GetUserDTO>();

            foreach (User user in users)
            {
                GetUserDTO dto = new GetUserDTO();

                dto.FirstName = user.FirstName;
                dto.LastName = user.LastName;

                usersDTO.Add(dto);
            }

            return usersDTO;
        }

        public async Task<List<GetUserDTO>> FindByName(string name)
        {
            var users = await context.users
            .Where(e => e.FirstName == name)
            .ToListAsync();

            if (users.Count == 0) return null;

            List<GetUserDTO> usersDTO = new List<GetUserDTO>();

            foreach (User user in users)
            {
                GetUserDTO dto = new GetUserDTO();

                dto.FirstName = user.FirstName;
                dto.LastName = user.LastName;

                usersDTO.Add(dto);
            }

            return usersDTO;

        }

        public async Task<User> Edit(long id, UserDTO dto)
        {
            var user = await context.users.Where(e => e.Email == dto.Email).FirstOrDefaultAsync();

            if (user == null) return null;

            if (dto.Email != null) user.Email = dto.Email;
            if (dto.FirstName != null) user.FirstName = dto.FirstName;
            if (dto.LastName != null) user.LastName = dto.LastName;
            if (dto.Password != null) user.Password = dto.Password;
            context.Entry(user).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return user;
        }

        public async Task<User> FindOneByEmail(UserDTO dto)
        {
            return await context.users.Where(e => e.Email == dto.Email).FirstOrDefaultAsync();
        }
        


        public async Task<User> Add(UserDTO dto)
        {
               User user = new User();

                user.Email = dto.Email;
                user.FirstName = dto.FirstName;
                user.LastName = dto.LastName;
                user.Password = dto.Password;

                await context.users.AddAsync(user);
                await context.SaveChangesAsync();

                return user;
        }

        public async Task<User> Remove(UserDTO dto)
        {
            var user = await FindOneByEmail(dto);
            if (user == null) return null;
            context.users.Remove(user);
            await context.SaveChangesAsync();
            return user;
        }

        public Task<User> Remove(long id)
        {
            throw new NotImplementedException();
        }
        
        public Task<User> FindById(long id)
        {
            throw new NotImplementedException();
        }
        
        public IEnumerable<User> FindAll()
        {
            throw new NotImplementedException();
        }


    }
}