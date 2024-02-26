using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDoc_MVC_AdminDBEntity.ViewModels
{
    public class ViewNotesModel
    {
        public int Requestid { get; set; }
        public string? PhysicianNotes { get; set; }
        public string? AdminNotes { get; set; }
        public string? TransferNotes { get; set; }
    }
}
