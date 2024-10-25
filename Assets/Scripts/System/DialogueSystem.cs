using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// 對話系統，繼承自單例類別
public class DialogueSystem : MonoSingleton<DialogueSystem>
{
    [Header("文本")]
    public DialogueGroupData textData; // 儲存對話數據的資料類
    [SerializeField]
    private DialogueData currentDialogueData; // 當前正在顯示的對話數據
    [SerializeField]
    private List<DialogueData> dialogueDataList = new List<DialogueData>(); // 用於儲存所有的對話數據
    private List<string> dialogueContentList; // 儲存對話內容的列表
    public Text speakerNameText; // 顯示講話者名字的文本 UI
    public Text speakerContentText; // 顯示講話內容的文本 UI
    public List<Button> buttons; // 進行對話時的按鈕列表
    private int contentIndex = 0; // 當前對話內容的索引
    private int currentDialogueDataIndex; // 當前對話數據的索引
    public string speakerName; // 當前講話者的名稱
    public float textSpeed; // 每個字符顯示的速度
    private bool isTypeOver; // 標記是否已完成逐字顯示
    private bool hasText = false; // 標記是否有可用的對話文本
    private bool isButtonClicked = false; // 標記按鈕是否被點擊

    void Start()
    {
        // 為每個按鈕設置點擊事件的監聽器
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(OnButtonClick); // 將按鈕點擊事件連接到 OnButtonClick 方法
        }
    }

    void Update()
    {
        // 如果按鈕已被點擊，則不進行對話控制
        if (isButtonClicked)
        {
            return; // 提前返回，不進行後續操作
        }

        // 確保當前對話數據和對話內容的 UI 不為空
        if (currentDialogueData != null && speakerContentText != null)
        {
            // 控制對話的逐字顯示和進度
            DialogueControl(currentDialogueData.content, speakerContentText);
        }
    }

    // 設置對話數據，初始化對話內容
    public void SetDialogue(DialogueGroupData _textData)
    {
        StopAllCoroutines(); // 停止所有正在執行的協程，防止顯示混亂

        hasText = true; // 標記為有可用的對話文本
        GetTextFormFile(_textData.TextFile); // 從文件中獲取文本內容並儲存
    }

    // 開始對話，顯示第一行內容
    public void StartDialogue()
    {
        if (!hasText) // 如果沒有可用的對話文本則結束
        {
            return; // 提前返回
        }

        isTypeOver = false; // 將逐字顯示狀態設置為未完成
        currentDialogueDataIndex = 0; // 對話數據索引初始化為 0
        contentIndex = 0; // 對話中的索引初始化
        currentDialogueData = dialogueDataList[currentDialogueDataIndex]; // 獲取第一組對話數據
        ShowCurrentDialogueData(); // 顯示當前的對話資料
    }

    // 從文件中讀取文本內容，並存入對話內容列表
    private void GetTextFormFile(TextAsset file)
    {
        dialogueDataList.Clear(); // 清空現有的對話資料列表       

        string currentName = ""; // 暫存當前的講話者名字
        DialogueData _currentDialogueData = new DialogueData(); // 新的對話數據實例
        string[] lines = file.text.Split('\n'); // 將文件內容按行分割
        foreach (string line in lines)
        {
            string trimmedLine = line.Trim(); // 去掉行首尾的空白字符
            if (string.IsNullOrEmpty(trimmedLine)) // 檢查行是否為空
            {
                continue; // 如果是空行，則跳過
            }

            if (trimmedLine.StartsWith("#")) // 如果這行以 # 開頭，表示是講話者的名字
            {
                _currentDialogueData = new DialogueData(); // 創建新的對話數據實例
                currentName = trimmedLine.Substring(1); // 去掉 #，提取名字
                _currentDialogueData.characterID = NameToID(currentName); // 將名字轉換為 ID
            }
            else if (trimmedLine.StartsWith("@")) // 如果以 @ 開頭表示當前講話者的對話結束
            {
                if (_currentDialogueData.characterID != -1) // 確保有有效的講話者 ID
                {
                    dialogueDataList.Add(_currentDialogueData); // 將當前對話數據添加至列表
                }
            }
            else
            {
                // 將對話內容添加至當前對話數據的內容列表
                _currentDialogueData.content.Add(trimmedLine);
            }
        }
    }

    // 顯示講話內容，並逐字呈現
    private void ShowContentText(string str)
    {
        speakerContentText.text = string.Empty; // 清空顯示的對話內容
        StartCoroutine(TypeLine(str, speakerContentText)); // 啟動協程逐字顯示對話內容
    }

    // 顯示講話者名字
    private void ShowNameText(string str)
    {
        speakerNameText.text = str; // 設置名字文本的內容
    }

    // 控制對話顯示進度和切換邏輯
    private void DialogueControl(List<string> strings, Text text)
    {
        if (!hasText) // 如果沒有對話文本則不執行
        {
            return; // 提前返回
        }

        if (isTypeOver) // 如果逐字顯示已完成
        {
            NextDialogueData(); // 切換到下一組對話數據
            return; // 提前返回
        }

        // 檢測輸入：按下 Enter 鍵或 Z 鍵來進入下一行或顯示完整行
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z) || Input.GetMouseButtonDown(0))
        {
            if (text.text == strings[contentIndex]) // 如果當前顯示的文字與對話列表中的一致
            {
                if (contentIndex == currentDialogueData.content.Count - 1) // 如果是最後一行對話
                {
                    isTypeOver = true; // 標記對話已完成
                    return; // 提前返回
                }
                NextLine(); // 進入下一行對話
                ShowContentText(strings[contentIndex]); // 顯示下一行對話
            }
            else
            {
                StopAllCoroutines(); // 停止逐字顯示
                text.text = strings[contentIndex]; // 直接顯示完整的對話文字
                if (contentIndex == currentDialogueData.content.Count - 1) // 如果是最後一行對話
                {
                    isTypeOver = true; // 標記對話已完成
                }
            }
        }
    }

    // 切換到下一行對話
    public void NextLine()
    {
        if (contentIndex < currentDialogueData.content.Count - 1) // 如果還有下一行
        {
            contentIndex++; // 將索引加 1
        }
    }

    // 切換到下一組對話數據
    private void NextDialogueData()
    {
        contentIndex = 0; // 重置對話行的索引
        if (currentDialogueDataIndex < dialogueDataList.Count - 1) // 檢查是否有更多對話數據
        {
            currentDialogueDataIndex++; // 前往下一組對話數據
            currentDialogueData = dialogueDataList[currentDialogueDataIndex]; // 獲取新的對話數據
            ShowCurrentDialogueData(); // 顯示當前對話資料
        }
        else
        {
            MainSystem.instance.DialogueGroupOver(); // 通知主系統對話結束
            return; // 提前返回
        }
        isTypeOver = false; // 重置逐字顯示狀態
    }

    // 顯示當前對話資料的名字與內容
    private void ShowCurrentDialogueData()
    {
        ShowNameText(CharacterDataBaseSystem.instance.CharacterIDToName(currentDialogueData.characterID)); // 顯示講話者名稱
        ShowContentText(currentDialogueData.content[contentIndex]); // 顯示對話內容
    }

    // 協程逐字顯示每一行對話內容
    IEnumerator TypeLine(string line, Text text)
    {
        foreach (char _char in line.ToCharArray()) // 將行轉為字符數組
        {
            text.text += _char; // 將字符逐個添加到文本 UI 中
            yield return new WaitForSeconds(textSpeed); // 每個字符間隔 textSpeed 秒
        }
    }

    // 按鈕點擊事件的處理函數
    private void OnButtonClick()
    {
        isButtonClicked = true; // 設置標誌，阻止對話控制
    }

    // 重新啟動對話控制
    public void ResumeDialogue()
    {
        isButtonClicked = false; // 清除標誌，恢復對話控制
    }

    // 將角色名轉換為 ID
    private int NameToID(string _name)
    {
        return CharacterDataBaseSystem.instance.CharacterNameToID(_name); // 角色名轉數字
    }
}
