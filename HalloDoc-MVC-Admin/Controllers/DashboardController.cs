using HalloDoc_MVC_AdminDBEntity.Models;
using HalloDoc_MVC_AdminDBEntity.ViewModels;
using HalloDoc_MVC_AdminRepositories.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HalloDoc_MVC_Admin.Controllers
{
    public class DashboardController : Controller
    {
        
        private readonly IRequestRepository _requestRepository;
        public DashboardController(IRequestRepository requestRepository) {
          
            _requestRepository = requestRepository;
            
        }
        public async Task<IActionResult> Index()
        {
            
            ViewBag.NewCount = await _requestRepository.NewCount();
            ViewBag.ActiveCount = await _requestRepository.ActiveCount();
            ViewBag.PendingCount = await _requestRepository.PendingCount();
            ViewBag.ConcludeCount = await _requestRepository.ConcludeCount();
            ViewBag.ToCloseCount = await _requestRepository.ToCloseCount();
            ViewBag.UnpaidCount = await _requestRepository.UnpaidCount();
            ViewBag.CaseTagCombobox = await _requestRepository.CaseTagComboBox();
            ViewBag.RegionCombobox = await _requestRepository.RegionComboBox();
            var Regions = _requestRepository.GetRegions();
            ViewBag.Regions = new SelectList(Regions, "RegionId", "RegionName");


            ViewBag.PhysiciansByRegion = new SelectList(Enumerable.Empty<SelectListItem>());

            return View();
        }
        public async Task<IActionResult> GetRequestTable(int state,int requesttype)
        {
            List<ViewDashboradList> requestTableData = await _requestRepository.RequestTableAsync(state,requesttype);
        
            return PartialView("_DashboardTable",requestTableData);
        }
        public async Task<IActionResult> ViewCase(int requestclientid)
        {
            ViewCaseModel viewCaseModel =await _requestRepository.GetViewCase(requestclientid);

            return View("ViewCase",viewCaseModel);
        }
        public async Task<IActionResult> SaveViewCase(ViewCaseModel viewCaseModel)
        {
            await _requestRepository.SaveViewCase(viewCaseModel);

            return View("ViewCase");
        }
        public async Task<IActionResult> ViewNotes(int id)
        {
            ViewNotesModel viewNotesModel = await _requestRepository.GetViewNotes(id);

            return View("ViewNotes", viewNotesModel);
        }
        public async Task<IActionResult> SaveViewNotes(int? Requestid, string? AdminNotes, string? PhysicianNotes)
        {

            await _requestRepository.SaveViewNotes(Requestid, AdminNotes , PhysicianNotes);
            //ViewNotes((int)Requestid);
            //List<ViewNotesModel> viewNotesModel =  _requestRepository.GetViewNotes((int)Requestid);
            
                return RedirectToAction("ViewNotes", new { id = Requestid });
               
            
        }
      
        public async Task<IActionResult> CancelCase(int requestid, CancelCaseModel cancelCaseModel)
        {
            
            await _requestRepository.CancelCase(requestid, cancelCaseModel);
            return RedirectToAction("Index");

        }
        public IActionResult GetPhysicianByRegion(int regionid)
        {
            var PhysiciansByRegion = _requestRepository.GetPhysicianByRegion(regionid);
            
            return Json(PhysiciansByRegion);
        }
        public async Task<IActionResult> SaveAssignCase(int RequestId, AssignCaseModel assignCaseModel)
        {

            await _requestRepository.SaveAssignCase(RequestId, assignCaseModel);

            return RedirectToAction("Index");

        }
        public async Task<IActionResult> BlockCase(int RequestId, CancelCaseModel cancelCaseModel)
        {

            await _requestRepository.BlockCase(RequestId, cancelCaseModel);

            return RedirectToAction("Index");

        }
    }
}
