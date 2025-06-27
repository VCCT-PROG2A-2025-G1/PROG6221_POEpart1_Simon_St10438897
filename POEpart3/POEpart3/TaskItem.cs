using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POEpart3
{
    public class TaskItem
    {
        public string Title { get; set; }
        public string Prompt { get; set; }
        public string Schedule { get; set; }
        public bool Completed { get; set; }
    }
}
