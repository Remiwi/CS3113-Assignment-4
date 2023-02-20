using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineEnemyController : MonoBehaviour
{
    VineEnemy vine_enemy;

    // State
    string state = "down"; // down/hurt/up
    float timer = 0;

    // Down stuff
    public float time_per_movement_down = 1;

    // Up stuff
    public float time_per_movement_up;
    Cow captured_cow;

    // Hurt stuff
    public int shots_per_tile = 5; // How many times the flower needs to be shot before it goes back 1 tile
    public float time_to_recover = 5f; // How many seconds the enemy will stay still after being hurt
    int shots_taken = 0;


    void Start()
    {
        vine_enemy = GetComponent<VineEnemy>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (state == "down")
        {
            GoingDown();
        }
        else if (state == "up")
        {
            GoingUp();
        }
        else if (state == "hurt")
        {
            Hurting();
        }
    }

    void GoingDown()
    {
        if (timer < time_per_movement_down)
        {
            return;
        }

        timer = 0;
        vine_enemy.SetPosition(vine_enemy.GetPosition() + 1);
    }

    void GoingUp()
    {
        if (timer < time_per_movement_up)
        {
            return;
        }

        timer = 0;
        vine_enemy.SetPosition(vine_enemy.GetPosition() - 1);
    }

    void Hurting()
    {
        if (timer < time_to_recover)
        {
            return;
        }

        timer = 0;
        state = "down";
    }

    public void GrabCow(Cow cow)
    {
        if (state == "hurt")
        {
            return;
        }

        captured_cow = cow;
        cow.Capture(vine_enemy.GetFlowerTransform(), Vector3.zero);
        state = "up";
        timer = 0;
    }

    public void Hurt()
    {
        vine_enemy.TriggerHurtAnimation();
        shots_taken++;
        if (shots_taken < shots_per_tile)
        {
            return;
        }

        shots_taken = 0;
        vine_enemy.SetPosition(vine_enemy.GetPosition() - 1);
        state = "hurt";
        timer = 0;
        if (captured_cow)
        {
            captured_cow.Release();
            captured_cow = null;
        }
    }
}