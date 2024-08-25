using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ҼҦ�
/// �ت��G�e���X�ݥB�ߤ@
/// </summary>
/// <typeparam name="T"></typeparam>
public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    //where T : MonoBehaviour �@�w�n�~��MonoBehaviour
{
    private static T m_instance;//instance���

    /// <summary>
    /// �p�G��Ҭ��šA�����
    /// </summary>
    public static T instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<T>();
                //��x��(<T>)�����O������(�Ĥ@��)�A�æ^�Ǧ��Ǧ椧���O
            }
            return m_instance;
        }

    }
    /// <summary>
    /// �קK���Ƨ����
    /// </summary>
    private void Awake()
    {
        if (m_instance != null)
        {
            Destroy(this.gameObject);//Destroy����������
        }
    }

}
