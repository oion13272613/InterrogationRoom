using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DialogueGroupData))] // �۩w�q�s�边�A�w�� DialogueGroupData ���O
public class DialogueGroupDataEditor : Editor
{
    private Vector2 scrollPos; // �Ω�u�ʵ��Ϫ���m

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI(); // ø�s�q�{�� Inspector �ɭ�

        DialogueGroupData dialogueData = (DialogueGroupData)target; // �����e�s�誺 DialogueGroupData ���

        // �D�奻���e��ܩM�s��
        if (dialogueData.TextFile != null)
        {
            // �}�l�u�ʵ���
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Height(200));
            EditorGUILayout.LabelField("�D�奻���e:"); // ���D
            string newText = EditorGUILayout.TextArea(dialogueData.TextFile.text, GUILayout.Height(300)); // ��� TextFile �����e���i�s�誺�奻�ϰ�

            // �p�G�奻���e�o���ܤơA�h��s TextFile �����e
            if (GUI.changed) // �ˬd�O�_�����
            {
                UpdateTextFile(dialogueData.TextFile, newText); // ��s TextFile
            }

            EditorGUILayout.EndScrollView(); // �����u�ʵ���
        }
        else
        {
            EditorGUILayout.LabelField("�S�����w�D TextAsset�C"); // �p�G�S�����w TextFile�A��ܴ���
        }

        // ����Ҧ� DialogueGroupData ���
        DialogueGroupData[] allDialogueData = Resources.FindObjectsOfTypeAll<DialogueGroupData>();

        // �C�X�Ҧ��� DialogueGroupData ���
        EditorGUILayout.LabelField("�Ҧ� DialogueGroupData ���:");
        foreach (var data in allDialogueData) // �M���Ҧ���ܼƾ�
        {
            EditorGUILayout.LabelField($"ID: {data.textID}, Name: {data.name}"); // ��� ID �M�W��
        }

        // �d��M�s�西�T����ܤ��e
        string correctDialogue = GetDialogueText(allDialogueData, dialogueData.textTrueID); // �ھ� textTrueID �����������ܤ��e
        EditorGUILayout.LabelField("���T��ܤ��e:"); // ���D
        string newCorrectText = EditorGUILayout.TextArea(correctDialogue, GUILayout.Height(100)); // ��ܥi�s�誺���T��ܤ奻�ϰ�

        // ��s���T��ܪ� TextFile
        if (dialogueData.textTrueID != -1) // �T�O textTrueID �O���Ī�
        {
            UpdateDialogueText(allDialogueData, dialogueData.textTrueID, newCorrectText); // ��s���T��ܪ����e
        }

        // �d��M�s����~����ܤ��e
        string incorrectDialogue = GetDialogueText(allDialogueData, dialogueData.textFalseID); // �ھ� textFalseID �����������ܤ��e
        EditorGUILayout.LabelField("���~��ܤ��e:"); // ���D
        string newIncorrectText = EditorGUILayout.TextArea(incorrectDialogue, GUILayout.Height(100)); // ��ܥi�s�誺���~��ܤ奻�ϰ�

        // ��s���~��ܪ� TextFile
        if (dialogueData.textFalseID != -1) // �T�O textFalseID �O���Ī�
        {
            UpdateDialogueText(allDialogueData, dialogueData.textFalseID, newIncorrectText); // ��s���~��ܪ����e
        }
    }

    // �ھ� ID �����ܤ��e
    private string GetDialogueText(DialogueGroupData[] dialogues, int id)
    {
        foreach (var dialogue in dialogues) // �M���Ҧ�����ܼƾ�
        {
            if (dialogue.textID == id) // �ھ� textID �d��
            {
                if (dialogue.TextFile != null) // �p�G������ TextFile �s�b
                {
                    return dialogue.TextFile.text.Trim(); // ��^������ TextFile ���e�A�h���e��ť�
                }
                return $"ID {id} ������ TextFile ���šC"; // �p�G TextFile ���šA��^����
            }
        }
        return $"ID {id} ���s�b���ܼƾڤ��C"; // �p�G�S���������� ID�A��^����
    }

    // ��s TextFile �����e
    private void UpdateTextFile(TextAsset textFile, string newText)
    {
        // �T�O�i�H�ק� TextAsset
        var path = AssetDatabase.GetAssetPath(textFile); // ��� TextFile �����|
        System.IO.File.WriteAllText(path, newText); // �g�J�s���e
        AssetDatabase.Refresh(); // ��s�겣�ƾڮw�H�ϬM�ܧ�
    }

    // ��s���� ID ����ܤ奻
    private void UpdateDialogueText(DialogueGroupData[] dialogues, int id, string newText)
    {
        foreach (var dialogue in dialogues) // �M���Ҧ�����ܼƾ�
        {
            if (dialogue.textID == id && dialogue.TextFile != null) // �ھ� textID �d����ˬd TextFile �O�_�s�b
            {
                UpdateTextFile(dialogue.TextFile, newText); // ��s������ TextFile
                break; // ����h�X�`��
            }
        }
    }
}
