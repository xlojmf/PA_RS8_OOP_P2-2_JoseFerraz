using RSGym_Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSGym_Client.Interfaces
{
    public interface IRequestRepository
    {
        bool RequestExists(DateTime booking, int personalTrainerID);
        void CreateRequest(Request request);
        IList<Request> GetAllRequestsByStateAndDate();
        IList<Request> ListRequestsByState(RequestState state);
        IList<Request> ListRequestsByDateAndTime(DateTime booking);
    }
}
