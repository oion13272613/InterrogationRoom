using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �޲z��⪺��q�A�]�A�̤j��q�M��e��q�C���ѳ]�m�B��֩M���m��q���\��C
/// </summary>
public class OpponentHeart
{
    
    private int maxHeart;// �̤j��q�G��ܹ��i�H�֦����̤j��q    
    private int currentHeart;// ��e��q�G��ܹ���e�֦�����q

    /// <summary>
    /// �̤j��q�����}�uŪ�ݩ�
    /// </summary>
    public int MaxHeart
    {
        get { return maxHeart; }
    }

    /// <summary>
    /// ��e��q�����}�uŪ�ݩ�
    /// </summary>
    public int CurrentHeart
    {
        get { return currentHeart; }
    }

    /// <summary>
    /// �]�m��q�A��l�Ƴ̤j��q�ñN��e��q�]�m���̤j��q
    /// </summary>
    /// <param name="max">��l�ƪ��̤j��q��</param>
    public void SetHeart(int max)
    {
        maxHeart = max;       // �]�m�̤j��q���ǤJ����
        currentHeart = maxHeart; // �N��e��q�]�m���̤j��q
    }

    /// <summary>
    /// ��ַ�e��q
    /// </summary>
    /// <param name="value">�n��֪���q�q</param>
    public void ReduceHeart(int value)
    {
        currentHeart -= value; // ��ַ�e��q

        // �p�G��e��q�p��ε���0�A�h�N��]�m��0
        if (currentHeart <= 0)
        {
            currentHeart = 0;
        }
    }

    /// <summary>
    /// ���m��e��q���̤j��q
    /// </summary>
    public void ResetHeart()
    {
        currentHeart = maxHeart; // �N��e��q�]�m���̤j��q
    }
}
