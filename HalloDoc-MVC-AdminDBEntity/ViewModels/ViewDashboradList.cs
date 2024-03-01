using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDoc_MVC_AdminDBEntity.ViewModels
{
    public class ViewDashboradList
    {
        public string PatientF { get; set; }
        public string? PatientL { get; set; }
        public string Patient => $"{PatientF} {PatientL}";
       
        public string? RequestorF { get; set; }
        public string? RequestorL { get; set; }
        public string Requestor => $"{RequestorF} {RequestorL}";

        public DateTime RequestedDate { get; set; }

        public string PhysicianF { get; set; }
        public string? PhysicianL { get; set; }
        public string Physician => $"{PhysicianF} {PhysicianL}";

        public DateOnly? DOB { get; set; }

        public string Phone{ get; set; }       
        
        public string? Email { get; set; }
        
        public short Status { get; set; }

        public string Region { get; set; }

        public string? Notes { get; set; }

        public string? Address { get; set; }


        public int RequestTypeId { get; set; }
        public int RequestId { get; set; }

       

        public int state
        {
            get
            {
                switch (Status) // Use the db status here
                {
                    case 1:
                        return 1;
                    case 2:
                        return 3;
                    case 3:
                        return 5;
                    case 7:
                        return 5;
                    case 8:
                        return 5;
                    case 4:
                        return 2;
                    case 5:
                        return 2;
                    case 6:
                        return 4;
                    case 9:
                        return 6;
                    default:
                        return 0;
                }
            }
            set { Status = (short)value; } // Set the db status here
        }

        public int RequestClientId { get; set; }
        public string? PhoneO { get; set; }
    }

}
