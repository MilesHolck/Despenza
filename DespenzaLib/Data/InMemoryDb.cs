using DespenzaLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespenzaLib.Data
{
    public class InMemoryDb
    {
     
        public Dictionary<Type, object> Sets { get; } = new();


        public InMemoryDb()
        {
            Sets[typeof(User)] = new List<User>
            {
                new Admin
                {
                    UserId = 1,
                    Name = "Mester",
                    Email = "mester@despenza.dk",
                    Password = "1234",
                    Role = "Admin"
                },
                new Baker
                {
                    UserId = 2,
                    Name = "Bager",
                    Email = "bager@despenza.dk",
                    Password = "1234",
                    Role = "User"
                },
                new Apprentice
                {
                    UserId = 3,
                    Name = "Lærling",
                    Email = "laerling@despenza.dk",
                    Password = "1234",
                    Role = "User"

                }
            };

            //'Tables' klar til brug:

            Sets[typeof(Ingredient)] = new List<Ingredient>();
            Sets[typeof(Recipe)] = new List<Recipe>();
            Sets[typeof(InventoryItem)] = new List<InventoryItem>();
            Sets[typeof(Product)] = new List<Product>();


            }
        }
    }





 
