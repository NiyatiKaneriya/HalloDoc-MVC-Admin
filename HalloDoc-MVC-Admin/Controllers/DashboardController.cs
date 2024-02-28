using HalloDoc_MVC_AdminDBEntity.Models;
using HalloDoc_MVC_AdminDBEntity.ViewModels;
using HalloDoc_MVC_AdminRepositories.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;

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
        public ViewResult ViewNotes(int id)
        {
            List<ViewNotesModel> viewNotesModel = _requestRepository.GetViewNotes(id);

            return View("ViewNotes", viewNotesModel);
        }
        public ViewResult SaveViewNotes(int? Requestid, string? AdminNotes ,string PhysicianNotes)
        {
             _requestRepository.SaveViewNotes(Requestid, AdminNotes);
            List<ViewNotesModel> viewNotesModel =  _requestRepository.GetViewNotes((int)Requestid);

            return View("ViewNotes", new { id = Requestid });
        }
        //public PartialViewResult Cancelpopup()
        //{
        //    return PartialView("_cancelCase");
        //}

        public async Task<IActionResult> CancelCase(int requestid, CancelCaseModel cancelCaseModel)
        {
            //ViewBag.CaseTagCombobox = await _requestRepository.CaseTagComboBox();
            await _requestRepository.CancelCase(requestid, cancelCaseModel);
            return RedirectToAction("Index");

        }
    }
}
