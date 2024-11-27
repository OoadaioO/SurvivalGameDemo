using UnityEngine;

public class Weaponary : MonoBehaviour {

    public static Weaponary instance;
    public int ProjectileCount { get; set; }

    public GameObject bulletPrefab;


    public float shootFrequency = 5;
    private float shootTimer = 0;
    private float shootTimerCD;



    public void Awake() {
        instance = this;
    }

    private void Start() {
        shootTimerCD = 1 / shootFrequency;
    }

    private void FixedUpdate() {
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootTimerCD) {
            Shoot();
            shootTimer = 0;
        }
    }

    private void Shoot() {

        if (GameController.instance.PlayerScript.NoNearbyEnemies) {
            return;
        }
        Vector2 nearestEnemyPosition = GameController.instance.PlayerScript.NearestEnemyPosition;
        Vector2 direction = nearestEnemyPosition - (Vector2)transform.position;
        direction.Normalize();

        GameObject bulletGameObject = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Bullet bullet = bulletGameObject.GetComponent<Bullet>();
        bullet.MovementDirection = direction;
        int spatialGroup = GameController.instance.GetSpatialGroupStatic(transform.position.x, transform.position.y);
        GameController.instance.bulletSpatialGroups[spatialGroup].Add(bullet);

        ProjectileCount++;
    }
}
