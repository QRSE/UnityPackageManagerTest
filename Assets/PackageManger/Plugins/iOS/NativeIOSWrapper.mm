//
//  NativeiOSWarpper.mm
//  Unity-iPhone
//
//  Created by liuzhiyu on 17/09/2020.
//

#import "NativeIOSWrapper.h"
#import <Foundation/Foundation.h>
//#import <AppTrackingTransparency/ATTrackingManager.h>

AdvertisingStatusDelegate status_callback;
AdvertisingAuthorityDelegate authority_callback;

//发送消息到Unity
static void unityCallBack(NSString* objectName, const char* method, const char* msg) {
    if(objectName){
        UnitySendMessage([objectName UTF8String], method, msg);
    }
}

@implementation NativeIOSWrapper
extern "C" {
    const void _getDeviceVersion(const char* objectName){
        nativeCallbackObject = [[NSString alloc] initWithUTF8String:objectName];
        
        NSString *sysVersion = [[UIDevice currentDevice] systemVersion];
        unityCallBack(nativeCallbackObject, NATIVE_GET_DEVICE_SYSTEM_CALLBACK, [sysVersion UTF8String]);
    }
    
    const void _setAdvertisingStatusCallback(AdvertisingStatusDelegate callback){
        status_callback = callback;
    }
    
    const void _setAdvertisingAuthorityCallback(AdvertisingAuthorityDelegate callback){
            authority_callback = callback;
    }
    
    const void _requestAdvertisingStatus(const char* objectName) {
         requestAdvertisingCallbackObject = [[NSString alloc] initWithUTF8String:objectName];
         NSString *result;
         
//         if (@available(iOS 14, *)) {
//                 ATTrackingManagerAuthorizationStatus status = ATTrackingManager.trackingAuthorizationStatus;
//                 switch (status) {
//                     case ATTrackingManagerAuthorizationStatusNotDetermined:
//                         //未向用户请求授权
//                         result = @"NotDetermined";
//                         status_callback("0");
//                         break;
//                     case ATTrackingManagerAuthorizationStatusRestricted:
//                         //用户在系统级别开启了限制广告追踪
//                         result = @"Restricted";
//                         status_callback("1");
//                         break;                        
//                     case ATTrackingManagerAuthorizationStatusDenied:
//                         //用户拒绝向App授权
//                         result = @"Denied";
//                         status_callback("2");
//                         break;
//                     case ATTrackingManagerAuthorizationStatusAuthorized:
//                         //用户同意向App授权
//                         result = @"Authorized";
//                         status_callback("3");
//                         break;
//                }
//             } else {
//                 // Fallback on earlier versions
//                 result = @"None";
//                 status_callback("4");   
//             }
//             unityCallBack(requestAdvertisingCallbackObject, REQUEST_ADVERTISING_STATUS_CALLBACK, [result UTF8String]);
         }   
    
    const void _requestAdvertisingAuthority(const char* objectName) {
         requestAdvertisingCallbackObject = [[NSString alloc] initWithUTF8String:objectName];
         NSString *result;
         
//          if (@available(iOS 14, *)) {
//                 [ATTrackingManager requestTrackingAuthorizationWithCompletionHandler:^(ATTrackingManagerAuthorizationStatus status) {
//                     NSLog(@"app追踪权限：%lu",(unsigned long)status);
//                     switch (status) {
//                         case ATTrackingManagerAuthorizationStatusNotDetermined:
//                         {
//                              //未向用户请求授权
//                              //打开设置界面
//                              //NSURL *url = [NSURL URLWithString:UIApplicationOpenSettingsURLString];
//                              //if ([[UIApplication sharedApplication] canOpenURL:url]) {
//                              //    [[UIApplication sharedApplication] openURL:url];
//                              //}
//                              authority_callback("0");
//                         }
//                              break;
//                         case ATTrackingManagerAuthorizationStatusRestricted:
//                         {
//                              //用户在系统级别开启了限制广告追踪
//                              //打开设置界面
//                              //NSURL *url = [NSURL URLWithString:UIApplicationOpenSettingsURLString];
//                              //if ([[UIApplication sharedApplication] canOpenURL:url]) {
//                              //    [[UIApplication sharedApplication] openURL:url];
//                              //}
//                              authority_callback("1");
//                         }
//                              break;
//                         case ATTrackingManagerAuthorizationStatusDenied:
//                         {
//                             //用户拒绝向App授权
//                              //打开设置界面
//                              //NSURL *url = [NSURL URLWithString:UIApplicationOpenSettingsURLString];
//                              //if ([[UIApplication sharedApplication] canOpenURL:url]) {
//                              //    [[UIApplication sharedApplication] openURL:url];
//                              //}
//                              authority_callback("2");
//                         }
//                              break;
//                         case ATTrackingManagerAuthorizationStatusAuthorized:
//                         {
//                              //用户同意向App授权
//                              authority_callback("3");
//                         }
//                              break;
//                     }
//                 }];
//              } else {
//                 // Fallback on earlier versions
//                 authority_callback("4");
//              }   
//         //unityCallBack(requestAdvertisingCallbackObject, REQUEST_ADVERTISING_AUTHORITY_CALLBACK, [result UTF8String]);
    }
   
}


@end

