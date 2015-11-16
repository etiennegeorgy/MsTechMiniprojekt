using System.Collections.Generic;
using AutoReservation.Dal;
using System.Linq;
namespace AutoReservation.BusinessLayer
{
    public class AutoReservationBusinessComponent
    {
        public IEnumerable<Auto> GetAutos()
        {
            using (var context = new AutoReservationEntities())
            {
                return context.Autos.ToList();
            }
        }

        public Auto GetAutoById(int id)
        {
            using (var context = new AutoReservationEntities())
            {
                var query = (from c in context.Autos
                            where c.Id == id
                            select c).FirstOrDefault();
                return query;
            }
        }
        //private static void HandleDbConcurrencyException<T>(AutoReservationEntities context, T original) where T : class
        //{
        //    var databaseValue = context.Entry(original).GetDatabaseValues();
        //    context.Entry(original).CurrentValues.SetValues(databaseValue);

        //    throw new LocalOptimisticConcurrencyException<T>(string.Format("Update {0}: Concurrency-Fehler", typeof(T).Name), original);
        //}
    }
}