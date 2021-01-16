using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Core
{
    public class WorkHour
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }

        public decimal? TotalTime { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? FinishedAt { get; set; }

        public virtual Project Project { get; set; }
        public virtual User User { get; set; }
    }
}
