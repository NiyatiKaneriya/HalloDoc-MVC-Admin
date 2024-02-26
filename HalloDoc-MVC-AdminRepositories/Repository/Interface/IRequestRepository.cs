using HalloDoc_MVC_AdminDBEntity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDoc_MVC_AdminRepositories.Repository.Interface
{
    public interface IRequestRepository
    {
        public Task<int> NewCount();
        public Task<int> ActiveCount();
        public Task<int> PendingCount();
        public Task<int> ConcludeCount();
        public  Task<int> ToCloseCount();
        public Task<int> UnpaidCount();
        public  Task<List<ViewDashboradList>> RequestTableAsync(int state,int requesttype);
        public Task<ViewCaseModel> GetViewCase(int requestclientid);
        public Task<Boolean> SaveViewCase(ViewCaseModel viewCase);
        public Task<ViewNotesModel> GetViewNotes(int requestid);
        public Task<Boolean> SaveViewNotes(  int? Requestid, string? AdminNotes);
    }
}
