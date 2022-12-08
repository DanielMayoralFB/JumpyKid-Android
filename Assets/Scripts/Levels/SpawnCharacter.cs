using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCharacter : MonoBehaviour
{
    /// <summary>
    /// Assaing the respawn position to this gameobject position and spawn the player in this position. Then destroy this gameobject.
    /// </summary>
    void Awake()
    {
        GameManager.instance.setRespawnPosition(new Vector3(transform.position.x, transform.position.y +1, transform.position.z));
        Instantiate(GameManager.instance.getJugadorLista(GameManager.instance.getPlayerChoice()), transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
