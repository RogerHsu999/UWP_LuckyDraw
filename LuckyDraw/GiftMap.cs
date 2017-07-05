using CsvHelper.Configuration;

namespace FY18LuckyDraw
{
   public class GiftrMap : CsvClassMap<Gift>
   {
      public GiftrMap()
      {
         Map( m => m.Name ).Name( "Name" );
         Map( m => m.Quantity ).Name( "Quantity" );
         Map( m => m.Prize ).Name( "Prize" );
         Map( m => m.Source ).Name( "Source" );
      }
   }
}
