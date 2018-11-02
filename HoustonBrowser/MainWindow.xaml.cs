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
            panel.Controls.Add(myButton);

            myLabel=new Label();
            myLabel.Top=myLabel.Left=100;
            panel.Controls.Add(myLabel);

            myTextBox=new HoustonBrowser.Controls.TextBox();
            myTextBox.Top=myTextBox.Left=150;
            panel.Controls.Add(myTextBox);

            myTab=new HoustonBrowser.Controls.TabControl();
            myTab.Top=myTab.Left=200;
            panel.Controls.Add(myTab);

            myButton.Click+=(s,e)=>Method();
            this.PointerPressed+=(s,e)=>Method();
        }

        public event EventHandler<PointerPressedEventArgs> onMouseClick;
        public event EventHandler<KeyEventArgs> onKeyDown;
        public event EventHandler<object> onPageLoad;

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
           
        }

        private void Method()
        {
            myTab.Top=400;
            this.InvalidateVisual();
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