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

namespace WPF17
{
    /// <summary>
    /// Логика взаимодействия для ColorPickerUserControl.xaml
    /// </summary>
    public partial class ColorPickerUserControl : UserControl
    {
        public ColorPickerUserControl()
        {
            InitializeComponent();
        }

        public static DependencyProperty ColorProperty;
        public static DependencyProperty RedProperty;
        public static DependencyProperty GreenProperty;
        public static DependencyProperty BlueProperty;



        static ColorPickerUserControl()
        {
            ColorProperty = DependencyProperty.Register("Color", typeof(Color),
                typeof(ColorPickerUserControl),
                new FrameworkPropertyMetadata(Colors.Black, new PropertyChangedCallback(OnColorChangedCallback)));

            RedProperty = DependencyProperty.Register("Red", typeof(byte),
                typeof(ColorPickerUserControl),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnColorRGBChangedCallback)));

            GreenProperty = DependencyProperty.Register("Green", typeof(byte),
                typeof(ColorPickerUserControl),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnColorRGBChangedCallback)));

            BlueProperty = DependencyProperty.Register("Blue", typeof(byte),
                typeof(ColorPickerUserControl),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnColorRGBChangedCallback)));
        }


        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        public byte Red
        {
            get { return (byte)GetValue(RedProperty); }
            set { SetValue(RedProperty, value); }
        }

        public byte Green
        {
            get { return (byte)GetValue(GreenProperty); }
            set { SetValue(GreenProperty, value); }
        }

        public byte Blue
        {
            get { return (byte)GetValue(BlueProperty); }
            set { SetValue(BlueProperty, value); }
        }

        private static void OnColorChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {

            Color newColor = (Color)e.NewValue;
            ColorPickerUserControl colorPicker = (ColorPickerUserControl)sender;
            colorPicker.Red = newColor.R;
            colorPicker.Green = newColor.G;
            colorPicker.Blue = newColor.B;


            RoutedPropertyChangedEventArgs<Color> args = new RoutedPropertyChangedEventArgs<Color>(colorPicker.Color, newColor);
            args.RoutedEvent = ColorChangedEvent;
            colorPicker.RaiseEvent(args);

        }

        private static void OnColorRGBChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ColorPickerUserControl colorPicker = (ColorPickerUserControl)sender;
            Color color = colorPicker.Color;
            if (e.Property == RedProperty)
            {
                color.R = (byte)e.NewValue;
            }
            else if (e.Property == GreenProperty)
            {
                color.G = (byte)e.NewValue;
            }
            else if (e.Property == BlueProperty)
            {
                color.B = (byte)e.NewValue;
            }
            colorPicker.Color = color;
        }

        public static readonly RoutedEvent ColorChangedEvent =
           EventManager.RegisterRoutedEvent("ColorChanged", RoutingStrategy.Bubble,
           typeof(RoutedPropertyChangedEventHandler<Color>), typeof(ColorPickerUserControl));

        public event RoutedPropertyChangedEventHandler<Color> ColorChanged
        {
            add { AddHandler(ColorChangedEvent, value); }
            remove { RemoveHandler(ColorChangedEvent, value); }
        }

    }
}
