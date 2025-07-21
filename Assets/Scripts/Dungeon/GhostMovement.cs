using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    public float speed = 3f;
    public float changeDirection = 1f;
    public Transform area;
    public float edgeBuffer = 0.3f;

    private Vector3 dir;
    private float t;
    private Rigidbody rb;
    private Vector3 minB;
    private Vector3 maxB;

    // Stuck prevention
    private Vector3 lastPos;
    private float stuckT = 0f;
    public float stuckInterval = 0.5f;
    public float stuckDist = 0.1f;

    // Direction history to prevent spinning
    private const int histLen = 5;
    private Vector3[] dirHist = new Vector3[histLen];
    private int histIdx = 0;

    // Spin check
    private int spinCount = 0;
    private const int spinMax = 6;
    private Vector3 lastDir;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lastPos = rb.position;
        PickNewDir();
        lastDir = dir;

        if (area != null)
        {
            Vector3 c = area.position;
            Vector3 h = area.localScale * 0.5f;
            minB = c - h;
            maxB = c + h;
        }
    }

    void FixedUpdate()
    {
        Vector3 next = rb.position + dir * speed * Time.fixedDeltaTime;

        if (area != null)
        {
            stuckT += Time.fixedDeltaTime;
            if (stuckT >= stuckInterval)
            {
                float d = Vector3.Distance(rb.position, lastPos);
                if (d < stuckDist)
                {
                    PickEscapeDir();
                    t = 0f;
                }
                lastPos = rb.position;
                stuckT = 0f;
            }

            bool near = false;
            if (next.x < minB.x + edgeBuffer || next.x > maxB.x - edgeBuffer)
                near = true;
            if (next.z < minB.z + edgeBuffer || next.z > maxB.z - edgeBuffer)
                near = true;
            if (near)
            {
                TryPickNewDir();
                t = 0f;
            }
        }

        rb.linearVelocity = dir * speed;

        t += Time.fixedDeltaTime;
        if (t >= changeDirection)
        {
            TryPickNewDir();
            t = 0f;
        }

        if (dir.sqrMagnitude > 0.01f)
        {
            Quaternion rot = Quaternion.LookRotation(dir, Vector3.up);
            rb.MoveRotation(rot);
        }
    }

    void TryPickNewDir()
    {
        if (spinCount >= spinMax)
        {
            ForceFarDir();
            spinCount = 0;
        }
        else
        {
            PickNewDir();
            if (Vector3.Dot(dir, lastDir) < 0.5f)
                spinCount = 0;
            else
                spinCount++;
            lastDir = dir;
        }
    }

    void ForceFarDir()
    {
        if (area != null)
        {
            Vector3 c = area.position;
            Vector3 away = (rb.position - c).normalized;
            away.y = 0;
            if (away.sqrMagnitude < 0.01f)
                away = new Vector3(Random.value, 0, Random.value).normalized;
            SetDir(away);
        }
        else
        {
            PickRandDirHist();
        }
    }

    void PickEscapeDir()
    {
        if (area != null)
        {
            Vector3 c = area.position;
            Vector3 esc = (c - rb.position).normalized;
            esc.y = 0;
            if (esc.sqrMagnitude < 0.01f)
            {
                PickRandDirHist();
            }
            else
            {
                SetDir(esc);
            }
        }
        else
        {
            PickRandDirHist();
        }
    }

    void PickRandDirHist()
    {
        Vector3 newDir;
        int tries = 0;
        bool ok = false;
        do
        {
            float a = Random.Range(0f, 360f);
            newDir = new Vector3(Mathf.Cos(a * Mathf.Deg2Rad), 0, Mathf.Sin(a * Mathf.Deg2Rad)).normalized;
            ok = true;
            for (int i = 0; i < dirHist.Length; i++)
            {
                if (dirHist[i] != Vector3.zero && Vector3.Dot(newDir, dirHist[i]) > 0.85f)
                {
                    ok = false;
                    break;
                }
            }
            tries++;
        } while (!ok && tries < 20);

        SetDir(newDir);
    }

    void PickNewDir()
    {
        PickRandDirHist();
    }

    void SetDir(Vector3 d)
    {
        dir = d;
        dirHist[histIdx] = d;
        histIdx = (histIdx + 1) % histLen;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("wall"))
        {
            TryPickNewDir();
            t = 0f;
        }
    }
}