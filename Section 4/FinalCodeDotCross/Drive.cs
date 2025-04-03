using UnityEngine;
using System.Collections;

public class Drive : MonoBehaviour
{
    float speed = 5f;
    public GameObject fuel;
    public Vector3 direction;
    public float stoppingDistance = 0.1f;

    void Start()
    {
        direction = fuel.transform.position - this.transform.position;
        Coords dirNormal = HolisticMath.GetNormal(new Coords(direction));
        direction = dirNormal.ToVector();
        float a = HolisticMath.Angle(new Coords(0, 1, 0), new Coords(direction));
        Debug.Log("Angle to Fuel: " + a);
        bool turnDir = false;
        if (HolisticMath.Cross(new Coords(0, 1, 0), new Coords(direction)).z > 0)
            turnDir = false;
        else if (HolisticMath.Cross(new Coords(0, 1, 0), new Coords(direction)).z < 0)
            turnDir = true;

        Debug.Log("Turn Direction: "+ turnDir);

        Coords newDir = HolisticMath.Rotate(new Coords(0, 1, 0), a, turnDir);

        this.transform.up = new Vector3(newDir.x, newDir.y, newDir.z);

        //do turn calcs

    }

    void Update()
    {
        if(HolisticMath.Distance(new Coords(this.transform.position), 
                                 new Coords(fuel.transform.position)) > stoppingDistance)
            this.transform.position += direction * speed * Time.deltaTime;


    }
}