using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePositionOnLevel : MonoBehaviour
{
    private void OnDisable()
    {
        PlayerPrefs.SetFloat("PlayerPositionX", transform.position.x);
        PlayerPrefs.SetFloat("PlayerPositionY", transform.position.y);
        PlayerPrefs.SetFloat("PlayerPositionZ", transform.position.z);
    }
    private void Start()
    {
        transform.position = new Vector3(PlayerPrefs.GetFloat("PlayerPositionX"), PlayerPrefs.GetFloat("PlayerPositionY"), PlayerPrefs.GetFloat("PlayerPositionZ"));

    }
}
