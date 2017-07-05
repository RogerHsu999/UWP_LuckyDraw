using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.IO;
using CsvHelper;
using Windows.Storage;

namespace FY18LuckyDraw
{
   class MainViewModel
   {
      public ObservableCollection<Winner> Winners { get; private set; }
      private List<Winner> Attendees;
      private List<int> Drawed = new List<int>();
      private StorageFile nameStorageFile;
      private StorageFile resultFile;
      private Stream resultWriteStream;

      public MainViewModel()
      {
         Winners = new ObservableCollection<Winner>();
         Attendees = new List<Winner>();

         Init();
      }

      private async void Init()
      {
         Windows.Storage.Pickers.FileOpenPicker picker = new Windows.Storage.Pickers.FileOpenPicker();
         picker.FileTypeFilter.Add( ".csv" );
         StorageFile file = await picker.PickSingleFileAsync();
         if( file != null )
         {
            nameStorageFile = file;
         }
         resultFile = await DownloadsFolder.CreateFileAsync( "result.csv", CreationCollisionOption.GenerateUniqueName );

         await Task.Run( () =>
         {
            loadAttendee();
            loadGift();
            Gift.Init();
         } );
      }

      private async void loadAttendee()
      {
         
         //using( FileStream fileStream = new FileStream( filePath, FileMode.Open, FileAccess.Read ) )
         //using( StreamReader reader = new StreamReader( fileStream, Encoding.UTF8 ) )
         using( Stream fileStream = await nameStorageFile.OpenStreamForReadAsync() )
         using( StreamReader reader = new StreamReader( fileStream, Encoding.UTF8 ) )
         using( CsvReader csv = new CsvReader( reader ) )
         {
            csv.Configuration.RegisterClassMap<WinnerMap>();
            while( csv.Read() )
            {
               var attendee = csv.GetRecord<Winner>();
               Attendees.Add( new Winner() { Name = attendee.Name, Segment = attendee.Segment } );
            }
         }

         return;
      }

      private async void loadGift()
      {
         string filePath = @"Assets\giftlist.csv";
         using( FileStream fileStream = new FileStream( filePath, FileMode.Open, FileAccess.Read ) )
         using( StreamReader reader = new StreamReader( fileStream ) )
         using( CsvReader csv = new CsvReader( reader ) )
         {
            csv.Configuration.RegisterClassMap<GiftrMap>();
            while( csv.Read() )
            {
               var gift = csv.GetRecord<Gift>();
               Gift.Gifts.Add( new Gift() { Name = gift.Name, Quantity = gift.Quantity, Prize = gift.Prize, Source = gift.Source} );
            }
         }

         return;
      }


      public async void StartDraw()
      {
         if(Gift.curGift != null )
         {
            Winners.Clear();

            var rand = new Random();
            int length = Attendees.Count;
            int index;

            for( int i = 0; i < Gift.curGift.Quantity; i++ )
            {
               do
               {
                  index = rand.Next() % length;
               } while( Drawed.Contains( index ) );
               Drawed.Add( index );

               Winners.Add( Attendees[ index ] );
            }

            Output(Gift.curGift.Name);
            Gift.GetNext();

         }
      }

      public async void Output( string giftName )
      {
         using( var resultWriteStream = await resultFile.OpenStreamForWriteAsync() )
         using( var writer = new StreamWriter( resultWriteStream ) )
         {
            writer.BaseStream.Seek( 0, SeekOrigin.End );
            using( var csvWriter = new CsvWriter( writer ) )
               foreach( var winner in Winners )
               {
                  csvWriter.WriteField( giftName );
                  csvWriter.WriteField( winner.Name );
                  csvWriter.WriteField( winner.Segment );
                  csvWriter.NextRecord();
               }
         }
         return;
      }
   }
}
