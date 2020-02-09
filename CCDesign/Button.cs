using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;

namespace CCTools.CCDesign
{
	class Button : System.Windows.Forms.Button
	{
		#region Constructor

		public Button()
		{
			FlatStyle = FlatStyle.System;
		}

		#endregion

		#region Properties

		public new Image Image
		{
			get { return base.Image; }
			set
			{
				if (base.Image != value)
				{
					base.Image = value;
					if (IsHandleCreated)
						SetImage();
				}
			}
		}

		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
			get
			{
				var cp = base.CreateParams;
				if (string.IsNullOrEmpty(base.Text) && base.Image != null)
					cp.Style |= NativeMethods.BS_BITMAP;
				return cp;
			}
		}

		#endregion

		#region Methods

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			SetImage();
		}

		private void SetImage()
		{
			var img = Image;
			if (img != null)
			{
				var bmp = img as Bitmap;
				if (bmp != null)
					NativeMethods.SendMessage(new HandleRef(this, Handle), NativeMethods.BM_SETIMAGE, IntPtr.Zero, bmp.GetHbitmap());
				else
					NativeMethods.SendMessage(new HandleRef(this, Handle), NativeMethods.BM_SETIMAGE, IntPtr.Zero, IntPtr.Zero);
			}
			else
				NativeMethods.SendMessage(new HandleRef(this, Handle), NativeMethods.BM_SETIMAGE, IntPtr.Zero, IntPtr.Zero);
		}

		#endregion
	}
}
