using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class toVector2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}*/

 
public static class ExtensionMethods
{
    public static Vector2 toVector2(this Vector3 vec3)
    {
        return new Vector2(vec3.x, vec3.y);
    }
}
