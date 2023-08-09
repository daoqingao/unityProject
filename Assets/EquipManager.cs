using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameData gamedata;
    public Equipment spawnNewEquip()
    {
        Equipment a1 = ScriptableObject.CreateInstance<Equipment>();
        return a1;
    }
}
