using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Computer : MonoBehaviour
{
    [System.Serializable]
    public class OnAssembledEvent : UnityEvent<int> { }

    [System.Serializable]
    public class PartSlot
    {
        public Part.PartType type;
    }

    public OnAssembledEvent onAssembled;

    public PartSlot[] slots;

    public int value = 100;

    private List<PartSlot> m_emptySlots;

    private int m_partLayer;

    public bool Interactable { get; set; }
    public bool IsAssembled { get; private set; }

    private void Awake()
    {
        m_partLayer = LayerMask.NameToLayer("Part");
    }

    public void Initialize()
    {
        m_emptySlots = new List<PartSlot>();
        m_emptySlots.AddRange(slots);
        Interactable = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(Interactable && other.gameObject.layer == m_partLayer)
        {
            Part part = other.GetComponent<Part>();

            int slotIndex = -1;

            if(HasEmptySlotFor(part, out slotIndex))
            {
                part.Owner.Despawn(part);
                m_emptySlots.RemoveAt(slotIndex);
            }

            if(m_emptySlots.Count == 0)
            {
                onAssembled.Invoke(value);
                Interactable = false;
                IsAssembled = true;
            }
        }
    }

    private bool HasEmptySlotFor(Part part, out int slotIndex)
    {
        for (int i = 0; i < m_emptySlots.Count; i++)
        {
            if(m_emptySlots[i].type == part.type)
            {
                slotIndex = i;
                return true;
            }
        }

        slotIndex = -1;
        return false;
    }
}
