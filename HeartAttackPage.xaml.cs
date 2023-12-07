using HyperData.DataTemplates;
using Microsoft.ML;

namespace HyperData;

public partial class HeartAttackPage : ContentPage
{
    public HeartAttackPage()
    {
        InitializeComponent();
    }

    private void FirstEtr_Completed(object sender, EventArgs e)
    {

    }

    private void ChestPainBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {

    }

    private void SubmitBtn_Clicked(object sender, EventArgs e)
    {
        MLContext context = new();
        ITransformer model = context.Model.Load(File.Open("", FileMode.Open), out _);
        var predictionEngine = context.Model.CreatePredictionEngine<HealthData, OutData>(model);


        HealthData healthData = new(ChestPainBox.IsChecked);

        var prediction = predictionEngine.Predict(healthData);

        PredictionLbl.Text = prediction.Moron.ToString();
    }
}