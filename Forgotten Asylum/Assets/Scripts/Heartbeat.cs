using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heartbeat : MonoBehaviour
{
    float distance;
    float clampedDistance;

    [SerializeField] float heartbeatVolume;
    [SerializeField] float heartbeatPitch;
    [SerializeField] float maxDistance = 15;
    [SerializeField] float minDistance = 0;
    [SerializeField] float maxPitch;
    [SerializeField] float minPitch;

    AudioSource heartbeatAudio;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        heartbeatAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(player.transform.position, transform.position);

        clampedDistance = Mathf.Clamp01((distance - maxDistance) / (minDistance - maxDistance));

        heartbeatVolume = Mathf.Lerp(0f, 1f, clampedDistance);
        heartbeatPitch = Mathf.Lerp(minPitch, maxPitch, clampedDistance);

        heartbeatAudio.volume = heartbeatVolume;
        heartbeatAudio.pitch = heartbeatPitch;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, maxDistance);
    }
}
