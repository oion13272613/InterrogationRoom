using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

/// <summary>
/// �N GameData �����ഫ�� JSON �榡���ɮסA�åB��N JSON �ɮ�Ū�^�� GameData ����
/// �䴩��ƥ[�K�\��
/// </summary>
public class FileDataHandler
{
    // �x�s�ɮ׸�ƪ��ؿ����|
    private string dataDirPath = "";
    // �x�s�ɮת��ɦW
    private string dataFileName = "";
    // �O�_�ϥΥ[�K
    private bool useEncyption = false;
    // �[�K�K�X�A�ȧ@²�� XOR �[�K���N�X
    private readonly string encyptionCodWord = "word";

    /// <summary>
    /// �غc�l�A��l���ɮ��x�s�����|�B�ɮצW�٩M�[�K�ﶵ
    /// </summary>
    /// <param name="_dataDirPath">�x�s�ɮת��ؿ����|</param>
    /// <param name="_dataFileName">�x�s�ɮת��W��</param>
    /// <param name="useEncyption">�O�_�ҥΥ[�K</param>
    public FileDataHandler(string _dataDirPath, string _dataFileName, bool useEncyption)
    {
        this.dataDirPath = _dataDirPath;
        this.dataFileName = _dataFileName;
        this.useEncyption = useEncyption;
    }

    /// <summary>
    /// ���J JSON �ɮר��ഫ�� GameData ����
    /// </summary>
    /// <returns>���J�� GameData ����A�p�G�ɮפ��s�b��Ū�����ѫh��^ null</returns>
    public GameData Load()
    {
        // �զX�����ɮ׸��|
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadedData = null;

        // �ˬd�ɮ׬O�_�s�b
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";

                // �}���ɮר�Ū�����e
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd(); // Ū������ɮפ��e
                    }
                }

                // �p�G�ҥΤF�[�K�A�h�i��ѱK�B�z
                if (useEncyption)
                {
                    dataToLoad = EncryptDecrypt(dataToLoad);
                }

                // �N JSON �榡���r����^ GameData ����
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                // ��Ū���L�{���o�Ϳ��~�ɡA��X���~�T��
                Debug.LogError("���ձN���Ū�����ɮ׮ɵo�Ϳ��~�G" + fullPath + "\n" + e);
            }
        }

        return loadedData;
    }

    /// <summary>
    /// �N GameData �����x�s�� JSON �ɮ�
    /// </summary>
    /// <param name="data">�n�x�s�� GameData ����</param>
    public void Save(GameData data)
    {
        // �զX�����ɮ׸��|
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        try
        {
            // �T�O�ؿ��s�b�A�p���s�b�h�إߥؿ�
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            // �N GameData �����ର JSON �榡���r��A�Ѽ� true ��ܮ榡�ƿ�X�]�K��\Ū�^
            string dataToStore = JsonUtility.ToJson(data, true);

            // �p�G�ҥΤF�[�K�A�h�i��[�K�B�z
            if (useEncyption)
            {
                dataToStore = EncryptDecrypt(dataToStore);
            }

            // �N JSON �r��g�J�ɮ�
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore); // �g�J�ɮ�
                }
            }
        }
        catch (Exception e)
        {
            // ���x�s�L�{���o�Ϳ��~�ɡA��X���~�T��
            Debug.LogError("���ձN����x�s���ɮ׮ɵo�Ϳ��~�G" + fullPath + "\n" + e);
        }
    }

    /// <summary>
    /// �[�K/�ѱK��k�A�ϥ�²�檺 XOR �B��ӥ[�K�θѱK�r��
    /// </summary>
    /// <param name="data">�ݭn�[�K�θѱK���r��</param>
    /// <returns>�[�K�θѱK�᪺�r��</returns>
    private string EncryptDecrypt(string data)
    {
        string modifiedData = "";

        // ��C�Ӧr���i�� XOR �B��ӥ[�K�θѱK
        for (int i = 0; i < data.Length; i++)
        {
            // �N�r���P�K�X�r�����줸�i�� XOR �B��
            modifiedData += (char)(data[i] ^ encyptionCodWord[i % encyptionCodWord.Length]);
        }

        return modifiedData;
    }
}
