using UnityEngine;

public class EnableObjectAtStart : MonoBehaviour
{
    [SerializeField]
    private GameObject thing;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        thing.SetActive(true);
    }
}
