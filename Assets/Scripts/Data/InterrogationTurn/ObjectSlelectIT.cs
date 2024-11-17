using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DialogueIT", menuName = "Create SO/DialogueIT")]
[System.Serializable]
public class ObjectSlelectIT : InterrogationTurn
{
    public int objectID;

    public ObjectSlelectIT() : base() { }

    public ObjectSlelectIT(DialogueData _beforeDialogue, DialogueData _successDialogue, DialogueData _failDialogue, int _turnID, bool _isDMG)
        : base(_beforeDialogue, _successDialogue, _failDialogue, _turnID, _isDMG)
    {

    }

    /// <summary>
    /// 構建選擇模式
    /// </summary>
    public override void BuildChooseMode()
    {

    }
    /// <summary>
    /// 勝利獎勵
    /// </summary>
    public override void VictoryAward()
    {

    }

    /// <summary>
    /// 失敗獎勵
    /// </summary>
    public override void FailAward()
    {

    }
}
