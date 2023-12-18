using System.Diagnostics;

namespace HyperData;

public partial class HeartAttackPage : ContentPage
{
    public HeartAttackPage()
    {
        InitializeComponent();
    }

    private void Submit(object sender, EventArgs e)
    {
        string age = AgeEntry.Text;
        string sex = SexEntry.Text;
        string chestPainType = ChestPainTypeEntry.Text;
        string restingBP = RestingBPEntry.Text;
        string cholesterol = CholesterolEntry.Text;
        string fastingBS = FastingBSEntry.Text;
        string restingECG = RestingECGEntry.Text;
        string maxHR = MaxHREntry.Text;
        string exerciseAngina = ExerciseAnginaEntry.Text;
        string oldpeak = OldpeakEntry.Text;
        string stSlope = STSlopeEntry.Text;

        ProcessStartInfo startInfo = new()
        {
            FileName = "python",
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
            Arguments = $"model.py {age} {sex} {chestPainType} {restingBP} {cholesterol} {fastingBS} {restingECG} {maxHR} {exerciseAngina} {oldpeak} {stSlope}"
        };

        // Create and start the process
        using Process process = new() { StartInfo = startInfo };

        process.Start();

        string output = process.StandardOutput.ReadToEnd();
        string error = process.StandardError.ReadToEnd();

        // Wait for the process to exit
        process.WaitForExit();

        PredictionLbl.Text = output == "0" ? "No Heart disease predicted" : output == "1" ? "There is a probability of heart disease" : error;
    }
}