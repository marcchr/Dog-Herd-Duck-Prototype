using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float radius = 5f;
    [Range(1, 360)] public float angle = 45f;
    public LayerMask targetLayer;
    public LayerMask obstructionLayer;

    public GameObject playerRef;
    public Collider2D[] rangeCheck;

    public List<Transform> objectsInFOV;
    public float pushSpeed = 3f;

    void Start()
    {
        objectsInFOV = new List<Transform>();
        // playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVCheck());
    }

    private IEnumerator FOVCheck()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FOV();
            //PushAway();
        }
    }

    private void FOV()
    {
        objectsInFOV.Clear();
        rangeCheck = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);

        if (rangeCheck.Length > 0 )
        {
            for (int i = 0; i < rangeCheck.Length; i++)
            {
                Transform target = rangeCheck[i].transform;

                Vector2 directionToTarget = (target.position - transform.position).normalized;

                if (Vector2.Angle(transform.up, directionToTarget) < angle * 0.5)
                {
                    float distanceToTarget = Vector2.Distance(transform.position, target.position);

                    if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionLayer))
                    {
                        objectsInFOV.Add(target); 
                        //objectsInFOV.Add(target.gameObject);
                        //target.gameObject.GetComponent<DuckMovement>().inFOV = true;
                        //canSee = true;
                    }
                }
            }
        }
    }

    private void PushAway()
    {
        float speedMultiplier = 1f;
        float step = pushSpeed * speedMultiplier * Time.deltaTime;
        for (int i = 0; i < objectsInFOV.Count; i++)
        {
            Transform target = objectsInFOV[i].transform;
            Vector2 directionToTarget = (target.position - transform.position).normalized;
            
            speedMultiplier = 5f * radius/Vector2.Distance(target.position, transform.position);

            target.transform.position = Vector2.MoveTowards(target.position, (Vector2)target.position+directionToTarget, step);
        }

        for (int i = 0; i < rangeCheck.Length; i++)
        {
            Transform target = rangeCheck[i].transform;
            Vector2 directionToTarget = (target.position - transform.position).normalized;

            speedMultiplier = 1f * radius / Vector2.Distance(target.position, transform.position);

            target.transform.position = Vector2.MoveTowards(target.position, (Vector2)target.position + directionToTarget, step);
        }
    }

    private void FixedUpdate()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        transform.up = direction;

        PushAway();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, radius);

        Vector3 angle01 = DirectionFromAngle(-transform.eulerAngles.z, -angle * 0.5f);
        Vector3 angle02 = DirectionFromAngle(-transform.eulerAngles.z, angle * 0.5f);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + angle01 * radius);
        Gizmos.DrawLine(transform.position, transform.position + angle02 * radius);

            for (int i = 0; i < objectsInFOV.Count; i++)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, objectsInFOV[i].transform.position);

            }

    }

    private Vector2 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
