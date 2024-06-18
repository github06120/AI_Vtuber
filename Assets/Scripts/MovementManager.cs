using System;
using Live2D.Cubism.Core;
using Live2D.Cubism.Framework;
using UnityEngine;


public class MovementManager : MonoBehaviour
{
    private static CubismModel _model;
    
    void Awake()
    {
        _model = this.FindCubismModel();
    }

    [SerializeField] MovementParameter[] movementParameters;
    private void LateUpdate()
    {
        for(int i = 0; i < movementParameters.Length; i++)
        {
            SetMovementParameter(i);
        }
    }
    
    private void Reset(int _num)
    {
        var para = movementParameters[_num];
        para.rightSwitch = false;
        para.rightCount = 0;
        para.rightTime = 0;
        para.leftSwitch = false;
        para.leftCount = 0;
        para.leftTime = 0;
        para.waitSwitch = false;
        para.waitCount = 0;
        para.waitTime = 0;
    }
    private void SetMovementParameter(int _num)
    {
        var para = movementParameters[_num];
        var idKey = para.idKey;
        if(para.rightSwitch)
        {
            MovementRight(_num, idKey);
            if(para.rightCount > para.rightTime)
            {
                Reset(_num);
                SetMovement(_num);
            }
        }
        else if(para.leftSwitch)
        {
            MovementLeft(_num, idKey);
            if(para.leftCount > para.leftTime)
            {
                Reset(_num);
                SetMovement(_num);
            }
        }
        else if(para.waitSwitch)
        {
            MovementWait(_num, idKey);
            if(para.waitCount > para.waitTime)
            {
                Reset(_num);
                SetMovement(_num);
            }
        }
        else
        {
            SetMovement(_num);
        }
        
    }
    private void SetMovement(int _num)
    {
        var para = movementParameters[_num];
        int action;
        if(para.nowPos >= para.paraMax)
        {
            action = UnityEngine.Random.Range(0, 2);
            if(action == 1){action = action + 1;}
        }
        else if(para.nowPos <= para.paraMin)
        {
            action = UnityEngine.Random.Range(0, 2);
        }
        else
        {
            action = UnityEngine.Random.Range(0, 3);
        }
        if(action == 0)
        {
            var waitMin = para.waitTimeMin;
            var waitMax = para.waitTimeMax;
            var waitTime = UnityEngine.Random.Range(waitMin, waitMax);

            para.waitSwitch = true;
            para.waitTime = waitTime;
        }
        else if(action == 1)
        {
            var rightMin = para.rightTimeMin;
            var rightMax = para.rightTimeMax;
            var rightTime = UnityEngine.Random.Range(rightMin, rightMax);

            para.rightSwitch = true;
            para.rightTime = rightTime;
        }
        else if(action == 2)
        {
            var leftMin = para.leftTimeMin;
            var leftMax = para.leftTimeMax;
            var leftTime = UnityEngine.Random.Range(leftMin, leftMax);

            para.leftSwitch = true;
            para.leftTime = leftTime;
        }
        else
        {
            return;
        }
    }
    private void MovementRight(int _num, int _idKey)
    {
        var modelPara = _model.Parameters[_idKey];
        var movePara = movementParameters[_num];
        movePara.rightCount = movePara.rightCount + 1;
        var _speedMin = movePara.speedMin;
        var _speedMax = movePara.speedMax;
        var speed = UnityEngine.Random.Range(_speedMin, _speedMax);
        var _nowPos = movePara.nowPos;
        var move = _nowPos + Time.deltaTime * speed;
        if(move > movePara.paraMax){move = movePara.paraMax;}
        modelPara.Value = move;
        movePara.nowPos = move;
    }
    private void MovementLeft(int _num, int _idKey)
    {
        var modelPara = _model.Parameters[_idKey];
        var movePara = movementParameters[_num];
        movePara.leftCount = movePara.leftCount + 1;
        var _speedMin = movePara.speedMin;
        var _speedMax = movePara.speedMax;
        var speed = UnityEngine.Random.Range(_speedMin, _speedMax);
        var _nowPos = movePara.nowPos;
        var move = _nowPos - Time.deltaTime * speed;
        if(move < movePara.paraMin){move = movePara.paraMin;}
        modelPara.Value = move;
        movePara.nowPos = move;
    }
    private void MovementWait(int _num, int _idKey)
    {
        var modelPara = _model.Parameters[_idKey];
        var movePara = movementParameters[_num];
        movePara.waitCount = movePara.waitCount + 1;
        modelPara.Value = movePara.nowPos;
    }
}
