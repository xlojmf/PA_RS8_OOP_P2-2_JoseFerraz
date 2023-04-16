using RSGym_Client.Interfaces;
using RSGym_Dal.DBContext;
using RSGym_Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSGym_Client.Repositories
{
    public class RequestRepository : IRequestRepository
    {
        public bool RequestExists(DateTime booking, int personalTrainerID)
        {
            using (var context = new RSGymContext())
            {
                return context.Request.Any(r => r.Booking == booking && r.PersonalTrainerID == personalTrainerID);
            }
        }

        public void CreateRequest(Request request)
        {
            using (var context = new RSGymContext())
            {
                context.Request.Add(request);
                context.SaveChanges();
            }
        }

        public IList<Request> GetAllRequestsByStateAndDate()
        {
            using (var context = new RSGymContext())
            {
                return context.Request
                    .Include("Client")
                    .Include("PersonalTrainer")
                    .OrderBy(r => r.State)
                    .ThenBy(r => r.Booking)
                    .ToList();
            }
        }


        public IList<Request> ListRequestsByState(RequestState state)
        {
            using (var context = new RSGymContext())
            {
                return context.Request.Where(r => r.State == state).ToList();
            }
        }


        public IList<Request> ListRequestsByDateAndTime(DateTime booking)
        {
            using (var context = new RSGymContext())
            {
                return context.Request.Where(r => r.Booking == booking).ToList();
            }
        }


    }
}
