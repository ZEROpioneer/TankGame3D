using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    [Header("移动设置")]
    public float moveSpeed = PlayerSetManager.CurrentMoveSpeed;
    public float rotationSpeed = PlayerSetManager.CurrentRotateSpeed;
    
    [Header("射击设置")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.5f;
    
    private Rigidbody rb;
    private float nextFireTime = 0f;
    
    //public Camera playerCamera;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -0.5f, 0); // 降低重心，防止翻车
        // 如果没有指定摄像机，尝试获取子物体中的摄像机
        /*if (playerCamera == null)
        {
            playerCamera = GetComponentInChildren<Camera>();
        }*/
    }
    
    void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleShooting();
    }
    
    void HandleMovement()
    {
        float moveInput = Input.GetAxis("Vertical");
        Vector3 movement = transform.forward * moveInput * moveSpeed * Time.deltaTime;
        transform.Translate(movement, Space.World);
    }
    
    void HandleRotation()
    {
        float rotateInput = Input.GetAxis("Horizontal");
        float rotation = rotateInput * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, rotation, 0);
    }
    
    void HandleShooting()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }
    
    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            // 给子弹添加向前的力
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            if (bulletRb != null)
            {
                bulletRb.velocity = firePoint.forward * 20f;
            }
        }
    }
}
