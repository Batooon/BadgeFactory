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
#import <UIKit/UIKit.h>
#import <Metal/Metal.h>
#ifdef INCLUDE_UNITY_FOLDER_PREFIX
    #import "Unity/UnityAppController.h"
#else
    #import "UnityAppController.h"
#endif

/**
* Extends UnityAppController to override shouldAttachRenderDelegate
* so that it can register a low-level rendering plugin.
*
* Unlike other platforms (like Desktop) where the plugin dynamic library is automatically
* loaded and registered by Unity, that must be done manually on iOS. VXWebViewAppController.mm uses Unity's
* IMPL_APP_CONTROLLER_SUBCLASS() macro to tell Unity to use VXWebViewAppController
* instead of UnityAppController. It's possible (but rare) that multiple iOS plugins
* in a project do this, which results in one plugin's use of IMPL_APP_CONTROLLER_SUBCLASS()
* overwriting another's.
*
* If you find yourself in this scenario where another iOS plugin in your project needs to extend
* UnityAppController and call IMPL_APP_CONTROLLER_SUBCLASS(), you can comment-out the call to
* IMPL_APP_CONTROLLER_SUBCLASS() in VXWebViewAppController.mm and update your other plugin to extend
* VXWebViewAppController instead of UnityAppController.
*/
@interface VXWebViewAppController : UnityAppController

/**
* Overrides [UnityAppController shouldAttachRenderDelegate]
* to hook into the Unity trampoline.
*/
- (void)shouldAttachRenderDelegate;

+ (id<MTLDevice>)metalDevice;

@end
