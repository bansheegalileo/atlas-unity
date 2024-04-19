using UnityEngine;
using UnityEngine.Audio;

public class DistanceBasedSnapshotFader : MonoBehaviour
{
    public Transform player;
    public AudioMixerSnapshot regSnapshot;
    public AudioMixerSnapshot closeToCarSnapshot;
    public float maxDistance = 10f;
    public float blendTime = 1f;

    private void Update()
    {
        if (player == null || regSnapshot == null || closeToCarSnapshot == null)
            return;

        // Calculate distance to player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Print distance to player
        Debug.Log("Distance to player: " + distanceToPlayer);

        // Calculate normalized distance
        float normalizedDistance = Mathf.Clamp01(distanceToPlayer / maxDistance);

        // Calculate blend parameter
        float blend = 1f - normalizedDistance;

        // Debug statements for transition steps
        Debug.Log("Blend parameter: " + blend);
        Debug.Log("Transitioning from regSnapshot to closeToCarSnapshot");

        // Transition from regular snapshot to close-to-car snapshot
        regSnapshot.TransitionTo(blendTime * blend);

        // Debug statements for transition steps
        Debug.Log("Transitioning from closeToCarSnapshot to regSnapshot");
        Debug.Log("Blend parameter: " + (1 - blend));

        // Transition from close-to-car snapshot to regular snapshot
        closeToCarSnapshot.TransitionTo(blendTime * (1 - blend));
    }
}
