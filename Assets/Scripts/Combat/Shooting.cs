using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.Networking;

public static class MathUtils
{
    //public static float GaussRand()
    //{
        //TODO: to be implemented
    //}
}

public class ShootToParams
{
    public Vector3 targetPosition { get; }
    public Transform target { get; }
    public IReadOnlyList<string> activeWeapons => activeWeaponsList;

    private List<string> activeWeaponsList;

    public void SetWeaponUsed(string weaponType)
    {
        if (!activeWeaponsList.Remove(weaponType))
            throw new System.InvalidOperationException("weapon type not exisis");
    }

    public ShootToParams(Vector3 targetPos, Transform target, params string[] activeWeapons)
    {
        targetPosition = targetPos;
        activeWeaponsList = activeWeapons.ToList();
        this.target = target;
    }
}

public class Shooting : NetworkBehaviour
{
    public GameObject bulletPrototype;
    public float bulletSpeed = 10.0f;
    public float targetDeviationRange = 1.0f;
    public float reloadTime = 0.5f;
    public float bullets = 1;

    public bool requireAim = false;
    public bool useBallisticTrajectory = false;

    public string[] weaponType;

    [Header("Shooting bursts")]
    public float burstReloadTime = 0.0f;
    public int burstSize = 0;

    private AudioSource audioSource;
    public AudioClip[] clips;
    public float minPitch = 1.0f;
    public float maxPitch = 1.0f;

    private System.Func<Vector3, float, Vector3> trajectoryCalculator;
    private float reloadingTimer = 0.0f;
    private Vector3 m_prevPos, m_velocity;
    private int shootsUntilReload;

    private void Awake()
    {
        var clip = clips[Random.Range(0, clips.Length)];
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clip;
    }

    private void Start()
    {
        m_prevPos = transform.position;

        if (useBallisticTrajectory)
            trajectoryCalculator = BallisticTrajectoryDirection;
        else
            trajectoryCalculator = FlatTrajectoryDirection;

        shootsUntilReload = burstSize;
    }

    private void Update()
    {
        reloadingTimer = Mathf.Max(reloadingTimer - Time.deltaTime, 0.0f);
        audioSource.pitch = Random.Range(minPitch, maxPitch);
    }

    private Vector3 FlatTrajectoryDirection(Vector3 targetPos, float desiredHeight)
    {
        var direction = targetPos - transform.position;
        return direction.normalized * bulletSpeed;
    }

    private Vector3 BallisticTrajectoryDirection(Vector3 targetPos, float desiredHeight)
    {
        Debug.Assert(Mathf.Approximately(Physics.gravity.normalized.y, -1.0f), "BallisticTrajectoryDirection supports only gravity at y axis!");

        var flatDirection = targetPos - transform.position;
        float delthaY = flatDirection.y;
        var delthaXZ = new Vector2(flatDirection.x, flatDirection.z);

        float g = -Physics.gravity.y;
        float V0y = Mathf.Sqrt(2 * g * desiredHeight);
        float subSqr = V0y * V0y - 2 * g * delthaY;
        if (subSqr < 0.0)
            return Vector3.zero;

        float V0xz = delthaXZ.magnitude * g / (V0y + Mathf.Sqrt(subSqr));

        delthaXZ.Normalize();
        var desiredVelocity = new Vector3(V0xz * delthaXZ.x, V0y, V0xz * delthaXZ.y) - m_velocity;
        //Debug.Log($"velocity is {desiredVelocity.magnitude}");
        return Vector3.ClampMagnitude(desiredVelocity, bulletSpeed);
    }

    //[Command]
    public void ShootTo(ShootToParams shootParameters)
    {
        var intersection = weaponType.Intersect(shootParameters.activeWeapons).ToList();
        if (intersection.Count == 0)
            return;

        if (!shootParameters.target && requireAim)
            return;

        //Debug.DrawRay(transform.position, direction, Color.green, 0.1f);

        if (Mathf.Approximately(reloadingTimer, 0.0f))
        {
            if (burstSize > 0 && --shootsUntilReload <= 0)
            {
                shootsUntilReload = burstSize;
                reloadingTimer = burstReloadTime;
            }
            else
                reloadingTimer = reloadTime;


            GameObject bullet = Instantiate(bulletPrototype, transform.position, transform.rotation);            
            Rigidbody rigidBody = bullet.GetComponent<Rigidbody>();
            NetworkServer.Spawn(bullet);
            if (rigidBody)
            {
                var velocity = trajectoryCalculator(shootParameters.targetPosition + Random.insideUnitSphere * targetDeviationRange, 10.0f);
                rigidBody.velocity = velocity + m_velocity;
            }

            //TODO: remove
            var homingMissile = bullet.GetComponent<HomingMissile>();
            if (homingMissile)
                homingMissile.target = shootParameters.target;

            if (bullets > 0 && --bullets == 0)
                gameObject.SetActive(false);

            foreach (var wt in intersection)
                shootParameters.SetWeaponUsed(wt);

            if (audioSource)
                audioSource.PlayOneShot(audioSource.clip);
            
        }
    }

    void FixedUpdate()
    {
        //V = dx/dt. We can not just ask rigid body because it's position could be manually changed, also not every shooter could have rigidbody
        m_velocity = (transform.position - m_prevPos) / Time.fixedDeltaTime;
        m_prevPos = transform.position;
    }
}
