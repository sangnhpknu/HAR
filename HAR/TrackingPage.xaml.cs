using System.Timers;

namespace HAR;

public partial class TrackingPage : ContentPage
{
    private System.Timers.Timer _exerciseTimer;
    private TimeSpan _exerciseDuration;

    public TrackingPage()
    {
        InitializeComponent();

        // Initialize Timer
        _exerciseDuration = TimeSpan.Zero;
        _exerciseTimer = new System.Timers.Timer(1000); // 1-second interval
        _exerciseTimer.Elapsed += OnTimerElapsed;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _exerciseTimer.Start();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _exerciseTimer.Stop();
    }

    private void OnTimerElapsed(object sender, ElapsedEventArgs e)
    {
        _exerciseDuration = _exerciseDuration.Add(TimeSpan.FromSeconds(1));
        MainThread.BeginInvokeOnMainThread(() =>
        {
            TimerLabel.Text = _exerciseDuration.ToString(@"hh\:mm\:ss");
        });
    }

    private void OnUpdateDataClicked(object sender, EventArgs e)
    {
        // Update exercise data (demo values)
        ExerciseNameLabel.Text = "Bicep Curl";
        PerformanceBar.Progress = 0.85; // Example muscle performance activation
    }
}