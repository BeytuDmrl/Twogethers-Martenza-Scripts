using UnityEngine;



public class DoorController : MonoBehaviour

{

    public Vector3 openOffset = new Vector3(0, 4, 0); // Kap� ne kadar y�kselecek?

    public float speed = 2f;



    private Vector3 closedPos;

    private Vector3 openPos;

    private int activeButtons = 0;



    void Start()

    {

        closedPos = transform.position;

        openPos = closedPos + openOffset;

    }



    void Update()

    {

        // 2 buton bas�l�ysa openPos'a, de�ilse closedPos'a git

        Vector3 target = (activeButtons >= 2) ? openPos : closedPos;

        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

    }



    public void AddButton() => activeButtons++;

    public void RemoveButton() => activeButtons = Mathf.Max(0, activeButtons - 1);

}