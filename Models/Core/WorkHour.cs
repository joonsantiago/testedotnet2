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

        public DateTime? CreatedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
    }
}
