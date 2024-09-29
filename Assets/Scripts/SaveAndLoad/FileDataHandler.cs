using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;


/// <summary>
/// 把GameData轉成Json檔，及把Json檔轉回GameData
/// </summary>
public class FileDataHandler
{
    private string dataDirPath = "";
    private string dataFileName = "";
    private bool useEncyption = false;
    private readonly string encyptionCodWord = "word";

    public FileDataHandler(string _dataDirPath, string _dataFileName, bool useEncyption)
    {
        this.dataDirPath = _dataDirPath;
        this.dataFileName = _dataFileName;
        this.useEncyption = useEncyption;       
    }

    public GameData Load()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                if(useEncyption)
                    {
                        dataToLoad = EncryptDecrypt(dataToLoad);
                    }


                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("嘗試將資料讀取到檔案時發生錯誤：" + fullPath + "\n" + e);
            }
        }
        return loadedData;
    }

    public void Save(GameData data) 
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            string dataToStore = JsonUtility.ToJson(data,true);

            if (useEncyption)
            {
                dataToStore = EncryptDecrypt(dataToStore);

            }

            using (FileStream stream = new FileStream(fullPath,FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch(Exception e)
        {
            Debug.LogError("嘗試將資料儲存到檔案時發生錯誤：" + fullPath + "\n" +e);

        }
    }
    
    private string EncryptDecrypt(string data)
    {
        string modifiedData = "";
        for (int i = 0; i < data.Length; i++)
        {
            modifiedData += (char)(data[i] ^ encyptionCodWord[i % encyptionCodWord.Length]);
        }
        return modifiedData;
    }
}
