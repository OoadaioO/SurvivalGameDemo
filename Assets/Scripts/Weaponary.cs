using UnityEngine;

public class Weaponary : MonoBehaviour {
    public enum WeaponType {
        AutoTrack,
        Spread,
    }

    public static Weaponary instance;
    public int ProjectileCount { get; set; }

    public GameObject bulletPrefab;

    public WeaponType weaponType = WeaponType.AutoTrack;
    public float shootFrequency = 5;

    [Range(0, 360)]
    public float spreadAngle = 360;

    [Range(0.1f, 360f)]
    public float spreadAngleMax = 360;

    [Range(1, 50)]
    public int spreadSegmentCountMin = 1;

    [Range(1, 50)]
    public int spreadSegmentCountMax = 1;

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

        if (weaponType == WeaponType.AutoTrack) {
            // auto track shoot
            ShootAutoTrack();

        } else if (weaponType == WeaponType.Spread) {
            // spread shoot
            ShootSpread();
        }

    }
    private void ShootAutoTrack() {
        if (GameController.instance.PlayerScript.NoNearbyEnemies) {
            return;
        }
        Vector2 nearestEnemyPosition = GameController.instance.PlayerScript.NearestEnemyPosition;
        Vector2 direction = nearestEnemyPosition - (Vector2)transform.position;
        direction.Normalize();
        SpawnBullet(transform.position, direction);
    }

    private void ShootSpread() {
        Vector2 direction = GameController.instance.player.transform.right;

        float startAngle = Random.Range(0, spreadAngle);
        int segmentCount = Random.Range(spreadSegmentCountMin, spreadSegmentCountMax);
        float deltaAngle = spreadAngleMax / segmentCount;
        for (int i = 0; i < segmentCount; i++) {
            float angleOffset = Random.Range(0, 10);
            float angle = (startAngle + deltaAngle * i + angleOffset) % spreadAngleMax;

            Vector2 angleDirection = Utils.RotateV2(direction, angle);
            angleDirection.Normalize();

            SpawnBullet(transform.position, angleDirection);
        }
    }

    private Bullet SpawnBullet(Vector3 position, Vector2 direction) {
        GameObject bulletGameObject = Instantiate(bulletPrefab, position, Quaternion.identity);
        Bullet bullet = bulletGameObject.GetComponent<Bullet>();
        bullet.MovementDirection = direction;

        int spatialGroup = GameController.instance.GetSpatialGroupStatic(position.x, position.y);
        GameController.instance.bulletSpatialGroups[spatialGroup].Add(bullet);
        ProjectileCount++;

        return bullet;
    }
}

