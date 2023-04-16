using RSGym_Client.Interfaces;
using RSGym_Client.IO;
using RSGym_Dal.DBContext;
using RSGym_Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSGym_Client.Repositories
{
    public class PersonalTrainerRepository : IPersonalTrainerRepository
    {

        public bool PersonalTrainerExists(string nif)
        {
            using (var context = new RSGymContext())
            {
                return context.PersonalTrainer.Any(p => p.Nif == nif);
            }
        }

        public void CreatePersonalTrainer(PersonalTrainer personalTrainer)
        {
            using (var context = new RSGymContext())
            {
                context.PersonalTrainer.Add(personalTrainer);
                context.SaveChanges();
            }
        }

        public IList<PersonalTrainer> GetAllPersonalTrainers()
        {
            using (var context = new RSGymContext())
            {
                return context.PersonalTrainer
                    .OrderBy(p => p.FullName)
                    .ToList();
            }
        }

        public bool PTExists(int ptId)
        {
            using (var context = new RSGymContext())
            {
                return context.PersonalTrainer.Any(c => c.PersonalTrainerID == ptId);
            }
        }
    }
}
