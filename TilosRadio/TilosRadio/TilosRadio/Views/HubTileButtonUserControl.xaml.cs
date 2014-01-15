namespace TD1990.TilosRadio.WP7.Views
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public partial class HubTileButtonUserControl : UserControl
    {
        public HubTileButtonUserControl()
        {
            InitializeComponent();
            if (DesignerProperties.IsInDesignTool)
            {
                LeftTopTextBlock.Text = "Left Top Text Block";
                RightBottomTextBlock.Text = "Right Bottom Text Block";
                BackRightBottomTextBlock.Text = "Right Bottom Text Block";
                BackTextBlock.Text = "Back Text Block";
                MainBorder.BorderBrush = new SolidColorBrush(Colors.Red);
                BackBorder.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            Loaded += HubTileButtonUserControl_Loaded;
        }

        int Storyboarddelay;

        private static Random RndValue;
        private static Random Rnd
        {
            get
            {
                if (RndValue == null)
                {
                    RndValue = new Random();
                }

                return RndValue;
            }
        }

        void HubTileButtonUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Storyboarddelay = Rnd.Next(10, 360);
            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        void CompositionTarget_Rendering(object sender, System.EventArgs e)
        {
            if (Storyboarddelay < 0 && MainBackStoryboard.GetCurrentState() == System.Windows.Media.Animation.ClockState.Stopped)
            {
                CompositionTarget.Rendering -= CompositionTarget_Rendering;
                MainBackStoryboard.Begin();
            }
            else
            {
                Storyboarddelay -= 1;
            }
        }

        public static readonly DependencyProperty LeftTopTextProperty = DependencyProperty.Register(
            "LeftTopText",
            typeof(string),
            typeof(HubTileButtonUserControl),
            //null);
            new PropertyMetadata(OnLeftTopTextChanged));

        public static readonly DependencyProperty RightBottomTextProperty = DependencyProperty.Register(
            "RightBottomText",
            typeof(string),
            typeof(HubTileButtonUserControl),
            //null);
            new PropertyMetadata(OnRightBottomTextChanged));

        public static readonly DependencyProperty BackTextProperty = DependencyProperty.Register(
            "BackText",
            typeof(string),
            typeof(HubTileButtonUserControl),
            //null);
            new PropertyMetadata(OnBackTextChanged));

        public static readonly DependencyProperty HubBackgroundProperty = DependencyProperty.Register(
            "HubBackground",
            typeof(Brush),
            typeof(HubTileButtonUserControl),
            //null);
            new PropertyMetadata(OnHubBackgroundChanged));

        //public static readonly DependencyProperty OnClickProperty = DependencyProperty.Register(
        //    "OnClick",
        //    typeof(RoutedEventHandler),
        //    typeof(HubTileButtonUserControl),
        //    null);
        //    //new PropertyMetadata(OnHubBackgroundChanged));

        private static void OnLeftTopTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            string s = e.NewValue as string ?? string.Empty;
            s = s.Trim();
            HubTileButtonUserControl control = d as HubTileButtonUserControl;
            control.LeftTopTextBlock.FontSize = GetFontSize(s.Length);
            control.LeftTopTextBlock.Text = s;
        }

        private static void OnRightBottomTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            HubTileButtonUserControl control = d as HubTileButtonUserControl;
            string s = e.NewValue as string ?? string.Empty;
            s = s.Trim();
            control.RightBottomTextBlock.Text = s;
            control.BackRightBottomTextBlock.Text = s;
        }

        private static void OnBackTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            string s = e.NewValue as string ?? string.Empty;
            s = s.Trim();
            HubTileButtonUserControl control = d as HubTileButtonUserControl;
            control.BackTextBlock.FontSize = GetFontSize(s.Length);
            control.BackTextBlock.Text = s;
        }

        private static void OnHubBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            HubTileButtonUserControl control = d as HubTileButtonUserControl;
            control.MainBorder.Background = e.NewValue as Brush;
            control.BackBorder.Background = e.NewValue as Brush;
        }

        private static double GetFontSize(int len)
        {
            if (len > 200) return 15;
            else if (len > 110) return 16;
            else if (len > 100) return 17;
            else if (len > 95) return 18;
            else if (len > 90) return 19;
            else if (len > 85) return 20;
            else if (len > 80) return 21;
            else if (len > 75) return 22;
            else if (len > 70) return 23;
            else if (len > 65) return 24;
            else if (len > 60) return 25;
            else if (len > 55) return 26;
            else if (len > 50) return 27;
            else if (len > 45) return 28;
            else if (len > 40) return 29;
            else if (len > 35) return 30;
            else if (len > 30) return 31;

            return 32;
        }

        public string LeftTopText
        {
            get
            {
                return (string)GetValue(LeftTopTextProperty);
            }
            set
            {
                SetValue(LeftTopTextProperty, value);
            }
        }

        public string RightBottomText
        {
            get
            {
                return (string)GetValue(RightBottomTextProperty);
            }
            set
            {
                SetValue(RightBottomTextProperty, value);
            }
        }

        public string BackText
        {
            get
            {
                return (string)GetValue(BackTextProperty);
            }
            set
            {
                SetValue(BackTextProperty, value);
            }
        }

        public Brush HubBackground
        {
            get
            {
                return (Brush)GetValue(HubBackgroundProperty);
            }
            set
            {
                SetValue(HubBackgroundProperty, value);
            }
        }

        public event RoutedEventHandler Click;
        //public RoutedEventHandler OnClick
        //{
        //    get
        //    {
        //        return (RoutedEventHandler)GetValue(OnClickProperty);
        //    }
        //    set
        //    {
        //        SetValue(OnClickProperty, value);
        //    }
        //}

        private void HubButton_Click(object sender, RoutedEventArgs e)
        {
            if (Click != null)
            {
                Click(this, e);
            }
        }
    }
}
