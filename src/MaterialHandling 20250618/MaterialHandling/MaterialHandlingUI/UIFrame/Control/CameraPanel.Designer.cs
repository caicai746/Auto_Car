
namespace MaterialHandling.MaterialHandlingUI.UIFrame.Control
{
    partial class CameraFrame
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.webView2Camera1 = new Microsoft.Web.WebView2.WinForms.WebView2();
            ((System.ComponentModel.ISupportInitialize)(this.webView2Camera1)).BeginInit();
            this.SuspendLayout();
            // 
            // webView2Camera1
            // 
            this.webView2Camera1.AllowExternalDrop = true;
            this.webView2Camera1.CreationProperties = null;
            this.webView2Camera1.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webView2Camera1.Location = new System.Drawing.Point(0, 0);
            this.webView2Camera1.Margin = new System.Windows.Forms.Padding(0);
            this.webView2Camera1.Name = "webView2Camera1";
            this.webView2Camera1.Size = new System.Drawing.Size(382, 267);
            this.webView2Camera1.TabIndex = 1;
            this.webView2Camera1.ZoomFactor = 1D;
            // 
            // CameraFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.webView2Camera1);
            this.Name = "CameraFrame";
            this.Size = new System.Drawing.Size(382, 267);
            ((System.ComponentModel.ISupportInitialize)(this.webView2Camera1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 webView2Camera1;
    }
}
