using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerSpawner : MonoBehaviour
{
    public enum CurrentStateType { None, MoveIn, Assemble, MoveOut }

    public Transform moveFromPosition, assemblyPoint, moveToPosition;
    public float moveInTime, moveOutTime;
    public Computer[] computerPrefabs;

    public UI_PartsDisplay uiPartsDisplay;

    private CurrentStateType m_state;
    private Computer m_currentComputer;
    private bool m_inAssembly;
    private float m_moveTimer;

    private void Start()
    {
        Spawn();
    }

    private void Update()
    {
        switch (m_state)
        {
            case CurrentStateType.MoveIn:
                {
                    m_currentComputer.transform.position = Vector3.Lerp(moveFromPosition.position, assemblyPoint.position, m_moveTimer / moveInTime);
                    m_moveTimer += Time.deltaTime;

                    if(m_moveTimer > moveInTime)
                    {
                        m_currentComputer.Initialize();
                        m_state = CurrentStateType.Assemble;
                    }
                }
                break;
            case CurrentStateType.Assemble:
                {
                    m_inAssembly = true;

                    if(m_currentComputer.IsAssembled)
                    {
                        m_moveTimer = 0f;
                        m_inAssembly = false;
                        m_state = CurrentStateType.MoveOut;
                    }
                }
                break;
            case CurrentStateType.MoveOut:
                {
                    m_currentComputer.transform.position = Vector3.Lerp(assemblyPoint.position, moveToPosition.position, m_moveTimer / moveInTime);
                    m_moveTimer += Time.deltaTime;

                    if(m_moveTimer > moveOutTime)
                    {
                        Destroy(m_currentComputer.gameObject);
                        m_currentComputer = null;
                        Spawn();
                    }
                }
                break;
            default:
                break;
        }
    }

    private void OnDrawGizmos()
    {
        Color prevColor = Gizmos.color;
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, Vector3.one * 0.25f);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(moveFromPosition.position, Vector3.one * 0.25f);
        Gizmos.DrawWireCube(assemblyPoint.position, Vector3.one * 0.25f);
        Gizmos.DrawWireCube(moveToPosition.position, Vector3.one * 0.25f);
        Gizmos.color = prevColor;
    }

    public void Spawn()
    {
        m_currentComputer = Instantiate<Computer>(computerPrefabs[Random.Range(0, computerPrefabs.Length)], moveFromPosition.position, Quaternion.identity);
        m_currentComputer.Owner = this;

        uiPartsDisplay.ClearAll();

        for (int i = 0; i < m_currentComputer.slots.Length; i++)
        {
            uiPartsDisplay.Spawn(m_currentComputer.slots[i].type);
        }

        m_moveTimer = 0f;
        m_state = CurrentStateType.MoveIn;
    }
}
