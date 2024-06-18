using UnityEngine;

public class MovementParameter : MonoBehaviour
{
    [SerializeField] public int idKey;
    public float nowPos {get; set;}
    [SerializeField] public float paraMin;
    [SerializeField] public float paraMax;
    [SerializeField] public float speedMin;
    [SerializeField] public float speedMax;
    public bool rightSwitch = false;
    public int rightCount {get; set;}
    public int rightTime {get; set;}
    [SerializeField] public int rightTimeMin;
    [SerializeField] public int rightTimeMax;
    public bool leftSwitch = false;
    public int leftCount {get; set;}
    public int leftTime {get; set;}
    [SerializeField] public int leftTimeMin;
    [SerializeField] public int leftTimeMax;
    public bool waitSwitch = false;
    public int waitCount {get; set;}
    public int waitTime {get; set;}
    [SerializeField] public int waitTimeMin;
    [SerializeField] public int waitTimeMax;
}
