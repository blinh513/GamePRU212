using UnityEngine;
using UnityEngine.UI;

public class HBController : MonoBehaviour
{
    private Slider healthBarFill;
    private Transform target;
    private Vector3 offset;

    public Slider HealthBarFill { get => healthBarFill; set => healthBarFill = value; }
    public Transform Target { get => target; set => target = value; }
    public Vector3 Offset { get => offset; set => offset = value; }

    void Start()
    {
            healthBarFill = gameObject.GetComponent<Slider>();
            healthBarFill.value = 1;
    }

    void Update()
    {
        if (target != null)
        {
            transform.position = Camera.main.WorldToScreenPoint(target.position + offset);
        }
    }

    public void SetHealth(float health, float maxHealth)
    {
        float amount = health / maxHealth;
        healthBarFill.value = amount;
    }
}