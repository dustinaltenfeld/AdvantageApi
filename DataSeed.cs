using System;
using System.Linq;
using System.Collections.Generic;
using Advantage.API.Models;

namespace Advantage.API
{
    public class DataSeed{
        private readonly ApiContext _ctx;

        public DataSeed(ApiContext ctx)
        {
        _ctx =ctx;
        }

        public void SeedData( int nUsers)
        {
            if (!_ctx.Genders.Any())
            {
                SeedGenders();
                _ctx.SaveChanges();
            }
            if (!_ctx.Users.Any())
            {
                SeedUsers(nUsers);
                _ctx.SaveChanges();
            }

            if (!_ctx.Servers.Any())
            {
                SeedServers();
                _ctx.SaveChanges();
            }
            
        }
        private void SeedGenders()
        {
            List<Gender> genders = new List<Gender>();
            genders.Add(new Gender{Id = 1, Name = "divers"});
            genders.Add(new Gender{Id = 2, Name = "female"});
            genders.Add(new Gender{Id = 3, Name = "male"});

            foreach(var gender in genders)
            {
                _ctx.Genders.Add(gender);
            }

        }
        private void SeedUsers(int n)
        {
            List<User> users = BuildUserList(n);

            foreach(var user in users)
            {
                _ctx.Users.Add(user);
            }
        }

        private void SeedServers()
        {
            List<Server> servers = BuildServerList();

            foreach (var server in servers)
            {
                _ctx.Servers.Add(server);
            }

        }
        private List<User> BuildUserList(int nUsers)
        {
            var users = new List<User>();
            var rand = new Random();

            for (var i=1 ; i <= nUsers; i++)
            {
                var name = Helpers.MakeUsername();
                var randGenderId = rand.Next(1,_ctx.Genders.Count()+1);
                var genders = _ctx.Genders.ToList();

                users.Add(new User 
                {
                    Id = i,
                    Name = name,
                    Email = name.Replace(" ",string.Empty) + "@gmail.com",
                    Gender = genders.First(g => g.Id == randGenderId),
                    Bday = DateTime.Now.AddYears(-20),
                    Registration_Day = DateTime.Now
                });
            
            }
            return users;
        }

        private List<Server> BuildServerList()
        {
            return new List<Server>()
            {
                new Server{
                    Id = 1,
                    Name = "Dev-Web",
                    IsOnline = true
                },
                new Server{
                    Id = 2,
                    Name = "Prod-Web",
                    IsOnline = true
                },
                new Server{
                    Id = 3,
                    Name = "Dev-Mail",
                    IsOnline = true
                },
                new Server{
                    Id = 4,
                    Name = "Dev-Blub",
                    IsOnline = true
                },
                new Server{
                    Id = 5,
                    Name = "Dev-Blubb",
                    IsOnline = true
                },
            };
        }
    }
}