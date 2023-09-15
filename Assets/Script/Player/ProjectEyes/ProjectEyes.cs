using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectEyes : MonoBehaviour
{
    public Transform EyesPoint;
    public GameObject projectEyes;
    public void FireProject()
    {
        GameObject projectfire = Instantiate(projectEyes, EyesPoint.position, projectEyes.transform.rotation);
        Vector3 origScale = projectfire.transform.localScale;
        projectfire.transform.localScale = new Vector3(origScale.x * transform.localScale.x > 0 ? 1 : -1,
            origScale.y, origScale.z);
    }

}
