using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    [SerializeField] private ToolTypes type;
    [SerializeField] private ToolMaterials material;
    public int damageToEnemy;
    public int damageToBlocks;

    private void Start()
    {
        damageToEnemy = (int)type * (int)material;
        switch (type)
        {
            case ToolTypes.PICKAXE:
                damageToBlocks = (int)material;
                break;
            case ToolTypes.SWORD:
                damageToBlocks = (int)material;
                break;
        }
    }
}
