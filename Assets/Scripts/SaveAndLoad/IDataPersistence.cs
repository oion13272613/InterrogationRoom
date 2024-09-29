using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistence
{   
    void LoadData(GameData date);
    void SaveData(ref GameData date);
}
