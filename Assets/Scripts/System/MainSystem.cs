using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

/// <summary>
/// 總管理器：對話、時間、血量
/// </summary>
public class MainSystem : MonoSingleton<MainSystem>
{
    // 物品欄視窗的 GameObject，控制物品欄的顯示與隱藏
    public GameObject inventoryWindow;
    public GameObject inventoryBotton;

    // 玩家時間狀態
    public PlayerTime playerTime = new PlayerTime();

    // 對手血量狀態
    public OpponentHeart opponentHeart = new OpponentHeart();

    
    public int initialPlayerTime;// 初始玩家時間
    public int initialOpponentHeart;// 初始對手血量

    
    public TextMeshProUGUI TimeText;// 用於顯示玩家時間的 UI 元件
    public TextMeshProUGUI HeartText;// 用於顯示對手血量的 UI 元件

    
    public List<DialogueGroupData> textDataList;// 存放對話數據的列表

   
    public DialogueGroupData currentDialogueGroupData; // 當前對話數據
    private DialogueGroupData nextTextData; //下一個對話數據

    private DialogueGroupData previousTextData;//前一個TextData
    private bool isTalking = false;
    public bool IsTalking 
    { 
        get { return isTalking; }
        set
        {
            isTalking = value;
            SetButtonActive(!value);
        }
    }

    void Start()
    {
        IsTalking = false;

        playerTime.SetTime(initialPlayerTime); // 設置玩家的初始時間
        opponentHeart.SetHeart(initialOpponentHeart); // 設置對手的初始血量

        // 隱藏物品欄視窗
        inventoryWindow.SetActive(false);

        // 設置當前對話數據為列表中的第一個元素
        currentDialogueGroupData = textDataList[0];

        // 開始執行當前對話
        RunCurentDialogue();
    }

    // 每一幀更新
    void Update()
    {
        // 顯示玩家時間和對手血量的狀態
        ShowState();
    }

    // 顯示物品欄視窗
    public void OpenInventoryWindow()
    {
        inventoryWindow.SetActive(true);
    }

    // 隱藏物品欄視窗
    public void CloseInventoryWindow()
    {
        inventoryWindow.SetActive(false);
    }

    // 更新顯示的玩家時間和對手血量
    public void ShowState()
    {
        // 更新 TimeText 來顯示當前時間/最大時間
        TimeText.text = string.Format("{0}/{1}", playerTime.CurrentTime.ToString(), playerTime.MaxTime.ToString());

        // 更新 HeartText 來顯示當前血量/最大血量
        HeartText.text = string.Format("{0}/{1}", opponentHeart.CurrentHeart.ToString(), opponentHeart.MaxHeart.ToString());
    }

    // 減少玩家時間
    public void ReduceTime()
    {
        playerTime.ReduceTime(1);
    }

    public void ReduceHeart()
    {
        opponentHeart.ReduceHeart(1);
    }

    // 根據物件 ID 提交對話選擇
    public void SubmitObjectID(int id)
    {
        CloseInventoryWindow();
        // 檢查提交的 ID 是否匹配當前對話數據的正確 ID
        if (currentDialogueGroupData.ObjectTrueID == id)
        {
            // 如果匹配，找到下一個對話數據
            nextTextData = FindTextDataFromList(currentDialogueGroupData.textTrueID);
            ReduceHeart();
        }
        else
        {
            // 如果不匹配，找到另一個對話數據
            nextTextData = FindTextDataFromList(currentDialogueGroupData.textFalseID);
            ReduceTime();
        }

        previousTextData = currentDialogueGroupData;

        // 設置當前對話數據為找到的下一個對話數據
        currentDialogueGroupData = nextTextData;

        // 開始執行新的對話
        RunCurentDialogue();
    }

    // 根據 ID 在對話數據列表中查找對應的 DialogueGroupData
    private DialogueGroupData FindTextDataFromList(int id)
    {
        DialogueGroupData textData = null;

        // 遍歷對話數據列表
        foreach (DialogueGroupData item in textDataList)
        {
            // 找到匹配的對話數據
            if (item.textID == id)
            {
                textData = item;
                break; // 跳出循環
            }
        }

        return textData;
    }

    //文本已結束(由DialogueSystem通知)
    
    public void DialogueGroupOver() 
    {
        Debug.Log("對話組結束");        
        if(currentDialogueGroupData == null)
        {
            return;
        }
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
        //if (currentDialogueGroupData.isCorrectText)
        //{
        //    currentDialogueGroupData = FindTextDataFromList(currentDialogueGroupData.textTrueID);
        //    RunCurentDialogue();
        //}     
    }

    public void SetButtonActive(bool active)
    {
        inventoryBotton.SetActive(active);      
    }

    public void ReplayDialogue()
    {
        RunCurentDialogue();
    }


    // 設置並開始執行當前對話
    private void RunCurentDialogue()
    {
        IsTalking = true;
        DialogueSystem.instance.SetDialogue(currentDialogueGroupData);
        DialogueSystem.instance.StartDialogue();
    }
}
