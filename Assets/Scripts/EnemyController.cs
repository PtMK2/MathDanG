using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField]
    private EnemyData _enemyData;// �f�[�^������擾

    public string enemyName;
    public string enemyDescription;
    // Start is called before the first frame update
    void Start()
    {
        enemyName = _enemyData.enrmyName;
        enemyDescription = _enemyData.enemyDescription;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
