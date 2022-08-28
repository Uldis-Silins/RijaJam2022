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

    public AudioSource soundSource;
    public AudioClip[] addPartClips;
    public AudioClip[] wrongPartClips;

    private List<PartSlot> m_emptySlots;

    private int m_partLayer;

    public ComputerSpawner Owner { get; set; }
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

    private void OnTriggerStay(Collider other)
    {
        if(Interactable && other.gameObject.layer == m_partLayer)
        {
            Part part = other.GetComponent<Part>();

            if (part.InGrab) return;

            int slotIndex = -1;

            if(HasEmptySlotFor(part, out slotIndex))
            {
                part.Owner.Despawn(part);
                m_emptySlots.RemoveAt(slotIndex);
                Owner.uiPartsDisplay.Remove(part.type);

                if(soundSource)
                {
                    soundSource.clip = addPartClips[Random.Range(0, addPartClips.Length)];
                    soundSource.Play();
                }
            }
            else
            {
                part.Owner.Despawn(part);
                part.Owner.Spawn();
                Owner.uiPartsDisplay.ShowWrongPart(part.type);

                if(soundSource)
                {
                    soundSource.clip = wrongPartClips[Random.Range(0, wrongPartClips.Length)];
                    soundSource.Play();
                }
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
