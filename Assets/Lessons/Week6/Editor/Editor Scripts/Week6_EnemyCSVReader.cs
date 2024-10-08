using UnityEngine;
using UnityEditor;
using System.IO;
using System.Reflection;

public class Week6_EnemyCSVReader
{

    public static string enemyCSVPath = "/Lessons/Week6/Editor/CSV Files/EnemyCSV.csv";

    [MenuItem(itemName: "Utilities/Genereate Enemy SO")]
    public static void GenerateEnemy()
    {
        Week6_EnemySO enemySO;
        FieldInfo[] fields;
        FieldInfo field;
        string[] data;
        string datum;
        bool displayMessage;

        string[] enemiesData = File.ReadAllLines(Application.dataPath + enemyCSVPath);

        foreach (string enemyData in enemiesData)
        {
            enemySO = ScriptableObject.CreateInstance<Week6_EnemySO>();
            fields = typeof(Week6_EnemySO).GetFields(BindingFlags.Public | BindingFlags.Instance);

            data = enemyData.Split(',');

            for (int i = 0; i < fields.Length; i++)
            {
                field = fields[i];
                datum = data[i];

                if (field.FieldType == typeof(int))
                {
                    field.SetValue(enemySO, int.Parse(datum));
                }
                else if (field.FieldType == typeof(float))
                {
                    field.SetValue(enemySO, float.Parse(datum));
                }
                else if (field.FieldType == typeof(double))
                {
                    field.SetValue(enemySO, double.Parse(datum));
                }
                else if (field.FieldType == typeof(bool))
                {
                    field.SetValue(enemySO, bool.Parse(datum));
                }
                else if (field.FieldType == typeof(string))
                {
                    field.SetValue(enemySO, datum);
                }
            }

            displayMessage = EditorUtility.DisplayDialog(
                "Save Scriptable Object",
                $"Are you sure you want to save {enemySO.enemyName} to a Scriptable Object?",
                "Yes",
                "No");

            if (displayMessage)
            {
                AssetDatabase.CreateAsset(enemySO, $"Assets/Lessons/Week6/SO/Enemies/{enemySO.enemyName}.asset");
                AssetDatabase.SaveAssets();
            }
        }
    }

}
