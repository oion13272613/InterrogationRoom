using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InterrogationTurn
{
    public DialogueData beforeDialogue;
    public DialogueData successDialogue;
    public DialogueData failDialogue;
    public int turnID;
    public bool isDMG;
    public ChallengeLevel challengeLevel;

    public InterrogationTurn()
    {
        turnID = -1;
        isDMG = false;
        challengeLevel = ChallengeLevel.Easy;
    }

    public InterrogationTurn(DialogueData _beforeDialogue, DialogueData _sucessDialogue, DialogueData _failDialogue, int _turnID, bool _isDMG)
    {
        beforeDialogue = _beforeDialogue;
        successDialogue = _sucessDialogue;
        failDialogue = _failDialogue;
        turnID = _turnID;
        isDMG = _isDMG;
    }

    public virtual void AddDialogueData(DialogueData _beforeDialogue, DialogueData _sucessDialogue, DialogueData _failDialogue)
    {
        beforeDialogue = _beforeDialogue;
        successDialogue = _sucessDialogue;
        failDialogue = _failDialogue;

        Debug.Log("Before: " + (beforeDialogue != null ? "�K�[���\" : "Null"));
        Debug.Log("Success: " + (successDialogue != null ? "�K�[���\" : "Null"));
        Debug.Log("Fail: " + (failDialogue != null ? "�K�[���\" : "Null"));
    }

    

    /// <summary>
    /// �c�ؿ�ܼҦ�
    /// </summary>
    public virtual void BuildChooseMode()
    {

    }
    /// <summary>
    /// �ӧQ���y
    /// </summary>
    public virtual void VictoryAward()
    {

    }

    /// <summary>
    /// ���Ѽ��y
    /// </summary>
    public virtual void FailAward()
    {

    }
}
