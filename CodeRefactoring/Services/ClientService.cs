using CodeRefactoring.Models;
using System.Collections.Generic;

namespace CodeRefactoring.Services
{
    public class ClientService
    {
        public IReadOnlyCollection<Client> GetAll() 
        {
            return new List<Client>
            {
                new Client {
                    ClientId = 1,
                    Name = "Anexsoft"
                },
                new Client {
                    ClientId = 2,
                    Name = "Microsoft"
                },
                new Client {
                    ClientId = 3,
                    Name = "Oracle"
                }
            };
        }
    }
}
