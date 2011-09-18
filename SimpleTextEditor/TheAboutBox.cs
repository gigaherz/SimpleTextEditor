using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

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
                System.Windows.Forms.MouseEventArgs e)
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

            this.labelTitle.Text = String.Format("About {0}", AssemblyTitle);
            this.labelVersion.Text = String.Format("Version {0}", Application.ProductVersion);
            this.labelCopyright.Text = AssemblyCopyright;
            this.labelDescription.Text = AssemblyDescription;
        }

        #region Assembly Attribute Accessors

        private static string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title.Length > 0)
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        private static string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        private static string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }
        #endregion

        /// <para>Changes the current bitmap.</para>
        public void SetBitmap(Bitmap bitmap)
        {
            SetBitmap(bitmap, 255);
        }


        /// <para>Changes the current bitmap with a custom opacity level.  Here is where all happens!</para>
        public void SetBitmap(Bitmap bitmap, byte opacity)
        {
            if (bitmap.PixelFormat != PixelFormat.Format32bppArgb)
                throw new ApplicationException("The bitmap must be 32ppp with alpha-channel.");

            // The ideia of this is very simple,
            // 1. Create a compatible DC with screen;
            // 2. Select the bitmap with 32bpp with alpha-channel in the compatible DC;
            // 3. Call the UpdateLayeredWindow.

            IntPtr screenDc = NativeMethods.GetDC(IntPtr.Zero);
            IntPtr memDc = NativeMethods.CreateCompatibleDC(screenDc);
            IntPtr hBitmap = IntPtr.Zero;
            IntPtr oldBitmap = IntPtr.Zero;

            try
            {
                hBitmap = bitmap.GetHbitmap(Color.FromArgb(0));  // grab a GDI handle from this GDI+ bitmap
                oldBitmap = NativeMethods.SelectObject(memDc, hBitmap);

                NativeMethods.Size size = new NativeMethods.Size(bitmap.Width, bitmap.Height);
                NativeMethods.Point pointSource = new NativeMethods.Point(0, 0);
                NativeMethods.Point topPos = new NativeMethods.Point(Left, Top);
                NativeMethods.BLENDFUNCTION blend = new NativeMethods.BLENDFUNCTION();
                blend.BlendOp = NativeMethods.AC_SRC_OVER;
                blend.BlendFlags = 0;
                blend.SourceConstantAlpha = opacity;
                blend.AlphaFormat = NativeMethods.AC_SRC_ALPHA;

                NativeMethods.UpdateLayeredWindow(Handle, screenDc, ref topPos, ref size, memDc, ref pointSource, 0, ref blend, NativeMethods.ULW_ALPHA);
            }
            finally
            {
                NativeMethods.ReleaseDC(IntPtr.Zero, screenDc);
                if (hBitmap != IntPtr.Zero)
                {
                    NativeMethods.SelectObject(memDc, oldBitmap);
                    //Windows.DeleteObject(hBitmap); // The documentation says that we have to use the Windows.DeleteObject... but since there is no such method I use the normal DeleteObject from Win32 GDI and it's working fine without any resource leak.
                    NativeMethods.DeleteObject(hBitmap);
                }
                NativeMethods.DeleteDC(memDc);
            }
        }

        public static void SetRegionTransparentBitmap(Control control, Bitmap bitmap)
        {
            if (control == null || bitmap == null)
                return;

            GraphicsPath graphicsPath = CalculateControlGraphicsPathAlpha(bitmap);

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
            const int AlphaThreshold = 255;

            GraphicsPath graphicsPath = new GraphicsPath();

            int colOpaquePixel = 0;
            for (int row = 0; row < bitmap.Height; row++)
            {
                colOpaquePixel = 0;

                for (int col = 0; col < bitmap.Width; col++)
                {
                    if (bitmap.GetPixel(col, row).A >= AlphaThreshold)
                    {
                        colOpaquePixel = col;

                        int colNext = col;

                        for (colNext = colOpaquePixel; colNext < bitmap.Width; colNext++)
                        {
                            if (bitmap.GetPixel(colNext, row).A < AlphaThreshold)
                                break;
                        }

                        graphicsPath.AddRectangle(new Rectangle(colOpaquePixel, row, colNext - colOpaquePixel, 1));
                        col = colNext;
                    }
                }
            }

            return graphicsPath;
        }

        private void TheAboutBox_Load(object sender, EventArgs e)
        {

        }

        private void TheAboutBox_Shown(object sender, EventArgs e)
        {
            SetRegionTransparentBitmap(this, Properties.Resources.AboutBoxBackground);
            //SetBitmap(Properties.Resources.AboutBackground);
            Refresh();
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            if (e.Control is Label)
            {
                e.Control.MouseDown += new MouseEventHandler(theLabel_MouseDown);
            }
            base.OnControlAdded(e);
        }

        private void theLabel_MouseDown(object sender, MouseEventArgs e)
        {
            int xTrans = e.X + this.Location.X;
            int yTrans = e.Y + this.Location.Y;
            MouseEventArgs eTrans = new MouseEventArgs(e.Button, e.Clicks, xTrans, yTrans, e.Delta);
            this.OnMouseDown(eTrans);
        }
    }
}
