using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void OnCollisionStay2D(Collision2D collision)
    {
        print(name);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print(name);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        print(name);
    }
}
