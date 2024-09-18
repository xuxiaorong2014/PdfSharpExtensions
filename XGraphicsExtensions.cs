using PdfSharp.Drawing;
using ZXing.Common;
namespace Extensions
{
    public static class XGraphicsExtensions
    {
        public static void DrawBitMatrix(this XGraphics gfx, BitMatrix matrix, XPoint offset)
        {
            if (matrix == null)
                return;
            int width = matrix.Width;
            int height = matrix.Height;
            var processed = new BitMatrix(width, height);
            bool currentIsBlack = false;
            int startPosX = 0;
            int startPosY = 0;
            //test border
            //gfx.DrawRectangle(new XPen(XColor.FromArgb(0, 0, 0), 0.5), new XRect(offset.X, offset.Y, matrix.Width, matrix.Height));
            for (int x = 0; x < width; x++)
            {
                int endPosX;
                for (int y = 0; y < height; y++)
                {
                    if (processed[x, y])
                        continue;

                    processed[x, y] = true;

                    if (matrix[x, y])
                    {
                        if (!currentIsBlack)
                        {
                            startPosX = x;
                            startPosY = y;
                            currentIsBlack = true;
                        }
                    }
                    else
                    {
                        if (currentIsBlack)
                        {
                            FindMaximumRectangle(matrix, processed, startPosX, startPosY, y, out endPosX);
                            var rect = new XRect(startPosX + offset.X, startPosY + offset.Y, endPosX - startPosX + 1, y - startPosY);
                            gfx.DrawRectangle(XBrushes.Black, rect);
                            currentIsBlack = false;
                        }
                    }
                }
                if (currentIsBlack)
                {
                    FindMaximumRectangle(matrix, processed, startPosX, startPosY, height, out endPosX);
                    var rect = new XRect(startPosX + offset.X, startPosY + offset.Y, endPosX - startPosX + 1, height - startPosY);
                    gfx.DrawRectangle(XBrushes.Black, rect);
                    currentIsBlack = false;
                }
            }
        }
        private static void FindMaximumRectangle(BitMatrix matrix, BitMatrix processed, int startPosX, int startPosY, int endPosY, out int endPosX)
        {
            endPosX = startPosX;

            for (int x = startPosX + 1; x < matrix.Width; x++)
            {
                for (int y = startPosY; y < endPosY; y++)
                {
                    if (!matrix[x, y])
                    {
                        return;
                    }
                }
                endPosX = x;
                for (int y = startPosY; y < endPosY; y++)
                {
                    processed[x, y] = true;
                }
            }
        }
    }
}