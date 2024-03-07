using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public GameObject Slot1;
    public GameObject Slot2;
    public GameObject Slot3;

    public GameObject activeSlot;

    // Start is called before the first frame update
    void Start()
    {
        // เริ่มต้นให้ Slot1 เป็นอาวุธที่ใช้งานอยู่เริ่มต้น
        activeSlot = Slot1;

        // เริ่มต้นให้ทุกอาวุธเป็น non-active เมื่อเริ่มเกม
        Slot1.SetActive(true);
        Slot2.SetActive(false);
        Slot3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            Equip(Slot1);
        }

        if (Input.GetKeyDown("2"))
        {
            Equip(Slot2);
        }

        if (Input.GetKeyDown("3"))
        {
            Equip(Slot3);
        }
    }

    void Equip(GameObject slotToEquip)
    {
        // ถ้าสลับไปยังอาวุธที่มีการใช้งานอยู่แล้ว ไม่ต้องทำอะไร
        if (slotToEquip == activeSlot)
            return;

        // ปิดการใช้งานอาวุธที่ใช้งานอยู่
        activeSlot.SetActive(false);

        // เปิดการใช้งานอาวุธใหม่
        slotToEquip.SetActive(true);

        // กำหนดให้อาวุธใหม่เป็นอาวุธที่ใช้งานอยู่
        activeSlot = slotToEquip;

    }
}