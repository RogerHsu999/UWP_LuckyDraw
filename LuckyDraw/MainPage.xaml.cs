using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FY18LuckyDraw
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private MainViewModel viewModel = new MainViewModel();
      private bool readyToDraw = false;
        public MainPage()
        {
            this.InitializeComponent();
            this.DataContext = viewModel;
        }

        private void Button_Click( object sender, RoutedEventArgs e )
        {
         if( readyToDraw )
            viewModel.StartDraw();
         else
         {
            giftName.Text = ( Gift.curGift != null ) ? Gift.curGift.Name : "";
            giftNum.Text = ( Gift.curGift != null ) ? Gift.curGift.Quantity.ToString()+"位" : "";
            viewModel.Winners.Clear();
         }

         readyToDraw = !readyToDraw;

        }
   }
}
