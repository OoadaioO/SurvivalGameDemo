using UnityEngine;

public class UIHealthBar : MonoBehaviour {
    public static UIHealthBar instance;


    private void Awake() {
        instance = this;
    }
    
    public void UpdateBar(int health, int maxHealth) {
        Debug.Log("Updating health bar: " + health + " / " + maxHealth);
    }

}
