using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AIMode { Moving, Attacking, Idling, Wandering }

public class EnemyController : MonoBehaviour {

    public string CurrentStatus = "";
    public AIMode Mode = AIMode.Idling;

    //Moving    
    [HideInInspector]
    public Transform MoveTo;

    //Attack
    [HideInInspector]
    public GameObject AttackTarget;
    [HideInInspector]
    public float Range;

    private NavMeshAgent agent;
    private GameObject player;

    private GenericEnemy thisEnemy;


    void Start () {
        //get the player object from the Global Game script
        //something like player = GameController._____
        agent = GetComponent<NavMeshAgent>();
        //agent.destination = player.position;


        thisEnemy = GetComponentInChildren<GenericEnemy>();

	}

    bool genericEnemyRecieved = false;
	
	void Update () {

        //check to make sure all components can be referenced
        if(this.genericEnemyRecieved == false){
            thisEnemy = GetComponentInChildren<GenericEnemy>();

            if (thisEnemy != null)
            {
                thisEnemy.controller = this;
                this.genericEnemyRecieved = true;
            }
        }



        switch(Mode)
        {
            case AIMode.Moving:
                UpdateMove();
                break;
            case AIMode.Attacking:
                UpdateAttack();
                break;
            case AIMode.Idling:
                UpdateIdle();
                break;
            case AIMode.Wandering:
                UpdateWander(thisEnemy.wanderRange);
                break;
            default:
                //idling
                break;

        }

	}

    //this method is called from Update when the enemy is in Moving mode
    private void UpdateMove() {

    }

    //this method is called from Update when the enemy is in Attacking mode
    private void UpdateAttack() {
        thisEnemy.Attack(player);
    }

    //this method is called from Update when the enemy is in Idle mode
    private void UpdateIdle() {
        //have the enemy move around randomly
        agent.destination = transform.position;
    }

    private void UpdateWander(float wanderRange) {
        Vector3 random = new Vector3(Random.Range(-1.0f * wanderRange, 1.0f * wanderRange), 0, Random.Range(-1.0f * wanderRange, 1.0f * wanderRange));
        agent.destination = transform.position + random;
    }

    public AIMode GetCurrentMode(){
        return Mode;
    }

   public bool InRangeOfTarget(GameObject target){

        //todo

        return false;
    }

    public void Move(GameObject target){

    }

    public void Move(Vector3 position){
        //for the start of move
        //todo
        //some logic for how the enemy will behave in Moving mode
    }

    /*
     * (Optimizing Nav Mesh)
     * 1(Also). Test if any paths are near by that you can take (radius pre determined)
     * 1. Create Path in beginning (Push it to global list [Remove path after it is finished])
     * 2. Move/Test if path is still possible some how
     * 3. If path is not possible create new path continue.
     * 
    */

    GameObject GetNearestTarget(string[] targets, float range) {

        List<GameObject> objects = GetTargetsInRange(targets, range);

        if(objects.Count == 0) {
            //if there are no trees to attack, attack the player
            //agent.destination = player.position;
            return null;
        } else {
            //find the closest tree
            
            GameObject closest = objects[0];
            float closestDistance = Vector3.Distance(transform.position, objects[0].transform.position);
            
            for (int j = 1; j < objects.Count; j++) {
                float tempDistance = Vector3.Distance(transform.position, objects[j].transform.position);
                if(tempDistance < closestDistance) {
                    closest = objects[j];
                    closestDistance = tempDistance;
                }
            }
            agent.destination = closest.transform.position;
            return closest;
        }

    }

    GameObject GetNearestTarget(string target, float range) {
        string[] targetArray = { target };
        return GetNearestTarget(targetArray, range);
    }

    List<GameObject> GetTargetsInRange(string[] targets, float radius)
    {
        //list that contains all of the targets in range
        List<GameObject> targetsInRange = new List<GameObject>();

        //go through all possible tags
        for (int i = 0; i < targets.Length; i++) {
            GameObject[] objects = GameObject.FindGameObjectsWithTag(targets[i]);
            //go through all the objects with the current tag
            for (int j = 0; j < objects.Length; j++) {
                //see if a the tagged object is in range and add it to the list if it is
                Vector3 myPos = transform.position;
                Vector3 otherPos = objects[j].transform.position;
                float distance = Vector3.Distance(myPos, otherPos);
                if(distance < radius) {
                    targetsInRange.Add(objects[j]);
                }
            }
        }

        return targetsInRange;

    }
    List<GameObject> GetTargetsInRange(string target, float radius)
    {
        string[] i = { target };
        return this.GetTargetsInRange(i, radius);
    }
}
