namespace HyperData
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void HeartAttackBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HeartAttackPage());
        }
    }
}