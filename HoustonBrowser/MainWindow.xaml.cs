using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using HoustonBrowser.Core;
using HoustonBrowser.Controls;
using Avalonia.Media;
using System.Collections.Generic;
using Avalonia.Interactivity;

namespace HoustonBrowser
{
    public class MainWindow : Window, IUI
    {
        private Avalonia.Controls.Button checkButton;
        private TextBlock checkString;
        private MyPanel panel;
        private HoustonBrowser.Controls.Button myButton;
        private Label myLabel;
        private HoustonBrowser.Controls.TextBox myTextBox;
        private HoustonBrowser.Controls.TabControl myTab;

        public MainWindow()
        {
            InitializeComponent();

            panel=this.Find<MyPanel>("panel");
            panel.Controls=new List<IBrowserControl>();

            myButton=new HoustonBrowser.Controls.Button();
            myButton.Left=myButton.Top=50;
            myButton.DefaultStyles();
            myButton.Text="Button";
            panel.Controls.Add(myButton);

            myLabel=new Label();
            myLabel.Top=myLabel.Left=100;
            myLabel.DefaultStyles();
            panel.Controls.Add(myLabel);

            myTextBox=new HoustonBrowser.Controls.TextBox();            
            myTextBox.Top=myTextBox.Left=150;
            myTextBox.DefaultStyles();
            panel.Controls.Add(myTextBox);

            myTab=new HoustonBrowser.Controls.TabControl();            
            myTab.Top=myTab.Left=200;
            myTab.DefaultStyles();
            panel.Controls.Add(myTab);

            this.PointerPressed+=panel_OnClick;
            myButton.PointerPressed+=myButton_OnClick;
        }

        public event EventHandler<PointerPressedEventArgs> onMouseClick;
        public event EventHandler<KeyEventArgs> onKeyDown;
        public event EventHandler<object> onPageLoad;

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
           
        }

        private void panel_OnClick(object sender, PointerPressedEventArgs e)
        {
            Point location = e.GetPosition(panel);
            if (location.X>=myButton.Left && location.X<=myButton.Width+myButton.Left && location.Y>=myButton.Top && location.Y<=myButton.Top+myButton.Height)
                { 
                    myButton.OnPointerPressed(sender,e);
                }
        }

        private void myButton_OnClick(object sender, PointerPressedEventArgs e)
        {
            myTab.BackgroundBrush=new SolidColorBrush(new Color(145,20,20,20));
            panel.InvalidateVisual();
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