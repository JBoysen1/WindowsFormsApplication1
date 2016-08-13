using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;
using System.Drawing;


namespace WindowsFormsApplication1
{


    // We can make our image 256 pixels in width.The height is entirely up to you.For this exercise, we will create an image that is 100 pixels high.
    // The final maths required to allocate a byte array big enough for this image is therefore 
    //    (256 * 3) * 100 or 76800 bytes for a 256x100 pixel image (WxH)


    // Windows consideration: You might have seen images described as "RGB Alpha." 
    // Generally, this means that the image pixels are 8 bits Red, Green, and Blue as we've already seen, but have an extra 8 bit value added to them, called an "Alpha Value"
    // The advantage here means that our image pixel data is now always a multiple of 4, so you don't have to do anything extra to make sure one line of your image data is padded out so that it fits into a width that's 
    // divisible by 4(which can get very horrible, very fast: -) ), and it means you also, if you want, get the ability to control an individual pixel's transparency value if you desire.
    // Alpha works exactly the same way as specifying a colour value, except the value describes how "See though" that pixel is, with 0 being completely invisible and 255 being not see through at all.
    // For the purposes of this post, we'll set all our alpha values to 255, but with a little experimentation you can create some interesting effects, especially when you start to combine images.
    // The formulae to work out the size of our array now becomes:
    //      (4 * 256) * 100 or 102400 bytes




    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        //[STAThread]

        private static readonly byte[] _imageBuffer = new byte[102400];

        static void PlotPixel(int x, int y, byte redValue, byte greenValue, byte blueValue)
        {
            int offset = ((256 * 4) * y) + (x * 4);
            _imageBuffer[offset] = blueValue;
            _imageBuffer[offset + 1] = greenValue;
            _imageBuffer[offset + 2] = redValue;
            // Fixed alpha value (No transparency)
            _imageBuffer[offset + 3] = 255;
        }


        static void Main()
        {
        // PlotPixel(10, 10, 255, 0, 0);
        for (int y = 0; y < 100; y++)
        {
            for (int x = 0; x < 256; x++)
            {
                //byte val = (byte)x;
                PlotPixel(x, y, 255, 0, 0);
            }
        }
        // The unsafe section where byte pointers are used.
        unsafe
        {
            byte* ptr = (byte*)_imageBuffer[0];
            //byte* ptr = &_imageBuffer[0];
            Bitmap bitmap = new Bitmap(256, 100, 256 * 4, System.Drawing.Imaging.PixelFormat.Format32bppArgb, new IntPtr(ptr));
            //bitmap.Save(@"C:\temp\test.png");
        }
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
            
        Application.Run(new Form1());
        }
    }
}
