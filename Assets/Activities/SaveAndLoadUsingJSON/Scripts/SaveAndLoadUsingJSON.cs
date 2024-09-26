using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;

public class SaveAndLoadUsingJSON : MonoBehaviour
{

    #region Input Event Reader
    [Header("Input Reader")]
    [SerializeField] private InputReader _inputReader;
    #endregion

    private void Start()
    {
        #region Input Event Handler
        _inputReader.SaveEvent += HandleSave;
        _inputReader.LoadEvent += HandleLoad;
        #endregion
    }

    #region Input Event Handler
    private void HandleSave()
    {

    }

    private void HandleLoad()
    {

    }
    #endregion


    //public GameObject player;
    //public List<GameObject> items;
    //private CollectItems collectItems;
    //private void Start()
    //{
    //    collectItems = player.GetComponent<CollectItems>();
    //}
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        SaveData saveData = new SaveData()
    //        {
    //            position = player.transform.position,
    //            rotation = player.transform.rotation,
    //            collectedItems = collectItems.itemNames,
    //        };
    //        string savedData = JsonUtility.ToJson(saveData);
    //        File.WriteAllText(Application.persistentDataPath + "Savefile.txt",
    //        savedData);
    //        Debug.Log(savedData);
    //        Debug.Log(Application.persistentDataPath + "Savefile.txt");
    //    }
    //    if (Input.GetKeyDown(KeyCode.Backspace))
    //    {
    //        if (File.Exists(Application.persistentDataPath + "Savefile.txt"))
    //        {
    //            string ReadData = File.ReadAllText(Application.persistentDataPath +
    //            "Savefile.txt");
    //            SaveData saveData = JsonUtility.FromJson<SaveData>(ReadData);
    //            player.transform.position = saveData.position;
    //            player.transform.rotation = saveData.rotation;
    //            List<string> itemNames = saveData.collectedItems;
    //            for (int i = 0; i < itemNames.Count; i++)
    //            {
    //                foreach (GameObject item in items)
    //                {
    //                    if (item.name == itemNames[i])
    //                    {
    //                        item.SetActive(false);
    //                        break;
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}
    //public class SaveData
    //{
    //    public Vector3 position;
    //    public Quaternion rotation;
    //    public List<string> collectedItems;
    //}
}

