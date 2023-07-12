using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public float Speed = 5;
    public GameObject ball;
    public float shootForce = 10;
    public float shootOriginDistance = 1;
    public GameObject cam;
    Rigidbody2D rb;
    float cd = 5;
    float cdTimer = 0;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        cdTimer -= Time.deltaTime;
        float camAux = cam.transform.position.z;
        cam.transform.position = Vector2.Lerp(cam.transform.position, transform.position, Speed);
        cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, camAux);

        Vector2 move = Application.isMobilePlatform ? InputHandle.joystickAxis : new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        transform.Translate(move.normalized * Time.deltaTime * Speed, Space.World);
        if (move.magnitude != 0) transform.up = move.normalized;

        if (Application.isMobilePlatform ? InputHandle.buttonDown[0] : Input.GetButtonDown("Cancel")) Application.Quit();
        if (Application.isMobilePlatform ? InputHandle.buttonDown[1] : Input.GetButtonDown("Fire1"))
        {
            var aux = Instantiate(ball);
            aux.transform.position = transform.position + transform.up * shootOriginDistance;
            aux.GetComponent<Rigidbody2D>().AddForce(transform.up * shootForce);
            Destroy(aux, 5);
        }
        if (Application.isMobilePlatform ? InputHandle.buttonDown[2] : Input.GetButtonDown("Jump"))
        {
            if (cdTimer <= 0)
            {
                rb.AddForce(transform.up * Speed * 100);
                cdTimer = cd;
            }
        }
    }
}
