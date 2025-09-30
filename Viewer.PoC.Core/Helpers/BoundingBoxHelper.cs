using System;
using System.Collections.Generic;
using Viewer.PoC.Core.Transform.Bounding;
using Viewer.PoC.Model.Models;

namespace Viewer.PoC.Core.Helpers
{
    internal static class BoundingBoxHelper
    {
        private const double CANVAS_MARGIN = 10;

        internal static (double MinX, double MinY, double Width, double Height) ComputeBoundingBox(IEnumerable<IShape> shapes, ShapeBoundsService boundsService)
        {
            bool boundsInitialized = false;
            double minX = 0;
            double maxX = 0;
            double minY = 0;
            double maxY = 0;
            
            foreach (var shape in shapes)
            {
                foreach (var pt in boundsService.GetBoundingPoints(shape))
                {
                    if (!boundsInitialized)
                    {
                        minX = maxX = pt.X;
                        minY = maxY = pt.Y;
                        boundsInitialized = true;
                    }
                    else
                    {
                        minX = Math.Min(minX, pt.X); maxX = Math.Max(maxX, pt.X);
                        minY = Math.Min(minY, pt.Y); maxY = Math.Max(maxY, pt.Y);
                    }
                }
            }

            if (!boundsInitialized)
            {
                return (0, 0, double.NaN, double.NaN);
            }

            return (minX, minY, maxX - minX, maxY - minY);
        }

        internal static TransformationMatrix GetWorldToScreen(Dimensions canvasSize, IEnumerable<IShape> shapes, ShapeBoundsService boundsService)
        {
            var boundingBox = ComputeBoundingBox(shapes, boundsService);
            if (double.IsNaN(boundingBox.Width) || boundingBox.Width == 0 && boundingBox.Height == 0)
            {
                return TransformationMatrix.Identity;
            }

            var availableWidth = System.Math.Max(1, canvasSize.Width - CANVAS_MARGIN * 2);
            var availableHeight = System.Math.Max(1, canvasSize.Height - CANVAS_MARGIN * 2);

            var worldWidth = boundingBox.Width;
            var worldHeight = boundingBox.Height;

            var scaleX = availableWidth / worldWidth;
            var scaleY = availableHeight / worldHeight;
            var uniformScale = System.Math.Min(scaleX, scaleY);

            var scaledWorldWidth = worldWidth * uniformScale;
            var scaledWorldHeight = worldHeight * uniformScale;

            var centerOffsetX = (canvasSize.Width - scaledWorldWidth) / 2.0;
            var centerOffsetY = (canvasSize.Height - scaledWorldHeight) / 2.0;

            var translateToOrigin = new TransformationMatrix(1, 0, 0, 1, -boundingBox.MinX, -boundingBox.MinY);
            var scaleAndFlipY = new TransformationMatrix(uniformScale, 0, 0, -uniformScale, 0, 0);
            var centerOnCanvas = new TransformationMatrix(1, 0, 0, 1, centerOffsetX, centerOffsetY + scaledWorldHeight);

            return Combine(Combine(translateToOrigin, scaleAndFlipY), centerOnCanvas);
        }

        private static TransformationMatrix Combine(TransformationMatrix a, TransformationMatrix b)
        {
            return new TransformationMatrix(
                a.M11 * b.M11 + a.M12 * b.M21,
                a.M11 * b.M12 + a.M12 * b.M22,
                a.M21 * b.M11 + a.M22 * b.M21,
                a.M21 * b.M12 + a.M22 * b.M22,
                a.OffsetX * b.M11 + a.OffsetY * b.M21 + b.OffsetX,
                a.OffsetX * b.M12 + a.OffsetY * b.M22 + b.OffsetY
            );
        }
    }
}