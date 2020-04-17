using ManageTask.Models;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Serialization;
using System.Windows.Data;
using System.Text.RegularExpressions;

namespace ManageTask
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Models.Task> displayTasks = null;
        private ObservableCollection<Models.Task> displaySearchTasks = null;

        Task AHour = new Task();
        TaskList taskList = new TaskList();
        TaskList searchTaskList = new TaskList();
        TaskList savedTaskList = new TaskList();

        string ErrorMessage="";

        int Flag = 0;
        public ObservableCollection<Models.Task> DisplayTasks {
            get => displayTasks;
            set => displayTasks = value;
        } 

        public ObservableCollection<Models.Task> DisplaySearchTasks {
            get => displaySearchTasks;
            set => displaySearchTasks = value;
        }

        public MainWindow()
        {
            InitializeComponent();
            txtWorkedHours.Text = "0-10";
            txtAssignTo.Text = "Assigned To";
            txtJobName.Text = "Job Name";
            txtSearch.Text = "Minimum 2 Char.";
            txtWorkedHours.Foreground = Brushes.DarkGray;
            txtAssignTo.Foreground = Brushes.DarkGray;
            txtJobName.Foreground = Brushes.DarkGray;
            txtSearch.Foreground = Brushes.DarkGray;
            displaySearchTasks = new ObservableCollection<Task>();
            displayTasks = new ObservableCollection<Models.Task>();
            DataContext = this;
            ReadDataFromFile();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (CheckAllValidation())
            {
                Models.Task T = new Models.Task(
                    jobName: txtJobName.Text.Trim(),
                    workedHours: double.Parse(txtWorkedHours.Text.Trim()),
                    assignedTo: txtAssignTo.Text.Trim());
                DisplayTasks.Add(T);
                ClearForm();
            }
            else {
                MessageBox.Show(ErrorMessage.Trim(), "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearForm()
        {
            txtAssignTo.Text = "Assigned To";
            txtJobName.Text = "Job Name";
            txtWorkedHours.Text = "0-10";
            txtAssignTo.Foreground = Brushes.DarkGray;
            txtJobName.Foreground = Brushes.DarkGray;
            txtWorkedHours.Foreground = Brushes.DarkGray;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            taskList.Clear();
            foreach (Models.Task T in DisplayTasks) {
                taskList.AddNewItemInList(T);
            }
            if (taskList.Count() > 0)
            {
                if (SaveDataInFile())
                {
                    MessageBox.Show("Data Saved Successfully In File.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Unable To Save Data!","Error",MessageBoxButton.OK,MessageBoxImage.Error);
                }
            }
            else {
                MessageBox.Show("No Data To Save In File!", "Info.", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            
        }

        private bool SaveDataInFile() {
            XmlSerializer serializer = new XmlSerializer(typeof(TaskList));
            TextWriter TW = new StreamWriter("AllTasksList.xml");
            serializer.Serialize(TW, taskList);
            TW.Close();
            return true;
        }

        private void ReadDataFromFile() {
            XmlSerializer serializer = new XmlSerializer(typeof(TaskList));
            TextReader TR = new StreamReader("AllTasksList.xml");
            savedTaskList = (TaskList)serializer.Deserialize(TR);
            TR.Close();
            foreach (Models.Task T in savedTaskList.TasksList) {
                DisplayTasks.Add(T);
            }
        }

        private bool CheckAllValidation()
        {
            ErrorMessage = "";
            int Temp = 0;
            if (txtJobName.Text.Trim() == "" || txtJobName.Text.Trim() == null || txtJobName.Text.Trim() == "Job Name")
            {
                ErrorMessage += "\nPlease Enter Job Name.";
            }
            if (txtAssignTo.Text.Trim() == "" || txtAssignTo.Text.Trim() == null || txtAssignTo.Text.Trim() == "Assigned To")
            {
                ErrorMessage += "\nPlease Enter Assigned To.";
            }
            if (txtWorkedHours.Text.Trim() == "" || txtWorkedHours.Text.Trim() == null || txtWorkedHours.Text.Trim() == "0-10")
            {
                ErrorMessage += "\nPlease Enter Worked Hours.";
            }
            else
            {
                Temp = 1;
            }
            if (Temp == 1)
            {
                double WHour;
                if (!double.TryParse(txtWorkedHours.Text.Trim(), out WHour))
                {
                    ErrorMessage += "\nPlease Enter Valid Worked Hours.";
                }
            }
            if (ErrorMessage == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void txtJobName_GotFocus(object sender, RoutedEventArgs e)
        {

            txtJobName.Foreground = Brushes.Black;
            if (txtJobName.Text.Trim() == "Job Name")
            {
                txtJobName.Text = "";
            }
        }

        private void txtJobName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtJobName.Text.Trim() == "")
            {
                txtJobName.Text = "Job Name";
                txtJobName.Foreground = Brushes.DarkGray;
            }
        }

        private void txtWorkedHours_GotFocus(object sender, RoutedEventArgs e)
        {

            txtWorkedHours.Foreground = Brushes.Black;
            if (txtWorkedHours.Text.Trim() == "0-10")
            {
                txtWorkedHours.Text = "";
            }
        }

        private void txtAssignTo_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtAssignTo.Text.Trim() == "Assigned To")
            {
                txtAssignTo.Text = "";
            }
            txtAssignTo.Foreground = Brushes.Black;
        }

        private void txtAssignTo_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtAssignTo.Text.Trim() == "")
            {
                txtAssignTo.Text = "Assigned To";
                txtAssignTo.Foreground = Brushes.DarkGray;
            }
        }

        private void txtWorkedHours_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtWorkedHours.Text.Trim() == "")
            {
                txtWorkedHours.Foreground = Brushes.DarkGray;
                txtWorkedHours.Text = "0-10";
            }
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Flag == 0)
            {
                Flag = 1;
            }
            else {
                if (txtSearch.Text.Trim().Length < 1 || txtSearch.Text.Trim() == "Minimum 2 Char.") {
                    DisplaySearchTasks.Clear();
                    lblNotFound.Visibility = Visibility.Hidden;
                }
                if (txtSearch.Text.Trim().Length > 1) {
                    searchTaskList.Clear();
                    DisplaySearchTasks.Clear();
                    XmlSerializer serializer = new XmlSerializer(typeof(TaskList));
                    TextReader TR = new StreamReader("AllTasksList.xml");
                    searchTaskList = (TaskList)serializer.Deserialize(TR);
                    TR.Close();
                    var SearchedResults = from T in searchTaskList.TasksList
                                          where T.JobName.ToLower().Contains(txtSearch.Text.ToLower().Trim()) ||
                                          T.AssignedTo.ToLower().Contains(txtSearch.Text.ToLower().Trim())
                                          select T;

                    if (SearchedResults.Count() > 0)
                    {
                        lblNotFound.Visibility = Visibility.Hidden;
                        foreach (Models.Task T in SearchedResults)
                        {
                            DisplaySearchTasks.Add(T);
                        }
                    }
                    else {
                        lblNotFound.Visibility = Visibility.Visible;
                    }
                }
            }
        }

        private void txtSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtSearch.Text.Trim() == "Minimum 2 Char.") {
                txtSearch.Text = "";
            }
            txtSearch.Foreground = Brushes.Black;
        }

        private void txtSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtSearch.Text.Trim() == "")
            {
                txtSearch.Foreground = Brushes.DarkGray;
                txtSearch.Text = "Minimum 2 Char.";
            }
            lblNotFound.Visibility = Visibility.Hidden;
        }

        private void txtWorkedHours_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
