namespace HyperData.DataTemplates;

public record HealthData(
    int Age,
    int RestingBP,
    int Cholesterol,
    int FastingBS,
    int MaxHR,
    float OldPeak,
    bool Sex_F,
    bool Sex_M,
    bool ChestPainType_ASY,
    bool ChestPainType_ATA,
    bool ChestPainType_NAP,
    bool ChestPainType_TA,
    bool RestingECG_LVH,
    bool RestingECG_Normal,
    bool RestingECG_ST,
    bool ExerciseAngina_N,
    bool ExerciseAngina_Y,
    bool StSlope_Down,
    bool StSlope_Flat,
    bool StSlope_Up
);