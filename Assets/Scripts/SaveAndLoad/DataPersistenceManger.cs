using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

/// <summary>
/// 資料持久化管理器，負責遊戲資料的載入、儲存及初始化流程
/// </summary>
public class DataPersistenceManager : MonoSingleton<DataPersistenceManager>
{
    [Header("檔案儲存配置")]
    [SerializeField] private string fileName;          // 儲存的檔案名稱
    [SerializeField] private string dataPath;          // 資料儲存的路徑
    [SerializeField] private bool useEncyption;        // 是否使用加密儲存

    private GameData gameData;                         // 用於儲存遊戲的主要資料
    private List<IDataPersistence> dataPersistenceObjects; // 所有實作 IDataPersistence 介面的物件
    private FileDataHandler dataHandler;               // 處理資料讀取及儲存的文件處理器
    public Button saveButton;                          // SAVE 按鈕，負責儲存遊戲資料
    public Button loadButton;                          // LOAD 按鈕，負責載入遊戲資料

    /// <summary>
    /// 初始設置，配置檔案路徑、找到資料持久化物件，並載入遊戲資料
    /// </summary>
    private void Start()
    {
        // 設定資料儲存路徑為持久性資料路徑（持久性資料夾在不同平台上位置不同）
        dataPath = Application.persistentDataPath;
        // 初始化文件處理器，設定檔案路徑、名稱和加密選項
        this.dataHandler = new FileDataHandler(dataPath, fileName, useEncyption);
        // 搜尋所有實作 IDataPersistence 介面的物件，並儲存至列表中
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        // 載入遊戲資料
        LoadGame();

        // 綁定按鈕點擊事件
        if (saveButton != null)
        {
            saveButton.onClick.AddListener(SaveGame); // 綁定 SAVE 按鈕的事件
        }

        if (loadButton != null)
        {
            loadButton.onClick.AddListener(LoadGame); // 綁定 LOAD 按鈕的事件
        }
    }

    /// <summary>
    /// 建立新遊戲資料物件，用於初始化或重置遊戲資料
    /// </summary>
    public void NewGame()
    {
        this.gameData = new GameData();
    }

    /// <summary>
    /// 載入遊戲資料，從檔案中讀取並套用到所有資料持久化物件上
    /// </summary>
    public void LoadGame()
    {
        // 從文件處理器中載入 GameData 資料
        this.gameData = dataHandler.Load();

        // 檢查是否成功載入資料
        if (this.gameData == null)
        {
            Debug.Log("沒有找到資料。將資料初始化為預設值");
            NewGame(); // 若未載入到資料則建立新的遊戲資料
        }

        // 對每個資料持久化物件呼叫 LoadData，將載入的資料應用至遊戲狀態
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
    }

    /// <summary>
    /// 儲存遊戲資料，將所有資料持久化物件的資料更新至 gameData 並寫入檔案
    /// </summary>
    public void SaveGame()
    {
        // 對每個資料持久化物件呼叫 SaveData，將其資料儲存到 gameData 中
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }
        // 使用文件處理器儲存 gameData 到檔案
        dataHandler.Save(gameData);
    }

    /// <summary>
    /// 當應用程式結束時自動呼叫，確保遊戲資料在離開前被儲存
    /// </summary>
    private void OnApplicationQuit()
    {
        SaveGame(); // 在應用程式結束時儲存遊戲資料
    }

    /// <summary>
    /// 搜尋場景中所有實作 IDataPersistence 介面的物件
    /// </summary>
    /// <returns>回傳實作 IDataPersistence 介面的物件清單</returns>
    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        // 使用 Linq 搜尋場景中所有的 MonoBehaviour，並篩選出實作 IDataPersistence 介面的物件
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        // 將篩選出的物件轉換成 List 回傳
        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
