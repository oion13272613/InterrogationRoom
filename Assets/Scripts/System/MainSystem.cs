using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

/// <summary>
/// �`�޲z���G��ܡB�ɶ��B��q
/// </summary>
public class MainSystem : MonoSingleton<MainSystem>
{
    // ���~������� GameObject�A����~�檺��ܻP����
    public GameObject inventoryWindow;
    public GameObject inventoryBotton;

    // ���a�ɶ����A
    public PlayerTime playerTime = new PlayerTime();

    // ����q���A
    public OpponentHeart opponentHeart = new OpponentHeart();

    
    public int initialPlayerTime;// ��l���a�ɶ�
    public int initialOpponentHeart;// ��l����q

    
    public TextMeshProUGUI TimeText;// �Ω���ܪ��a�ɶ��� UI ����
    public TextMeshProUGUI HeartText;// �Ω���ܹ���q�� UI ����

    
    public List<DialogueGroupData> textDataList;// �s���ܼƾڪ��C��

   
    public DialogueGroupData currentDialogueGroupData; // ��e��ܼƾ�
    private DialogueGroupData nextTextData; //�U�@�ӹ�ܼƾ�

    private DialogueGroupData previousTextData;//�e�@��TextData
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

        playerTime.SetTime(initialPlayerTime); // �]�m���a����l�ɶ�
        opponentHeart.SetHeart(initialOpponentHeart); // �]�m��⪺��l��q

        // ���ê��~�����
        inventoryWindow.SetActive(false);

        // �]�m��e��ܼƾڬ��C�����Ĥ@�Ӥ���
        currentDialogueGroupData = textDataList[0];

        // �}�l�����e���
        RunCurentDialogue();
    }

    // �C�@�V��s
    void Update()
    {
        // ��ܪ��a�ɶ��M����q�����A
        ShowState();
    }

    // ��ܪ��~�����
    public void OpenInventoryWindow()
    {
        inventoryWindow.SetActive(true);
    }

    // ���ê��~�����
    public void CloseInventoryWindow()
    {
        inventoryWindow.SetActive(false);
    }

    // ��s��ܪ����a�ɶ��M����q
    public void ShowState()
    {
        // ��s TimeText ����ܷ�e�ɶ�/�̤j�ɶ�
        TimeText.text = string.Format("{0}/{1}", playerTime.CurrentTime.ToString(), playerTime.MaxTime.ToString());

        // ��s HeartText ����ܷ�e��q/�̤j��q
        HeartText.text = string.Format("{0}/{1}", opponentHeart.CurrentHeart.ToString(), opponentHeart.MaxHeart.ToString());
    }

    // ��֪��a�ɶ�
    public void ReduceTime()
    {
        playerTime.ReduceTime(1);
    }

    public void ReduceHeart()
    {
        opponentHeart.ReduceHeart(1);
    }

    // �ھڪ��� ID �����ܿ��
    public void SubmitObjectID(int id)
    {
        CloseInventoryWindow();
        // �ˬd���檺 ID �O�_�ǰt��e��ܼƾڪ����T ID
        if (currentDialogueGroupData.ObjectTrueID == id)
        {
            // �p�G�ǰt�A���U�@�ӹ�ܼƾ�
            nextTextData = FindTextDataFromList(currentDialogueGroupData.textTrueID);
            ReduceHeart();
        }
        else
        {
            // �p�G���ǰt�A���t�@�ӹ�ܼƾ�
            nextTextData = FindTextDataFromList(currentDialogueGroupData.textFalseID);
            ReduceTime();
        }

        previousTextData = currentDialogueGroupData;

        // �]�m��e��ܼƾڬ���쪺�U�@�ӹ�ܼƾ�
        currentDialogueGroupData = nextTextData;

        // �}�l����s�����
        RunCurentDialogue();
    }

    // �ھ� ID �b��ܼƾڦC���d������� DialogueGroupData
    private DialogueGroupData FindTextDataFromList(int id)
    {
        DialogueGroupData textData = null;

        // �M����ܼƾڦC��
        foreach (DialogueGroupData item in textDataList)
        {
            // ���ǰt����ܼƾ�
            if (item.textID == id)
            {
                textData = item;
                break; // ���X�`��
            }
        }

        return textData;
    }

    //�奻�w����(��DialogueSystem�q��)
    
    public void DialogueGroupOver() 
    {
        Debug.Log("��ܲյ���");        
        if(currentDialogueGroupData == null)
        {
            return;
        }
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


    // �]�m�ö}�l�����e���
    private void RunCurentDialogue()
    {
        IsTalking = true;
        DialogueSystem.instance.SetDialogue(currentDialogueGroupData);
        DialogueSystem.instance.StartDialogue();
    }
}
