using UnityEngine;

public class TurnTrigger : MonoBehaviour
{
    public float targetYRotation; // Rotația pe axa Y pe care player-ul trebuie să o aibă (ex: 90 pentru dreapta)

    private void OnDrawGizmos()
    {
        // Desenează un indicator vizual în scenă pentru a vedea direcția cotiturii
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + Quaternion.Euler(0, targetYRotation, 0) * Vector3.forward * 2);
    }
}
