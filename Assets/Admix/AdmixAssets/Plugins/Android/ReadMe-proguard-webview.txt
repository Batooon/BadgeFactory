# If you use Proguard for your Android build, add these rules to your Proguard
# file (proguard-user.txt) to prevent the needed Java classes from being obfuscated.

-keep public interface com.vuplex.webview.BooleanCallback { public *; }
-keep public interface com.vuplex.webview.ByteArrayCallback { public *; }
-keep public interface com.vuplex.webview.StringCallback { public *; }
-keep public class com.vuplex.webview.ByteArrayCallbackResult { public *; }
-keep public class com.vuplex.webview.WebView { public *; }