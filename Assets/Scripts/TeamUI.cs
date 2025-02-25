using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamUI : MonoBehaviour
{
    public static TeamUI instance;
    public Sprite[] flagTeam;
    public string[] nameTeam;
    public Sprite[] starTeam;
    public int[] starNumberTeam;
    public Sprite[] stadiums;
    public string[] typePowers;

    public Sprite[] head, body;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
