using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using SimpleTextEditor.Properties;

namespace SimpleTextEditor
{
    partial class TheAboutBox : Form
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                //cp.ExStyle |= Win32.WS_EX_LAYERED; // This form has to have the WS_EX_LAYERED extended style
                cp.ClassStyle |= NativeMethods.CS_DROPSHADOW;
                return cp;
            }
        }

        private void Form1_MouseDown(object sender, 
                MouseEventArgs e)
        {     
            if (e.Button == MouseButtons.Left)
            {
                NativeMethods.ReleaseCapture();
                NativeMethods.SendMessage(Handle, NativeMethods.WM_NCLBUTTONDOWN, (UIntPtr)NativeMethods.HT_CAPTION, IntPtr.Zero);
            }
        }

        public TheAboutBox()
        {
            InitializeComponent();

            labelTitle.Text = String.Format(Resources.AboutText, AssemblyTitle);
            labelVersion.Text = String.Format(Resources.VersionText, Application.ProductVersion);
            labelCopyright.Text = AssemblyCopyright;
            labelDescription.Text = AssemblyDescription;
        }

        #region Assembly Attribute Accessors

        private static string AssemblyTitle
        {
            get
            {
                var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    var titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title.Length > 0)
                        return titleAttribute.Title;
                }
                return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        private static string AssemblyDescription
        {
            get
            {
                var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                    return "";

                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        private static string AssemblyCopyright
        {
            get
            {
                var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                    return "";

                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }
        #endregion

        #region Bitmap
        /// <para>Changes the current bitmap.</para>
        public void SetBitmap(Bitmap bitmap)
        {
            SetBitmap(bitmap, 255);
        }
        
        /// <para>Changes the current bitmap with a custom opacity level.  Here is where all happens!</para>
        private void SetBitmap(Bitmap bitmap, byte opacity)
        {
            if (bitmap.PixelFormat != PixelFormat.Format32bppArgb)
                throw new ApplicationException(@"The bitmap must be 32ppp with alpha-channel.");

            // The ideia of this is very simple,
            // 1. Create a compatible DC with screen;
            // 2. Select the bitmap with 32bpp with alpha-channel in the compatible DC;
            // 3. Call the UpdateLayeredWindow.

            var screenDc = NativeMethods.GetDC(IntPtr.Zero);
            var memDc = NativeMethods.CreateCompatibleDC(screenDc);
            var hBitmap = IntPtr.Zero;
            var oldBitmap = IntPtr.Zero;

            try
            {
                hBitmap = bitmap.GetHbitmap(Color.FromArgb(0));  // grab a GDI handle from this GDI+ bitmap
                oldBitmap = NativeMethods.SelectObject(memDc, hBitmap);

                var size = new NativeMethods.Size(bitmap.Width, bitmap.Height);
                var pointSource = new NativeMethods.Point(0, 0);
                var topPos = new NativeMethods.Point(Left, Top);
                var blend = new NativeMethods.BLENDFUNCTION
                                {
                                    BlendOp = NativeMethods.AC_SRC_OVER,
                                    BlendFlags = 0,
                                    SourceConstantAlpha = opacity,
                                    AlphaFormat = NativeMethods.AC_SRC_ALPHA
                                };

                NativeMethods.UpdateLayeredWindow(Handle, screenDc, ref topPos, ref size, memDc, ref pointSource, 0, ref blend, NativeMethods.ULW_ALPHA);
            }
            finally
            {
                NativeMethods.ReleaseDC(IntPtr.Zero, screenDc);
                if (hBitmap != IntPtr.Zero)
                {
                    NativeMethods.SelectObject(memDc, oldBitmap);
                    NativeMethods.DeleteObject(hBitmap);
                }
                NativeMethods.DeleteDC(memDc);
            }
        }
        #endregion

        #region Region from bitmap
        private static void SetRegionTransparentBitmap(Control control, Bitmap bitmap)
        {
            if (control == null || bitmap == null)
                return;

            var graphicsPath = CalculateControlGraphicsPathAlpha(bitmap);

            if (control is Form)
                (control as Form).FormBorderStyle = FormBorderStyle.None;

            control.Width = bitmap.Width;
            control.Height = bitmap.Height;
            control.Text = "";
            control.Cursor = Cursors.Hand;
            control.BackgroundImage = bitmap;
            control.Region = new Region(graphicsPath);

            graphicsPath.Dispose();
        }

        private static GraphicsPath CalculateControlGraphicsPathAlpha(Bitmap bitmap)
        {
            const int alphaThreshold = 255;

            var graphicsPath = new GraphicsPath();

            for (int row = 0; row < bitmap.Height; row++)
            {
                for (int col = 0; col < bitmap.Width; col++)
                {
                    if (bitmap.GetPixel(col, row).A < alphaThreshold) 
                        continue;

                    var colOpaquePixel = col;

                    int colNext;

                    for (colNext = colOpaquePixel; colNext < bitmap.Width; colNext++)
                    {
                        if (bitmap.GetPixel(colNext, row).A < alphaThreshold)
                            break;
                    }

                    graphicsPath.AddRectangle(new Rectangle(colOpaquePixel, row, colNext - colOpaquePixel, 1));

                    col = colNext;
                }
            }

            return graphicsPath;
        }
        #endregion

        private void TheAboutBox_Load(object sender, EventArgs e)
        {

        }

        private void TheAboutBox_Shown(object sender, EventArgs e)
        {
            SetRegionTransparentBitmap(this, Resources.AboutBoxBackground);
            //SetBitmap(Properties.Resources.AboutBackground);
            Refresh();
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            if (e.Control is Label)
                e.Control.MouseDown += theLabel_MouseDown;

            base.OnControlAdded(e);
        }

        private void theLabel_MouseDown(object sender, MouseEventArgs e)
        {
            var xTrans = e.X + Location.X;
            var yTrans = e.Y + Location.Y;
            var eTrans = new MouseEventArgs(e.Button, e.Clicks, xTrans, yTrans, e.Delta);
            OnMouseDown(eTrans);
        }
    }
}
