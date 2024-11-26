using System.Collections.Generic;
using UnityEngine;

public class ExperiencePoint : MonoBehaviour
{
    public int Amount { get; set; }
    public int SpatialGroup { get; set; }
    public HashSet<int> SurroundingSpatialGroups { get; set; }

}
