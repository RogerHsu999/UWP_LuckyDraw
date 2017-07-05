using CsvHelper.Configuration;

namespace FY18LuckyDraw
{
   public class WinnerMap : CsvClassMap<Winner>
   {
      public WinnerMap()
      {
         Map( m => m.Name ).Name( "Name" );
         Map( m => m.Segment ).Name( "Segment" );
      }
   }
}
