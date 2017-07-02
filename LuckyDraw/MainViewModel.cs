using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.IO;
using CsvHelper;
using System.Threading.Tasks;

namespace FY18LuckyDraw
{
   class MainViewModel
   {
      public ObservableCollection<Winner> Winners { get; private set; }
      private List<Winner> Attendees;

      public MainViewModel()
      {
         Winners = new ObservableCollection<Winner>();
         Attendees = new List<Winner>();
         Init();
      }

      private async void Init()
      {
         await Task.Run( () =>
         {
            loadAttendee();
            loadGift();
            Gift.Init();
         } );
      }

      private async void loadAttendee()
      {
         //string filePath = "C:\\Users\\fanwan\\Desktop\\namelist.csv";
         string filePath = "namelist.csv";

         using( FileStream fileStream = new FileStream( filePath, FileMode.Open, FileAccess.Read ) )
         using( StreamReader reader = new StreamReader( fileStream, Encoding.UTF8 ) )
         using( CsvReader csv = new CsvReader( reader ) )
         {
            csv.Configuration.RegisterClassMap<WinnerMap>();
            while( csv.Read() )
            {
               var attendee = csv.GetRecord<Winner>();
               Attendees.Add( new Winner() { Name = attendee.Name, Department = attendee.Department } );
            }
         }

         return;
      }

      private async void loadGift()
      {
         //string filePath = "C:\\Users\\fanwan\\Desktop\\giftlist.csv";
         string filePath = "giftlist.csv";
         using( FileStream fileStream = new FileStream( filePath, FileMode.Open, FileAccess.Read ) )
         using( StreamReader reader = new StreamReader( fileStream, Encoding.UTF8 ) )
         using( CsvReader csv = new CsvReader( reader ) )
         {
            csv.Configuration.RegisterClassMap<GiftrMap>();
            while( csv.Read() )
            {
               var gift = csv.GetRecord<Gift>();
               Gift.Gifts.Add( new Gift() { Name = gift.Name, Quantity = gift.Quantity, Prize = gift.Prize } );
            }
         }

         return;
      }


      public void StartDraw()
      {
         if(Gift.curGift != null )
         {
            Winners.Clear();

            var rand = new Random();
            var drawed = new List<int>();
            int length = Attendees.Count;
            int index;

            for( int i = 0; i < Gift.curGift.Quantity; i++ )
            {
               do
               {
                  index = rand.Next() % length;
               } while( drawed.Contains( index ) );
               drawed.Add( index );

               Winners.Add( Attendees[ index ] );
            }

            Gift.curGift.Output();
            Gift.GetNext();

         }
      }
   }
}
