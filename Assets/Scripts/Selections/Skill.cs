using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    public int horizontalNum;
    public string name;
    public SkillType skillType;
    private Text skillNameText;
    
    private void Awake()
    {
        skillNameText = GetComponentInChildren<Text>();
        
        // Random skill type
        skillType = (SkillType)Random.Range(0, 4);
        name = skillType.ToString();
    } 

    public void ButtonEvents()
    {
        switch (skillType)
        {
            case SkillType.BuffGate:
                CreateBuffGate();
                break;
            case SkillType.Blade:
                // Add behavior for Blade skill
                break;
            case SkillType.ShootUpgrade:
                // Add behavior for ShootUpgrade skill
                break;
            case SkillType.Shield:
                // Add behavior for Shield skill
                break;
        }
    }
    
    /// <summary>
    /// Buff Gate Setting Area
    /// </summary>
    [Header("Buff Gate")]
    public GameObject buffGatePrefab;
    private static GameObject buffGate;
    
    void CreateBuffGate()
    {
        if (buffGate == null)
        {
            GameObject buffGateInstance = Instantiate(buffGatePrefab, GameObject.Find("BuffGateSpawnPoint").transform.position, Quaternion.identity);
            buffGate = buffGateInstance;
        }
    }
}