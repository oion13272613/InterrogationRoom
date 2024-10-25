using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

/// <summary>
/// ��ƫ��[�ƺ޲z���A�t�d�C����ƪ����J�B�x�s�Ϊ�l�Ƭy�{
/// </summary>
public class DataPersistenceManager : MonoSingleton<DataPersistenceManager>
{
    [Header("�ɮ��x�s�t�m")]
    [SerializeField] private string fileName;          // �x�s���ɮצW��
    [SerializeField] private string dataPath;          // ����x�s�����|
    [SerializeField] private bool useEncyption;        // �O�_�ϥΥ[�K�x�s

    private GameData gameData;                         // �Ω��x�s�C�����D�n���
    private List<IDataPersistence> dataPersistenceObjects; // �Ҧ���@ IDataPersistence ����������
    private FileDataHandler dataHandler;               // �B�z���Ū�����x�s�����B�z��
    public Button saveButton;                          // SAVE ���s�A�t�d�x�s�C�����
    public Button loadButton;                          // LOAD ���s�A�t�d���J�C�����

    /// <summary>
    /// ��l�]�m�A�t�m�ɮ׸��|�B����ƫ��[�ƪ���A�ø��J�C�����
    /// </summary>
    private void Start()
    {
        // �]�w����x�s���|�����[�ʸ�Ƹ��|�]���[�ʸ�Ƨ��b���P���x�W��m���P�^
        dataPath = Application.persistentDataPath;
        // ��l�Ƥ��B�z���A�]�w�ɮ׸��|�B�W�٩M�[�K�ﶵ
        this.dataHandler = new FileDataHandler(dataPath, fileName, useEncyption);
        // �j�M�Ҧ���@ IDataPersistence ����������A���x�s�ܦC��
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        // ���J�C�����
        LoadGame();

        // �j�w���s�I���ƥ�
        if (saveButton != null)
        {
            saveButton.onClick.AddListener(SaveGame); // �j�w SAVE ���s���ƥ�
        }

        if (loadButton != null)
        {
            loadButton.onClick.AddListener(LoadGame); // �j�w LOAD ���s���ƥ�
        }
    }

    /// <summary>
    /// �إ߷s�C����ƪ���A�Ω��l�Ʃέ��m�C�����
    /// </summary>
    public void NewGame()
    {
        this.gameData = new GameData();
    }

    /// <summary>
    /// ���J�C����ơA�q�ɮפ�Ū���îM�Ψ�Ҧ���ƫ��[�ƪ���W
    /// </summary>
    public void LoadGame()
    {
        // �q���B�z�������J GameData ���
        this.gameData = dataHandler.Load();

        // �ˬd�O�_���\���J���
        if (this.gameData == null)
        {
            Debug.Log("�S������ơC�N��ƪ�l�Ƭ��w�]��");
            NewGame(); // �Y�����J���ƫh�إ߷s���C�����
        }

        // ��C�Ӹ�ƫ��[�ƪ���I�s LoadData�A�N���J��������ΦܹC�����A
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
    }

    /// <summary>
    /// �x�s�C����ơA�N�Ҧ���ƫ��[�ƪ��󪺸�Ƨ�s�� gameData �üg�J�ɮ�
    /// </summary>
    public void SaveGame()
    {
        // ��C�Ӹ�ƫ��[�ƪ���I�s SaveData�A�N�����x�s�� gameData ��
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }
        // �ϥΤ��B�z���x�s gameData ���ɮ�
        dataHandler.Save(gameData);
    }

    /// <summary>
    /// �����ε{�������ɦ۰ʩI�s�A�T�O�C����Ʀb���}�e�Q�x�s
    /// </summary>
    private void OnApplicationQuit()
    {
        SaveGame(); // �b���ε{���������x�s�C�����
    }

    /// <summary>
    /// �j�M�������Ҧ���@ IDataPersistence ����������
    /// </summary>
    /// <returns>�^�ǹ�@ IDataPersistence ����������M��</returns>
    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        // �ϥ� Linq �j�M�������Ҧ��� MonoBehaviour�A�ÿz��X��@ IDataPersistence ����������
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        // �N�z��X�������ഫ�� List �^��
        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
