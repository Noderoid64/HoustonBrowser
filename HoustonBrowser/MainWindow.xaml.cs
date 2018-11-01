using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using HoustonBrowser.Core;
using HoustonBrowser.Controls;
using Avalonia.Media;
using System.Collections.Generic;

namespace HoustonBrowser
{
    public class MainWindow : Window, IUI
    {
        private Avalonia.Controls.Button checkButton;
        private TextBlock checkString;
        private IBrowserControl control;
        private MyPanel panel;

        public MainWindow()
        {
            InitializeComponent();

            control=new BrowserControl();
            control.BackgroundBrush=new SolidColorBrush(new Color(145,220,66,0));
            control.Form=new RectangleGeometry(new Rect(10,10,30,30));
            panel=this.Find<MyPanel>("panel");
            panel.Controls=new List<IBrowserControl>();
            panel.Controls.Add(control);
        }

        public event EventHandler<PointerPressedEventArgs> onMouseClick;
        public event EventHandler<KeyEventArgs> onKeyDown;
        public event EventHandler<object> onPageLoad;

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
           
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            onKeyDown(sender, e);
        }

        private void MainWindow_PointerPressed(object sender, PointerPressedEventArgs e)
        {
            onMouseClick(sender, e);
        }
        
    }
}