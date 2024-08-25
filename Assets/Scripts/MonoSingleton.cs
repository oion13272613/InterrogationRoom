using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 單例模式
/// 目的：容易訪問且唯一
/// </summary>
/// <typeparam name="T"></typeparam>
public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    //where T : MonoBehaviour 一定要繼承MonoBehaviour
{
    private static T m_instance;//instance實例

    /// <summary>
    /// 如果實例為空，找到實例
    /// </summary>
    public static T instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<T>();
                //找泛型(<T>)中類別的物件(第一個)，並回傳此犯行之類別
            }
            return m_instance;
        }

    }
    /// <summary>
    /// 避免重複找到實例
    /// </summary>
    private void Awake()
    {
        if (m_instance != null)
        {
            Destroy(this.gameObject);//Destroy物件消除函數
        }
    }

}
