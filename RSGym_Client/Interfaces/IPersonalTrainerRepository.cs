using RSGym_Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSGym_Client.Interfaces
{
    public interface IPersonalTrainerRepository
    {
        bool PersonalTrainerExists(string nif);
        void CreatePersonalTrainer(PersonalTrainer personalTrainer);
        IList<PersonalTrainer> GetAllPersonalTrainers();
        bool PTExists(int ptId);
    }
}
