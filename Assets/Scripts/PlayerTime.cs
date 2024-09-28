using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �޲z���a���ɶ����A�A�]�A�̤j�ɶ��M���e�ɶ��C���ѳ]�m�B��֩M���m�ɶ����\��C
/// </summary>
public class PlayerTime
{
    
    private int maxTime;// �̤j�ɶ��G���ܪ��a�i�H�֦����̤j�ɶ��q   
    private int currentTime;// ���e�ɶ��G���ܪ��a���e�֦����ɶ��q

    /// <summary>
    /// �̤j�ɶ������}�uŪ�ݩ�
    /// </summary>
    public int MaxTime
    {
        get { return maxTime; }
    }

    /// <summary>
    /// ���e�ɶ������}�uŪ�ݩ�
    /// </summary>
    public int CurrentTime
    {
        get { return currentTime; }
    }

    /// <summary>
    /// �]�m�ɶ��A��l�Ƴ̤j�ɶ��ñN���e�ɶ��]�m���̤j�ɶ�
    /// </summary>
    /// <param name="max">��l�ƪ��̤j�ɶ���</param>
    public void SetTime(int max)
    {
        maxTime = max;       // �]�m�̤j�ɶ����ǤJ����
        currentTime = maxTime; // �N���e�ɶ��]�m���̤j�ɶ�
    }

    /// <summary>
    /// ��ַ��e�ɶ�
    /// </summary>
    /// <param name="value">�n��֪��ɶ��q</param>
    public void ReduceTime(int value)
    {
        currentTime -= value; // ��ַ��e�ɶ�

        // �p�G���e�ɶ��p��ε���0�A�h�N��]�m��0
        if (currentTime <= 0)
        {
            currentTime = 0;
        }
    }

    /// <summary>
    /// ���m���e�ɶ����̤j�ɶ�
    /// </summary>
    public void ResetTime()
    {
        currentTime = maxTime; // �N���e�ɶ��]�m���̤j�ɶ�
    }
}