using UnityEngine;

public class UIExperienceBar : MonoBehaviour
{
    public static UIExperienceBar instance;


    private void Awake()
    {
        instance = this;
    }


    public void SetLevelText(int level) {
        Debug.Log("Setting level text to: " + level);
    }

    public void UpdateExperienceBar(long current, long max) {
        Debug.Log("Updating experience bar: " + current + " / " + max);
    }
}
