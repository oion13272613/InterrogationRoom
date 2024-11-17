using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class InterrogationTurn : ScriptableObject
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

        Debug.Log("Before: " + (beforeDialogue != null ? "添加成功" : "Null"));
        Debug.Log("Success: " + (successDialogue != null ? "添加成功" : "Null"));
        Debug.Log("Fail: " + (failDialogue != null ? "添加成功" : "Null"));
    }

    

    /// <summary>
    /// 構建選擇模式
    /// </summary>
    public virtual void BuildChooseMode()
    {

    }
    /// <summary>
    /// 勝利獎勵
    /// </summary>
    public virtual void VictoryAward()
    {

    }

    /// <summary>
    /// 失敗獎勵
    /// </summary>
    public virtual void FailAward()
    {

    }
}
