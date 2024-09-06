﻿// Type: Microsoft.Windows.Themes.SystemDropShadowChromeClassic
// Assembly: PresentationFramework.Aero2, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: C98188E3-4E0A-48B2-B299-7C58CA504797
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\PresentationFramework.Aero2\v4.0_4.0.0.0__31bf3856ad364e35\PresentationFramework.Aero2.dll

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace KsWare.Presentation.Themes.Aero2Dark //Microsoft.Windows.Themes
{

	public sealed class SystemDropShadowChromeClassic : Decorator {

		public static readonly DependencyProperty ColorProperty = DependencyProperty.Register("Color", typeof (Color), typeof (SystemDropShadowChromeClassic), (PropertyMetadata) new FrameworkPropertyMetadata((object) Color.FromArgb((byte) 113, (byte) 0, (byte) 0, (byte) 0), FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(ClearBrushes)));
		public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof (CornerRadius), typeof (SystemDropShadowChromeClassic), (PropertyMetadata) new FrameworkPropertyMetadata((object) new CornerRadius(), FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(ClearBrushes)), new ValidateValueCallback(IsCornerRadiusValid));
		private static object _resourceAccess = new object();
		private const double ShadowDepth = 5.0;
		private const int TopLeft = 0;
		private const int Top = 1;
		private const int TopRight = 2;
		private const int Left = 3;
		private const int Center = 4;
		private const int Right = 5;
		private const int BottomLeft = 6;
		private const int Bottom = 7;
		private const int BottomRight = 8;
		private static Brush[] _commonBrushes;
		private static CornerRadius _commonCornerRadius;
		private Brush[] _brushes;

		public Color Color { get { return (Color) GetValue(ColorProperty); } set { SetValue(ColorProperty, (object) value); } }

		public CornerRadius CornerRadius { get { return (CornerRadius) GetValue(CornerRadiusProperty); } set { SetValue(CornerRadiusProperty, (object) value); } }

		static SystemDropShadowChromeClassic() { }

		private static bool IsCornerRadiusValid(object value) {
			CornerRadius cornerRadius = (CornerRadius) value;
			if (cornerRadius.TopLeft >= 0.0 && cornerRadius.TopRight >= 0.0 && (cornerRadius.BottomLeft >= 0.0 && cornerRadius.BottomRight >= 0.0) && (!double.IsNaN(cornerRadius.TopLeft) && !double.IsNaN(cornerRadius.TopRight) && (!double.IsNaN(cornerRadius.BottomLeft) && !double.IsNaN(cornerRadius.BottomRight))) && (!double.IsInfinity(cornerRadius.TopLeft) && !double.IsInfinity(cornerRadius.TopRight) && !double.IsInfinity(cornerRadius.BottomLeft))) return !double.IsInfinity(cornerRadius.BottomRight);
			else return false;
		}

		private static void ClearBrushes(DependencyObject o, DependencyPropertyChangedEventArgs e) { ((SystemDropShadowChromeClassic) o)._brushes = (Brush[]) null; }

		protected override void OnRender(DrawingContext drawingContext) {
			CornerRadius cornerRadius = CornerRadius;
			Rect rect = new Rect(new Point(5.0, 5.0), new Size(RenderSize.Width, RenderSize.Height));
			Color color = Color;
			if (rect.Width <= 0.0 || rect.Height <= 0.0 || (int) color.A <= 0) return;
			double width1 = rect.Right - rect.Left - 10.0;
			double height1 = rect.Bottom - rect.Top - 10.0;
			double val2 = Math.Min(width1*0.5, height1*0.5);
			cornerRadius.TopLeft = Math.Min(cornerRadius.TopLeft, val2);
			cornerRadius.TopRight = Math.Min(cornerRadius.TopRight, val2);
			cornerRadius.BottomLeft = Math.Min(cornerRadius.BottomLeft, val2);
			cornerRadius.BottomRight = Math.Min(cornerRadius.BottomRight, val2);
			Brush[] brushes = GetBrushes(color, cornerRadius);
			double num1 = rect.Top + 5.0;
			double num2 = rect.Left + 5.0;
			double num3 = rect.Right - 5.0;
			double num4 = rect.Bottom - 5.0;
			double[] guidelinesX = new double[6] {
				num2,
				num2 + cornerRadius.TopLeft,
				num3 - cornerRadius.TopRight,
				num2 + cornerRadius.BottomLeft,
				num3 - cornerRadius.BottomRight,
				num3
			};
			double[] guidelinesY = new double[6] {
				num1,
				num1 + cornerRadius.TopLeft,
				num1 + cornerRadius.TopRight,
				num4 - cornerRadius.BottomLeft,
				num4 - cornerRadius.BottomRight,
				num4
			};
			drawingContext.PushGuidelineSet(new GuidelineSet(guidelinesX, guidelinesY));
			cornerRadius.TopLeft = cornerRadius.TopLeft + 5.0;
			cornerRadius.TopRight = cornerRadius.TopRight + 5.0;
			cornerRadius.BottomLeft = cornerRadius.BottomLeft + 5.0;
			cornerRadius.BottomRight = cornerRadius.BottomRight + 5.0;
			Rect rectangle1 = new Rect(rect.Left, rect.Top, cornerRadius.TopLeft, cornerRadius.TopLeft);
			drawingContext.DrawRectangle(brushes[0], (Pen) null, rectangle1);
			double width2 = guidelinesX[2] - guidelinesX[1];
			if (width2 > 0.0) {
				Rect rectangle2 = new Rect(guidelinesX[1], rect.Top, width2, 5.0);
				drawingContext.DrawRectangle(brushes[1], (Pen) null, rectangle2);
			}
			Rect rectangle3 = new Rect(guidelinesX[2], rect.Top, cornerRadius.TopRight, cornerRadius.TopRight);
			drawingContext.DrawRectangle(brushes[2], (Pen) null, rectangle3);
			double height2 = guidelinesY[3] - guidelinesY[1];
			if (height2 > 0.0) {
				Rect rectangle2 = new Rect(rect.Left, guidelinesY[1], 5.0, height2);
				drawingContext.DrawRectangle(brushes[3], (Pen) null, rectangle2);
			}
			double height3 = guidelinesY[4] - guidelinesY[2];
			if (height3 > 0.0) {
				Rect rectangle2 = new Rect(guidelinesX[5], guidelinesY[2], 5.0, height3);
				drawingContext.DrawRectangle(brushes[5], (Pen) null, rectangle2);
			}
			Rect rectangle4 = new Rect(rect.Left, guidelinesY[3], cornerRadius.BottomLeft, cornerRadius.BottomLeft);
			drawingContext.DrawRectangle(brushes[6], (Pen) null, rectangle4);
			double width3 = guidelinesX[4] - guidelinesX[3];
			if (width3 > 0.0) {
				Rect rectangle2 = new Rect(guidelinesX[3], guidelinesY[5], width3, 5.0);
				drawingContext.DrawRectangle(brushes[7], (Pen) null, rectangle2);
			}
			Rect rectangle5 = new Rect(guidelinesX[4], guidelinesY[4], cornerRadius.BottomRight, cornerRadius.BottomRight);
			drawingContext.DrawRectangle(brushes[8], (Pen) null, rectangle5);
			if (cornerRadius.TopLeft == 5.0 && cornerRadius.TopLeft == cornerRadius.TopRight && (cornerRadius.TopLeft == cornerRadius.BottomLeft && cornerRadius.TopLeft == cornerRadius.BottomRight)) {
				Rect rectangle2 = new Rect(guidelinesX[0], guidelinesY[0], width1, height1);
				drawingContext.DrawRectangle(brushes[4], (Pen) null, rectangle2);
			} else {
				PathFigure pathFigure = new PathFigure();
				if (cornerRadius.TopLeft > 5.0) {
					pathFigure.StartPoint = new Point(guidelinesX[1], guidelinesY[0]);
					pathFigure.Segments.Add((PathSegment) new LineSegment(new Point(guidelinesX[1], guidelinesY[1]), true));
					pathFigure.Segments.Add((PathSegment) new LineSegment(new Point(guidelinesX[0], guidelinesY[1]), true));
				} else { pathFigure.StartPoint = new Point(guidelinesX[0], guidelinesY[0]); }
				if (cornerRadius.BottomLeft > 5.0) {
					pathFigure.Segments.Add((PathSegment) new LineSegment(new Point(guidelinesX[0], guidelinesY[3]), true));
					pathFigure.Segments.Add((PathSegment) new LineSegment(new Point(guidelinesX[3], guidelinesY[3]), true));
					pathFigure.Segments.Add((PathSegment) new LineSegment(new Point(guidelinesX[3], guidelinesY[5]), true));
				} else { pathFigure.Segments.Add((PathSegment) new LineSegment(new Point(guidelinesX[0], guidelinesY[5]), true)); }
				if (cornerRadius.BottomRight > 5.0) {
					pathFigure.Segments.Add((PathSegment) new LineSegment(new Point(guidelinesX[4], guidelinesY[5]), true));
					pathFigure.Segments.Add((PathSegment) new LineSegment(new Point(guidelinesX[4], guidelinesY[4]), true));
					pathFigure.Segments.Add((PathSegment) new LineSegment(new Point(guidelinesX[5], guidelinesY[4]), true));
				} else { pathFigure.Segments.Add((PathSegment) new LineSegment(new Point(guidelinesX[5], guidelinesY[5]), true)); }
				if (cornerRadius.TopRight > 5.0) {
					pathFigure.Segments.Add((PathSegment) new LineSegment(new Point(guidelinesX[5], guidelinesY[2]), true));
					pathFigure.Segments.Add((PathSegment) new LineSegment(new Point(guidelinesX[2], guidelinesY[2]), true));
					pathFigure.Segments.Add((PathSegment) new LineSegment(new Point(guidelinesX[2], guidelinesY[0]), true));
				} else { pathFigure.Segments.Add((PathSegment) new LineSegment(new Point(guidelinesX[5], guidelinesY[0]), true)); }
				pathFigure.IsClosed = true;
				pathFigure.Freeze();
				PathGeometry pathGeometry = new PathGeometry();
				pathGeometry.Figures.Add(pathFigure);
				pathGeometry.Freeze();
				drawingContext.DrawGeometry(brushes[4], (Pen) null, (Geometry) pathGeometry);
			}
			drawingContext.Pop();
		}

		private static GradientStopCollection CreateStops(Color c, double cornerRadius) {
			double num = 1.0/(cornerRadius + 5.0);
			GradientStopCollection gradientStopCollection = new GradientStopCollection();
			gradientStopCollection.Add(new GradientStop(c, (0.5 + cornerRadius)*num));
			Color color = c;
			color.A = (byte) (0.74336*(double) c.A);
			gradientStopCollection.Add(new GradientStop(color, (1.5 + cornerRadius)*num));
			color.A = (byte) (0.38053*(double) c.A);
			gradientStopCollection.Add(new GradientStop(color, (2.5 + cornerRadius)*num));
			color.A = (byte) (0.12389*(double) c.A);
			gradientStopCollection.Add(new GradientStop(color, (3.5 + cornerRadius)*num));
			color.A = (byte) (0.02654*(double) c.A);
			gradientStopCollection.Add(new GradientStop(color, (4.5 + cornerRadius)*num));
			color.A = (byte) 0;
			gradientStopCollection.Add(new GradientStop(color, (5.0 + cornerRadius)*num));
			gradientStopCollection.Freeze();
			return gradientStopCollection;
		}

		private static Brush[] CreateBrushes(Color c, CornerRadius cornerRadius) {
			Brush[] brushArray = new Brush[9];
			brushArray[4] = (Brush) new SolidColorBrush(c);
			brushArray[4].Freeze();
			GradientStopCollection stops = CreateStops(c, 0.0);
			LinearGradientBrush linearGradientBrush1 = new LinearGradientBrush(stops, new Point(0.0, 1.0), new Point(0.0, 0.0));
			linearGradientBrush1.Freeze();
			brushArray[1] = (Brush) linearGradientBrush1;
			LinearGradientBrush linearGradientBrush2 = new LinearGradientBrush(stops, new Point(1.0, 0.0), new Point(0.0, 0.0));
			linearGradientBrush2.Freeze();
			brushArray[3] = (Brush) linearGradientBrush2;
			LinearGradientBrush linearGradientBrush3 = new LinearGradientBrush(stops, new Point(0.0, 0.0), new Point(1.0, 0.0));
			linearGradientBrush3.Freeze();
			brushArray[5] = (Brush) linearGradientBrush3;
			LinearGradientBrush linearGradientBrush4 = new LinearGradientBrush(stops, new Point(0.0, 0.0), new Point(0.0, 1.0));
			linearGradientBrush4.Freeze();
			brushArray[7] = (Brush) linearGradientBrush4;
			GradientStopCollection gradientStopCollection1 = cornerRadius.TopLeft != 0.0 ? CreateStops(c, cornerRadius.TopLeft) : stops;
			RadialGradientBrush radialGradientBrush1 = new RadialGradientBrush(gradientStopCollection1);
			radialGradientBrush1.RadiusX = 1.0;
			radialGradientBrush1.RadiusY = 1.0;
			radialGradientBrush1.Center = new Point(1.0, 1.0);
			radialGradientBrush1.GradientOrigin = new Point(1.0, 1.0);
			radialGradientBrush1.Freeze();
			brushArray[0] = (Brush) radialGradientBrush1;
			GradientStopCollection gradientStopCollection2 = cornerRadius.TopRight != 0.0 ? (cornerRadius.TopRight != cornerRadius.TopLeft ? CreateStops(c, cornerRadius.TopRight) : gradientStopCollection1) : stops;
			RadialGradientBrush radialGradientBrush2 = new RadialGradientBrush(gradientStopCollection2);
			radialGradientBrush2.RadiusX = 1.0;
			radialGradientBrush2.RadiusY = 1.0;
			radialGradientBrush2.Center = new Point(0.0, 1.0);
			radialGradientBrush2.GradientOrigin = new Point(0.0, 1.0);
			radialGradientBrush2.Freeze();
			brushArray[2] = (Brush) radialGradientBrush2;
			GradientStopCollection gradientStopCollection3 = cornerRadius.BottomLeft != 0.0 ? (cornerRadius.BottomLeft != cornerRadius.TopLeft ? (cornerRadius.BottomLeft != cornerRadius.TopRight ? CreateStops(c, cornerRadius.BottomLeft) : gradientStopCollection2) : gradientStopCollection1) : stops;
			RadialGradientBrush radialGradientBrush3 = new RadialGradientBrush(gradientStopCollection3);
			radialGradientBrush3.RadiusX = 1.0;
			radialGradientBrush3.RadiusY = 1.0;
			radialGradientBrush3.Center = new Point(1.0, 0.0);
			radialGradientBrush3.GradientOrigin = new Point(1.0, 0.0);
			radialGradientBrush3.Freeze();
			brushArray[6] = (Brush) radialGradientBrush3;
			RadialGradientBrush radialGradientBrush4 = new RadialGradientBrush(cornerRadius.BottomRight != 0.0 ? (cornerRadius.BottomRight != cornerRadius.TopLeft ? (cornerRadius.BottomRight != cornerRadius.TopRight ? (cornerRadius.BottomRight != cornerRadius.BottomLeft ? CreateStops(c, cornerRadius.BottomRight) : gradientStopCollection3) : gradientStopCollection2) : gradientStopCollection1) : stops);
			radialGradientBrush4.RadiusX = 1.0;
			radialGradientBrush4.RadiusY = 1.0;
			radialGradientBrush4.Center = new Point(0.0, 0.0);
			radialGradientBrush4.GradientOrigin = new Point(0.0, 0.0);
			radialGradientBrush4.Freeze();
			brushArray[8] = (Brush) radialGradientBrush4;
			return brushArray;
		}

		private Brush[] GetBrushes(Color c, CornerRadius cornerRadius) {
			if (_commonBrushes == null) {
				lock (_resourceAccess) {
					if (_commonBrushes == null) {
						_commonBrushes = CreateBrushes(c, cornerRadius);
						_commonCornerRadius = cornerRadius;
					}
				}
			}
			if (c == ((SolidColorBrush) _commonBrushes[4]).Color && cornerRadius == _commonCornerRadius) {
				_brushes = (Brush[]) null;
				return _commonBrushes;
			} else {
				if (_brushes == null) _brushes = CreateBrushes(c, cornerRadius);
				return _brushes;
			}
		}

	}

}