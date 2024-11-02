using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveOS", menuName = "Wave/WaveOS")]

public class WaveOS : ScriptableObject
{
    public int wavenumber;
    
    public EnemyType enemyType;

    public List<Wave> waveList = new List<Wave>();
}