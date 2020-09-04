/**
* Copyright 2019 Vuplex Inc. All rights reserved.
*
* Licensed under the Vuplex Commercial Software Library License, you may
* not use this file except in compliance with the License. You may obtain
* a copy of the License at
*
*     https://vuplex.com/commercial-library-license
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/
#import "IUnityGraphicsMetal.h"
#import "VXWebViewAppController.h"

static IUnityGraphicsMetal *_metalGraphics;

void UNITY_INTERFACE_EXPORT UNITY_INTERFACE_API UnityPluginLoad(IUnityInterfaces* unityInterfaces) {

    _metalGraphics = unityInterfaces->Get<IUnityGraphicsMetal>();
}

void UNITY_INTERFACE_EXPORT UNITY_INTERFACE_API UnityPluginUnload() {}

@implementation VXWebViewAppController

- (void)shouldAttachRenderDelegate {

    UnityRegisterRenderingPluginV5(&UnityPluginLoad, &UnityPluginUnload);
}

+ (id<MTLDevice>)metalDevice {

    return _metalGraphics->MetalDevice();
}

@end

IMPL_APP_CONTROLLER_SUBCLASS(VXWebViewAppController);
