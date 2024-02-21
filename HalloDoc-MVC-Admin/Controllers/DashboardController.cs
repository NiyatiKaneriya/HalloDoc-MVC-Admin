using HalloDoc_MVC_AdminDBEntity.Models;
using HalloDoc_MVC_AdminDBEntity.ViewModels;
using HalloDoc_MVC_AdminRepositories.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace HalloDoc_MVC_Admin.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IContactRepository _contactRepository;
        private readonly IRequestRepository _requestRepository;
        public DashboardController(IContactRepository contactRepository,IRequestRepository requestRepository) {
            _contactRepository = contactRepository;
            _requestRepository = requestRepository;
            
        }
        public async Task<IActionResult> Index()
        {
            //RequestClient s = await _contactRepository.niyati();
            ViewBag.NewCount = await _requestRepository.NewCount();
            ViewBag.ActiveCount = await _requestRepository.ActiveCount();
            ViewBag.PendingCount = await _requestRepository.PendingCount();
            ViewBag.ConcludeCount = await _requestRepository.ConcludeCount();
            ViewBag.ToCloseCount = await _requestRepository.ToCloseCount();
            ViewBag.UnpaidCount = await _requestRepository.UnpaidCount();


            return View();
        }
        public async Task<IActionResult> GetRequestTable(int state)
        {
            List<ViewDashboradList> requestTableData = await _requestRepository.RequestTableAsync(state);
        
            return PartialView("_DashboardTable",requestTableData);
        }

    }
}
