using HalloDoc_MVC_AdminDBEntity.Data;
using HalloDoc_MVC_AdminDBEntity.Models;
using HalloDoc_MVC_AdminDBEntity.ViewModels;
using HalloDoc_MVC_AdminRepositories.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDoc_MVC_AdminRepositories.Repository
{
    public class RequestRepository: IRequestRepository
    {
        private readonly ApplicationDbContext _context;

        public RequestRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> NewCount()
        {
            var query = await _context.Requests.CountAsync(e => e.Status == 1);
            return query;
        }
        public async Task<int> ActiveCount()
        {
            var query = await _context.Requests.CountAsync(e => e.Status == 2);
            return query;
        }
        public async Task<int> PendingCount()
        {
            var query = await _context.Requests.CountAsync(e => e.Status == 3);
            return query;
        }
        public async Task<int> ConcludeCount()
        {
            var query = await _context.Requests.CountAsync(e => e.Status == 4);
            return query;
        }
        public async Task<int> ToCloseCount()
        {
            var query = await _context.Requests.CountAsync(e => e.Status == 5);
            return query;
        }
        public async Task<int> UnpaidCount()
        {
            var query = await _context.Requests.CountAsync(e => e.Status == 6);
            return query;
        }
        public async Task<List<ViewDashboradList>> RequestTableAsync(int state)
        {
            var query = (from r in _context.Requests
                        join rc in _context.RequestClients on r.RequestId equals rc.RequestId
                        join p in _context.Physicians on r.PhysicianId equals p.PhysicianId into physicianGroup
                        from physician in physicianGroup.DefaultIfEmpty()
                        join re in _context.Regions on rc.RegionId equals re.RegionId into regionGroup
                        from region in regionGroup.DefaultIfEmpty()
                        where r.Status == state
                        select new ViewDashboradList
                        {
                            RequestId = r.RequestId,
                            PatientF = rc.FirstName,
                            PatientL = rc.LastName,
                            Email = rc.Email,
                            Status = r.Status,
                            DOB = new DateTime((int)rc.IntYear, int.Parse(rc.StrMonth), (int)rc.IntDate),
                            RequestTypeId = r.RequestTypeId,
                            RequestorF = r.FirstName,
                            RequestorL = r.LastName,
                            RequestedDate = r.CreatedDate,
                            Phone = rc.PhoneNumber,
                            PhysicianF = physician.FirstName,
                            PhysicianL = physician.LastName,
                            Address = rc.Address,
                            Notes = rc.Notes,
                            Region = region.Name
                        }).ToList();

            return (List<ViewDashboradList>)query;
        }

    }
}
