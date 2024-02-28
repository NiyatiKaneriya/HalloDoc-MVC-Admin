using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDoc_MVC_AdminDBEntity.ViewModels
{
    public class CancelCaseModel
    {
        public int Requestid { get; set; }
        //public int Status { get; set; }
        public int AdminId { get; set; } = 1;
        public string Notes { get; set; }
        public int ReasonId { get; set; }
        public string ReasonTag { get; set; }
    }
}
