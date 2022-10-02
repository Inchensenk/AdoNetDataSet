using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNetDataSet
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public DelegateCommand ClickCommand { get; } = null!;
        public ObservableCollection<Person> People
        {
            get;
            set;
        }
        public MainWindowViewModel()
        {
            ClickCommand = new(OnClickAsync);

            People = new();
        }

        private /*async*/ void OnClickAsync()
        {
            string connectionString = "Server=PIXEL; Database=AdoExplanationDb; Trusted_Connection=True; Encrypt=false";

            string sql = "SELECT * FROM Persons";

            using (SqlConnection connection = new(connectionString))
            {
                SqlDataAdapter dataAdapter = new(sql, connection);

                DataSet dataSet = new();

                dataAdapter.Fill(dataSet);

                var table = dataSet.Tables[0];

                foreach (DataRow row in table.Rows)
                {
                    var person = new Person();

                    person.Name = row["Name"] as string;

                    var age = row["Age"] as int?;
                    person.Age = age.HasValue ? age.Value : 0;

                    var height = row["Age"] as float?;
                    person.Height = height.HasValue ? height.Value : 0;

                    People.Add(person);
                }
               /* //ожидает в текущем потоке пока выполнится другой поток
                await connection.OpenAsync();*/
            }
        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
