using System;

namespace ManageTask.Models
{
    public interface ITask : IComparable<ITask>
    {
        public string JobName { get; set; }
        public double WorkedHours { get; set; }
        public string AssignedTo { get; set; }
    }
}
