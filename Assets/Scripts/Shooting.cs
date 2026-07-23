using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    public float bulletForce = 20f;
    private bool isButtonPressed;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    public void OnLeftMouseButtonClick(InputAction.CallbackContext context)
    {
        isButtonPressed = context.ReadValueAsButton();
    }

    private void Shoot()
    {
        if (isButtonPressed)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();
            bulletRB.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
        }        
    }
}
