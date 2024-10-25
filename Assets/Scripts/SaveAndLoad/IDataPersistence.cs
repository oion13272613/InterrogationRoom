using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �w�q��ƫ��[�ƪ������A�Ω�Ū���M�x�s�C����� (GameData)�C
/// ��@�����������O�����Ѩ��骺��Ƹ��J�P�x�s�޿�C
/// </summary>
public interface IDataPersistence
{
    /// <summary>
    /// ���J��Ʀ� GameData ����
    /// </summary>
    /// <param name="data">�n���J�� GameData ����A�]�t�C��������ƪ��A</param>
    /// <remarks>
    /// ����k����@�N���Ū���ø��J�ܴ��Ѫ� GameData ���󤤪��޿�A
    /// �Ҧp�q�ɮסB�ƾڮw�ζ���Ū����ơC
    /// </remarks>
    void LoadData(GameData data);

    /// <summary>
    /// �x�s GameData ���󪺸��
    /// </summary>
    /// <param name="data">�n�x�s�� GameData ����A�H ref �ǻ��Ѧҫ��O</param>
    /// <remarks>
    /// ����k����@�N GameData ���󤤪���ƶi���x�s���޿�A
    /// �Ҧp�N��Ƽg�J�ɮסB�ƾڮw�ΤW�Ǧܶ��ݡC
    /// �ϥ� ref �ѼƤ��\�b�x�s�L�{���� GameData �i��ק�]�p��s�ɶ��W�^�C
    /// </remarks>
    void SaveData(ref GameData data);
}

