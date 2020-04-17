using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace ManageTask.Models
{
    [XmlRoot("TaskList")]
    public class TaskList
    {
        private List<Task> tasksList = new List<Task>();

        [XmlArray("Tasks")]
        [XmlArrayItem("Task", typeof(Task))]

        public List<Task> TasksList {
            get => tasksList;
            set => tasksList = value;
        }

        public void AddNewItemInList(Task T) {
            TasksList.Add(T);   
        }

        public int Count()
        {
            return TasksList.Count;
        }

        public void Clear() {
            TasksList.Clear();
        }
    }
}
