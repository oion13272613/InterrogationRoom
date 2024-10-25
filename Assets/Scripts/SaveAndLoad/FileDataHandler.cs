using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

/// <summary>
/// 將 GameData 物件轉換為 JSON 格式的檔案，並且能將 JSON 檔案讀回成 GameData 物件
/// 支援資料加密功能
/// </summary>
public class FileDataHandler
{
    // 儲存檔案資料的目錄路徑
    private string dataDirPath = "";
    // 儲存檔案的檔名
    private string dataFileName = "";
    // 是否使用加密
    private bool useEncyption = false;
    // 加密密碼，僅作簡單 XOR 加密的代碼
    private readonly string encyptionCodWord = "word";

    /// <summary>
    /// 建構子，初始化檔案儲存的路徑、檔案名稱和加密選項
    /// </summary>
    /// <param name="_dataDirPath">儲存檔案的目錄路徑</param>
    /// <param name="_dataFileName">儲存檔案的名稱</param>
    /// <param name="useEncyption">是否啟用加密</param>
    public FileDataHandler(string _dataDirPath, string _dataFileName, bool useEncyption)
    {
        this.dataDirPath = _dataDirPath;
        this.dataFileName = _dataFileName;
        this.useEncyption = useEncyption;
    }

    /// <summary>
    /// 載入 JSON 檔案並轉換為 GameData 物件
    /// </summary>
    /// <returns>載入的 GameData 物件，如果檔案不存在或讀取失敗則返回 null</returns>
    public GameData Load()
    {
        // 組合完整檔案路徑
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadedData = null;

        // 檢查檔案是否存在
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";

                // 開啟檔案並讀取內容
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd(); // 讀取整個檔案內容
                    }
                }

                // 如果啟用了加密，則進行解密處理
                if (useEncyption)
                {
                    dataToLoad = EncryptDecrypt(dataToLoad);
                }

                // 將 JSON 格式的字串轉回 GameData 物件
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                // 當讀取過程中發生錯誤時，輸出錯誤訊息
                Debug.LogError("嘗試將資料讀取到檔案時發生錯誤：" + fullPath + "\n" + e);
            }
        }

        return loadedData;
    }

    /// <summary>
    /// 將 GameData 物件儲存為 JSON 檔案
    /// </summary>
    /// <param name="data">要儲存的 GameData 物件</param>
    public void Save(GameData data)
    {
        // 組合完整檔案路徑
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        try
        {
            // 確保目錄存在，如不存在則建立目錄
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            // 將 GameData 物件轉為 JSON 格式的字串，參數 true 表示格式化輸出（便於閱讀）
            string dataToStore = JsonUtility.ToJson(data, true);

            // 如果啟用了加密，則進行加密處理
            if (useEncyption)
            {
                dataToStore = EncryptDecrypt(dataToStore);
            }

            // 將 JSON 字串寫入檔案
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore); // 寫入檔案
                }
            }
        }
        catch (Exception e)
        {
            // 當儲存過程中發生錯誤時，輸出錯誤訊息
            Debug.LogError("嘗試將資料儲存到檔案時發生錯誤：" + fullPath + "\n" + e);
        }
    }

    /// <summary>
    /// 加密/解密方法，使用簡單的 XOR 運算來加密或解密字串
    /// </summary>
    /// <param name="data">需要加密或解密的字串</param>
    /// <returns>加密或解密後的字串</returns>
    private string EncryptDecrypt(string data)
    {
        string modifiedData = "";

        // 對每個字元進行 XOR 運算來加密或解密
        for (int i = 0; i < data.Length; i++)
        {
            // 將字元與密碼字元按位元進行 XOR 運算
            modifiedData += (char)(data[i] ^ encyptionCodWord[i % encyptionCodWord.Length]);
        }

        return modifiedData;
    }
}
