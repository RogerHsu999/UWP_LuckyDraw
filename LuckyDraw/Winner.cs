using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;



namespace FY18LuckyDraw
{
   public class Winner : INotifyPropertyChanged
   {
      private string _name;
      private string _segment;

      public string Name
      {
         get
         {
            return _name;
         }
         set
         {
            if( !value.Equals( _name ) )
            {
               _name = value;
               NotifyPropertyChanged( "Name" );
            }
         }
      }


      public string Segment
      {
         get
         {
            return _segment;
         }
         set
         {
            if( !value.Equals( _segment ) )
            {
               _segment = value;
               NotifyPropertyChanged( "Department" );
            }
         }
      }

      public event PropertyChangedEventHandler PropertyChanged;
      private void NotifyPropertyChanged( string propertyName )
      {
         PropertyChangedEventHandler handler = PropertyChanged;
         if( null != handler )
         {
            handler( this, new PropertyChangedEventArgs( propertyName ) );
         }
      }
   }
}
