using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueSystem : MonoSingleton<DialogueSystem>
{
    [Header("文本")]
    public DialogueGroupData textData; // 用來儲存對話的數據類
    private List<string> dialogueContentList; // 用來儲存對話內容
    public Text speakerNameText; // 顯示講話者名字的文本 UI
    public Text speakerContentText; // 顯示講話內容的文本 UI
    //public List<string> speakerContentTextList = new List<string>(); // 存儲對話內容的列表
    //public List<string> speakerNameList = new List<string>(); // 存儲講話者名字的列表
    [SerializeField]
    private List<DialogueData> dialogueDataList = new List<DialogueData>();
    private int contentIndex = 0; // 對話內容中的索引
    public string speakerName; // 當前講話者的名字
    private int currentDialogueDataIndex; // 當前對話的索引，用於追踪對話進度
    public float textSpeed; // 字符顯示的速度
    private bool isTypeOver; // 是否已完成逐字顯示
    private bool hasText = false; // 是否有對話文本可用
    public List<Button> buttons; // 多個按鈕
    private bool isButtonClicked = false; // 是否點擊了按鈕
    [SerializeField]
    private DialogueData currentDialogueData; // 儲存當前對話的數據

    void Start()
    {
        // 設置每個按鈕的事件監聽器
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(OnButtonClick);
        }
    }

    void Update()
    {
        // 檢查是否點擊了按鈕
        if (isButtonClicked)
        {
            return; // 如果點擊了按鈕，則不進行對話控制
        }
        if (currentDialogueData != null && speakerContentText!= null)
        {
            // 每幀檢測並控制對話顯示和進度
            DialogueControl(currentDialogueData.content, speakerContentText);  
        }

    
    }

    // 設置對話數據，初始化對話內容
    public void SetDialogue(DialogueGroupData _textData)
    {
        StopAllCoroutines(); // 停止所有正在執行的協程，防止干擾顯示

        hasText = true; // 設置為有可用的對話文本
        //dialogueDataList = _textData.TextFile.content; // 獲取對話文件
        GetTextFormFile(_textData.TextFile); // 從文件中獲取文本內容

        // 設置講話者名稱
        //speakerName = _textData.TextFile.character.characterName;
        //ShowNameText(speakerName); // 顯示講話者名字
    }

    // 開始對話，顯示第一行內容
    public void StartDialogue()
    {
        if (!hasText) // 如果沒有對話文本則不執行
        {
            return;
        }
        isTypeOver = false; // 將逐字顯示狀態設置為未完成
        currentDialogueDataIndex = 0; // 對話索引初始化為 0
        contentIndex = 0; // 對話中的索引
        currentDialogueData = dialogueDataList[currentDialogueDataIndex];
        ShowCurrentDialogueData();
    }

    // 從文件中讀取文本內容，並存入對話內容列表
    private void GetTextFormFile(TextAsset file)
    {
        dialogueDataList.Clear(); // 清空現有的對話列表       

        string currentName = ""; // 用來儲存當前的講話者名字
        DialogueData _currentDialogueData = new DialogueData();
        string[] lines = file.text.Split('\n');
        foreach (string line in lines)
        {
            string trimmedLine = line.Trim(); // 去掉行首尾的空白
            //IsNullOrEmpty(value) value為 null 或空字符串""返回 true
            //如果 value 不是 null 且包含至少一個字符返回 false
            if (string.IsNullOrEmpty(trimmedLine))
            {
                continue;         
            }

            if (trimmedLine.StartsWith("#")) // 如果這行以 # 開頭，表示是講話者名字
            {                    
                _currentDialogueData = new DialogueData();
                currentName = trimmedLine.Substring(1); // 去掉 # 並提取名字
                Debug.Log(currentName);
                _currentDialogueData.characterID = NameToID(currentName);
                Debug.Log(NameToID(currentName).ToString());
            }
            else if(trimmedLine.StartsWith("@"))
            {
                if (_currentDialogueData.characterID != -1)
                {
                    dialogueDataList.Add(_currentDialogueData);
                }
            }
            else
            {
                Debug.Log(trimmedLine);
                //speakerContentTextList.Add(trimmedLine); // 將對話內容加入對話列表
                //speakerNameList.Add(currentName); // 將當前的講話者名字加入名字列表
                _currentDialogueData.content.Add(trimmedLine);               
            }
        }
    }

    // 顯示講話內容，並逐字呈現
    private void ShowContentText(string str)
    {
        speakerContentText.text = string.Empty; // 清空顯示內容

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
            return;
        }

        if (isTypeOver) // 如果逐字顯示已完成
        {
            NextDialogueData();       
            return;
        }

        // 檢測輸入：按下Enter鍵或Z鍵來進入下一行或顯示完整行
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z) || Input.GetMouseButtonDown(0))
        {
            if (text.text == strings[contentIndex]) // 如果當前顯示的文字與對話列表中的一致
            {
                if (contentIndex == currentDialogueData.content.Count - 1) // 如果是最後一行對話
                {
                    isTypeOver = true; // 對話已完成
                    return;
                }
                NextLine(); // 進入下一行對話
                //ShowNameText(dialogueDataList[currentDialogueDataIndex]); // 更新顯示名字
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

    private void NextDialogueData()
    {
        contentIndex = 0;
        if (currentDialogueDataIndex < dialogueDataList.Count - 1)
        {
            currentDialogueDataIndex++;
            currentDialogueData = dialogueDataList[currentDialogueDataIndex];
            ShowCurrentDialogueData();
        }
        else 
        {
            MainSystem.instance.DialogueGroupOver(); // 通知 MainSystem 對話結束
            return;
        }
        isTypeOver = false;
    }

    private void ShowCurrentDialogueData()
    {
        ShowNameText(CharacterDataBaseSystem.instance.CharacterIDToName(currentDialogueData.characterID)); // 顯示講話者名字
        ShowContentText(currentDialogueData.content[contentIndex]); //顯示對話內容
    }

    // 協程逐字顯示每一行對話內容
    IEnumerator TypeLine(string line, Text text)
    {
        foreach (char _char in line.ToCharArray()) // 將行轉為字符數組
        {   //當你在協程中使用 yield return new WaitForSeconds(x) 時，Unity 會暫停該協程，並在等待 x 秒後繼續執行剩餘的代碼。
            text.text += _char; // 將字符逐個添加到文本 UI 中
            yield return new WaitForSeconds(textSpeed); // 每個字符間隔 textSpeed 秒
        }
    }
    private void OnButtonClick()
    {
        isButtonClicked = true; // 設置標誌，阻止對話控制
    }

    // 重新啟動對話控制
    public void ResumeDialogue()
    {
        isButtonClicked = false; // 清除標誌，恢復對話控制
    }
    private int NameToID(string _name)
    {

        return CharacterDataBaseSystem.instance.CharacterNameToID(_name);//角色名轉數字 //資料庫 
    }   
}
