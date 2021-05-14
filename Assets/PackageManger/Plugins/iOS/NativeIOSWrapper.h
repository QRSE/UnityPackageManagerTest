//
//  NativeiOSWarpper.h
//  Unity-iPhone
//
//  Created by liuzhiyu on 17/09/2020.
//


@interface NativeIOSWrapper : NSObject

@end

NSString* requestAdvertisingCallbackObject;
NSString* nativeCallbackObject;

typedef void (*AdvertisingStatusDelegate)(const char *status);
typedef void (*AdvertisingAuthorityDelegate)(const char *authority);

static const char* REQUEST_ADVERTISING_STATUS_CALLBACK = "onRequestAdvertisingStatus";
static const char* REQUEST_ADVERTISING_AUTHORITY_CALLBACK = "onRequestAdvertisingAuthority";

static const char* NATIVE_GET_DEVICE_SYSTEM_CALLBACK = "onGetDeviceSystem";
