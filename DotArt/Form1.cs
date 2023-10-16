using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DotArt
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}

		private void scaleToMinimumToolStripMenuItem_Click(object sender, EventArgs e)
		{
			int dotsPerWidth = 15;
			int scale = 500;
			float border = 0.15f;

			try
			{
				this.Cursor = Cursors.WaitCursor;
				Image originalImage = Image.FromFile(@"C:\Users\Philip Love\Desktop\DotArt\DotArt\mona.jpg");

				float aspect = Convert.ToSingle(originalImage.Height / originalImage.Width);
				int targetWidth = dotsPerWidth;
				int targetHeight = Convert.ToInt32(aspect * Convert.ToSingle(targetWidth));
				Image newImage = ImageUtils.FixedSize(originalImage, targetHeight, targetWidth);
				newImage.Save(@"C:\Users\Philip Love\Desktop\DotArt\DotArt\target_small.jpg");
				newImage.Dispose();
			}
			finally
			{
				this.Cursor = Cursors.Default;
			}

			try
			{
				this.Cursor = Cursors.WaitCursor;
				Image newImage = ImageUtils.Explode(@"C:\Users\Philip Love\Desktop\DotArt\DotArt\target_small.jpg", scale, border);
				newImage.Save(@"C:\Users\Philip Love\Desktop\DotArt\DotArt\target_large.jpg");
				newImage.Dispose();
			}
			finally
			{
				this.Cursor = Cursors.Default;
			}

			pictureBox1.Image = Image.FromFile(@"C:\Users\Philip Love\Desktop\DotArt\DotArt\target_large.jpg");
			pictureBox1.AutoSize = true;
			MessageBox.Show("done");
		}
	}
}
