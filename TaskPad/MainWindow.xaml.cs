using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TaskPad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static Settings settings = new Settings();
        static Parser parser = new Parser(settings.GetPath());
        

        Actions a = new Actions();
        public MainWindow()
        {
            InitializeComponent();

            
            this.TaskList.ItemsSource = parser.GetTaskNames();


            

        }

        

           

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            PathSelect pathSelect = new PathSelect();
            // pathSelect.Show();
            a.test("TaskPad");
        }

        private void TaskList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            var tmp = this.TaskList.SelectedItem;
            string temp = Convert.ToString(this.TaskList.SelectedItem);
            Task task = (Task)parser.getTask(temp);
            Console.WriteLine(task.name);
            string tt = task.name;
            this.TaskName.Text = task.name;

            this.TaskNotes.Text = task.notes;
            this.TaskStepList.ItemsSource = task.steps;

            




        }

        private void butun_Click(object sender, RoutedEventArgs e)
        {
            List<Step> stepList = new List<Step>();
            foreach(Step step in this.TaskStepList.ItemsSource)
            {
                stepList.Add(step);
                Console.WriteLine(step.stepname);
                Console.WriteLine(step.stepstate);
                Console.WriteLine(step.priority);
            }
        }




        /* try
         {
             string stepname = Convert.ToString(this.TaskStepList.SelectedItem);
             string taskname = this.TaskName.Text;
             Step step = (Step)parser.getStep(taskname, stepname);

                 this.StepName.Text = step.stepname;
                 this.StepPriority.Text = Convert.ToString(step.priority);
                 this.StepState.Text = Convert.ToString(step.priority);

             }
         catch(NullReferenceException ex)
         {
             Console.WriteLine("OBJECT IS NULL!!!");
             Console.WriteLine(ex);
         }*/

    }
}
