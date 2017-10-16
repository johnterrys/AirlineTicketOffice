using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PlaceInFlightChart
{
    public partial class PlaceChartControl : UserControl
    {
        public static DependencyProperty EconomFreeRectProperty;

        public static DependencyProperty EconomBusyRectProperty;

        public static DependencyProperty BusinessFreeRectProperty;

        public static DependencyProperty BusinessBusyRectProperty;

        public static DependencyProperty EconomyPlaceFreeProperty;

        public static DependencyProperty EconomyPlaceBusyProperty;

        public static DependencyProperty BusinessPlaceBusyProperty;

        public static DependencyProperty BusinessPlaceFreeProperty;

        static PlaceChartControl()
        {
            EconomFreeRectProperty = DependencyProperty.Register("EconomFreeRect", typeof(Rect), typeof(PlaceChartControl),
                new FrameworkPropertyMetadata(default(Rect)));

            EconomBusyRectProperty = DependencyProperty.Register("EconomBusyRect", typeof(Rect), typeof(PlaceChartControl),
                new FrameworkPropertyMetadata(default(Rect)));

            BusinessFreeRectProperty = DependencyProperty.Register("BusinessFreeRect", typeof(Rect), typeof(PlaceChartControl),
                new FrameworkPropertyMetadata(default(Rect)));

            BusinessBusyRectProperty = DependencyProperty.Register("BusinessBusyRect", typeof(Rect), typeof(PlaceChartControl),
                 new FrameworkPropertyMetadata(default(Rect)));

            EconomyPlaceFreeProperty = DependencyProperty.Register("EconomyPlaceFree", typeof(string), typeof(PlaceChartControl),
                 new FrameworkPropertyMetadata(""));

            EconomyPlaceBusyProperty = DependencyProperty.Register("EconomyPlaceBusy", typeof(string), typeof(PlaceChartControl),
                 new FrameworkPropertyMetadata(""));

            BusinessPlaceBusyProperty = DependencyProperty.Register("BusinessPlaceBusy", typeof(string), typeof(PlaceChartControl),
                 new FrameworkPropertyMetadata(""));

            BusinessPlaceFreeProperty = DependencyProperty.Register("BusinessPlaceFree", typeof(string), typeof(PlaceChartControl),
                 new FrameworkPropertyMetadata(""));


        }

        public Rect EconomFreeRect
        {
            get { return (Rect)GetValue(EconomFreeRectProperty); }
            set { SetValue(EconomFreeRectProperty, value); }
        }

        public Rect EconomBusyRect
        {
            get { return (Rect)GetValue(EconomBusyRectProperty); }
            set { SetValue(EconomBusyRectProperty, value); }
        }

        public Rect BusinessFreeRect
        {
            get { return (Rect)GetValue(BusinessFreeRectProperty); }
            set { SetValue(BusinessFreeRectProperty, value); }
        }

        public Rect BusinessBusyRect
        {
            get { return (Rect)GetValue(BusinessBusyRectProperty); }
            set { SetValue(BusinessBusyRectProperty, value); }
        }

        public string EconomyPlaceFree
        {
            get { return (string)GetValue(EconomyPlaceFreeProperty); }
            set { SetValue(EconomyPlaceFreeProperty, value); }
        }

        public string EconomyPlaceBusy
        {
            get { return (string)GetValue(EconomyPlaceBusyProperty); }
            set { SetValue(EconomyPlaceBusyProperty, value); }
        }

        public string BusinessPlaceBusy
        {
            get { return (string)GetValue(BusinessPlaceBusyProperty); }
            set { SetValue(BusinessPlaceBusyProperty, value); }
        }

        public string BusinessPlaceFree
        {
            get { return (string)GetValue(BusinessPlaceFreeProperty); }
            set { SetValue(BusinessPlaceFreeProperty, value); }
        }
    }
}