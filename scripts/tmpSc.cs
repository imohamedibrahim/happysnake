using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tmpSc : MonoBehaviour
{
    Animator anim;
    public bool ok = false;
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (anim != null && ok)
            StartCoroutine(tr());
    }

    IEnumerator tr()
    {
        anim.SetBool("eat", true);
        yield return new WaitForSeconds(3);
        anim.SetBool("eat", false);
    }
}
