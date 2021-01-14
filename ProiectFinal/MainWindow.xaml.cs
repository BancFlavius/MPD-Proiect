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
using System.Data;
using System.Data.Entity;
using StudentModel;

namespace ProiectFinal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    enum ActionState
    {
        New,
        Edit,
        Delete,
        Nothing
    }


    public partial class MainWindow : Window
    {
        ActionState action = ActionState.Nothing;
        StudentEntitiesModel sem = new StudentEntitiesModel();
        System.Windows.Data.CollectionViewSource studentiViewSource;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            studentiViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("studentiViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // studentiViewSource.Source = [generic data source]
            studentiViewSource.Source = sem.Studenti.Local;
            sem.Studenti.Load();
        }
       

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.New;
            btnCancel.IsEnabled = true;
            btnSave.IsEnabled = true;
            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;
            btnPrev.IsEnabled = false;
            btnNext.IsEnabled = false;
            BindingOperations.ClearBinding(numeTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(prenumeTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(emailTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(formaInvatamantTextBox, TextBox.TextProperty);
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Edit;
            btnCancel.IsEnabled = true;
            btnSave.IsEnabled = true;
            btnNew.IsEnabled = false;
            btnDelete.IsEnabled = false;
            studentiDataGrid.IsEnabled = false;
            btnPrev.IsEnabled = false;
            btnNext.IsEnabled = false;

        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Delete;
            btnCancel.IsEnabled = true;
            btnSave.IsEnabled = true;
            btnNew.IsEnabled = false;
            btnEdit.IsEnabled = false;
            studentiDataGrid.IsEnabled = false;
            btnPrev.IsEnabled = false;
            btnNext.IsEnabled = false;
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Nothing;
            btnNew.IsEnabled = true;
            btnEdit.IsEnabled = true;
            btnDelete.IsEnabled = true;
            btnSave.IsEnabled = false;
            btnCancel.IsEnabled = false;
            btnNext.IsEnabled = true;
            btnPrev.IsEnabled = true;
            studentiDataGrid.IsEnabled = true;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("save click");
            Console.ReadLine();
            Student student = null;
            if (action == ActionState.New)
            {
                Console.WriteLine("new button");
                Console.ReadLine();
                try
                {
                    student = new Student()
                    {
                        Nume = numeTextBox.Text.Trim(),
                        Prenume = prenumeTextBox.Text.Trim(),
                        FormaInvatamant = formaInvatamantTextBox.Text.Trim(),
                        Email = emailTextBox.Text.Trim()
                    };
                    sem.Studenti.Add(student);
                    studentiViewSource.View.Refresh();
                    sem.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            if (action == ActionState.Edit)
            {
                try
                {
                    student = (Student)studentiDataGrid.SelectedItem;
                    student.Nume = numeTextBox.Text.Trim();
                    student.Prenume = prenumeTextBox.Text.Trim();
                    student.FormaInvatamant = formaInvatamantTextBox.Text.Trim();
                    student.Email = emailTextBox.Text.Trim();
                    sem.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                studentiViewSource.View.Refresh();
                studentiViewSource.View.MoveCurrentTo(student);
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    student = (Student)studentiDataGrid.SelectedItem;
                    sem.Studenti.Remove(student);
                    sem.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                studentiViewSource.View.Refresh();

            }
            btnNew.IsEnabled = true;
            btnEdit.IsEnabled = true;
            btnDelete.IsEnabled = true;
            btnSave.IsEnabled = false;
            btnCancel.IsEnabled = false;
            btnNext.IsEnabled = true;
            btnPrev.IsEnabled = true;
            studentiDataGrid.IsEnabled = true;
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            studentiViewSource.View.MoveCurrentToNext();
        }
        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            studentiViewSource.View.MoveCurrentToPrevious();
        }

        
    }
}
