using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable] // �аO�o�����O�i�H�Q�ǦC�ơA�H�K��O�s�M�s��
[CreateAssetMenu(fileName = "NewCharacterData", menuName = "Create SO/CharacterData")]
/// <summary>
/// OpponentData�X�i Character ���O�A��ܹ�⪺�ƾڡA�]�A��⪺��q�C
/// </summary>
public class OpponentData : Character
{   
    public OpponentHeart opponentHeart;// ��⪺��q�޲z

    /// <summary>
    /// �򥻺c�y��ơA��l�ƹ�⪺ ID �M�W�١A�ó]�m��⪺��q���q�{�� 5�C
    /// </summary>
    /// <param name="_id">��⪺�ߤ@�ѧO�X</param>
    /// <param name="opponentName">��⪺�W��</param>
    public OpponentData(int _id, string opponentName) : base(_id, opponentName)
    {
        // ��l�� opponentHeart ���s�� OpponentHeart ���
        opponentHeart = new OpponentHeart();
        // �]�m��q���q�{�� 5
        opponentHeart.SetHeart(5);
    }

    /// <summary>
    /// �����c�y��ơA��l�ƹ�⪺ ID �M�W�١A�ó]�m��⪺��q�����w���ȡC
    /// </summary>
    /// <param name="_id">��⪺�ߤ@�ѧO�X</param>
    /// <param name="opponentName">��⪺�W��</param>
    /// <param name="_maxHeart">��⪺�̤j��q</param>
    public OpponentData(int _id, string opponentName, int _maxHeart) : base(_id, opponentName)
    {
        // ��l�� opponentHeart ���s�� OpponentHeart ���
        opponentHeart = new OpponentHeart();
        // �]�m��q�����w���̤j��q
        opponentHeart.SetHeart(_maxHeart);
    }
}
