﻿using System;
using Mapsui.Tests.Common;
using NUnit.Framework;
using System.IO;
#if OPENTK
using Mapsui.Rendering.OpenTK;
#elif GDI
using Mapsui.Rendering.Gdi;
#endif

namespace Mapsui.Rendering.Xaml.Tests
{
    [TestFixture, RequiresSTA]
    class MapRendererTests
    {
        private readonly string _originalImagesFolder = Path.Combine("Resources", "Images", "Original");
        private readonly string _generatedImagesFolder = Path.Combine("Resources", "Images", "Generated");
#if MONOGAME
        private readonly Microsoft.Xna.Framework.Graphics.GraphicsDevice _graphicsDevice;

        public MapRendererTests(Microsoft.Xna.Framework.Graphics.GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
        }
#endif
        
        [Test]
        public void RenderPointsWithVectorStyle()
        {
            // arrange
            var map = ArrangeRenderingTests.PointsWithVectorStyle();
            const string fileName = "vector_symbol.png";
            
            // act
            var bitmap = RenderToBitmap(map);
            
#if !MONOGAME
            // aside
            WriteFile(Path.Combine(_generatedImagesFolder, fileName), bitmap);

            // assert
            Assert.AreEqual(ReadFile(Path.Combine(_originalImagesFolder, fileName)), bitmap.ToArray());
#endif
        }

        [Test]
        public void RenderPointWithBitmapSymbols()
        {
            // arrange
            var map = ArrangeRenderingTests.PointsWithBitmapSymbols();
            const string fileName = "points_with_symbolstyle.png";
            
            // act
            var bitmap = RenderToBitmap(map);
            
#if !MONOGAME
            // aside
            WriteFile(Path.Combine(_generatedImagesFolder, fileName), bitmap);

            // assert
            Assert.AreEqual(ReadFile(Path.Combine(_originalImagesFolder, fileName)), bitmap.ToArray());
#endif
        }

        [Test]
        public void RenderRotatedBitmapSymbolWithOffset()
        {
            // arrange
            var map = ArrangeRenderingTests.PointsWithBitmapRotatedAndOffset();
            const string fileName = "bitmap_symbol.png";
            // act
            var bitmap = RenderToBitmap(map);

#if !MONOGAME
            // aside
            WriteFile(Path.Combine(_generatedImagesFolder, fileName), bitmap);

            // assert
            Assert.AreEqual(ReadFile(Path.Combine(_originalImagesFolder, fileName)), bitmap.ToArray());
#endif
        }

        [Test]
        public void RenderPointsWithDifferentSymbolTypes()
        {
            // arrange
            var map = ArrangeRenderingTests.PointsWithDifferentSymbolTypes();
            const string fileName = "vector_symbol_symboltype.png";
            
            // act
            var bitmap = RenderToBitmap(map);

#if !MONOGAME
            // aside
            WriteFile(Path.Combine(_generatedImagesFolder, fileName), bitmap);

            // assert
            Assert.AreEqual(ReadFile(Path.Combine(_originalImagesFolder, fileName)), bitmap.ToArray());
#endif
        }

        [Test]
        public void RenderSymbolWithWorldUnits()
        {
            // arrange
            var map = ArrangeRenderingTests.PointsWithWorldUnits();
            const string fileName = "vector_symbol_unittype.png";
            
            // act
            var bitmap = RenderToBitmap(map);

#if !MONOGAME
            // aside
            WriteFile(Path.Combine(_generatedImagesFolder, fileName), bitmap);

            // assert
            Assert.AreEqual(ReadFile(Path.Combine(_originalImagesFolder, fileName)), bitmap.ToArray());
#endif
        }

        [Test]
        public void RenderPolygon()
        {
            // arrange
            var map = ArrangeRenderingTests.Polygon();
            const string fileName = "polygon.png";

            // act
            var bitmap = RenderToBitmap(map);

#if !MONOGAME
            // aside
            WriteFile(Path.Combine(_generatedImagesFolder, fileName), bitmap);

            // assert
            Assert.AreEqual(ReadFile(Path.Combine(_originalImagesFolder, fileName)), bitmap.ToArray());
#endif
        }

        [Test]
        public void RenderLine()
        {
            // arrange
            var map = ArrangeRenderingTests.Line();
            const string fileName = "line.png";
            
            // act
            var bitmap = RenderToBitmap(map);

#if !MONOGAME
            // aside
            WriteFile(Path.Combine(_generatedImagesFolder, fileName), bitmap);

            // assert
            Assert.AreEqual(ReadFile(Path.Combine(_originalImagesFolder, fileName)), bitmap.ToArray());
#endif
        }

        [Test]
        public void RenderTiles()
        {
            // arrange
            var map = ArrangeRenderingTests.Tiles();
            const string fileName = "tilelayer.png";

            // act
            var bitmap = RenderToBitmap(map);

#if !MONOGAME
            // aside;
            WriteFile(Path.Combine(_generatedImagesFolder, fileName), bitmap);

            // assert
            Assert.AreEqual(ReadFile(Path.Combine(_originalImagesFolder, fileName)), bitmap.ToArray());
#endif
        }

        [Test]
        public void RenderLabels()
        {
            // arrange
            var map = ArrangeRenderingTests.Labels();
            const string fileName = "labels.png";

            // act
            var bitmap = RenderToBitmap(map);

#if !MONOGAME
            // aside;
            WriteFile(Path.Combine(_generatedImagesFolder, fileName), bitmap);

            // assert
            Assert.AreEqual(ReadFile(Path.Combine(_originalImagesFolder, fileName)), bitmap.ToArray());
#endif
        }

#if !MONOGAME
        private static void WriteFile(string imagePath, MemoryStream bitmap)
        {
            var folder = Path.GetDirectoryName(imagePath);
            if (folder == null) throw new Exception("Images folder was not found");
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
            
            using (var fileStream = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
            {
                bitmap.WriteTo(fileStream);
            }
        }

        public static byte[] ReadFile(string filePath)
        {
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return Mapsui.Tests.Common.Utilities.ToByteArray(fileStream);
        }
#endif

        private static MemoryStream RenderToBitmap(Map map)
        {
            var rasterizer = new MapRenderer();
            return rasterizer.RenderToBitmapStream(map.Viewport, map.Layers);
        }
    }
}
