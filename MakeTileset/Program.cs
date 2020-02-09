using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CCTools.MakeTileset
{
	class Program
	{
		static void Main(string[] args)
		{
			string imagePath;
			string overlayImagePath;
			if (args.Length >= 1)
				imagePath = args[0];
			else
			{
				Console.Write("Enter image path: ");
				imagePath = Console.ReadLine();
			}
			if (args.Length >= 2)
				overlayImagePath = args[1];
			else
			{
				Console.Write("Enter overlay image path: ");
				overlayImagePath = Console.ReadLine();
			}
			if (!File.Exists(imagePath))
			{
				Console.Error.WriteLine("The image file {0} does not exist!", imagePath);
				return;
			}
			if (!File.Exists(overlayImagePath))
			{
				Console.Error.WriteLine("The image file {0} does not exist!", overlayImagePath);
				return;
			}

			var newPath = Path.ChangeExtension(imagePath, "tileset");
			var tiles = new Bitmap[224];
			var image = new Bitmap(imagePath);
			var overlayImage = new Bitmap(overlayImagePath);
			if (image.Width != 224 || image.Height != 512)
			{
				Console.Error.WriteLine("The image is not the correct size!");
				return;
			}

			for (int x = 0; x < 7; x++)
				for (int y = 0; y < 16; y++)
				{
					tiles[x * 16 + y] = image.Clone(new Rectangle(x * 32, y * 32, 32, 32), PixelFormat.Format32bppArgb);
					tiles[x * 16 + y + 112] = overlayImage.Clone(new Rectangle(x * 32, y * 32, 32, 32), PixelFormat.Format32bppArgb);
				}

			var f = new BinaryFormatter();
			using (var fs = File.Create(newPath))
				f.Serialize(fs, tiles);
		}
	}
}
