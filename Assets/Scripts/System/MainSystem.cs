using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

/// <summary>
/// �D�t�ΡG�޲z��ܡB�ɶ��M��q���D�n�޿�
/// </summary>
public class MainSystem : MonoSingleton<MainSystem>, IDataPersistence
{
    // ���~������� GameObject�A����~�檺��ܻP����
    public GameObject inventoryWindow;
    public GameObject inventoryBotton;

    // ���a�ɶ����A
    public PlayerTime playerTime = new PlayerTime();

    // ����q���A
    public OpponentHeart opponentHeart = new OpponentHeart();

    // ��l���a�ɶ��M����q
    public int initialPlayerTime;
    public int initialOpponentHeart;

    // �Ω���ܪ��a�ɶ��M����q�� UI ����
    public TextMeshProUGUI TimeText;
    public TextMeshProUGUI HeartText;

    // ��ܼƾڪ��C��
    public List<DialogueGroupData> textDataList;

    // ��e�M�U�@�ӹ�ܼƾ�
    public DialogueGroupData currentDialogueGroupData;
    private DialogueGroupData nextTextData;

    // �e�@�ӹ�ܼƾ�
    private DialogueGroupData previousTextData;
    private bool isTalking = false;

    // ����O�_�b�i���ܡA�îھڪ��A�]�m���s���ҥΩθT��
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
    /// ��l�Ƴ]�m
    /// </summary>
    void Start()
    {
        IsTalking = false;

        // �]�m���a�M��⪺��l���A
        ShowState();

        // ���ê��~�����
        inventoryWindow.SetActive(false);

        // �]�m��e��ܼƾڬ��C�����Ĥ@�Ӥ���
        currentDialogueGroupData = textDataList[0];

        // �}�l�����e���
        RunCurentDialogue();
    }

    /// <summary>
    /// �C�V��s���a�ɶ��M����q�����A���
    /// </summary>
    void Update()
    {
        ShowState();
    }

    /// <summary>
    /// ��ܪ��~�����
    /// </summary>
    public void OpenInventoryWindow()
    {
        inventoryWindow.SetActive(true);
    }

    /// <summary>
    /// ���ê��~�����
    /// </summary>
    public void CloseInventoryWindow()
    {
        inventoryWindow.SetActive(false);
    }

    /// <summary>
    /// ��s��ܪ����a�ɶ��M����q
    /// </summary>
    public void ShowState()
    {
        TimeText.text = string.Format("{0}/{1}", playerTime.CurrentTime.ToString(), playerTime.MaxTime.ToString());
        HeartText.text = string.Format("{0}/{1}", opponentHeart.CurrentHeart.ToString(), opponentHeart.MaxHeart.ToString());
    }

    /// <summary>
    /// ��֪��a�ɶ�
    /// </summary>
    public void ReduceTime()
    {
        playerTime.ReduceTime(1);
    }

    /// <summary>
    /// ��ֹ���q
    /// </summary>
    public void ReduceHeart()
    {
        opponentHeart.ReduceHeart(1);
    }

    /// <summary>
    /// �ھڪ��� ID �����ܿ��
    /// </summary>
    /// <param name="id">��ܪ����� ID</param>
    public void SubmitObjectID(int id)
    {
        CloseInventoryWindow();

        // �ˬd���檺 ID �O�_�ǰt��e��ܼƾڪ����T ID
        if (currentDialogueGroupData.ObjectTrueID == id)
        {
            // �ǰt�h��ܤU�@�ӥ��T���
            nextTextData = FindTextDataFromList(currentDialogueGroupData.textTrueID);
            ReduceHeart();
        }
        else
        {
            // ���ǰt�h��ܿ��~���
            nextTextData = FindTextDataFromList(currentDialogueGroupData.textFalseID);
            ReduceTime();
        }

        previousTextData = currentDialogueGroupData;

        // ��s��e��ܼƾ�
        currentDialogueGroupData = nextTextData;

        // �}�l�s�����
        RunCurentDialogue();
    }

    /// <summary>
    /// �ھ� ID �b��ܼƾڦC���d������� DialogueGroupData
    /// </summary>
    /// <param name="id">��ܼƾڪ� ID</param>
    /// <returns>��쪺��ܼƾ�</returns>
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
    /// ��e��ܲյ����ɪ��B�z
    /// </summary>
    public void DialogueGroupOver()
    {
        Debug.Log("��ܲյ���");

        if (currentDialogueGroupData == null) return;

        if (!currentDialogueGroupData.isCorrectText)
        {
            Debug.Log("���ƹ��");
            currentDialogueGroupData = previousTextData;
            RunCurentDialogue();
        }
        else
        {
            IsTalking = false;
        }
    }

    /// <summary>
    /// ����~����s����ܪ��A
    /// </summary>
    /// <param name="active">���s�O�_���</param>
    public void SetButtonActive(bool active)
    {
        inventoryBotton.SetActive(active);
    }

    /// <summary>
    /// ���s������
    /// </summary>
    public void ReplayDialogue()
    {
        RunCurentDialogue();
    }

    /// <summary>
    /// �]�m�ö}�l�����e���
    /// </summary>
    private void RunCurentDialogue()
    {
        IsTalking = true;
        DialogueSystem.instance.SetDialogue(currentDialogueGroupData);
        DialogueSystem.instance.StartDialogue();
    }

    /// <summary>
    /// �q�s�ɤ��[�����
    /// </summary>
    public void LoadData(GameData data)
    {
        this.playerTime = data.playerTime;
        this.opponentHeart = data.opponentHeart;
        ShowState();

        // �ھڦs�ɪ����ID�A���ë�_��e��ܼƾ�
        this.currentDialogueGroupData = FindTextDataFromList(data.currentDialogueDataID);

        if (this.currentDialogueGroupData == null && textDataList.Count > 0)
        {
            Debug.LogWarning("�䤣���������ܼƾڡA�]�m���q�{���");
            this.currentDialogueGroupData = textDataList[0];
        }

        RunCurentDialogue();
    }

    /// <summary>
    /// �O�s��ƨ� GameData ��
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
