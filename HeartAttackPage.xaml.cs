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
        SubmitBtn_Clicked(null, null);
    }

    private void ChestPainBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        SubmitBtn_Clicked(null, null);
    }

    private void SubmitBtn_Clicked(object sender, EventArgs e)
    {
        HealthData healthData;
        try
        {
            healthData = new(ChestPainBox.IsChecked);
        }
        catch (ArgumentNullException)
        {
            PredictionLbl.Text = "One or more boxes is empty.";
            return;
        }
        catch (ArgumentException ex)
        {
            PredictionLbl.Text = ex.Message;
            return;
        }

        MLContext context = new();

        ITransformer model;
        try
        {
            model = context.Model.Load(File.Open("", FileMode.Open), out _);
        }
        catch (Exception ex)
        {
            PredictionLbl.Text = ex.Message;
            return;
        }

        var predictionEngine = context.Model.CreatePredictionEngine<HealthData, OutData>(model);

        var prediction = predictionEngine.Predict(healthData);

        PredictionLbl.Text = prediction.Moron.ToString();
    }
}