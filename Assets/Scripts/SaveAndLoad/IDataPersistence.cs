using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 定義資料持久化的介面，用於讀取和儲存遊戲資料 (GameData)。
/// 實作此介面的類別應提供具體的資料載入與儲存邏輯。
/// </summary>
public interface IDataPersistence
{
    /// <summary>
    /// 載入資料至 GameData 物件
    /// </summary>
    /// <param name="data">要載入的 GameData 物件，包含遊戲中的資料狀態</param>
    /// <remarks>
    /// 此方法應實作將資料讀取並載入至提供的 GameData 物件中的邏輯，
    /// 例如從檔案、數據庫或雲端讀取資料。
    /// </remarks>
    void LoadData(GameData data);

    /// <summary>
    /// 儲存 GameData 物件的資料
    /// </summary>
    /// <param name="data">要儲存的 GameData 物件，以 ref 傳遞參考型別</param>
    /// <remarks>
    /// 此方法應實作將 GameData 物件中的資料進行儲存的邏輯，
    /// 例如將資料寫入檔案、數據庫或上傳至雲端。
    /// 使用 ref 參數允許在儲存過程中對 GameData 進行修改（如更新時間戳）。
    /// </remarks>
    void SaveData(ref GameData data);
}

