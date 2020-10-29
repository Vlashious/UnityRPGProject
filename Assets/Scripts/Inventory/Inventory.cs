using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private GameObject _slotPrefab;
    [SerializeField]
    private const int _slotsNum = 5;
    private Image[] _itemImages = new Image[_slotsNum];
    private Item[] _items = new Item[_slotsNum];
    private GameObject[] _slots = new GameObject[_slotsNum];
    public void Start()
    {
        CreateSlots();
    }

    private void CreateSlots()
    {
        if (_slotPrefab)
        {
            for (int i = 0; i < _slotsNum; i++)
            {
                var newSlot = Instantiate<GameObject>(_slotPrefab);
                newSlot.name = $"ItemSlot_{i}";

                newSlot.transform.SetParent(transform.GetChild(0).transform);

                _slots[i] = newSlot;

                _itemImages[i] = newSlot.transform.GetChild(1).GetComponent<Image>();
            }
        }
    }

    public bool AddItem(Item item)
    {
        for (int i = 0; i < _items.Length; i++)
        {
            if (_items[i] && _items[i].type == item.type && item.stackable)
            {
                _items[i].quantity++;

                Slot slotScript = _slots[i].GetComponent<Slot>();

                Text qtext = slotScript.qtyText;
                qtext.gameObject.SetActive(true);
                qtext.text = _items[i].quantity.ToString();

                return true;
            }
            if (!_items[i])
            {
                _items[i] = Instantiate(item);
                _items[i].quantity = 1;
                _itemImages[i].sprite = item.sprite;
                _itemImages[i].gameObject.SetActive(true);
                return true;
            }
        }
        return false;
    }
}
