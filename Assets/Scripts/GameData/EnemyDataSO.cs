using UnityEngine;
[CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy/EnemyData")]
public class EnemyDataSO : ScriptableObject
{
    public GameObject enemyPrefab;
    
    public EnemyType enemyType;
}