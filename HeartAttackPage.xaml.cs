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
        string sex = SexEntry.Text.ToUpper();
        string chestPainType = ChestPainTypeEntry.Text;
        string restingBP = RestingBPEntry.Text;
        string cholesterol = CholesterolEntry.Text;

        string fastingBS;
        if (!float.TryParse(FastingBSEntry.Text, out float fastingBSValue))
        {
            DisplayAlert("Error", "Please enter a valid fasting blood sugar.", "OK");
            return;
        }
        fastingBS = fastingBSValue > 120 ? "1" : "0";

        string restingECG = RestingECGEntry.Text;
        string maxHR = MaxHREntry.Text;
        string exerciseAngina = ExerciseAnginaEntry.Text;
        string oldpeak = OldpeakEntry.Text;
        string stSlope = STSlopeEntry.Text;

        // Check for reasonableness
        if (string.IsNullOrWhiteSpace(age) || !int.TryParse(age, out int ageValue) || ageValue < 0)
        {
            // Handle the case where age is not reasonable
            DisplayAlert("Error", "Please enter a valid age.", "OK");
            return;
        }
        if (string.IsNullOrWhiteSpace(sex) || (sex.ToUpper() != "M" && sex.ToUpper() != "F"))
        {
            // Handle the case where sex is not reasonable
            DisplayAlert("Error", "Please enter a valid sex (M or F).", "OK");
            return;
        }
        if (string.IsNullOrWhiteSpace(chestPainType))
        {
            // Handle the case where chestPainType is not reasonable
            DisplayAlert("Error", "Please enter a valid chest pain type.", "OK");
            return;
        }
        if (string.IsNullOrWhiteSpace(restingBP) || !float.TryParse(restingBP, out _))
        {
            // Handle the case where restingBP is not reasonable
            DisplayAlert("Error", "Please enter a valid resting blood pressure.", "OK");
            return;
        }
        if (string.IsNullOrWhiteSpace(cholesterol) || !float.TryParse(cholesterol, out _))
        {
            // Handle the case where cholesterol is not reasonable
            DisplayAlert("Error", "Please enter a valid serum cholesterol.", "OK");
            return;
        }
        if (string.IsNullOrWhiteSpace(restingECG))
        {
            // Handle the case where restingECG is not reasonable
            DisplayAlert("Error", "Please enter valid resting ECG results.", "OK");
            return;
        }
        if (string.IsNullOrWhiteSpace(maxHR) || !int.TryParse(maxHR, out int maxHRValue) || maxHRValue < 60 || maxHRValue > 202)
        {
            // Handle the case where maxHR is not reasonable
            DisplayAlert("Error", "Please enter a valid maximum heart rate (between 60 and 202).", "OK");
            return;
        }
        if (string.IsNullOrWhiteSpace(exerciseAngina) || (exerciseAngina.ToUpper() != "Y" && exerciseAngina.ToUpper() != "N"))
        {
            // Handle the case where exerciseAngina is not reasonable
            DisplayAlert("Error", "Please enter a valid exercise-induced angina (Y or N).", "OK");
            return;
        }
        if (string.IsNullOrWhiteSpace(oldpeak) || !double.TryParse(oldpeak, out _))
        {
            // Handle the case where oldpeak is not reasonable
            DisplayAlert("Error", "Please enter a valid oldpeak value.", "OK");
            return;
        }
        if (string.IsNullOrWhiteSpace(stSlope))
        {
            // Handle the case where stSlope is not reasonable
            DisplayAlert("Error", "Please enter a valid ST slope.", "OK");
            return;
        }

        ProcessStartInfo startInfo = new()
        {
            FileName = "python",
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
            Arguments = $"[pathToFile.py] {age} {sex == "M"} {chestPainType} {restingBP} {cholesterol} {fastingBS} {restingECG} {maxHR} {exerciseAngina} {oldpeak} {stSlope}"
        };

        // Create and start the process
        using Process process = new() { StartInfo = startInfo };

        process.Start();

        string output = process.StandardOutput.ReadToEnd();
        string error = process.StandardError.ReadToEnd();

        // Wait for the process to exit
        process.WaitForExit();

        PredictionLbl.Text = output.Trim() == "[0]" ? "No Heart disease predicted" : output.Trim() == "[1]" ? "There is a probability of heart disease" : error;
    }
}