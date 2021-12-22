using UnityEngine.AI;
using UnityEngine;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMoveVerText : MonoBehaviour
{
    //[SerializeField] PlayerController playerController;
    NavMeshAgent agent;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void OnDetectObject(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            agent.destination = collider.transform.position;
        }
    }



    /*
    // Update is called once per frame
    void Update()
    {
        agent.destination = playerController.transform.position;
    }
    */
}
