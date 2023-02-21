using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthSystem : MonoBehaviour
{
    public int health = 3;
    public int enenmyType = 1;
    public GameObject deathAnimation;
    
    //AudioSource _audioSource;


    void Start()
    {
        //_audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    
    private void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Projectile")){
            health -= 1;
            
            if (health <= 0){
                Instantiate(deathAnimation, transform.position, Quaternion.identity);
                if (enenmyType == 1){
                    GetComponent<Enemy1>().releaseCow();
                }
                else if (enenmyType == 2){
                    GetComponent<Enemy2>().releaseCow();
                }
                else if (enenmyType == 3){
                    GetComponent<Enemy3>().releaseCow();
                }
                else{
                    GetComponent<Enemy4>().releaseCow();
                }
                
                Destroy(gameObject);
            }
            
        }
        else if (other.CompareTag("Kill Zone")){
            Destroy(gameObject);
        }

    }
}
