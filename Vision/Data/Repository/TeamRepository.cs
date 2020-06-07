using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vision.Data.IRepository;
using Vision.Models;

namespace Vision.Data.Repository
{
    public class TeamRepository: Repository<Team>, ITeamRepository
    {
        private readonly ApplicationDbContext db;
        public TeamRepository(ApplicationDbContext db):base(db)
        {
            this.db = db;
        }
    }
}
