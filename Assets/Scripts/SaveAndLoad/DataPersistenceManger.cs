using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

/// <summary>
/// 資料持久化管理器
/// </summary>
public class DataPersistenceManger : MonoSingleton<DataPersistenceManger>
{
    [Header("檔案儲存配置")]
    [SerializeField] private string fileName;

    [SerializeField] private bool useEncyption;

    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;
    public Button saveButton;  // SAVE 按鈕
    public Button loadButton;  // LOAD 按鈕

    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath,fileName, useEncyption);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();

        // 綁定按鈕點擊事件
        if (saveButton != null)
        {
            saveButton.onClick.AddListener(SaveGame);
        }

        if (loadButton != null)
        {
            loadButton.onClick.AddListener(LoadGame);
        }
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        this.gameData = dataHandler.Load();
        if(this.gameData == null)
        {
            Debug.Log("沒有找到資料。將資料初始化為預設值");
            NewGame();
        }
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }

    }
    public void SaveGame()
    {
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }
        dataHandler.Save(gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
