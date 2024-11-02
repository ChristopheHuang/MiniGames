using UnityEngine;
[CreateAssetMenu(fileName = "SkillData", menuName = "Skill/SkillData")]
public class SkillDataSO : ScriptableObject
{
    public string skillName;
    public SkillType skillType;
}
