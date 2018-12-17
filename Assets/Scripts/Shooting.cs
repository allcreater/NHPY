using UnityEngine;

public class Shooting : MonoBehaviour 
{
    public GameObject bulletPrototype;
    public float bulletSpeed = 10.0f;
    public float reloadTime = 0.5f;

    public bool useBallisticTrajectory = false;

    private System.Func<Vector3, float, Vector3> trajectoryCalculator;
    private float reloadingTimer = 0.0f;
    private Vector3 m_prevPos, m_velocity;

    // Use this for initialization
    void Start () 
    {
        m_prevPos = transform.position;

        if (useBallisticTrajectory)
            trajectoryCalculator = BallisticTrajectoryDirection;
        else
            trajectoryCalculator = FlatTrajectoryDirection;
    }

    // Update is called once per frame
    void Update ()
    {
        reloadingTimer += Time.deltaTime;
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

    public void ShootTo(Vector3 position)
    {
        //Debug.DrawRay(transform.position, direction, Color.green, 0.1f);

        if (reloadingTimer > reloadTime)
        {
            reloadingTimer = 0.0f;
            var bullet = GameObject.Instantiate(bulletPrototype, transform.position, Random.rotation);

            var velocity = trajectoryCalculator(position, 10.0f);
            bullet.GetComponent<Rigidbody>().velocity = velocity + m_velocity;
        }
    }

    void FixedUpdate()
    {
        //V = dx/dt. We can not just ask rigid body because it's position could be manually changed, also not every shooter could have rigidbody
        m_velocity = (transform.position - m_prevPos) / Time.fixedDeltaTime;
        m_prevPos = transform.position;
    }
}
