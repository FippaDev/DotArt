using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace DotArt
{
	class ImageUtils
	{
		internal static Image FixedSize(Image imgPhoto, int Width, int Height)
		{
			int sourceWidth = imgPhoto.Width;
			int sourceHeight = imgPhoto.Height;
			int sourceX = 0;
			int sourceY = 0;
			int destX = 0;
			int destY = 0;

			float nPercent = 0;
			float nPercentW = 0;
			float nPercentH = 0;

			nPercentW = ((float) Width/(float) sourceWidth);
			nPercentH = ((float) Height/(float) sourceHeight);
			if (nPercentH < nPercentW)
			{
				nPercent = nPercentH;
				destX = System.Convert.ToInt16((Width -
				                                (sourceWidth*nPercent))/2);
			}
			else
			{
				nPercent = nPercentW;
				destY = System.Convert.ToInt16((Height -
				                                (sourceHeight*nPercent))/2);
			}

			int destWidth = (int) (sourceWidth*nPercent);
			int destHeight = (int) (sourceHeight*nPercent);

			Bitmap bmPhoto = new Bitmap(Width, Height,
			                            PixelFormat.Format24bppRgb);
			bmPhoto.SetResolution(imgPhoto.HorizontalResolution,
			                      imgPhoto.VerticalResolution);

			Graphics grPhoto = Graphics.FromImage(bmPhoto);			
			//grPhoto.Clear(Color.Red);
			grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
	
			grPhoto.DrawImage(imgPhoto,
			                  new Rectangle(destX, destY, destWidth, destHeight),
			                  new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
			                  GraphicsUnit.Pixel);

			grPhoto.Dispose();
			return bmPhoto;
		}

		internal static Image Explode(string filename, int scale, float border_pct)
		{			
			Bitmap _mask = new Bitmap(scale, scale);
			Graphics g = Graphics.FromImage(_mask);
			float border = Convert.ToSingle(scale * border_pct);
			g.FillEllipse(new SolidBrush(Color.Black), border, border, scale-border, scale-border);
			_mask.Save(@"C:\Users\Philip Love\Desktop\DotArt\DotArt\mask.bmp");
			_mask.Dispose();
			
			Bitmap mask = new Bitmap(@"C:\Users\Philip Love\Desktop\DotArt\DotArt\mask.bmp");

			Bitmap orig = new Bitmap(Image.FromFile(filename));

			int newWidth = orig.Width * scale;
			int newHeight = orig.Height * scale;

			Bitmap newBitmap = new Bitmap(newWidth, newHeight);

			for (int i = 0; i < orig.Width; i++)
			{
				for (int j = 0; j < orig.Height; j++)
				{
					Color c = orig.GetPixel(i, j);
					DrawSquare(newBitmap, i * scale, j * scale, c, mask, scale);
				}
			}
			orig.Dispose();
			mask.Dispose();
			return newBitmap;
		}

		private static void DrawSquare(Bitmap newBitmap, int x, int y, Color c, Bitmap mask, int scale)
		{
			for (int a = 0; a < scale; a++)
			{
				for (int b = 0; b < scale; b++)
				{
					if(mask.GetPixel(a, b).A > 0)
						newBitmap.SetPixel(x+a, y+b, c);
					else
						newBitmap.SetPixel(x+a, y+b, Color.White);
				}
			}
		}
	}
}
