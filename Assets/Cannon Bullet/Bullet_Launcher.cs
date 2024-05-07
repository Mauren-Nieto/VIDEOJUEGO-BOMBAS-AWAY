using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bullet_Launcher : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform LaunchPoint;
    public GameObject bullet_cannon;
    public float LaunchSpeed = 10f;

    [Header("****Trajectory Display****")]
    public LineRenderer LineRenderer;
    public int LinePoints = 190;
    public float TimeIntervalinPoints = 0.05f;


    // Update is called once per frame
    void Update()
    {
        if(LineRenderer = null)
        {
            if (Input.GetMouseButton(1))
            {
                DrawTrajectory();
                LineRenderer.enabled = true;
            }
            else
                LineRenderer.enabled = false;
        }
        if(Input.GetMouseButtonDown(0))
        {
            var _bullet_cannon = Instantiate(bullet_cannon, LaunchPoint.position, LaunchPoint.rotation);
            _bullet_cannon.GetComponent<Rigidbody>().velocity = LaunchSpeed * LaunchPoint.up;
        }
    }
    void DrawTrajectory()
    {
        Vector3 origin = LaunchPoint.position;
        Vector3 startVelocity = LaunchSpeed * LaunchPoint.up;
        LineRenderer.positionCount = LinePoints;
        float time = 0;
        for (int i = 0; i < LinePoints; i++)
        {
            // s = u*t + 1/2*g*t*t
            var x = (startVelocity.x * time) + (Physics.gravity.x / 2 * time * time);
            var y = (startVelocity.y * time) + (Physics.gravity.y / 2 * time * time);
            Vector3 point = new Vector3(x, y, 0);
            LineRenderer.SetPosition(i, origin + point);
            time += TimeIntervalinPoints;
            
        }
    }

}
