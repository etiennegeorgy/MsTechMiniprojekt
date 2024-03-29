﻿using System.Collections.Generic;
using AutoReservation.Dal;
using System.Linq;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
namespace AutoReservation.BusinessLayer
{
    public class AutoReservationBusinessComponent
    {
        public IEnumerable<Auto> GetAutos()
        {
            using (var context = new AutoReservationEntities())
            {
                return context.Autos.Include(c => c.Reservations).ToList();
            }
        }

        public Auto GetAutoById(int id)
        {
            using (var context = new AutoReservationEntities())
            {
                var query = (from c in context.Autos.Include(c => c.Reservations)
                            where c.Id == id
                            select c).FirstOrDefault();
                return query;
            }
        }

        public void AddAuto(Auto auto)
        {
            using (var context = new AutoReservationEntities())
            {
                context.Autos.Add(auto);
                context.SaveChanges();
            }
        }

        public void UpdateAuto(Auto original, Auto modified)
        {
            using (var context = new AutoReservationEntities())
            {
                context.Autos.Attach(original);
                context.Entry(original).CurrentValues.SetValues(modified);
                try
                {
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    HandleDbConcurrencyException(context, original);
                }
            }
        }

        public void DeleteAuto(Auto auto)
        {
            using (var context = new AutoReservationEntities())
            {
                context.Autos.Attach(auto);
                context.Autos.Remove(auto);
                context.SaveChanges();
            }
        }

        public IEnumerable<Kunde> GetKunden()
        {
            using (var context = new AutoReservationEntities())
            {
                return context.Kunden.Include(c => c.Reservations).ToList();
            }
        }

        public Kunde GetKundeById(int id)
        {
            using (var context = new AutoReservationEntities())
            {
                var query = (from c in context.Kunden.Include(c => c.Reservations)
                             where c.Id == id
                             select c).FirstOrDefault();
                return query;
            }
        }

        public void AddKunde(Kunde kunde)
        {
            using (var context = new AutoReservationEntities())
            {
                context.Kunden.Add(kunde);
                context.SaveChanges();
            }
        }

        public void UpdateKunde(Kunde original, Kunde modified)
        {
            using (var context = new AutoReservationEntities())
            {
                context.Kunden.Attach(original);
                context.Entry(original).CurrentValues.SetValues(modified);
                try
                {
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    HandleDbConcurrencyException(context, original);
                }
            }
        }

        public void DeleteKunde(Kunde kunde)
        {
            using (var context = new AutoReservationEntities())
            {
                context.Kunden.Attach(kunde);
                context.Kunden.Remove(kunde);
                context.SaveChanges();
            }
        }

        public IEnumerable<Reservation> GetReservations()
        {
            using (var context = new AutoReservationEntities())
            {
                return context.Reservationen.Include(c => c.Kunde).Include(c => c.Auto).ToList();
            }
        }

        public Reservation GetReservationById(int id)
        {
            using (var context = new AutoReservationEntities())
            {
                var query = (from c in context.Reservationen.Include(c => c.Kunde).Include(c => c.Auto)
                             where c.ReservationNr == id
                             select c).FirstOrDefault();
                return query;
            }
        }

        public void AddReservation(Reservation reservation)
        {
            using (var context = new AutoReservationEntities())
            {
                context.Reservationen.Add(reservation);
                context.SaveChanges();
            }
        }
        public void DeleteReservation(Reservation reservation)
        {
            using (var context = new AutoReservationEntities())
            {
                context.Reservationen.Attach(reservation);
                context.Reservationen.Remove(reservation);
                context.SaveChanges();
            }
        }

        public void UpdateReservation(Reservation original, Reservation modified)
        {
            using (var context = new AutoReservationEntities())
            {
                context.Reservationen.Attach(original);
                context.Entry(original).CurrentValues.SetValues(modified);
                try
                {
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    HandleDbConcurrencyException(context, original);
                }
            }
        }



        private static void HandleDbConcurrencyException<T>(AutoReservationEntities context, T original) where T : class
        {
            var databaseValue = context.Entry(original).GetDatabaseValues();
            context.Entry(original).CurrentValues.SetValues(databaseValue);

            throw new LocalOptimisticConcurrencyException<T>(string.Format("Update {0}: Concurrency-Fehler", typeof(T).Name), original);
        }
    }
}