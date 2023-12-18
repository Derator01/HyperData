import sys
import joblib
import pandas as pd

args = sys.argv

loaded_model = joblib.load(r"[pathToModel.pkl]")

args = {'age': int(args[1]), 'sex': args[2], 'chestpaintype': args[3], 'restingbp': int(args[4]), 'cholesterol': int(args[5]), 'fastingbs': int(args[6]), 'restingecg': args[7], 'maxhr': int(args[8]), 'exerciseangina': args[9], 'oldpeak': float(args[10]), 'st_slope': args[11]}
unique_values = {
    'sex': ['F', 'M'],
    'chestpaintype': ['ASY', 'ATA', 'NAP', 'TA'],
    'restingecg': ['LVH', 'Normal', 'ST'],
    'exerciseangina': ['N', 'Y'],
    'st_slope': ['Down', 'Flat', 'Up']
}
dummy_frames = []
for col, vals in unique_values.items():
    dummy_frame = pd.DataFrame({f"{col}_{val}": [0] for val in vals})
    dummy_frames.append(dummy_frame)
df_args = pd.DataFrame(args, index=[0])
df = pd.concat([df_args] + dummy_frames, axis=1)
for col, val in args.items():
    if col in unique_values:
        dummy_col = f"{col}_{val}"
        df[dummy_col] = 1
df = df.drop(list(unique_values.keys()), axis=1)

# print(df.columns)

predictions_new_data = loaded_model.predict(df)

print(predictions_new_data)