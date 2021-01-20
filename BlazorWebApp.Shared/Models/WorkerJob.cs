using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorWebApp.Shared.Models
{
    public class WorkerJob
    {
        public Guid Id { get; set; }
        public Guid PawnId { get; set; }
        public Pawn Pawn { get; set; }
        public WorkerJobType WorkerJobType { get; set; }
    }

    public enum WorkerJobType
    {
        CreateParents = 0,
        CreateOthenRelations = 1
    }
}
