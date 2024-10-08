using UnityEngine;
using UnityEditor;
using System.IO;
using System.Reflection;

public class WeaponCSVReader
{

    public static string weaponCSVPath = "/Activities/SO_Creation/Editor/CSV Files/WeaponCSV.csv";

    [MenuItem(itemName: "Utilities/Generate Weapon SO")]
    public static void GenerateWeapon()
    {
        WeaponSO weaponSO;
        FieldInfo[] fields;
        FieldInfo field;
        string[] data;
        string datum;
        bool displayMessage;

        string[] weaponsData = File.ReadAllLines(Application.dataPath + weaponCSVPath);

        foreach (string weaponData in weaponsData)
        {
            weaponSO = ScriptableObject.CreateInstance<WeaponSO>();
            fields = typeof(WeaponSO).GetFields(BindingFlags.Public | BindingFlags.Instance);

            data = weaponData.Split(',');

            for (int i = 0; i < fields.Length; i++)
            {
                field = fields[i];
                datum = data[i];

                if (field.FieldType == typeof(int))
                {
                    field.SetValue(weaponSO, int.Parse(datum));
                }
                else if (field.FieldType == typeof(float))
                {
                    field.SetValue(weaponSO, float.Parse(datum));
                }
                else if (field.FieldType == typeof(double))
                {
                    field.SetValue(weaponSO, double.Parse(datum));
                }
                else if (field.FieldType == typeof(bool))
                {
                    field.SetValue(weaponSO, bool.Parse(datum));
                }
                else if (field.FieldType == typeof(string))
                {
                    field.SetValue(weaponSO, datum);
                }
            }

            displayMessage = EditorUtility.DisplayDialog(
                "Save Scriptable Object",
                $"Are you sure you want to save {weaponSO.weaponName} to a Scriptable Object?",
                "Yes",
                "No");

            if (displayMessage)
            {
                AssetDatabase.CreateAsset(weaponSO, $"Assets/Activities/SO_Creation/SO/Weapons/{weaponSO.weaponName}.asset");
                AssetDatabase.SaveAssets();
            }
        }
    }

}
