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
        LineGraph curveYanshi;//延时曲线
        LineGraph curveSuduan; //速断曲线
        
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

            plotter.Legend.RemoveFromPlotter();
            

        //    var genericPlotter = plotter.GetGenericPlotter();
        //    genericPlotter.DataRect = new GenericRect<double, double>(1, 0.001, 10, 1000);


            //  plotter.Viewport.Domain = new DataRect(1, 0.001, 10, 1000);
        ////    plotter.Viewport.Restrictions.Clear();
      //      plotter.Viewport.Restrictions.Add(new DomainRestriction(new Rect(1, 0.001, 10, 1000)));

         //   MinimalSizeRestriction minSize = new MinimalSizeRestriction();

         //   minSize.MinSize = 0.2;
         //   plotter.Viewport.Restrictions.Add(minSize);

            //该区域为显示区域.
           // plotter.Viewport.Domain = new Rect(1, 0.001, 10, 1000);

            plotter.MainHorizontalAxisVisibility= Visibility.Collapsed;
            plotter.MainHorizontalAxis.Visibility = Visibility.Collapsed;
            timeAxis.LabelProvider = new ToStringLabelProvider();
            timeAxis.LabelProvider.LabelStringFormat = "{0}";
            timeAxis.LabelProvider.SetCustomFormatter(info => (string.Format("{0:f2}",Math.Round(Math.Pow(10, info.Tick), 2))));
           
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
                    pts[i] = new Point(Math.Log10(x[i]), y[i]);
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

        void plotShowCurve(IPointDataSource source,ref LineGraph line, Pen pen,string str)
        {
            try
            {
                if (source == null)
                {
                    throw new Exception("数据为空!");
                }
                if (line != null)
                {
                    plotter.Children.Remove(line);
                }
           //     var pen = new Pen(new SolidColorBrush(Colors.Blue), 3);
                PenDescription dsc = new PenDescription(str);
                line = plotter.AddLineGraph(source, pen, dsc);
               
                plotter.FitToView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "绘制反时限特性曲线");
            }
        }
       
    }
}
