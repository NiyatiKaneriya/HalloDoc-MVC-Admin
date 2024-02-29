using HalloDoc_MVC_AdminDBEntity.Data;
using HalloDoc_MVC_AdminDBEntity.Models;
using HalloDoc_MVC_AdminDBEntity.ViewModels;
using HalloDoc_MVC_AdminRepositories.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HalloDoc_MVC_AdminDBEntity.ViewModels.ViewNotesModel;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HalloDoc_MVC_AdminRepositories.Repository
{
    public class RequestRepository : IRequestRepository
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
            var query = await _context.Requests.CountAsync(e => e.Status == 4 || e.Status == 5);
            return query;
        }
        public async Task<int> PendingCount()
        {
            var query = await _context.Requests.CountAsync(e => e.Status == 2);
            return query;
        }
        public async Task<int> ConcludeCount()
        {
            var query = await _context.Requests.CountAsync(e => e.Status == 6);
            return query;
        }
        public async Task<int> ToCloseCount()
        {
            var query = await _context.Requests.CountAsync(e => e.Status == 3 || e.Status == 7 || e.Status == 8);
            return query;
        }
        public async Task<int> UnpaidCount()
        {
            var query = await _context.Requests.CountAsync(e => e.Status == 9);
            return query;
        }
        public async Task<List<ViewDashboradList>> RequestTableAsync(int state, int requesttype)
        {

            List<int> statusList = new List<int>();
            if (state == 5)
            {
                statusList.Add(3);
                statusList.Add(7);
                statusList.Add(8);
            }
            else if (state == 3)
            {
                statusList.Add(4);
                statusList.Add(5);
            }
            else if (state == 1)
            {
                statusList.Add(1);
            }
            else if (state == 2)
            {
                statusList.Add(2);
            }
            else if (state == 4)
            {
                statusList.Add(6);
            }
            else if (state == 6)
            {
                statusList.Add(9);
            }
            List<ViewDashboradList> query;
            if (requesttype == 0)
            {
                query = (from r in _context.Requests
                         join rc in _context.RequestClients on r.RequestId equals rc.RequestId
                         join p in _context.Physicians on r.PhysicianId equals p.PhysicianId into physicianGroup
                         from physician in physicianGroup.DefaultIfEmpty()
                         join re in _context.Regions on rc.RegionId equals re.RegionId into regionGroup
                         from region in regionGroup.DefaultIfEmpty()
                         where statusList.Contains(r.Status)
                         select new ViewDashboradList
                         {
                             RequestClientId = rc.RequestClientId,
                             RequestId = r.RequestId,
                             PatientF = rc.FirstName,
                             PatientL = rc.LastName,
                             Email = r.Email,
                             Status = r.Status,
                             DOB = new DateOnly((int)rc.IntYear, Convert.ToInt32(rc.StrMonth), (int)rc.IntDate),
                             RequestTypeId = r.RequestTypeId,
                             RequestorF = r.FirstName,
                             RequestorL = r.LastName,
                             RequestedDate = r.CreatedDate,
                             Phone = rc.PhoneNumber,
                             PhoneO = r.PhoneNumber,
                             PhysicianF = physician.FirstName,
                             PhysicianL = physician.LastName,
                             Address = rc.Address,
                             Notes = rc.Notes,
                             Region = region.Name
                         }).ToList();
            }
            else
            {
                query = (from r in _context.Requests
                         join rc in _context.RequestClients on r.RequestId equals rc.RequestId
                         join p in _context.Physicians on r.PhysicianId equals p.PhysicianId into physicianGroup
                         from physician in physicianGroup.DefaultIfEmpty()
                         join re in _context.Regions on rc.RegionId equals re.RegionId into regionGroup
                         from region in regionGroup.DefaultIfEmpty()
                         where statusList.Contains(r.Status) && r.RequestTypeId == requesttype
                         select new ViewDashboradList
                         {
                             RequestClientId = rc.RequestClientId,
                             RequestId = r.RequestId,
                             PatientF = rc.FirstName,
                             PatientL = rc.LastName,
                             Email = r.Email,
                             Status = r.Status,
                             DOB = new DateOnly((int)rc.IntYear, Convert.ToInt32(rc.StrMonth), (int)rc.IntDate),
                             RequestTypeId = r.RequestTypeId,
                             RequestorF = r.FirstName,
                             RequestorL = r.LastName,
                             RequestedDate = r.CreatedDate,
                             Phone = rc.PhoneNumber,
                             PhoneO = r.PhoneNumber,
                             PhysicianF = physician.FirstName,
                             PhysicianL = physician.LastName,
                             Address = rc.Address,
                             Notes = rc.Notes,
                             Region = region.Name
                         }).ToList();
            }


            return (List<ViewDashboradList>)query;
        }

        public async Task<ViewCaseModel> GetViewCase(int requestclientid)
        {
            var query = await (from r in _context.Requests
                               join rc in _context.RequestClients on r.RequestId equals rc.RequestId
                               join re in _context.Regions on rc.RegionId equals re.RegionId into regionGroup
                               from region in regionGroup.DefaultIfEmpty()
                               where rc.RequestClientId == requestclientid
                               select new ViewCaseModel
                               {
                                   RequestId = r.RequestId,
                                   PatientF = rc.FirstName,
                                   PatientL = rc.LastName,
                                   Email = r.Email,
                                   Status = r.Status,
                                   DOB = new DateOnly((int)rc.IntYear, Convert.ToInt32(rc.StrMonth), (int)rc.IntDate),
                                   Phone = rc.PhoneNumber,

                                   Address = rc.Address,
                                   Notes = rc.Notes,
                                   Region = region.Name
                               }).FirstOrDefaultAsync();
            return query;

        }

        public async Task<Boolean> SaveViewCase(ViewCaseModel viewCase)
        {

            var requestclient = await _context.RequestClients.Where(r => r.RequestClientId == viewCase.RequestClientId).FirstAsync();

            requestclient.FirstName = viewCase.PatientF;
            requestclient.LastName = viewCase.PatientL;
            requestclient.PhoneNumber = viewCase.Phone;
            requestclient.Address = viewCase.Address;
            //requestclient.Region = viewCase.Region;
            _context.RequestClients.Update(requestclient);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<ViewNotesModel> GetViewNotes(int requestid)
        {
            try
            {
                ViewNotesModel? notes = await _context.RequestNotes
                    .Where(r => r.RequestId == requestid)
                    .Select(r => new ViewNotesModel
                    {
                        Requestid = r.RequestId,
                        AdministrativeNotes = r.AdministrativeNotes,
                        AdminNotes = r.AdminNotes,
                        CreatedBy = r.CreatedBy,
                        CreatedDate = r.CreatedDate,
                        //Intdate = r.Intdate,
                        //Intyear = r.Intyear,
                        //Ip = r.Ip,
                        ModifiedBy = r.ModifiedBy,
                        ModifiedDate = r.ModifiedDate,
                        PhysicianNotes = r.PhysicianNotes,
                        RequestNotesId = r.RequestNotesId,
                    })
                    .FirstOrDefaultAsync();

               
                    var requestlog = await _context.RequestStatusLogs
                        .Where(E => E.RequestId == requestid && E.TransToPhysician != null)
                    .ToListAsync();
                if (requestlog.Count <= 0 && notes == null)
                {
                    notes = new ViewNotesModel();
                    notes.Requestid = requestid;
                    notes.PhysicianNotes = "--";
                    notes.AdministrativeNotes = "--";
                    notes.AdminNotes = "--";
                }
                List<TransferNotesModel> transferlist = requestlog.Select(e => new TransferNotesModel
                    {
                        RequestId = e.RequestId,
                        Notes = e.Notes,
                        PhysicianId = e.PhysicianId,
                        CreatedDate = e.CreatedDate,
                        RequestStatusLogId = e.RequestStatusLogId,
                        TranstoAdmin = e.TransToAdmin,
                        TranstoPhysicianId = e.TransToPhysicianId
                    }).ToList();

                    notes.TransferNotes = transferlist;
                
                return notes;
            }
            catch (Exception e)
            {
                throw; 
            }

            //var query = (from rs in _context.RequestStatusLogs
            //             join r in _context.RequestNotes on rs.RequestId equals r.RequestId into rGroup
            //             from r
            //             join p in _context.Physicians on rs.TransToPhysicianId equals p.PhysicianId into pGroup
            //             from p in pGroup.DefaultIfEmpty()
            //             join a in _context.Admins on rs.AdminId equals a.AdminId into aGroup
            //             from a in aGroup.DefaultIfEmpty()
            //             where rs.RequestId == requestid
            //             select new ViewNotesModel
            //             {
            //                 Requestid = rs.RequestId,
            //                 AdminNotes = r.AdminNotes,
            //                 PhysicianNotes = r.PhysicianNotes,
            //                 Status = rs.Status,
            //                 TransferDate = rs.CreatedDate,
            //                 Notes = rs.Notes,
            //                 Physician = p.FirstName,
            //                 Admin = a.FirstName,


            //             }).ToList();

            //if(query.Count <= 0)
            //{
            //    query.Add(new ViewNotesModel { Requestid = requestid, AdminNotes = "-", PhysicianNotes = "-" });
            //}

            //return query;

        }
        public async Task<Boolean> SaveViewNotes(int? Requestid, string? AdminNotes, string? PhysicianNotes)
        {
            try
            {
                RequestNote notes =  _context.RequestNotes.FirstOrDefault(E => E.RequestId == Requestid);
                if (PhysicianNotes != null)
                {
                    if (notes != null)
                    {

                        notes.PhysicianNotes = PhysicianNotes;
                        notes.ModifiedDate = DateTime.Now;

                        _context.RequestNotes.Update(notes);
                        await _context.SaveChangesAsync();
                        return true;
                    }
                    else

                    {
                        RequestNote rn = new RequestNote
                        {
                            RequestId = (int)Requestid,
                            PhysicianNotes = PhysicianNotes,
                            CreatedDate = DateTime.Now,
                            CreatedBy = "5df80732-5198-4c87-b1ea-ebf86cbd7530"
                        };
                        _context.RequestNotes.Add(rn);
                        await _context.SaveChangesAsync();
                        return true;
                    }
                }
                else if (AdminNotes != null)
                {
                    if (notes != null)
                    {

                        notes.AdminNotes = AdminNotes;
                        notes.ModifiedDate = DateTime.Now;

                        _context.RequestNotes.Update(notes);
                        await _context.SaveChangesAsync();
                        return true;
                    }
                    else
                    {
                        RequestNote rn = new RequestNote
                        {
                            RequestId = (int)Requestid,
                            AdminNotes = AdminNotes,
                            CreatedDate = DateTime.Now,
                            CreatedBy = "5df80732-5198-4c87-b1ea-ebf86cbd7530"
                        };
                        _context.RequestNotes.Add(rn);
                        await _context.SaveChangesAsync();
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            //var  request = await _context.RequestNotes.Where(r => r.RequestId == Requestid).FirstOrDefaultAsync();
            //if(request == null)
            //{
            //    RequestNote requestNotes = new RequestNote
            //    {
            //        RequestId = (int)Requestid,
            //        AdminNotes = AdminNotes,
            //        CreatedDate = DateTime.Now,
            //        CreatedBy = "5df80732-5198-4c87-b1ea-ebf86cbd7530"
            //    };

            //    _context.RequestNotes.Add(requestNotes);
            //   await _context.SaveChangesAsync();
            //    return true;

            //}
            //else
            //{
            //    request.AdminNotes = AdminNotes;
            //    _context.RequestNotes.Update(request);
            //    await _context.SaveChangesAsync();
            //    return true;
            //}

            //return false;
        }
        public async Task<Boolean> CancelCase(int RequestId, CancelCaseModel cancelCaseModel)
        {
            var req = await _context.Requests.Where(e => e.RequestId == cancelCaseModel.Requestid).FirstAsync();
            if (req != null)
            {
                req.CaseTag = cancelCaseModel.ReasonTag;
                req.Status = 3;
                _context.Requests.Update(req);
                _context.SaveChanges();

                RequestStatusLog requestStatusLog = new RequestStatusLog
                {
                    RequestId = cancelCaseModel.Requestid,
                    Status = 3,
                    AdminId = cancelCaseModel.AdminId,
                    Notes = cancelCaseModel?.Notes,
                    CreatedDate= DateTime.Now,
                };

                _context.RequestStatusLogs.Add(requestStatusLog);
                _context.SaveChanges();

                return true;
            }
            return false;
        }
        public async Task<List<CaseTagComboBox>> CaseTagComboBox()
        {
            return await _context.CaseTags.Select(req => new CaseTagComboBox()
            {
                CaseTagId = req.CaseTagId,
                CaseTagName = req.Name,
            }).ToListAsync();

        }
        public async Task<List<RegionComboBox>> RegionComboBox()
        {
            return await _context.Regions.Select(req => new RegionComboBox()
            {
                RegionId = req.RegionId,
                RegionName = req.Name,
                RegionAbbr = req.Abbreviation,
            }).ToListAsync();
        }
        public IEnumerable<RegionModel> GetRegions()
        {
            return _context.Regions.ToList();
        }

    }
}
