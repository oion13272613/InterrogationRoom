using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InterrogationTurn
{
    public DialogueData beforeDialogue;
    public DialogueData sucessDialogue;
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
        sucessDialogue = _sucessDialogue;
        failDialogue = _failDialogue;
        turnID = _turnID;
        isDMG = _isDMG;
    }
}
