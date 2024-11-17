using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 主系統：管理對話、時間和血量的主要邏輯
/// </summary>
public class MainSystem : MonoSingleton<MainSystem>, IDataPersistence
{
    // 物品欄視窗的 GameObject，控制物品欄的顯示與隱藏
    public GameObject inventoryWindow;
    public GameObject inventoryButton;

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
    [SerializeField]
    public List<ObjectSlelectIT> interrogationTurns = new List<ObjectSlelectIT>();

    // 當前和下一個對話數據
    public ObjectSlelectIT currentTurn;
    private ObjectSlelectIT nextTurn;

    // 前一個對話數據
    private ObjectSlelectIT previousTurn;
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

        //設置當前對話數據為列表中的第一個元素
        //if (interrogationTurns.Count > 0)
        //{
        //    currentTurn = interrogationTurns[0];
        //    RunCurrentDialogue();
        //}
        //else
        //{
        //    Debug.LogWarning("對話列表為空");
        //}

            IsTalking = true;
            DialogueSystem.instance.SetDialogue(IT_DialogueData_CSVReader.instance.CSVToIT(1).beforeDialogue);
            DialogueSystem.instance.StartDialogue();
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
        TimeText.text = string.Format("{0}/{1}", playerTime.CurrentTime, playerTime.MaxTime);
        HeartText.text = string.Format("{0}/{1}", opponentHeart.CurrentHeart, opponentHeart.MaxHeart);
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

        // 檢查提交的 ID 是否匹配當前的成功對話
        if (currentTurn.objectID == id)
        {
            nextTurn.beforeDialogue = currentTurn.successDialogue;
            ReduceHeart();
        }
        else
        {
            nextTurn.beforeDialogue = currentTurn.failDialogue;
            ReduceTime();
        }

        previousTurn = currentTurn;
        currentTurn = nextTurn;

        RunCurrentDialogue();
    }

    /// <summary>
    /// 根據 ID 在對話數據列表中查找對應的 ObjectSlelectIT
    /// </summary>
    private ObjectSlelectIT FindTurnFromList(int id)
    {
        foreach (ObjectSlelectIT item in interrogationTurns)
        {
            if (item.turnID == id)
                return item;
        }
        return null;
    }

    /// <summary>
    /// 當前對話組結束時的處理
    /// </summary>
    public void DialogueTurnOver()
    {
        Debug.Log("對話組結束");

        if (currentTurn == null) return;

        if (currentTurn.beforeDialogue == currentTurn.failDialogue)
        {
            Debug.Log("回答錯誤，重複之前的對話");
            currentTurn = previousTurn;
            RunCurrentDialogue();
        }
        else
        {
            IsTalking = false;
            SetButtonActive(true);
        }
    }

    /// <summary>
    /// 控制物品欄按鈕的顯示狀態
    /// </summary>
    public void SetButtonActive(bool active)
    {
        inventoryButton.SetActive(active);
    }

    /// <summary>
    /// 重新播放對話
    /// </summary>
    public void ReplayDialogue()
    {
        RunCurrentDialogue();
    }

    /// <summary>
    /// 設置並開始執行當前對話
    /// </summary>
    private void RunCurrentDialogue()
    {
        if (currentTurn == null || currentTurn.beforeDialogue == null)
        {
            Debug.LogError("當前對話或其數據未初始化！");
            return;
        }
        IsTalking = true;
        DialogueSystem.instance.SetDialogue(currentTurn.beforeDialogue);
        DialogueSystem.instance.StartDialogue();
    }

    /// <summary>
    /// 從存檔中加載資料
    /// </summary>
    public void LoadData(GameData data)
    {
        playerTime = data.playerTime;
        opponentHeart = data.opponentHeart;
        ShowState();

        currentTurn = FindTurnFromList(data.currentDialogueDataID);

        if (currentTurn == null && interrogationTurns.Count > 0)
        {
            Debug.LogWarning("找不到對應的對話數據，設置為默認對話");
            currentTurn = interrogationTurns[0];
        }

        RunCurrentDialogue();
    }

    /// <summary>
    /// 保存資料到 GameData 中
    /// </summary>
    public void SaveData(ref GameData data)
    {
        data.playerTime = playerTime;
        data.opponentHeart = opponentHeart;

        if (currentTurn != null)
        {
            data.currentDialogueDataID = currentTurn.turnID;
        }
    }
}
