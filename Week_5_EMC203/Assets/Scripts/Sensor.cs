using Unity.VisualScripting;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Transform target;
    // Update is called once per frame
    void Update()
    {
        if (target == null) return;
        var pos1 = new Vector2(target.position.x, target.position.y);
        var pos2 = new Vector2(this.transform.position.x, this.transform.position.y);

        pos1.Normalize();
        pos2.Normalize();

        var dot = Dot(pos1, pos2);
        Debug.Log($" Dot: {Mathf.Acos(dot) * Mathf.Rad2Deg}, Cross: {Cross(pos2, pos1)}");
    }

    public float Dot(Vector2 pos, Vector2 dir)
    {
        return pos.x * dir.x + pos.y * dir.y;
    }

    public float Cross(Vector2 pos, Vector2 dir)
    {
        return pos.x * dir.y - pos.y * dir.x;
    }
}
