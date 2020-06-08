using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vision.Data.IRepository;
using Vision.Models;

namespace Vision.Data.Repository
{
    public class FeedbackRepository:Repository<Feedback>, IFeedbackRepository
    {
        private ApplicationDbContext db;

        public FeedbackRepository(ApplicationDbContext db) : base(db)
        {
            this.db = db;
        }

        public void Update(Feedback feedback)
        {
            var objFromDb = db.Feedbacks.FirstOrDefault(f => f.Id == feedback.Id);
            objFromDb.Comment = feedback.Comment;
            
            db.SaveChanges();
                
        }
    }
}
