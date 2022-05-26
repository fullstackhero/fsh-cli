using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSHCodeGenerator.Classes
{
    public class ConsoleDataService
    {
        private readonly DbContext _dbContext;

        public ConsoleDataService(DbContext dbcontext)
        {
            _dbContext = dbcontext;
        }
    }
}
