using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PassType { 
    Pass = 0,
    Set = 1,
    Hit = 2,
}

public class VolleyballGameManager : MonoBehaviour
{
    public static VolleyballGameManager instance;

    public Team currentPossesion;
    public int amountOfHits;
    public Ball currentBall;
    public Vector3 topOfNet; // Gotten from NetTop GameObject I guess

    public static float GROUND_OFFSET = 1.344f;

    public float centerPosition;

    void Start()
    {
        if (instance == null) 
        {
            instance = this;
        }

        else
        {
            Destroy(this.gameObject);
        }

        centerPosition = GameObject.FindGameObjectWithTag("Ground").transform.position.z; 
    }

    public void ChangePossesion()
    {
        amountOfHits = 0;
        currentPossesion = (currentPossesion == Team.RED) ? Team.BLUE : Team.RED;
    }

    public bool HandleInteraction(VolleyballPlayer player)
    {
        if (player.team != currentPossesion)
        {
            ChangePossesion();
            return true;
        }

        return false;
    }

    public void IncrementHitAmount() { amountOfHits++; }

    public Team FindTeamLandingZone()
    {
        return (currentBall.estimatedLandingPos.z > centerPosition) ? Team.BLUE : Team.RED;
    }
}
