using System.Diagnostics.CodeAnalysis;

namespace ManageTask.Models
{
    public class Task : ITask
    {
        private string jobName;
        private double workedHours;
        private string assignedTo;

        public string JobName { get => jobName; set => jobName = value; }
        public double WorkedHours { get => workedHours; set => workedHours = value; }
        public string AssignedTo { get => assignedTo; set => assignedTo = value; }

        public int CompareTo([AllowNull] ITask other)
        {
            return workedHours.CompareTo(other.WorkedHours);
        }
        public Task(string jobName, double workedHours,string assignedTo) {
            this.jobName = jobName;
            this.workedHours = workedHours;
            this.assignedTo = assignedTo;
        }

        public Task() {
            this.jobName = "";
            this.workedHours = 0;
            this.assignedTo = "";
        }
    }
}
