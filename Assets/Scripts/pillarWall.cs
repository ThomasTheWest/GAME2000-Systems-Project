using UnityEngine;

public class pillarWall : MonoBehaviour
{
    public Transform pointTop;
    public Transform pointBottom;

    private void Update()
    {
        position();
    }

    void position()
    {
        //uhhhhh basically this sets the wall segment's position between the two points, top and bottom
        transform.position = (pointTop.position + pointBottom.position) / 2;

        //this sets the scale (and hopefully the mesh collider will also stretch???? idk)
        float distance = Vector3.Distance(pointTop.position, pointBottom.position);
        transform.localScale = new Vector3(distance, transform.localScale.y, transform.localScale.z);

        //this just rotates it so it's alligned with the points
        Vector3 dir = pointBottom.position - pointTop.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}