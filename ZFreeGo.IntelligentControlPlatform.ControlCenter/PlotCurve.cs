using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.Charts;
using Microsoft.Research.DynamicDataDisplay.Charts.Axes.Numeric;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using Microsoft.Research.DynamicDataDisplay.ViewportRestrictions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace ZFreeGo.IntelligentControlPlatform.ControlCenter
{
    public partial class MainWindow
    {
        LineGraph curveFan;//反特性曲线
        
        private void plotter_Loaded(object sender, RoutedEventArgs e)
        {



            plotter.DataTransform = new Log10YTransform();
            VerticalAxis yAxis = new VerticalAxis
            {
                TicksProvider = new LogarithmNumericTicksProvider(10),
                LabelProvider = new UnroundingLabelProvider()
            };
            plotter.MainVerticalAxis = yAxis;
            plotter.AxisGrid.DrawVerticalMinorTicks = true;

        //    var genericPlotter = plotter.GetGenericPlotter();
        //    genericPlotter.DataRect = new GenericRect<double, double>(1, 0.001, 10, 1000);


            //  plotter.Viewport.Domain = new DataRect(1, 0.001, 10, 1000);
            plotter.Viewport.Restrictions.Clear();
            plotter.Viewport.Restrictions.Add(new DomainRestriction(new Rect(1, 0.01, 10, 100)));

            MinimalSizeRestriction minSize = new MinimalSizeRestriction();

            minSize.MinSize = 0.2;
            plotter.Viewport.Restrictions.Add(minSize);

            //该区域为显示区域.
           // plotter.Viewport.Domain = new Rect(1, 0.001, 10, 1000);

            plotter.FitToView();
        }

        /// <summary>
        /// 转化曲线数据
        /// </summary>
        /// <param name="x">横轴数据</param>
        /// <param name="y">纵轴数据</param>
        /// <returns>曲线数据</returns>
        private IPointDataSource PlotTrisCurveData(double[] x, double[] y)
        {
            try
            {
                int min = Math.Min(x.Length, y.Length);
                Point[] pts = new Point[min];
                for (int i = 0; i < min; i++)
                {
                    pts[i] = new Point(x[i], y[i]);
                }

                var ds = new EnumerableDataSource<Point>(pts);
                ds.SetXYMapping(pt => pt);
                return ds;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"转换曲线");
                return null;
            }
        }

        void plotShowCurve(IPointDataSource source)
        {
            try
            {
                if (source == null)
                {
                    throw new Exception("数据为空!");
                }
                if (curveFan != null)
                {
                     plotter.Children.Remove(curveFan);
                }
                //if (curveFan == null)
                //{
                //    curveFan = new LineGraph(source);
                //}
                //else
                //{
                //    plotter.Children.Remove(curveFan);
                //   // curveFan.DataSource = source;
                //}
                var pen = new Pen(new SolidColorBrush(Colors.Blue), 3);
                PenDescription dsc = new PenDescription("反特性");
                curveFan = plotter.AddLineGraph(source, pen, dsc);
                plotter.Legend.RemoveFromPlotter();
                plotter.FitToView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "绘制反时限特性曲线");
            }
        }
    }
}
