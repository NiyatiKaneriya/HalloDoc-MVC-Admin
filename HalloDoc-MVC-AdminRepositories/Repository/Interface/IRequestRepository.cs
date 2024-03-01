using HalloDoc_MVC_AdminDBEntity.Models;
using HalloDoc_MVC_AdminDBEntity.ViewModels;
using Microsoft.EntityFrameworkCore;
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
        public Task<Boolean> SaveViewNotes(int? Requestid, string? AdminNotes, string? PhysicianNotes);
        public Task<Boolean> CancelCase(int RequestId, CancelCaseModel cancelCaseModel);
        public Task<List<CaseTagComboBox>> CaseTagComboBox();
        public Task<List<RegionComboBox>> RegionComboBox();
        public IEnumerable<Region> GetRegions();
        public List<Physician> GetPhysicianByRegion(int RegionId);
        public Task<Boolean> SaveAssignCase(int RequestId, AssignCaseModel assignCaseModel);
        public Task<Boolean> BlockCase(int RequestId, CancelCaseModel cancelCaseModel);





    }
}
