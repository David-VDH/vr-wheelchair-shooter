using UnityEngine;

public class ShellCasingLifeTime : MonoBehaviour
{
    [SerializeField] float lifeDuration = 240f;

    private void Start()
    {
        DestroyCasingAfterLifetime();
    }

    private void DestroyCasingAfterLifetime()
    {
        Destroy(gameObject, lifeDuration);
    }
}
