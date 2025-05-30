using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour
{
    GameObject player;
    [SerializeField] Vector3 originalScale;
    [SerializeField] Vector3 distanceFromPlayer;
    [SerializeField] float division = 4f;
    [SerializeField] float distance;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        originalScale = player.transform.localScale;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(player.transform.position, transform.position);
        //distanceFromPlayer = (player.transform.position - transform.position).magnitude;

        player.transform.localScale = new Vector3(distance / division, distance / division, originalScale.z);
    }

    private void OnDisable()
    {
        player.transform.localScale = originalScale;
    }
}
