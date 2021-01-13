using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class WorkHour
    {
        public int Id { get; set; }
        public int IdProject { get; set; }
        public int IdUser { get; set; }

        public DateTime createdAt { get; set; }
        public DateTime finishedAt { get; set; }
    }
}
