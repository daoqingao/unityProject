using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemEquipFab : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<TMP_Text>();
        img = GetComponentInChildren<Image>();

        button = GetComponent<Button>();

        button.onClick.AddListener(() =>
        {
            equipListManager.handleSelectWeapon(equipmentData,type);
        });
    }

    public Equipment equipmentData;
    public TMP_Text text;
    public Image img;

    
    public Sprite weaponSpriteImg;
    public Sprite armorSpriteImg;

    public Button button;

    public EquipmentListManager equipListManager;
    public EquipmentListManager.EquipType type;

    public void initItem(Equipment equipment,EquipmentListManager.EquipType type)
    {
        
        equipmentData = equipment;
        text.text = equipmentData.ToString();
        this.type = type;
        if (type == EquipmentListManager.EquipType.WEAPON)
        {
            img.sprite = weaponSpriteImg;
        }
        else
        {
            img.sprite = armorSpriteImg;
        }
    }
}
