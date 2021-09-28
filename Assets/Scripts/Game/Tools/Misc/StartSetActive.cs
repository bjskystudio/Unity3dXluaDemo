using System.Reflection.Emit;
using UnityEngine;

public class StartSetActive : MonoBehaviour
{
    public bool isActiveOnStart = false;

    private void Awake()
    {
        gameObject.SetActive(isActiveOnStart);
    }
}
