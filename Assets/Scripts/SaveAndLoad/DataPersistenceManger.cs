using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

/// <summary>
/// ��ƫ��[�ƺ޲z��
/// </summary>
public class DataPersistenceManger : MonoSingleton<DataPersistenceManger>
{
    [Header("�ɮ��x�s�t�m")]
    [SerializeField] private string fileName;

    [SerializeField] private bool useEncyption;

    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;
    public Button saveButton;  // SAVE ���s
    public Button loadButton;  // LOAD ���s

    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath,fileName, useEncyption);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();

        // �j�w���s�I���ƥ�
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
            Debug.Log("�S������ơC�N��ƪ�l�Ƭ��w�]��");
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
