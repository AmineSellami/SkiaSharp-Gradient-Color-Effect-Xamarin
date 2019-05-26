using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SkiaGradientAppExample.Behaviors
{
    public class GradientBehavior : Behavior<SKCanvasView>
    {
        public readonly BindableProperty GradientColorProperty =
        BindableProperty.Create(
            "GradientColor",
            typeof(string),
            typeof(GradientBehavior),
            "#FFFFFF");

        public string GradientColor
        {
            get { return (string)GetValue(GradientColorProperty); }
            set { SetValue(GradientColorProperty, value); }
        }

        protected override void OnAttachedTo(SKCanvasView canvasView)
        {
            canvasView.PaintSurface += PaintSurface;
        }
        protected override void OnDetachingFrom(SKCanvasView canvasView)
        {
            canvasView.PaintSurface -= PaintSurface;
        }

        private void PaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;
            
            var startColor = SKColor.Parse(GradientColor);
            canvas.Clear();

            using (SKPaint paint = new SKPaint())
            {
                // Create 300-pixel square centered rectangle
                float x = (info.Width - 300) / 2;
                float y = (info.Height - 300) / 2;
                SKRect rect = SKRect.Create(info.Width, info.Height);

                // Create linear gradient from upper-left to lower-right
                paint.Shader = SKShader.CreateLinearGradient(
                                    new SKPoint(0, rect.Bottom),
                                    new SKPoint(0, rect.Top),
                                    new SKColor[] { startColor, SKColors.Transparent },
                                    new float[] { 0.05f, 1 },
                                    SKShaderTileMode.Repeat);
                // Draw the gradient on the rectangle
                canvas.DrawRect(rect, paint);
            }
        }
        
    }
}
