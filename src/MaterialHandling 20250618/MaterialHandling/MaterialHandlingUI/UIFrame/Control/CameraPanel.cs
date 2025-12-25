using Microsoft.Web.WebView2.Core; // 确保引入这个命名空间
using Microsoft.Web.WebView2.WinForms;
using System.Windows.Forms;
using System.Diagnostics; // 为了方便调试，可以添加这个

namespace MaterialHandling.MaterialHandlingUI.UIFrame.Control
{
    public partial class CameraFrame : UserControl
    {
        private string videoIpAddress;
        private int videoPort;
        public CameraFrame(string ipAddress, int port)
        {
            InitializeComponent();

            this.videoIpAddress = ipAddress;
            this.videoPort = port;

            // 在设计器中，确保 webView2Camera1 控件已正确添加并且可以访问
            if (this.webView2Camera1 != null)
            {
                InitializeAsync(this.webView2Camera1, string.Format("http://{0}:{1}/video_feed", videoIpAddress, videoPort));
            }
            else
            {
                // 处理 webView2Camera1 未初始化的情况，例如抛出错误或记录日志
                Debug.WriteLine("Error: webView2Camera1 is not initialized in CameraFrame.");
            }
        }

        private async void InitializeAsync(WebView2 webView, string url)
        {
            try
            {
                // 确保 CoreWebView2 环境已初始化
                // WebView2 的初始化可能因为多种原因失败，添加 try-catch 可以帮助诊断
                await webView.EnsureCoreWebView2Async(null);

                // 订阅 NavigationCompleted 事件
                // 建议在 EnsureCoreWebView2Async 成功之后并且在 Navigate 之前订阅
                // 同时，为防止多次订阅（如果 InitializeAsync 可能被多次调用），先移除再添加
                webView.CoreWebView2.NavigationCompleted -= CoreWebView2_NavigationCompleted; // 先尝试移除，避免重复订阅
                webView.CoreWebView2.NavigationCompleted += CoreWebView2_NavigationCompleted;

                Debug.WriteLine($"Navigating to URL: {url}");
                webView.CoreWebView2.Navigate(url); // 导航到指定的 IP 地址和端口
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine($"Error during WebView2 initialization or navigation: {ex.Message}");
                // 在此可以添加更复杂的错误处理逻辑，比如在UI上显示错误信息
            }
        }

        private async void CoreWebView2_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            var coreWebView = sender as CoreWebView2; // 直接从 sender 获取 CoreWebView2 对象
            if (coreWebView == null) // 添加一个检查，确保 coreWebView 不为 null
            {
                Debug.WriteLine("Error: CoreWebView2 object is null in NavigationCompleted handler.");
                return;
            }

            if (e.IsSuccess)
            {
                Debug.WriteLine($"Navigation successful to: {coreWebView.Source}");
                // 页面加载成功后注入CSS
                // 基于 video_feed 页面的 HTML 结构:
                // <body style="margin: 0px; background: #0a0a0a; height: 100%;">
                //     <img style="-webkit-user-select: none; margin: auto; display: block; ..." src="...">
                // </body>

                string cssToInject = @"
                    body {
                        /* 1. 更改背景色为白色，避免黑色区域 */
                        background-color: white !important; 
                        
                        /* 2. 确保 body 填满视口并移除内外边距 */
                        margin: 0 !important; 
                        padding: 0 !important; 
                        height: 100vh !important; /* 使用视口高度单位，更稳妥 */
                        
                        /* 3. 使用 Flexbox 使 img 元素在 body 内居中 */
                        display: flex !important;
                        align-items: center !important; /* 垂直居中 */
                        justify-content: center !important; /* 水平居中 */
                        overflow: hidden !important; /* 防止内容溢出时出现滚动条 */
                    }
                    img {
                        /* 4. 让图片尽可能大地显示，同时保持其宽高比 */
                        /* 高度和宽度都设置为100%，配合 object-fit: contain */
                        height: 100% !important; 
                        width: 100% !important;   
                        object-fit: contain !important; /* 关键：保持宽高比，完整显示图片，多余部分由body背景填充 */
                        
                        /* 5. 覆盖 img 标签原有的 margin 和其他可能影响布局的样式 */
                        margin: 0 !important; /* 由 Flexbox 控制居中，不再需要 img 的 margin: auto */
                        display: block !important; /* 保持块级显示 */
                        
                        /* 6. 覆盖可能由内联样式设定的固定像素尺寸 (虽然会被上面的width/height覆盖) */
                        /* 这些max属性确保图片不会超出其在flex容器中的分配空间 */
                        max-width: 100% !important;
                        max-height: 100% !important;
                        
                        /* 移除内联样式中可能存在的固定 width/height 像素值，以防它们干扰百分比设置 */
                        /* (注意: CSS无法直接移除内联style属性中的width/height, 但可以用更高优先级的CSS覆盖它们的值) */
                        /* 上面的 width: 100% !important 和 height: 100% !important 已经有最高优先级了 */
                    }
                ";

                // 将CSS字符串中的换行符正确转义为JavaScript字符串中的换行符 \n
                // 或者确保CSS字符串在JS中是合法的多行字符串（例如使用反引号）
                // C# 的 $"" 字符串插值配合 @"" 原始字符串字面量可以很好地处理多行
                // 但是为了注入到JS，最好还是明确处理换行。
                // 此处使用 Replace("\r\n", "\\n").Replace("\n", "\\n") 是为了同时兼容 Windows 和 Unix 换行符
                string script = $"var style = document.createElement('style'); style.type = 'text/css'; style.innerHTML = `{cssToInject.Replace("\r\n", "\\n").Replace("\n", "\\n")}`; document.head.appendChild(style);";
                // 或者，更简洁的JS注入方式（如果CSS字符串本身不包含反引号）：
                // string script = "var style = document.createElement('style'); style.type = 'text/css';";
                // script += "style.appendChild(document.createTextNode(`" + cssToInject.Replace("`", "\\`") + "`));"; // 转义CSS中的反引号
                // script += "document.head.appendChild(style);";


                Debug.WriteLine("Injecting CSS script...");
                await coreWebView.ExecuteScriptAsync(script);
                Debug.WriteLine("CSS script injected.");
            }
            else
            {
                Debug.WriteLine($"Navigation failed. ErrorStatus: {e.WebErrorStatus}, HttpStatusCode: {e.HttpStatusCode}");
                // 在此可以添加导航失败时的处理逻辑
            }
        }
    }
}