namespace HAR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;

public partial class ReportPage : ContentPage
{
    public ReportPage()
    {
        InitializeComponent();
        BindingContext = new ReportViewModel();
    }

    private void OnDateSelected(object sender, DateChangedEventArgs e)
    {
        var viewModel = BindingContext as ReportViewModel;
        viewModel?.UpdateExercisesForDate(e.NewDate);
    }

    public class Exercise
    {
        public string ExerciseName { get; set; }
        public string Duration { get; set; }
    }

    public class ChartDataModel
    {
        public string Date { get; set; }
        public int ExercisesCount { get; set; }
        public double TimeSpent { get; set; } // Time in minutes
    }

    public class ReportViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public List<ChartDataModel> ChartData { get; set; }
        public List<Exercise> ExercisesForSelectedDate { get; set; }
        public DateTime SelectedDate { get; set; }

        public ReportViewModel()
        {
            // Initialize chart data
            ChartData = new List<ChartDataModel>
            {
                new ChartDataModel { Date = "2025-01-25", ExercisesCount = 5, TimeSpent = 30 },
                new ChartDataModel { Date = "2025-01-26", ExercisesCount = 6, TimeSpent = 50 },
                new ChartDataModel { Date = "2025-01-27", ExercisesCount = 7, TimeSpent = 100 },
                new ChartDataModel { Date = "2025-01-28", ExercisesCount = 7, TimeSpent = 100 },
            };

            // Initialize exercises for the first date
            SelectedDate = DateTime.Parse("2025-01-25");
            UpdateExercisesForDate(SelectedDate);
        }

        public void UpdateExercisesForDate(DateTime date)
        {
            var dateString = date.ToString("yyyy-MM-dd");

            // Example logic to populate exercises based on date
            ExercisesForSelectedDate = dateString switch
            {
                "2025-01-25" => new List<Exercise>
                {
                    new Exercise { ExerciseName = "Pushup", Duration = "10 mins" },
                    new Exercise { ExerciseName = "Squat", Duration = "15 mins" },
                    new Exercise { ExerciseName = "Plank", Duration = "5 mins" }
                },
                "2025-01-26" => new List<Exercise>
                {
                    new Exercise { ExerciseName = "Bicep Curl", Duration = "15 mins" },
                    new Exercise { ExerciseName = "Lunges", Duration = "20 mins" },
                    new Exercise { ExerciseName = "Burpees", Duration = "10 mins" }
                },
                "2025-01-27" => new List<Exercise>
                {
                    new Exercise { ExerciseName = "Deadlift", Duration = "30 mins" },
                    new Exercise { ExerciseName = "Bench Press", Duration = "20 mins" }
                },
                "2025-01-28" => new List<Exercise>
                {
                    new Exercise { ExerciseName = "Deadlift", Duration = "30 mins" },
                    new Exercise { ExerciseName = "Bench Press", Duration = "20 mins" }
                },
                _ => new List<Exercise>()
            };

            // Notify the UI of property changes
            OnPropertyChanged(nameof(ExercisesForSelectedDate));
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}