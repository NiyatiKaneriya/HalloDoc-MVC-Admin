using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDoc_MVC_AdminDBEntity.ViewModels
{
    public class ViewNotesModel
    {
        public int? RequestNotesId { get; set; }

        public int? Requestid { get; set; }
        public string? PhysicianNotes { get; set; }
        public string? AdminNotes { get; set; }
        //public string? TransferNotes { get; set; }
        public int? Status { get; set; }
        public string? Notes { get; set; }
        public string? Physician { get; set; }
        public string? Admin { get; set; }
        public string? CreatedBy { get; set; } = null;
        public DateTime? CreatedDate { get; set; }

        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? AdministrativeNotes { get; set; }
        public List<TransferNotesModel>? TransferNotes { get; set; }
        public class TransferNotesModel
        {
            public int RequestStatusLogId { get; set; }
            public int RequestId { get; set; }
            public int? PhysicianId { get; set; }
            public int? TranstoPhysicianId { get; set; }
            public DateTime CreatedDate { get; set; }
            public string? Notes { get; set; }
            public BitArray? TranstoAdmin { get; set; }
        }

        //public string TransferNotes => $"{Admin} transferred to {Physician} on {TransferDate}: {Notes}";
        
        



    }
}
