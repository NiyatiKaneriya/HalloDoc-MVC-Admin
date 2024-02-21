using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDoc_MVC_AdminDBEntity.ViewModels
{
    public class ViewDashboradList
    {
        public string Patient => $"{PatientF} {PatientL}";


        public string Requestor => $"{RequestorF} {RequestorL}";

        public DateTime RequestedDate { get; set; }

        public string Physician => $"{PhysicianF} {PhysicianL}";

        

        public string Phone{ get; set; }
        public int RequestId { get; set; }
        public string PatientF { get; set; }
        public string? Email { get; set; }
        public string? PatientL { get; set; }
        public short Status { get; set; }
        public string Region { get; set; }
        public string? Notes { get; set; }
        public string? Address { get; set; }
        public int RequestTypeId { get; set; }
        public string? RequestorL { get; set; }
        public string? RequestorF { get; set; }
        public string PhysicianF { get; set; }
        public string? PhysicianL { get; set; }
        public DateTime DOB { get; set; }
    }
}
