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

        public void Update(Team team)
        {
            var objFromDb = db.Teams.FirstOrDefault(s => s.Id == team.Id);

            objFromDb.Name = team.Name;
            objFromDb.Description = team.Description;
            objFromDb.Role = team.Role;
            db.SaveChanges();

        }

    }
}
