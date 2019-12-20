﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstateAgencyBackend.DAL.Contexts;
using RealEstateAgencyBackend.Models;

namespace RealEstateAgencyBackend.DAL.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private AppDbContext _context;

        public ReservationRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Create(Reservation item)
        {
            _context.Reservations.Add(item);
        }

        public Reservation Find(int id)
        {
            return _context.Reservations.Find(id);
        }

        public IEnumerable<Reservation> GetAll()
        {
            return _context.Reservations;
        }

        public Reservation Remove(int id)
        {
            Reservation reserv = _context.Reservations.Find(id);
            if (reserv != null)
                reserv = _context.Reservations.Remove(reserv);
            return reserv;
        }

        public void Update(Reservation item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }
    }
}
