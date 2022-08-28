using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PartsDisplay : MonoBehaviour
{
    [System.Serializable]
    public class PartSprite
    {
        public Part.PartType type;
        public Sprite sprite;
    }

    public PartSprite[] sprites;
    public Image partIconPrefab;
    public GridLayoutGroup partIconContainer;

    public Image wrongPartImage;

    private List<Image> m_spawnedIcons;

    private float m_wrongTimer;
    private bool m_isShowingWrong;

    private void Awake()
    {
        m_spawnedIcons = new List<Image>();
    }

    private void Update()
    {
        if(m_isShowingWrong)
        {
            m_wrongTimer -= Time.deltaTime;

            Color c = Color.Lerp(new Color(1.0f, 0.0f, 0.0f, 0.0f), new Color(1.0f, 0.0f, 0.0f, 1.0f), Mathf.PingPong(Time.time, 1.0f));
            wrongPartImage.color = c;

            if(m_wrongTimer < 0)
            {
                wrongPartImage.color = new Color(1.0f, 0.0f, 0.0f, 0.0f);
                m_isShowingWrong = false;
            }
        }
    }

    public void Spawn(Part.PartType type)
    {
        var instance = Instantiate<Image>(partIconPrefab, partIconContainer.transform) as Image;

        for (int i = 0; i < sprites.Length; i++)
        {
            if(sprites[i].type == type)
            {
                instance.sprite = sprites[i].sprite;
                break;
            }
        }

        m_spawnedIcons.Add(instance);
    }

    public void Remove(Part.PartType type)
    {
        Image img = null;
        Sprite spr = null;

        for (int i = 0; i < sprites.Length; i++)
        {
            if(sprites[i].type == type)
            {
                spr = sprites[i].sprite;
                break;
            }
        }

        if (spr != null)
        {
            for (int i = 0; i < m_spawnedIcons.Count; i++)
            {
                if (m_spawnedIcons[i].sprite == spr)
                {
                    img = m_spawnedIcons[i];
                }
            }
        }

        if(img != null)
        {
            m_spawnedIcons.Remove(img);
            Destroy(img.gameObject);
        }
    }

    public void ClearAll()
    {
        if (m_spawnedIcons.Count == 0) return;

        for (int i = m_spawnedIcons.Count - 1; i > 0; i--)
        {
            Destroy(m_spawnedIcons[i].gameObject);
            m_spawnedIcons.RemoveAt(i);
        }
    }

    public void ShowWrongPart(Part.PartType type)
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            if (sprites[i].type == type)
            {
                wrongPartImage.sprite = sprites[i].sprite;
                break;
            }
        }

        wrongPartImage.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        m_isShowingWrong = true;
        m_wrongTimer = 5f;
    }
}
