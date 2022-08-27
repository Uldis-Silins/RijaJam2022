using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PartsFactory : MonoBehaviour
{
    public Part[] partPrefabs;

    [SerializeField] private Transform m_spawnPoint;
    [SerializeField] private Vector3 m_spawnOffset = new Vector3(0.0f, 1.0f, 0.0f);

    private List<Part> m_spawnedParts;

    private void Awake()
    {
        m_spawnedParts = new List<Part>();
    }

    public void Spawn()
    {
        if (m_spawnedParts.Count == 0)
        {
            int partIndex = Random.Range(0, partPrefabs.Length);
            var instance = Instantiate<Part>(partPrefabs[partIndex], m_spawnPoint.position + m_spawnOffset, Quaternion.identity);
            instance.SetOwner(this);
            m_spawnedParts.Add(instance);
        }
    }

    public void Despawn(Part part)
    {
        Assert.IsTrue(m_spawnedParts.Contains(part));
        m_spawnedParts.Remove(part);
        Destroy(part.gameObject);
    }

    private void OnDrawGizmos()
    {
        Color prevColor = Gizmos.color;
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(transform.position, Vector3.one);
        Gizmos.color = prevColor;
    }
}
