using System.Windows;
using System.Windows.Media;

namespace SortTheBallsGameVariant9
{
    internal static class IngameCustomBrushes
    {
        private static RadialGradientBrush _holeBrush;

        internal static RadialGradientBrush HoleBrush
        {
            get
            {
                if (_holeBrush is null)
                {
                    var gradientStops = new GradientStopCollection
                    {
                        new GradientStop(Color.FromRgb(80, 20, 0), 0.0),
                        new GradientStop(Color.FromRgb(51, 9, 0), 0.7),
                        new GradientStop(Color.FromRgb(41, 7, 0), 1)
                    };
                    _holeBrush = new RadialGradientBrush(gradientStops);
                }

                return _holeBrush;
            }
        }

        private static RadialGradientBrush _whiteBallBrush;

        internal static RadialGradientBrush WhiteBallBrush
        {
            get
            {
                if (_whiteBallBrush is null)
                {
                    var gradientStops = new GradientStopCollection
                    {
                        new GradientStop(Color.FromRgb(250, 250, 250), 0.0),
                        new GradientStop(Color.FromRgb(140, 140, 140), 0.4),
                        new GradientStop(Color.FromRgb(110, 110, 110), 1)
                    };
                    _whiteBallBrush = new RadialGradientBrush(gradientStops) { GradientOrigin = new Point(0.75, 0.25) };
                }

                return _whiteBallBrush;
            }
        }


        private static RadialGradientBrush _blackBallBrush;
        internal static RadialGradientBrush BlackBallBrush
        {
            get
            {
                if (_blackBallBrush is null)
                {
                    var gradientStops = new GradientStopCollection
                    {
                        new GradientStop(Color.FromRgb(250, 250, 250), 0.0),
                        new GradientStop(Color.FromRgb(60, 60, 60), 0.4),
                        new GradientStop(Color.FromRgb(10, 10, 10), 1)
                    };
                    _blackBallBrush = new RadialGradientBrush(gradientStops) { GradientOrigin = new Point(0.75, 0.25) };
                }

                return _blackBallBrush;
            }
        }
    }
}