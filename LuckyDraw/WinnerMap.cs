using CsvHelper.Configuration;

namespace FY18LuckyDraw
{
   public class WinnerMap : CsvClassMap<Winner>
   {
      public WinnerMap()
      {
         Map( m => m.Name ).Name( "Full Name" );
         Map( m => m.Department ).Name( "Org" );
      }
   }
}
