using UnityEngine;

public class Weaponary : MonoBehaviour {
    public static Weaponary instance;

    public void Awake() {
        instance = this;
    }
    public int ProjectileCount { get; set; }
}
