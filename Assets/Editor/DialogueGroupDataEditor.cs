using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DialogueGroupData))] // 自定義編輯器，針對 DialogueGroupData 類別
public class DialogueGroupDataEditor : Editor
{
    private Vector2 scrollPos; // 用於滾動視圖的位置

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI(); // 繪製默認的 Inspector 界面

        DialogueGroupData dialogueData = (DialogueGroupData)target; // 獲取當前編輯的 DialogueGroupData 實例

        // 主文本內容顯示和編輯
        if (dialogueData.TextFile != null)
        {
            // 開始滾動視圖
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Height(200));
            EditorGUILayout.LabelField("主文本內容:"); // 標題
            string newText = EditorGUILayout.TextArea(dialogueData.TextFile.text, GUILayout.Height(300)); // 顯示 TextFile 的內容為可編輯的文本區域

            // 如果文本內容發生變化，則更新 TextFile 的內容
            if (GUI.changed) // 檢查是否有更改
            {
                UpdateTextFile(dialogueData.TextFile, newText); // 更新 TextFile
            }

            EditorGUILayout.EndScrollView(); // 結束滾動視圖
        }
        else
        {
            EditorGUILayout.LabelField("沒有指定主 TextAsset。"); // 如果沒有指定 TextFile，顯示提示
        }

        // 獲取所有 DialogueGroupData 實例
        DialogueGroupData[] allDialogueData = Resources.FindObjectsOfTypeAll<DialogueGroupData>();

        // 列出所有的 DialogueGroupData 實例
        EditorGUILayout.LabelField("所有 DialogueGroupData 實例:");
        foreach (var data in allDialogueData) // 遍歷所有對話數據
        {
            EditorGUILayout.LabelField($"ID: {data.textID}, Name: {data.name}"); // 顯示 ID 和名稱
        }

        // 查找和編輯正確的對話內容
        string correctDialogue = GetDialogueText(allDialogueData, dialogueData.textTrueID); // 根據 textTrueID 獲取對應的對話內容
        EditorGUILayout.LabelField("正確對話內容:"); // 標題
        string newCorrectText = EditorGUILayout.TextArea(correctDialogue, GUILayout.Height(100)); // 顯示可編輯的正確對話文本區域

        // 更新正確對話的 TextFile
        if (dialogueData.textTrueID != -1) // 確保 textTrueID 是有效的
        {
            UpdateDialogueText(allDialogueData, dialogueData.textTrueID, newCorrectText); // 更新正確對話的內容
        }

        // 查找和編輯錯誤的對話內容
        string incorrectDialogue = GetDialogueText(allDialogueData, dialogueData.textFalseID); // 根據 textFalseID 獲取對應的對話內容
        EditorGUILayout.LabelField("錯誤對話內容:"); // 標題
        string newIncorrectText = EditorGUILayout.TextArea(incorrectDialogue, GUILayout.Height(100)); // 顯示可編輯的錯誤對話文本區域

        // 更新錯誤對話的 TextFile
        if (dialogueData.textFalseID != -1) // 確保 textFalseID 是有效的
        {
            UpdateDialogueText(allDialogueData, dialogueData.textFalseID, newIncorrectText); // 更新錯誤對話的內容
        }
    }

    // 根據 ID 獲取對話內容
    private string GetDialogueText(DialogueGroupData[] dialogues, int id)
    {
        foreach (var dialogue in dialogues) // 遍歷所有的對話數據
        {
            if (dialogue.textID == id) // 根據 textID 查找
            {
                if (dialogue.TextFile != null) // 如果對應的 TextFile 存在
                {
                    return dialogue.TextFile.text.Trim(); // 返回對應的 TextFile 內容，去除前後空白
                }
                return $"ID {id} 對應的 TextFile 為空。"; // 如果 TextFile 為空，返回提示
            }
        }
        return $"ID {id} 不存在於對話數據中。"; // 如果沒有找到對應的 ID，返回提示
    }

    // 更新 TextFile 的內容
    private void UpdateTextFile(TextAsset textFile, string newText)
    {
        // 確保可以修改 TextAsset
        var path = AssetDatabase.GetAssetPath(textFile); // 獲取 TextFile 的路徑
        System.IO.File.WriteAllText(path, newText); // 寫入新內容
        AssetDatabase.Refresh(); // 刷新資產數據庫以反映變更
    }

    // 更新對應 ID 的對話文本
    private void UpdateDialogueText(DialogueGroupData[] dialogues, int id, string newText)
    {
        foreach (var dialogue in dialogues) // 遍歷所有的對話數據
        {
            if (dialogue.textID == id && dialogue.TextFile != null) // 根據 textID 查找並檢查 TextFile 是否存在
            {
                UpdateTextFile(dialogue.TextFile, newText); // 更新對應的 TextFile
                break; // 找到後退出循環
            }
        }
    }
}
