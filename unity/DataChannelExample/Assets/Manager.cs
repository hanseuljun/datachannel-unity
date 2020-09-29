using UnityEngine;

public class Manager : MonoBehaviour
{
    void Start()
    {
        string[] input = new string[] { "testing", "a" , "bcd"};
        print("test: " + Plugin.test(input, input.Length));
    }

    void Update()
    {
        
    }
}
