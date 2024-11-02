using System;
using UnityEngine;
using UnityEngine.UI;

public class SkillGenerator : MonoBehaviour
{
    public GameObject skillCardPrefab;
    public int skillCount = 3;

    public ScrollRect scrollRect;

    private void Start()
    {
        if (scrollRect == null)
        {
            scrollRect = GetComponentInChildren<ScrollRect>();
        }

        if (scrollRect == null || scrollRect.content == null)
        {
            Debug.LogError("ScrollRect is not set or content is not set.");
            return;
        }

        GenerateSkill();
    }

    void GenerateSkill()
    {
        for (int i = 0; i < skillCount; i++)
        {
            GameObject skillCard = Instantiate(skillCardPrefab, scrollRect.content);
            skillCard.gameObject.GetComponentInChildren<Text>().text = skillCard.GetComponent<Skill>().name;
        }
    }
}