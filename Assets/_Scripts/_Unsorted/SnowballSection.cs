using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballSection : MonoBehaviour
{
    public List<GameObject> iceSpikes;
    public int score;
    private bool hasPlayed;
    private Animator anim;
    private GameObject cliff, ogWalrus, fakeWalrus;


    private void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();
        cliff = transform.GetChild(3).gameObject;
        ogWalrus = transform.GetChild(9).gameObject;
        fakeWalrus = transform.GetChild(10).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (score >= 5 && !hasPlayed)
        {
            AnimStart();
            hasPlayed = true;
        }
    }


    void AnimStart()
    {
        anim.SetBool("Play", true);

        
        Invoke("DeactivateItems", 9);
    }

    void DeactivateItems()
    {
        cliff.SetActive(false);
        ogWalrus.SetActive(false);
        fakeWalrus.SetActive(false);
        for (int i = 0; i < iceSpikes.Count; i++)
        {
            iceSpikes[i].gameObject.SetActive(false);
        }
    }
}
