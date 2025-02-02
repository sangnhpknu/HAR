namespace HAR;

public partial class MePage : ContentPage
{
	public MePage()
	{
		InitializeComponent();
	}
    private void OnSaveButtonClicked(object sender, EventArgs e)
    {
        string name = NameEntry.Text;
        string email = EmailEntry.Text;
        string phone = PhoneEntry.Text;
        DateTime dob = DateOfBirthPicker.Date;
        string gender = GenderPicker.SelectedItem?.ToString();

        // Save or process the data (e.g., send to a server or save to local storage)
        DisplayAlert("User Information",
            $"Name: {name}\n" +
            $"Email: {email}\n" +
            $"Phone: {phone}\n" +
            $"Date of Birth: {dob.ToShortDateString()}\n" +
            $"Gender: {gender}",
            "OK");
    }
}