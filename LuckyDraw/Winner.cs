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
      private string _department;

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


      public string Department
      {
         get
         {
            return _department;
         }
         set
         {
            if( !value.Equals( _department ) )
            {
               _department = value;
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
