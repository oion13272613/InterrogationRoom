using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

/// <summary>
/// 主系統：管理對話、時間和血量的主要邏輯
/// </summary>
public class MainSystem : MonoSingleton<MainSystem>, IDataPersistence
{
    // 物品欄視窗的 GameObject，控制物品欄的顯示與隱藏
    public GameObject inventoryWindow;
    public GameObject inventoryBotton;

    // 玩家時間狀態
    public PlayerTime playerTime = new PlayerTime();

    // 對手血量狀態
    public OpponentHeart opponentHeart = new OpponentHeart();

    // 初始玩家時間和對手血量
    public int initialPlayerTime;
    public int initialOpponentHeart;

    // 用於顯示玩家時間和對手血量的 UI 元件
    public TextMeshProUGUI TimeText;
    public TextMeshProUGUI HeartText;

    // 對話數據的列表
    public List<DialogueGroupData> textDataList;

    // 當前和下一個對話數據
    public DialogueGroupData currentDialogueGroupData;
    private DialogueGroupData nextTextData;

    // 前一個對話數據
    private DialogueGroupData previousTextData;
    private bool isTalking = false;

    // 控制是否在進行對話，並根據狀態設置按鈕的啟用或禁用
    public bool IsTalking
    {
        get { return isTalking; }
        set
        {
            isTalking = value;
            SetButtonActive(!value);
        }
    }

    /// <summary>
    /// 初始化設置
    /// </summary>
    void Start()
    {
        IsTalking = false;

        // 設置玩家和對手的初始狀態
        ShowState();

        // 隱藏物品欄視窗
        inventoryWindow.SetActive(false);

        // 設置當前對話數據為列表中的第一個元素
        currentDialogueGroupData = textDataList[0];

        // 開始執行當前對話
        RunCurentDialogue();
    }

    /// <summary>
    /// 每幀更新玩家時間和對手血量的狀態顯示
    /// </summary>
    void Update()
    {
        ShowState();
    }

    /// <summary>
    /// 顯示物品欄視窗
    /// </summary>
    public void OpenInventoryWindow()
    {
        inventoryWindow.SetActive(true);
    }

    /// <summary>
    /// 隱藏物品欄視窗
    /// </summary>
    public void CloseInventoryWindow()
    {
        inventoryWindow.SetActive(false);
    }

    /// <summary>
    /// 更新顯示的玩家時間和對手血量
    /// </summary>
    public void ShowState()
    {
        TimeText.text = string.Format("{0}/{1}", playerTime.CurrentTime.ToString(), playerTime.MaxTime.ToString());
        HeartText.text = string.Format("{0}/{1}", opponentHeart.CurrentHeart.ToString(), opponentHeart.MaxHeart.ToString());
    }

    /// <summary>
    /// 減少玩家時間
    /// </summary>
    public void ReduceTime()
    {
        playerTime.ReduceTime(1);
    }

    /// <summary>
    /// 減少對手血量
    /// </summary>
    public void ReduceHeart()
    {
        opponentHeart.ReduceHeart(1);
    }

    /// <summary>
    /// 根據物件 ID 提交對話選擇
    /// </summary>
    /// <param name="id">選擇的物件 ID</param>
    public void SubmitObjectID(int id)
    {
        CloseInventoryWindow();

        // 檢查提交的 ID 是否匹配當前對話數據的正確 ID
        if (currentDialogueGroupData.ObjectTrueID == id)
        {
            // 匹配則選擇下一個正確對話
            nextTextData = FindTextDataFromList(currentDialogueGroupData.textTrueID);
            ReduceHeart();
        }
        else
        {
            // 不匹配則選擇錯誤對話
            nextTextData = FindTextDataFromList(currentDialogueGroupData.textFalseID);
            ReduceTime();
        }

        previousTextData = currentDialogueGroupData;

        // 更新當前對話數據
        currentDialogueGroupData = nextTextData;

        // 開始新的對話
        RunCurentDialogue();
    }

    /// <summary>
    /// 根據 ID 在對話數據列表中查找對應的 DialogueGroupData
    /// </summary>
    /// <param name="id">對話數據的 ID</param>
    /// <returns>找到的對話數據</returns>
    private DialogueGroupData FindTextDataFromList(int id)
    {
        DialogueGroupData textData = null;

        foreach (DialogueGroupData item in textDataList)
        {
            if (item.textID == id)
            {
                textData = item;
                break;
            }
        }

        return textData;
    }

    /// <summary>
    /// 當前對話組結束時的處理
    /// </summary>
    public void DialogueGroupOver()
    {
        Debug.Log("對話組結束");

        if (currentDialogueGroupData == null) return;

        if (!currentDialogueGroupData.isCorrectText)
        {
            Debug.Log("重複對話");
            currentDialogueGroupData = previousTextData;
            RunCurentDialogue();
        }
        else
        {
            IsTalking = false;
        }
    }

    /// <summary>
    /// 控制物品欄按鈕的顯示狀態
    /// </summary>
    /// <param name="active">按鈕是否顯示</param>
    public void SetButtonActive(bool active)
    {
        inventoryBotton.SetActive(active);
    }

    /// <summary>
    /// 重新播放對話
    /// </summary>
    public void ReplayDialogue()
    {
        RunCurentDialogue();
    }

    /// <summary>
    /// 設置並開始執行當前對話
    /// </summary>
    private void RunCurentDialogue()
    {
        IsTalking = true;
        DialogueSystem.instance.SetDialogue(currentDialogueGroupData);
        DialogueSystem.instance.StartDialogue();
    }

    /// <summary>
    /// 從存檔中加載資料
    /// </summary>
    public void LoadData(GameData data)
    {
        this.playerTime = data.playerTime;
        this.opponentHeart = data.opponentHeart;
        ShowState();

        // 根據存檔的對話ID，找到並恢復當前對話數據
        this.currentDialogueGroupData = FindTextDataFromList(data.currentDialogueDataID);

        if (this.currentDialogueGroupData == null && textDataList.Count > 0)
        {
            Debug.LogWarning("找不到對應的對話數據，設置為默認對話");
            this.currentDialogueGroupData = textDataList[0];
        }

        RunCurentDialogue();
    }

    /// <summary>
    /// 保存資料到 GameData 中
    /// </summary>
    public void SaveData(ref GameData data)
    {
        data.playerTime = this.playerTime;
        data.opponentHeart = this.opponentHeart;

        if (currentDialogueGroupData != null)
        {
            data.currentDialogueDataID = currentDialogueGroupData.textID;
        }
    }
}
