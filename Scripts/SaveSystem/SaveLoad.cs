using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveLoad : MonoBehaviour
{
    public static SaveLoad instance;

    private SaveObject saveObject = new SaveObject();

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        LoadGame();
    }

    public void NewGame()
    {
        //Default values
        saveObject.moneyAmount = 0f;
        saveObject.moneyFromLastStage = 0f;
        SaveGame();
    }

    public void LoadGame()
    {
        string fullPath = Path.Combine(Application.persistentDataPath, "save_file");

        if (File.Exists(fullPath))
        {
            string dataToLoad = "";

            using (FileStream stream = new FileStream(fullPath, FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    dataToLoad = reader.ReadToEnd();
                }
            }

            saveObject = JsonUtility.FromJson<SaveObject>(dataToLoad);
            GameManager.instance.moneyAmount = saveObject.moneyAmount;
            GameManager.instance.moneyCollectedInStage = saveObject.moneyFromLastStage;
        }
        else NewGame();
    }

    public void SaveGame()
    {
        string fullPath = Path.Combine(Application.persistentDataPath, "save_file");

        saveObject.moneyAmount = GameManager.instance.moneyAmount;
        saveObject.moneyFromLastStage = GameManager.instance.moneyCollectedInStage;
        string dataToStore = JsonUtility.ToJson(saveObject, true);

        using (FileStream stream = new FileStream(fullPath, FileMode.Create))
        {
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.Write(dataToStore);
            }
        }
    }

    private void OnApplicationQuit()
    {
        SaveGame(); //REMOVE SAVE GAME LATER! ONLY ON FINISH STAGE
    }


    /*
     * TEMP DELETE SAVE FILE AND LOAD BUTTON
     */
    public void DeleteSaveAndLoad()
    {
        string fullPath = Path.Combine(Application.persistentDataPath, "save_file");
        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
            LoadGame();
        }
    }

    private class SaveObject
    {
        public float moneyAmount;
        public float moneyFromLastStage;

        public SaveObject()
        {
            this.moneyAmount = 0;
            this.moneyFromLastStage = 0;
        }
    }
}
