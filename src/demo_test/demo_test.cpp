#include <memory>
#include <Windows.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include<iostream>
#include "MP_Public_API.h"
#include "MP_Motion_API.h"

using namespace std;
void RobotStatusCallback(void* pUser, void* pData, int nLen)
{
    ST_ROBOT_STATUS* pUserData = reinterpret_cast<ST_ROBOT_STATUS*>(pUser);
    *pUserData = *reinterpret_cast<ST_ROBOT_STATUS*>(pData);
}



int main()
{
    int64_t llRet = MP_SUCCESS;
    //加点注释
    // Create Handle
    void* pHandle = MP_CreateHandle();
    if (nullptr == pHandle)
    {
        printf("Create Handle fail!\n");
    }
    else
    {
        printf("Create Handle Success!\n");
        do
        {
            //Sleep(500);

            // 1. Set Robot Status User Call back 
            std::unique_ptr<ST_ROBOT_STATUS> pRobotStatus(new ST_ROBOT_STATUS);
            memset(pRobotStatus.get(), 0, sizeof(ST_ROBOT_STATUS));
            llRet = MP_SetUserCallback(pHandle, MP_CALLBACK_FUN_ROBOT_STATUS, RobotStatusCallback, pRobotStatus.get());
            if (MP_SUCCESS != llRet)
            {
                printf("Set Robot Status User Call back Info fail! llRet [0x%x]\n", llRet);
                break;
            }
            printf("Set Robot Status User Call back Successful!\n");

            // 2. Connect Controller With IP Address
            char szConnectInfo[BUFFER_LEN_32] = { "192.168.2.64:9000" };
            llRet = MP_ConnectCtrlWithIP(pHandle, szConnectInfo);
            if (MP_SUCCESS != llRet)
            {
                printf("Connect Ctrl With IP fail! llRet [0x%x]\n", llRet);
                MP_DestroyHandle(pHandle);
                break;
            }
            printf("Connect Controller With IP Success!\n");

            //Sleep(2000);

            // 3. Request Control Access
            llRet = MP_RequestControlAccess(pHandle);
            if (MP_SUCCESS != llRet)
            {
                printf("Request Control Access fail! llRet [0x%x]\n", llRet);
                MP_DestroyHandle(pHandle);
                break;
            }
            printf("Request Control Access Success!\n");

            //Sleep(2000);

            // 4. Switch User
            ST_USER_INFO stUserInfo;
            memset(&stUserInfo, 0, sizeof(stUserInfo));
            stUserInfo.eUserType = E_USER_TYPE::MP_USER_PROGRAMMER;
            strcpy_s(stUserInfo.szUserName, "programmer");
            strcpy_s(stUserInfo.szUserPwd, "WKD@2025");
            llRet = MP_SwitchUser(pHandle, &stUserInfo);
            if (MP_SUCCESS != llRet)
            {
                printf("Switch User fail! llRet [0x%x]\n", llRet);
                MP_DestroyHandle(pHandle);
                break;
            }
            printf("Switch User Success!\n");

            //Sleep(2000);

            // 5. Eliminate All Alarm
            llRet = MP_EliminateAllAlarm(pHandle);
            if (MP_SUCCESS != llRet)
            {
                printf("Eliminate All Alarm fail! llRet [0x%x]\n", llRet);
                MP_DestroyHandle(pHandle);
                break;
            }
            printf("Eliminate All Alarm Success!\n");
            //Sleep(2000);
            //切换到手动模式
            llRet = MP_SetWorkMode(pHandle, E_WORKING_MODE::MP_WORKING_MODE_MANUAL_LS);
            if (MP_SUCCESS != llRet)
            {
                printf("set work mode fail! nRet [0x%x]\n", llRet);
                break;
            }
            cout << "set work mode successful!....当前模式：手动低速" << endl;
            Sleep(2000);
            //Sleep(500);
           
            
         
            // 6. 上电 Set Motor On
            // 前置条件：1、获取控制权  2、切换到示教员及以上用户 
            llRet = MP_SetMotorOn(pHandle);
            if (MP_SUCCESS != llRet)
            {
                printf("Set Motor On fail! llRet [0x%x]\n", llRet);
                MP_DestroyHandle(pHandle);
                break;
            }
            printf("Set Motor On Success!\n");
            //Sleep(2000);
            
            //7. 切换到自动模式
  /*          llRet = MP_SetWorkMode(pHandle, E_WORKING_MODE::MP_WORKING_MODE_AUTO);
            if (MP_SUCCESS != llRet)
            {
                printf("set work mode fail! nRet [0x%x]\n", llRet);
                break;
            }
            cout << "set work mode successful!...当前模式：自动" << endl;*/
            //Sleep(2000);
            /*Sleep(500);*/


             //8.进入实时运动模式
            //llRet = MP_EnterRealTimeMotionMode(pHandle);
            //if (MP_SUCCESS != llRet)
            //{
            //    printf("进入实时运动模式 fail! nRet [0x%x]\n", llRet);
            //    break;
            //}
            //cout << "进入实时运动模式！"<<endl;
            //Sleep(500);


            /////* 先切换到平衡运动模式
            ////在这里写movc,mocj.....等函数
            ////*/
            // llRet = MP_ChangeRunMode(pHandle, E_MOTION_MODE::E_MOTION_EQUILIBRIUM_MODE);
            //if (MP_SUCCESS != llRet)
            //{
            //    printf("exchange run mode fail! nRet [0x%x]\n", llRet);
            //    break;
            //}
            //cout << "进入到平衡模式！" << endl;
            //Sleep(500);

            /*llRet = MP_ClearMotionQueue(pHandle);
            if (MP_SUCCESS != llRet)
            {
                printf("clear motion queue fail! nRet [0x%x]\n", llRet);
                break;
            }
            cout << "清除运动队列！" << endl;*/

            // 9.执行控制命令
            // 1) *********MOVJ**************
            ST_MOTION_MOVJ_PARAM_LIST stMoveJParam = { 0 };
            stMoveJParam.nTargetNum = 1;
            stMoveJParam.stMotionMovParam[0].stMotionTarget.nTargetId = 1;
            stMoveJParam.stMotionMovParam[0].stMotionTarget.ePosType = MP_TARGET_POSE_JNT;
            // Ŀ���λ��Ϣ
            stMoveJParam.stMotionMovParam[0].stMotionTarget.arrTargetPos[0] = 90;
            stMoveJParam.stMotionMovParam[0].stMotionTarget.arrTargetPos[1] = 60;
            stMoveJParam.stMotionMovParam[0].stMotionTarget.arrTargetPos[2] = 60;
            stMoveJParam.stMotionMovParam[0].stMotionTarget.arrTargetPos[3] = 0;
            stMoveJParam.stMotionMovParam[0].stMotionTarget.arrTargetPos[4] = -90;
            stMoveJParam.stMotionMovParam[0].stMotionTarget.arrTargetPos[5] = 0;
            llRet = MP_MovjPosToMotionQueue(pHandle, &stMoveJParam);
            if (MP_SUCCESS != llRet)
            {
                printf("feed in movs to motion queue fail! nRet [0x%x]\n", llRet);
                break;
            }

            // 2) *************************MOVL************************/
            //ST_MOTION_MOVL_PARAM_LIST stMoveLParam = { 0 };
            //stMoveLParam.nTargetNum = 1;
            //stMoveLParam.stMotionMovParam[0].stMotionTarget.nTargetId = 2;
            //stMoveLParam.stMotionMovParam[0].stMotionTarget.ePosType = MP_TARGET_POSE_EULER;
            //// Ŀ���λ��Ϣ
            //stMoveLParam.stMotionMovParam[0].stMotionTarget.arrTargetPos[0] = 400;
            //stMoveLParam.stMotionMovParam[0].stMotionTarget.arrTargetPos[1] = 0;
            //stMoveLParam.stMotionMovParam[0].stMotionTarget.arrTargetPos[2] = 400;
            //stMoveLParam.stMotionMovParam[0].stMotionTarget.arrTargetPos[3] = 180;
            //stMoveLParam.stMotionMovParam[0].stMotionTarget.arrTargetPos[4] = 0;
            //stMoveLParam.stMotionMovParam[0].stMotionTarget.arrTargetPos[5] = 0;
            //stMoveLParam.stMotionMovParam[0].stMotionParam.stCsSelect.csType = 1;
            //llRet = MP_MovlPosToMotionQueue(pHandle, &stMoveLParam);
            //if (MP_SUCCESS != llRet)
            //{
            //    printf("feed in movs to motion queue fail! nRet [0x%x]\n", llRet);
            //    break;
            //}
            // 3) *************************MOVC************************/
            //ST_MOTION_MOVC_PARAM_LIST stMoveCParam = { 0 };
            //stMoveCParam.nTargetNum = 1;
            //stMoveCParam.stMotionMovParam[0].stMotionTarget.nTargetId = 3;
            //stMoveCParam.stMotionMovParam[0].stMotionTarget.ePosType = MP_TARGET_POSE_EULER;
            //// Ŀ���λ��Ϣ
            //stMoveCParam.stMotionMovParam[0].stMotionTarget.arrTargetPos[0] = 500;
            //stMoveCParam.stMotionMovParam[0].stMotionTarget.arrTargetPos[1] = 0;
            //stMoveCParam.stMotionMovParam[0].stMotionTarget.arrTargetPos[2] = 400;
            //stMoveCParam.stMotionMovParam[0].stMotionTarget.arrTargetPos[3] = 180;
            //stMoveCParam.stMotionMovParam[0].stMotionTarget.arrTargetPos[4] = 0;
            //stMoveCParam.stMotionMovParam[0].stMotionTarget.arrTargetPos[5] = 0;
            //// ������λ��Ϣ
            //stMoveCParam.stMotionMovParam[0].stAppendixPosInfo.arrAppendixPos[0] = 450;
            //stMoveCParam.stMotionMovParam[0].stAppendixPosInfo.arrAppendixPos[1] = 50;
            //stMoveCParam.stMotionMovParam[0].stAppendixPosInfo.arrAppendixPos[2] = 400;
            //stMoveCParam.stMotionMovParam[0].stAppendixPosInfo.arrAppendixPos[3] = 180;
            //stMoveCParam.stMotionMovParam[0].stAppendixPosInfo.arrAppendixPos[4] = 0;
            //stMoveCParam.stMotionMovParam[0].stAppendixPosInfo.arrAppendixPos[5] = 0;
            //stMoveCParam.stMotionMovParam[0].stMotionParam.stCsSelect.csID = 0;
            //stMoveCParam.stMotionMovParam[0].stMotionParam.stCsSelect.csType = 1;
            //llRet = MP_MovcPosToMotionQueue(pHandle, &stMoveCParam);
            //if (MP_SUCCESS != llRet)
            //{
            //    printf("feed in movs to motion queue fail! nRet [0x%x]\n", llRet);
            //    break;
            //}


            // 10.Set Motor Off
            // 前置条件：
            // 1、获取控制权  2、切换到示教员及以上用户 
            llRet = MP_SetMotorOff(pHandle);
            if (MP_SUCCESS != llRet)
            {
                printf("Set Motor Off fail! llRet [0x%x]\n", llRet);
                MP_DestroyHandle(pHandle);
                break;
            }
            printf("Set Motor Off Success!\n");
            
            //Sleep(500);

            // 11.Disconnect Controller
            llRet = MP_DisConnectCTRL(pHandle);
            if (MP_SUCCESS != llRet)
            {
                printf("Disconnect Ctrl With IP fail! llRet [0x%x]\n", llRet);
                MP_DestroyHandle(pHandle);
                break;
            }
            printf("Disconnect Ctrl With IP Success!\n");

            //Sleep(500);

            // 12. Destroy Handle
            llRet = MP_DestroyHandle(pHandle);
            if (MP_SUCCESS != llRet)
            {
                printf("Destroy Handle fail! llRet [0x%x]\n", llRet);
                break;
            }
            printf("Destroy Handle  Success!\n");
        } while (false);
    }
    cout << "hahhaahhahh";
    system("pause");

    return 0;
}
